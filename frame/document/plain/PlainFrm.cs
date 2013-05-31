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
using haisan.frame.pdm.purchase;
using haisan.domain;

namespace haisan.frame.document.plain
{
    public partial class PlainFrm : Form
    {
        BaseDao baseDao = BaseDaoImpl.getInstance();
        private string tableName = "";
       // private PurchaseOrderFrm purFrm = null;
        private DataGridViewCell dataGridViewCell = null;

        public PlainFrm()
        {
            InitializeComponent();
        }

        public PlainFrm(DataGridViewCell dataGridViewCell)
        {
            InitializeComponent();
            this.dataGridViewCell = dataGridViewCell;
        }


        private void PlainFrm_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            PlainListHeadText();
        }


        //设置DataGridView标题
        private void PlainListHeadText()
        {
            int i = 0;
            dataGridViewPlain.Columns[i].Width += 100;
            dataGridViewPlain.Columns[i++].HeaderText = Text + "ID";
            dataGridViewPlain.Columns[i].Width += 100;
            dataGridViewPlain.Columns[i++].HeaderText = Text + "名";
        }

        private void 新增NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPlainFrm addFrm = new AddPlainFrm();
            addFrm.Text = "新增" + Text;
            addFrm.setLabelName(Text);
            addFrm.setTableName(tableName);

            addFrm.ShowDialog();
            refreshDataGridView();
        }


        private void refreshDataGridView()
        {
            dataGridViewPlain.DataSource = baseDao.getAllEntities(tableName).Tables[0].DefaultView;
        }

        private void 修改MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewPlain.CurrentRow) return;
            int index = dataGridViewPlain.CurrentRow.Index;

            AddPlainFrm addFrm = new AddPlainFrm();
            addFrm.setPlainId(int.Parse(dataGridViewPlain.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.setPlainName(dataGridViewPlain.Rows[index].Cells["name"].Value.ToString());
            addFrm.Text = "修改" + Text;
            addFrm.setLabelName(Text);
            addFrm.setTableName(tableName); 
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewPlain.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ids = Util.constructIds(dataGridViewPlain); //当所选择的行
            if ("".Equals(ids))
                return;

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的" + Text + "!", "警告",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities(tableName, ids);
                this.Enabled = true;
                refreshDataGridView();
            }
        }

        public void setTableName(string name)
        {
            tableName = name;
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewPlain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (null != dataGridViewCell)
            {
                if ("tb_category_stone".Equals(tableName))
                    dataGridViewCell.Tag = new CategoryOfStone(int.Parse(dataGridViewPlain.Rows[e.RowIndex].Cells["id"].Value.ToString()),
                        dataGridViewPlain.Rows[e.RowIndex].Cells["name"].Value.ToString());
                else if ("tb_product_name".Equals(tableName))
                    dataGridViewCell.Tag = new ProductName(int.Parse(dataGridViewPlain.Rows[e.RowIndex].Cells["id"].Value.ToString()),
                        dataGridViewPlain.Rows[e.RowIndex].Cells["name"].Value.ToString());

                dataGridViewCell.Value = dataGridViewPlain.Rows[e.RowIndex].Cells["name"].Value.ToString();

                this.Close();
            }
        }
    }
}
