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
using haisan.frame.pdm.purchase;

namespace haisan.frame.document.typeOfProcess
{
    public partial class TypeOfProcessFrm : Form
    {

        BaseDao baseDao = BaseDaoImpl.getInstance();
        private string tableName = "tb_typeOfProcess";
        private int columnIndex = -1;
        private int rowIndex = -1;
        private PurchaseOrderFrm purFrm = null;

        public TypeOfProcessFrm()
        {
            InitializeComponent();
        }
        public TypeOfProcessFrm(PurchaseOrderFrm purFrm, int columnIndex, int rowIndex)
        {
            InitializeComponent();
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
            this.purFrm = purFrm;
        }

        private void refreshDataGridView()
        {
            dataGridViewTypeOfProcess.DataSource = baseDao.getAllEntities(tableName).Tables[0].DefaultView;
        }

        private void 新增NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTypeOfProcessFrm addFrm = new AddTypeOfProcessFrm();
            addFrm.Text = "新增加工名称";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void 修改MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewTypeOfProcess.CurrentRow) return;
            int index = dataGridViewTypeOfProcess.CurrentRow.Index;

            AddTypeOfProcessFrm addFrm = new AddTypeOfProcessFrm();
            addFrm.Text = "修改加工名称";

            addFrm.SetTypeOfProcessId(int.Parse(dataGridViewTypeOfProcess.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.setName(dataGridViewTypeOfProcess.Rows[index].Cells["name"].Value.ToString());
            addFrm.setUnit(dataGridViewTypeOfProcess.Rows[index].Cells["unit"].Value.ToString());

            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewTypeOfProcess.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ids = Util.constructIds(dataGridViewTypeOfProcess); //当所选择的行
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

        private void TypeOfProcessFrm_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            ProcessListHeadText();
        }

        //设置DataGridView标题
        private void ProcessListHeadText()
        {
            int i = 0;
            dataGridViewTypeOfProcess.Columns[i].Width += 35;
            dataGridViewTypeOfProcess.Columns[i++].HeaderText = Text + "ID";
            dataGridViewTypeOfProcess.Columns[i].Width += 35;
            dataGridViewTypeOfProcess.Columns[i++].HeaderText = Text;
            dataGridViewTypeOfProcess.Columns[i].Width += 35;
            dataGridViewTypeOfProcess.Columns[i++].HeaderText = "单位";
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewTypeOfProcess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = purFrm.DataGridViewItem.Rows[rowIndex].Cells[columnIndex];
            if (null != cell)
            {
                TypeOfProcess typeOfProcess = new TypeOfProcess(int.Parse(dataGridViewTypeOfProcess.Rows[e.RowIndex].Cells["id"].Value.ToString()),
                            dataGridViewTypeOfProcess.Rows[e.RowIndex].Cells["name"].Value.ToString(),
                            dataGridViewTypeOfProcess.Rows[e.RowIndex].Cells["unit"].Value.ToString());
  
                if (null != cell.Tag && !((TypeOfProcess)cell.Tag).Name.Equals(typeOfProcess.Name))
                {
                    string columnName = purFrm.DataGridViewItem.Columns[columnIndex].Name;
                    int index = int.Parse(columnName.Substring(columnName.Length - 1, 1));
                    string number = "ColumnNumber" + index;
                    purFrm.insertIntoStats((TypeOfProcess)cell.Tag, purFrm.DataGridViewItem.Rows[rowIndex].Cells["ColumnThickness"].Value.ToString(),
                        -decimal.Parse(purFrm.DataGridViewItem.Rows[rowIndex].Cells[number].Value.ToString()));
                }

                // 对tag的修改一定要放在对value修改的前面，因为对value的修改，将会触发CellValueChanged事件
                cell.Tag = typeOfProcess;

                cell.Value = dataGridViewTypeOfProcess.Rows[e.RowIndex].Cells["name"].Value.ToString();
                this.Close();
            }
        }
    }
}
