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

namespace haisan.frame.document.warehouse
{
    public partial class WarehouseFrm : Form
    {
        BaseDao baseDao = BaseDaoImpl.getInstance();
        private readonly string TB_NAME = "tb_warehouse";

        public WarehouseFrm()
        {
            InitializeComponent();
        }

        private void warehouseFrm_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            WarehouseListHeadText();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, TB_NAME, dataGridViewWarehouse);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void refreshDataGridView()
        {
            dataGridViewWarehouse.DataSource = baseDao.getAllEntities(TB_NAME).Tables[0].DefaultView;
        }

        //设置DataGridView标题
        private void WarehouseListHeadText()
        {
            int i = 0;
            dataGridViewWarehouse.Columns[i++].HeaderText = "仓库ID";
            dataGridViewWarehouse.Columns[i++].HeaderText = "仓库名";
            dataGridViewWarehouse.Columns[i++].HeaderText = "仓库描述";
            dataGridViewWarehouse.Columns[i++].HeaderText = "是否锁定";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, TB_NAME, dataGridViewWarehouse);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddWarehouseFrm addFrm = new AddWarehouseFrm();
            addFrm.Text = "添加仓库信息";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewWarehouse.CurrentRow) return;
            int index = dataGridViewWarehouse.CurrentRow.Index;
            AddWarehouseFrm addFrm = new AddWarehouseFrm();
            addFrm.setId(int.Parse(dataGridViewWarehouse.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.fillField();
            addFrm.Text = "修改仓库信息";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewWarehouse.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewWarehouse.SelectedRows)
            {
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (dataGridViewWarehouse.SelectedRows.Count > 0)
                ids.Remove(ids.Length - 1, 1);

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的仓库信息!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities(TB_NAME, ids.ToString());
                this.Enabled = true;
            }

            refreshDataGridView();
        }
    }
}
