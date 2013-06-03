using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.dao;
using haisan.domain;
using haisan.util;

namespace haisan.frame.pdm.purchase
{
    public partial class QueryPurchaseOrderFrm : Form
    {
        PurchaseOrderDao purchaseOrderDao = PurchaseOrderDaoImpl.getInstance();
        PurchaseOrderFrm purchaseOrderFrm = null;

        public QueryPurchaseOrderFrm()
        {
            InitializeComponent();
        }

        public QueryPurchaseOrderFrm(PurchaseOrderFrm purchaseOrderFrm)
        {
            InitializeComponent();
            this.purchaseOrderFrm = purchaseOrderFrm;
        }

        private void QueryPurchaseOrderFrm_Load(object sender, EventArgs e)
        {
            dateTimePickerBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            buttonQuery_Click(null, null);
            OrderListHeadText();
        }

        //设置DataGridView标题
        private void OrderListHeadText()
        {
            int i = 0;
            dataGridViewOrder.Columns[i++].HeaderText = "订单ID";
            dataGridViewOrder.Columns[i++].HeaderText = "订单编号";
            dataGridViewOrder.Columns[i++].HeaderText = "客户名";
            dataGridViewOrder.Columns[i++].HeaderText = "提货方式";
            dataGridViewOrder.Columns[i++].HeaderText = "联系电话";
            dataGridViewOrder.Columns[i++].HeaderText = "数量合计";
            dataGridViewOrder.Columns[i++].HeaderText = "件数合计";
            dataGridViewOrder.Columns[i++].HeaderText = "货款";
            dataGridViewOrder.Columns[i++].HeaderText = "加工费";
            dataGridViewOrder.Columns[i++].HeaderText = "总计款";
            dataGridViewOrder.Columns[i++].HeaderText = "预收款";
            dataGridViewOrder.Columns[i++].HeaderText = "操作员";
            dataGridViewOrder.Columns[i++].HeaderText = "创建日期";
            dataGridViewOrder.Columns[i++].Visible  = false;//最后两列隐藏，因为是外键。
            dataGridViewOrder.Columns[i++].Visible = false;
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            dataGridViewOrder.DataSource = purchaseOrderDao.getPurchaseOrder(textBoxSN.Text.ToString(), textBoxNAME.Text.ToString(),
                dateTimePickerBegin.Value, dateTimePickerEnd.Value).Tables[0].DefaultView;
        }

        private void dataGridViewOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderId = int.Parse(dataGridViewOrder.Rows[e.RowIndex].Cells["id"].Value.ToString());
            Order order = purchaseOrderDao.getOrderById(orderId);
            if(null == order){
                Util.showInformation("该订单【"+dataGridViewOrder.Rows[e.RowIndex].Cells["sn"].Value+"】已不存在于数据库");
                return;
            }
            purchaseOrderFrm.disableCellValueChanged();
            Console.WriteLine("72 of queryPurFrm purchaseOrderFrm.Enabled to false");
            purchaseOrderFrm.fillPurchaseOrderFrm(order);
            Console.WriteLine("74 of queryPurFrm purchaseOrderFrm.Enabled to true");
            purchaseOrderFrm.enableCellValueChanged();
            this.Close();
        }
    }
}
