using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.frame.document.plain;
using haisan.util;
using haisan.dao;

namespace haisan.frame.pdm.purchase
{
    public partial class PurchaseOrderFrm : Form
    {
        private BaseDao baseDao = BaseDaoImpl.getInstance();
        private static string[] units = { "m^2", "m^3", "m", "p"};

        public PurchaseOrderFrm()
        {
            InitializeComponent();
            initDataGridViewItem();
        }

        private void initDataGridViewItem()
        {
            ColumnUnit.FlatStyle = FlatStyle.Popup;
            ColumnUnit.DefaultCellStyle.NullValue = units[0];
            ColumnCategoryStone.ReadOnly = true;
            fillColumnUnit();
        }

        private void fillColumnUnit()
        {
            foreach (string str in units)
                ColumnUnit.Items.Add(str);
        }

        private void dataGridViewItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridViewItem.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("ColumnCategoryStone"))
            {

                PlainFrm plainFrm = new PlainFrm(dataGridViewItem.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                plainFrm.setTableName("tb_category_stone");
                plainFrm.Text = "石材名称";
                plainFrm.ShowDialog();
            }
            else if (columnName.Equals("ColumnProductName"))
            {
                PlainFrm plainFrm = new PlainFrm(dataGridViewItem.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                plainFrm.setTableName("tb_product_name");
                plainFrm.Text = "产品名称";
                plainFrm.ShowDialog();
            }
        }

        private void buttonSaveTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, "tb_order_item", dataGridViewItem);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PurchaseOrderFrm_Load(object sender, EventArgs e)
        {
            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, "tb_order_item", dataGridViewItem);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
