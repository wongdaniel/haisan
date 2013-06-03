using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.dao;
using haisan.util;
using haisan.domain;

namespace haisan.frame.document.image
{
    public partial class ImageFrm : Form
    {
        BaseDao baseDao = BaseDaoImpl.getInstance();
        ProcessingImageDao processingImageDao = ProcessingImageDaoImpl.getInstance();

        public ImageFrm()
        {
            InitializeComponent();
        }

        private void ImageFrm_Load(object sender, EventArgs e)
        {
            refreshDataGridViewImage();
            ImageListHeadText();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, "tb_image", dataGridViewImage);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void refreshDataGridViewImage()
        {
            dataGridViewImage.DataSource = baseDao.getAllEntities("tb_image").Tables[0].DefaultView;
        }


        private void ImageListHeadText()
        {
            int i = 0;
            dataGridViewImage.Columns[i].ReadOnly = true;
            dataGridViewImage.Columns[i++].HeaderText = "ID";
            dataGridViewImage.Columns[i++].HeaderText = "图片名称";
            dataGridViewImage.Columns[i++].HeaderText = "图片内容";
            dataGridViewImage.Columns[i++].HeaderText = "上";
            dataGridViewImage.Columns[i++].HeaderText = "下";
            dataGridViewImage.Columns[i++].HeaderText = "左";
            dataGridViewImage.Columns[i++].HeaderText = "右";

        }

        private void 保存表格EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, "tb_image", dataGridViewImage);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void 退出EToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewImage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if ("image".Equals(dataGridViewImage.Columns[e.ColumnIndex].Name))
            {
                try
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
                    ofd.FilterIndex = 1;
                    ofd.RestoreDirectory = true;
                    ofd.Title = "浏览图片";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Image image = Image.FromFile(ofd.FileName);
                        dataGridViewImage.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = image;
                    }
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.Message);
                    MessageBox.Show(e2.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private ProcessingImage ConstructImage(DataGridViewRow row)
        {
            ProcessingImage proImage = new ProcessingImage();

            if ("".Equals(row.Cells["id"].Value.ToString()))
                proImage.Id = 0;
            else
                proImage.Id = int.Parse(row.Cells["id"].Value.ToString());

            proImage.Name = row.Cells["name"].Value.ToString();
            proImage.Image = Util.byteArrayToImage((byte[])row.Cells["image"].Value); // 不能采用row.Cells["image"].Value as Image;的方式
            proImage.Up = getBoolValue(row.Cells["up"].Value.ToString());
            proImage.Down = getBoolValue(row.Cells["down"].Value.ToString());
            proImage.Left = getBoolValue(row.Cells["left"].Value.ToString());
            proImage.Right = getBoolValue(row.Cells["right"].Value.ToString());

            return proImage;
        }

        private bool isNull(DataGridViewRow row)
        {
            if (null == row.Cells["id"].Value)
            {
                return true;
            }

            if("".Equals(row.Cells["id"].Value.ToString()) && "".Equals(row.Cells["name"].Value.ToString()))
            {
                Image image = row.Cells["image"].Value as Image;
                if (null == image && !getBoolValue(row.Cells["up"].FormattedValue.ToString()) && !getBoolValue(row.Cells["down"].FormattedValue.ToString()))
                {
                    if (!getBoolValue(row.Cells["left"].FormattedValue.ToString()) && !getBoolValue(row.Cells["right"].FormattedValue.ToString()))
                    {
                        return true;
                    }
                }
                    
            }
            return false;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dataGridViewImage.ClearSelection();
            this.dataGridViewImage.TabStop = false;
            this.Enabled = false;
            foreach (DataGridViewRow row in dataGridViewImage.Rows)
            {
                if (row.IsNewRow || isNull(row)) // 因为isNewRow是后来才发现的属性，所以原来成功运行的isNull，暂不排除
                {
                    continue;
                }

                if ("".Equals(row.Cells["name"].Value.ToString()) || null == row.Cells["image"].Value || DBNull.Value == row.Cells["image"].Value)
                {
                    MessageBox.Show("【名称】或【内容】不能为空", "提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Enabled = true;
                    return;
                }
               processingImageDao.saveOrUpdateProcessingImage(ConstructImage(row));
            }
            this.Enabled = true;
            refreshDataGridViewImage();
        }

        private bool getBoolValue(string value)
        {
            if ("".Equals(value))
                return false;
            return bool.Parse(value);
        }

        private void dataGridViewImage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isBitColumn(dataGridViewImage.Columns[e.ColumnIndex].Name))
            {
                if("false".Equals(dataGridViewImage.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString().ToLower())){
                    dataGridViewImage.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "true";
                }
                else{
                    dataGridViewImage.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "false";
                }
            }
        }

        private bool isBitColumn(string name)
        {
            return ("up".Equals(name) || "down".Equals(name) || "left".Equals(name) || "right".Equals(name)) ;
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewImage.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ids = Util.constructIds(dataGridViewImage); //当所选择的行
            if ("".Equals(ids))
                return;

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的" + Text + "!", "警告",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities("tb_image", ids);
                this.Enabled = true;
                refreshDataGridViewImage();
            }
        }
    }
}
