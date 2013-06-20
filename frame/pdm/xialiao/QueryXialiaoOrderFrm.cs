﻿using System;
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

namespace haisan.frame.pdm.xialiao
{
    public partial class QueryXialiaoOrderFrm : Form
    {
        XialiaoOrderDao xialiaoOrderDao = XialiaoOrderDaoImpl.getInstance();
        XialiaoOrderFrm xialiaoOrderFrm = null;

        public QueryXialiaoOrderFrm()
        {
            InitializeComponent();
        }

        public QueryXialiaoOrderFrm(XialiaoOrderFrm xialiaoOrderFrm)
        {
            InitializeComponent();
            this.xialiaoOrderFrm = xialiaoOrderFrm;
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
            dataGridViewOrder.Columns[i++].HeaderText = "操作员";
            dataGridViewOrder.Columns[i++].HeaderText = "创建日期";
            dataGridViewOrder.Columns[i++].Visible  = false;//最后三列隐藏，因为是外键。
            dataGridViewOrder.Columns[i++].Visible = false;
            dataGridViewOrder.Columns[i++].Visible = false;
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            dataGridViewOrder.DataSource = xialiaoOrderDao.getXialiaoOrder(textBoxSN.Text.ToString(), textBoxNAME.Text.ToString(),
                dateTimePickerBegin.Value, dateTimePickerEnd.Value).Tables[0].DefaultView;
        }

        private void dataGridViewOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xialiaoOrderId = int.Parse(dataGridViewOrder.Rows[e.RowIndex].Cells["id"].Value.ToString());
            XialiaoOrder xialiaoOrder = xialiaoOrderDao.getXialiaoOrderById(xialiaoOrderId);
            if (null == xialiaoOrder)
            {
                Util.showInformation("该下料订单【"+dataGridViewOrder.Rows[e.RowIndex].Cells["sn"].Value+"】已不存在于数据库");
                return;
            }
            xialiaoOrderFrm.disableCellValueChanged();
            xialiaoOrderFrm.disableTextChanged();

            xialiaoOrderFrm.fillXialiaoOrderFrm(xialiaoOrder);

            xialiaoOrderFrm.enableCellValueChanged();
            xialiaoOrderFrm.enableTextChanged();
            this.Close();
        }
    }
}
