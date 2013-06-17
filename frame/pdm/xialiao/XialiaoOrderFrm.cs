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
using haisan.frame.document.typeOfProcess;
using System.Globalization;
using haisan.domain;
using System.Text.RegularExpressions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Diagnostics;
using haisan.frame.pdm.purchase;

namespace haisan.frame.pdm.xialiao
{
    public partial class XialiaoOrderFrm : Form
    {
        private BaseDao baseDao = BaseDaoImpl.getInstance();
        private XialiaoOrderDao xialiaoOrderDao = XialiaoOrderDaoImpl.getInstance();
        PurchaseOrderDao purchaseOrderDao = PurchaseOrderDaoImpl.getInstance();

        private static string[] units = { Parameter.SQUARE_METER, Parameter.STERE, Parameter.METER, Parameter.PACKAGE };

        private static readonly int ILLEGAEL_ORDERID = -1;
        private static readonly string TABLE_NAME = "tb_xialiao_order_item";

        private int orderID = ILLEGAEL_ORDERID;
        private int xialiaoOrderId = ILLEGAEL_ORDERID;

        public XialiaoOrderFrm()
        {
            InitializeComponent();
            initDataGridViewItem();
        }

        public DataGridView DataGridViewItem
        {
            get { return dataGridViewItem; }
            set { dataGridViewItem = value; }
        }

        public DataGridView DataGridViewItemStats
        {
            get { return dataGridViewItemStats; }
            set { dataGridViewItemStats = value; }
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


        private void buttonSaveTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, TABLE_NAME, dataGridViewItem);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        
        }

        private void XialiaoOrderFrm_Load(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            initField();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, TABLE_NAME, dataGridViewItem);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void initField()
        {
            label1Operator.Text = Parameter.user.Username;
            label1CreateDate.Text = DateTime.Now.ToShortDateString();

            comboBoxGetStyle.Items.Add(Parameter.GET_CASH);
            comboBoxGetStyle.Items.Add(Parameter.GET_SIGN);

        }
        
        // 用以验证当前输入的值，是否合法。
        private void dataGridViewItem_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Console.WriteLine("135 of purFrm 触发：dataGridViewItem_CellValidating");
            dataGridViewItem.Rows[e.RowIndex].ErrorText = "";
            if (dataGridViewItem.Rows[e.RowIndex].IsNewRow)
                return;

            string columnName = dataGridViewItem.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("ColumnUsePackage"))
            {
                if (!Util.isDigitalGreaterZero(e.FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "格式非法！只能输入大于0的数字";
                    e.Cancel = true;
                    return;
                }

                if(Util.getIntValue(e.FormattedValue.ToString()) >
                    Util.getIntValue(dataGridViewItem.Rows[e.RowIndex].Cells["ColumnRemainPackage"].FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "本次下件数不能大于未下件数";
                    e.Cancel = true;
                    return;
                }
            }
        }

        //CellValueChanged事件是发生在，当Cell的Value值发生改变时触发，所以如果需要用tag保存对象，一定要在修改value之前完成。
        private void dataGridViewItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridViewItem.Columns[e.ColumnIndex].Name;
            Console.WriteLine("time:" + DateTime.Now.Second+ ":" + DateTime.Now.Millisecond);
            Console.WriteLine("触发CellValueChanged: " + columnName);

            if (columnName.Equals("ColumnUsePackage"))
            {
                columnPackageChanged(sender, e, columnName);
                refreshTotalFiled();
            }
        }

        private void columnPackageChanged(object sender, DataGridViewCellEventArgs e, string columnName)
        {
            //       Console.WriteLine("将刷新，数量，以及数量1,数量2，数量3");
            refreshColumnNumber(sender, e, columnName);

            refreshColumnNumberX(sender, e, "ColumnDiagram1");
            refreshColumnNumberX(sender, e, "ColumnDiagram2");
            refreshColumnNumberX(sender, e, "ColumnDiagram3");
        }

        //刷新ColumnNumber[1-3]
        private void refreshColumnNumberX(object sender, DataGridViewCellEventArgs e, string columnName)
        {
            int index = int.Parse(columnName.Substring(columnName.Length - 1, 1));
            string name = "ColumnName" + index;
            string diagram = "ColumnDiagram" + index;
            string number = "ColumnNumber" + index;

            decimal value = 0;
            if (-1 == columnName.IndexOf("ColumnName"))
            {
                if (Util.isDecimal(getFormattedValue(e, number)))
                    value = -decimal.Parse(getFormattedValue(e, number));
            }

            if (null == dataGridViewItem.Rows[e.RowIndex].Cells[name].Tag
                || null == dataGridViewItem.Rows[e.RowIndex].Cells[diagram].Tag)
            {
                dataGridViewItem.Rows[e.RowIndex].Cells[number].Value = 0;
            }
            else
            {
                TypeOfProcess typePro = (TypeOfProcess)dataGridViewItem.Rows[e.RowIndex].Cells[name].Tag;
                if (Parameter.PACKAGE.Equals(typePro.Unit))
                {
                    dataGridViewItem.Rows[e.RowIndex].Cells[number].Value = getFormattedValue(e, "ColumnUsePackage");
                }
                else if (Parameter.METER.Equals(typePro.Unit))
                {
                    ProcessingImage proImage = (ProcessingImage)dataGridViewItem.Rows[e.RowIndex].Cells[diagram].Tag;
                    int length = 0, width = 0, package = 0;

                    if (Util.isLengthOrWidth(getFormattedValue(e, "ColumnLength")))
                        length = Util.getValueOfLWT2(getFormattedValue(e, "ColumnLength"));

                    if (Util.isLengthOrWidth(getFormattedValue(e, "ColumnWidth")))
                        width = Util.getValueOfLWT2(getFormattedValue(e, "ColumnWidth"));

                    if (Util.isDigital(getFormattedValue(e, "ColumnUsePackage")))
                        package = int.Parse(getFormattedValue(e, "ColumnUsePackage"));

                    decimal sum = 0;
                    if (proImage.Up)
                        sum += ((decimal)length) / 1000 * package;
                    if (proImage.Down)
                        sum += ((decimal)length) / 1000 * package;
                    if (proImage.Left)
                        sum += ((decimal)width) / 1000 * package;
                    if (proImage.Right)
                        sum += ((decimal)width) / 1000 * package;
                    dataGridViewItem.Rows[e.RowIndex].Cells[number].Value = decimal.Round(sum, Parameter.NUMBER_MANTISSA);

                }
                else
                {
                    MessageBox.Show("不认识的加工类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                value = Util.getDecimalValue(dataGridViewItem.Rows[e.RowIndex].Cells[number].FormattedValue.ToString()) + value;
                insertIntoStats((TypeOfProcess)dataGridViewItem.Rows[e.RowIndex].Cells[name].Tag, dataGridViewItem.Rows[e.RowIndex].Cells["ColumnThickness"].Value.ToString(), value);
            }
        }

        //刷新金额
        private void refreshColumnCost(object sender, DataGridViewCellEventArgs e, string columnName)
        {
            decimal number = 0, price = 0;
            if (Util.isDecimal(getFormattedValue(e, "ColumnNumber")))
                number = decimal.Parse(getFormattedValue(e, "ColumnNumber"));

            if (Util.isDecimal(getFormattedValue(e, "ColumnUnitPrice")))
                price = decimal.Parse(getFormattedValue(e, "ColumnUnitPrice"));

            dataGridViewItem.Rows[e.RowIndex].Cells["ColumnCost"].Value = decimal.Round(number * price, Parameter.NUMBER_MANTISSA);
        }

        //改变数量
        private void refreshColumnNumber(object sender, DataGridViewCellEventArgs e, string columnName)
        {

            if (getFormattedValue(e, "ColumnUnit").Equals(Parameter.PACKAGE))
            {
                dataGridViewItem.Rows[e.RowIndex].Cells["ColumnNumber"].Value = getFormattedValue(e, "ColumnUsePackage");
            }
            else if (!columnName.Equals("ColumnUsePackage"))
            {
                int length = 0, width = 0, thickness = 0;

                if (Util.isLengthOrWidth(getFormattedValue(e, "ColumnLength")))
                    length = Util.getValueOfLWT(getFormattedValue(e, "ColumnLength"));

                if (Util.isLengthOrWidth(getFormattedValue(e, "ColumnWidth")))
                    width = Util.getValueOfLWT(getFormattedValue(e, "ColumnWidth"));
                if (Util.isDecimal(getFormattedValue(e, "ColumnThickness")))
                    thickness = Util.getValueOfLWT(getFormattedValue(e, "ColumnThickness"));

                dataGridViewItem.Rows[e.RowIndex].Cells["ColumnNumber"].Value = decimal.Round(computeNumber(getFormattedValue(e, "ColumnUnit"),
                    length, width, thickness), Parameter.NUMBER_MANTISSA);
            }
        }

        public void insertIntoStats(TypeOfProcess typeOfProcess, string thickness, decimal number)
        {
            Console.WriteLine("insertIntoStats  number:[" + number + "]");

            bool found = false;
            foreach (DataGridViewRow row in dataGridViewItemStats.Rows)
            {
                if (typeOfProcess.Equals(row.Cells["ColumnProcessingName"].Tag) && thickness.Equals(row.Cells["ColumnThicknessStats"].Value.ToString()))
                {
                    decimal numberStats = 0;
                    if (Util.isDecimal(row.Cells["ColumnNumberStats"].FormattedValue.ToString()))
                        numberStats = decimal.Parse(row.Cells["ColumnNumberStats"].FormattedValue.ToString());

                    row.Cells["ColumnNumberStats"].Value = numberStats + number;
                    Console.WriteLine("278 of purFrm row.Cells[ColumnNumberStats].Value: [" + row.Cells["ColumnNumberStats"].Value.ToString() + "]");
                    if (0  == Util.getDecimalValue(row.Cells["ColumnNumberStats"].Value.ToString()))
                    {
                        Console.WriteLine("delete row:[" + typeOfProcess.Name + "]");
                        dataGridViewItemStats.Rows.Remove(row);
                        return;
                    }
                    found = true;
                    break;
                }
            }

            if (!found)
            {

                //  row.CreateCells(dataGridViewItemStats);
                int index = dataGridViewItemStats.Rows.Add();
                DataGridViewRow row = dataGridViewItemStats.Rows[index];

                row.Cells["ColumnStatsID"].Value = 0;
                row.Cells["ColumnProcessingName"].Tag = typeOfProcess;
                row.Cells["ColumnProcessingName"].Value = typeOfProcess.Name.ToString();
                row.Cells["ColumnThicknessStats"].Value = thickness;
                row.Cells["ColumnProcessingDiagram"].Value = null;
                row.Cells["ColumnUnitStats"].Value = typeOfProcess.Unit.ToString();
                row.Cells["ColumnNumberStats"].Value = number.ToString();
                row.Cells["ColumnUnitPriceStats"].Value = 0;
                row.Cells["ColumnCostStats"].Value = 0;

                //     dataGridViewItemStats.Rows.Add(row);
            }
        }

        // 该方法只能使用在dataGridViewItem上。
        private string getFormattedValue(DataGridViewCellEventArgs e, string columnName)
        {
            return dataGridViewItem.Rows[e.RowIndex].Cells[columnName].FormattedValue.ToString();
        }

        private string getFormattedValue(DataGridView dataGridView, DataGridViewCellEventArgs e, string columnName)
        {
            return dataGridView.Rows[e.RowIndex].Cells[columnName].FormattedValue.ToString();
        }

        private decimal computeNumber(string unit, int length, int width, int thickness)
        {
            if (unit.Equals(Parameter.SQUARE_METER))
            {
                return ((decimal)length / 1000) * ((decimal)width / 1000);
            }
            else if (unit.Equals(Parameter.STERE))
            {
                return ((decimal)length / 1000) * ((decimal)width / 1000) * ((decimal)thickness / 1000);
            }
            else if ((unit.Equals(Parameter.METER)))
            {
                return (decimal)length;
            }
            else
            {
                return 0;
            }
        }

        private void dataGridViewItemStats_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridViewItemStats.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("ColumnNumberStats") || columnName.Equals("ColumnUnitPriceStats"))
            {
                refreshColumnCostStats(sender, e);
            }
            else if (columnName.Equals("ColumnCostStats"))
            {
                refreshStatsFields();
            }
        }

        private void refreshColumnCostStats(object sender, DataGridViewCellEventArgs e)
        {
            decimal numberStats = 0, priceStats = 0;
            numberStats = Util.getDecimalValue(getFormattedValue(dataGridViewItemStats, e, "ColumnNumberStats"));
            priceStats = Util.getDecimalValue(getFormattedValue(dataGridViewItemStats, e, "ColumnUnitPriceStats"));

            dataGridViewItemStats.Rows[e.RowIndex].Cells["ColumnCostStats"].Value =
                decimal.Round(numberStats * priceStats, Parameter.NUMBER_MANTISSA);
        }

        private void refreshStatsFields()
        {
            decimal sum = 0;
            foreach (DataGridViewRow row in dataGridViewItemStats.Rows)
            {
                sum += Util.getDecimalValue(row.Cells["ColumnCostStats"].FormattedValue.ToString());
            }

            textBoxProcessing.Text = sum.ToString();

            textBoxTotalCost.Text = (sum + decimal.Parse(textBoxPayment.Text)).ToString();
        }

        private void refreshTotalFiled()
        {
            decimal totalNumber = 0, totalPayment = 0;
            int totalPackage = 0;
            foreach (DataGridViewRow row in dataGridViewItem.Rows)
            {
                totalNumber += Util.getDecimalValue(row.Cells["ColumnNumber"].FormattedValue.ToString());
                totalPackage += Util.getIntValue(row.Cells["ColumnUsePackage"].FormattedValue.ToString());
                totalPayment += Util.getDecimalValue(row.Cells["ColumnCost"].FormattedValue.ToString());
            }

            textBoxTotalNumber.Text = totalNumber.ToString();
            textBoxTotalPackage.Text = totalPackage.ToString();
            textBoxPayment.Text = totalPayment.ToString();

            textBoxTotalCost.Text = (decimal.Parse(textBoxProcessing.Text) + decimal.Parse(textBoxPayment.Text)).ToString();
        }

        private void XialiaoOrderFrm_Resize(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
        }



        private XialiaoOrder getXialiaoOrder()
        {
            XialiaoOrder xialiaoOrder = constructXialiaoOrder();
            Console.WriteLine("dataGridViewItem.Rows.Count:" + dataGridViewItem.Rows.Count);

            foreach (DataGridViewRow row in dataGridViewItem.Rows)
            {
                if (row.IsNewRow) continue;
                XialiaoOrderItem item = constructXialiaoOrderItem(xialiaoOrder, row);
                xialiaoOrder.XialiaoOrderItems.AddLast(item);
            }

            Console.WriteLine("dataGridViewItemStats.Rows.Count: " + dataGridViewItemStats.Rows.Count);
            foreach (DataGridViewRow row in dataGridViewItemStats.Rows)
            {
                if (row.IsNewRow) continue;

                XialiaoOrderStats stats = constructXialiaoOrderStats(xialiaoOrder, row);
                xialiaoOrder.XialiaoOrderstats.AddLast(stats);
            }

            return xialiaoOrder;
        }

        private XialiaoOrder constructXialiaoOrder()
        {
            XialiaoOrder xialiaoOrder = new XialiaoOrder(xialiaoOrderId);

            xialiaoOrder.Order = new Order(orderID);
            xialiaoOrder.Sn = textBoxSN.Text.ToString();
            xialiaoOrder.CreateDate = DateTime.Now;
            xialiaoOrder.Operatr = (User)label1Operator.Tag == null ? Parameter.user : (User)label1Operator.Tag;
            xialiaoOrder.TotalNumber = Util.getDecimalValue(textBoxTotalNumber.Text.ToString());
            xialiaoOrder.TotalPackages = Util.getIntValue(textBoxTotalPackage.Text.ToString());
            xialiaoOrder.Payment = Util.getDecimalValue(textBoxPayment.Text.ToString());
            xialiaoOrder.ProcessingCharges = Util.getDecimalValue(textBoxProcessing.Text.ToString());
            xialiaoOrder.TotalCost = Util.getDecimalValue(textBoxTotalCost.Text.ToString());
            return xialiaoOrder;
        }

        private bool checkParameterForOrder()
        {
            bool success = true;
            string msg = "";

            if ("".Equals(textBoxCompany.Text))
            {
                msg += "【客户名】";
                success = false;
            }

            if(-1 == comboBoxOrderSN.SelectedIndex || "".Equals(comboBoxOrderSN.Text)){
                msg += " 【销售订单】";
                success = false;
            }

             // 因为只读， 石材名称或产品名称不用检查

            if(!success){
                Util.showInformation(msg + ", 不能为空！");
            }
           
            return success;
        }

        public void fillXialiaoOrderFrm(XialiaoOrder xialiaoOrder)
        {
            fillXialiaoOrderOnly(xialiaoOrder);

            int index = 0;

            disableCellValueValidating();

            dataGridViewItem.Rows.Clear();
            dataGridViewItemStats.Rows.Clear();

            disableCellValueChanged();

            foreach (XialiaoOrderItem item in xialiaoOrder.XialiaoOrderItems)
            {
                index = dataGridViewItem.Rows.Add();
                fillXialiaoOrderItemRow(item, dataGridViewItem.Rows[index]);
            }

            foreach (XialiaoOrderStats stats in xialiaoOrder.XialiaoOrderstats)
            {
                index = dataGridViewItemStats.Rows.Add();
                fillXialiaoOrderStatsRow(stats, dataGridViewItemStats.Rows[index]);
            }

            enableCellValueChanged();
            enableCellValueValidating();

            refreshTotalFiled();
        }

        private XialiaoOrderItem constructXialiaoOrderItem(XialiaoOrder xialiaoOrder, DataGridViewRow row)
        {
            XialiaoOrderItem item = new XialiaoOrderItem(Util.getIntValue(row.Cells["ColumnXialiaoItemID"].FormattedValue.ToString()));
            item.OrderItem = new OrderItem(Util.getIntValue(row.Cells["ColumnItemID"].FormattedValue.ToString()));
            item.XialiaoOrder = xialiaoOrder;
            item.OriginalPackage = int.Parse(row.Cells["ColumnPackage"].Value.ToString());
            item.RemainPackage = int.Parse(row.Cells["ColumnRemainPackage"].Value.ToString());
            item.UsePackage = int.Parse(row.Cells["ColumnUsePackage"].Value.ToString());

            item.WorkingNumber1 = Util.getDecimalValue(row.Cells["ColumnNumber1"].Value.ToString());
            item.WorkingNumber2 = Util.getDecimalValue(row.Cells["ColumnNumber2"].Value.ToString());
            item.WorkingNumber3 = Util.getDecimalValue(row.Cells["ColumnNumber3"].Value.ToString());

            return item;
        }

        private XialiaoOrderStats constructXialiaoOrderStats(XialiaoOrder xialiaoOrder, DataGridViewRow row)
        {
            XialiaoOrderStats stats = new XialiaoOrderStats(Util.getIntValue(row.Cells["ColumnXialiaoStatsID"].FormattedValue.ToString()));

            stats.OrderStats = new OrderStats(Util.getIntValue(row.Cells["ColumnStatsID"].Value.ToString()));
            stats.XialiaoOrder = xialiaoOrder;
            stats.TotalNumber = Util.getDecimalValue(row.Cells["ColumnNumberStats"].Value.ToString());
            stats.AmountOfMoney = Util.getDecimalValue(row.Cells["ColumnCostStats"].Value.ToString());

            return stats;
        }


        private void 保存订单NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            if (!checkParameterForOrder())
            {
                this.Enabled = true;
                return;
            }

            XialiaoOrder xialiaoOrder = getXialiaoOrder();
            MessageLocal msg = xialiaoOrderDao.saveOrUpdateXialiaoOrder(xialiaoOrder);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("526 purchase msg.Message: " + msg.Message);
            }
            else
            {
                textBoxSN.Text = xialiaoOrder.Sn;
                orderID = xialiaoOrder.Order.Id;
                xialiaoOrderId = xialiaoOrder.Id;

                Console.WriteLine("orderId:["+orderID+"] xialiaoOrderId:["+xialiaoOrderId+"]");

                int i = 0;
                foreach (XialiaoOrderItem item in xialiaoOrder.XialiaoOrderItems)
                {
                    dataGridViewItem.Rows[i++].Cells["ColumnXialiaoItemID"].Value = item.Id;
                }
                i = 0;
                foreach (XialiaoOrderStats stats in xialiaoOrder.XialiaoOrderstats)
                {
                    dataGridViewItemStats.Rows[i++].Cells["ColumnXialiaoStatsID"].Value = stats.Id;
                }

            }
            this.Enabled = true;
        }

        private void 查询订单QToolStripMenuItem_Click(object sender, EventArgs e)
        {
       //     QueryPurchaseOrderFrm queryFrm = new QueryPurchaseOrderFrm(this);
        //    queryFrm.Text = "查询订单";
        //    queryFrm.ShowDialog();
        }

        public void disableCellValueChanged()
        {
            this.dataGridViewItem.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItem_CellValueChanged);
            this.dataGridViewItemStats.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemStats_CellValueChanged);
        }

        public void enableCellValueChanged()
        {
            this.dataGridViewItem.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItem_CellValueChanged);
            this.dataGridViewItemStats.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemStats_CellValueChanged);
        }

        public void disableCellValueValidating()
        {
            this.dataGridViewItem.CellValidating -= new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewItem_CellValidating);
        }

        public void enableCellValueValidating()
        {
            this.dataGridViewItem.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewItem_CellValidating);
        }

        private void 新增订单NToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            clearAllField();
            this.Enabled = true;
        }

        private void clearAllField()
        {

            orderID = ILLEGAEL_ORDERID;
            xialiaoOrderId = ILLEGAEL_ORDERID;

            textBoxSN.Text = "";
            textBoxCompany.Tag = null;
            textBoxCompany.Text = "";

            comboBoxOrderSN.Items.Clear();

            comboBoxGetStyle.SelectedIndex = -1;
            comboBoxGetStyle.Text = "";

            textBoxPhone.Text = "";
            label1CreateDate.Text = "";
            label1Operator.Text = Parameter.user.Name;
            label1Operator.Tag = Parameter.user;

            textBoxTotalNumber.Text = "0";
            textBoxTotalPackage.Text = "0";
            textBoxPayment.Text = "0";
            textBoxProcessing.Text = "0";
            textBoxTotalCost.Text = "0";

            disableCellValueValidating();
            dataGridViewItem.Rows.Clear();
            dataGridViewItemStats.Rows.Clear();
            enableCellValueValidating();
        }

        private void 删除订单DToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ILLEGAEL_ORDERID == orderID)
            {
                MessageBox.Show("没有可以供删除的订单!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("将删除编号为[" + textBoxSN.Text + "]的订单!", "警告",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities("tb_order", orderID.ToString());
                this.Enabled = true;
                clearAllField();
            }
        }

        private void 报表预览PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReportOrderFrm roFrm = new ReportOrderFrm();
                Order order = purchaseOrderDao.loadOrderById(orderID);
                if (null == order) return;

                ParameterFields paramFields = new ParameterFields();

                Util.addParameterField(paramFields, "sn", order.Sn);
                Util.addParameterField(paramFields, "customName", order.Company.Name);
                Util.addParameterField(paramFields, "totalPackage", order.TotalPackages.ToString());
                Util.addParameterField(paramFields, "totalNumber", order.TotalNumber.ToString());

                string str = Application.StartupPath.ToString();
                str = str.Substring(0, str.LastIndexOf("\\"));
                str = str.Substring(0, str.LastIndexOf("\\"));
                str += @"\Resources\CrystalReportOrderItem.rpt";

                Console.WriteLine("str:" + str);
                ReportDocument rdDoc = new ReportDocument();
                rdDoc.Load(str);

                DataSet dataset = purchaseOrderDao.getOrderItems(order);
                rdDoc.SetDataSource(dataset.Tables[0]);


                ReportDocument rdStuff = rdDoc.Subreports["CrystalReportOrderStats.rpt"];
                dataset = purchaseOrderDao.getOrderStats(order);
                rdStuff.SetDataSource(dataset.Tables[0]);


                roFrm.setReportSoruce(rdDoc);

                // roFrm.refreshReport(); // 不能刷新，如果刷新，将会弹出参数窗口
                roFrm.setParameterFields(paramFields);

                roFrm.Show();
            }
            catch (Exception ex)
            {
                Util.showError(ex.Message);
            }
        }


        private bool checkParameterZeroBeforeClickNameXOrDiagramX(DataGridViewRow row)
        {
            return Util.isZeroDecimal(row.Cells["ColumnLength"].Value.ToString()) || Util.isZeroDecimal(row.Cells["ColumnWidth"].Value.ToString()) ||
                Util.isZeroDecimal(row.Cells["ColumnThickness"].Value.ToString()) || Util.isZeroDecimal(row.Cells["ColumnPackage"].Value.ToString());
        }

        private void comboBoxOrderSN_TextChanged(object sender, EventArgs e)
        {

            int id = ((Order)((ComboBox)sender).SelectedItem).Id;
            Console.WriteLine("id : " + id);

            DateTime begin = DateTime.Now;
            XialiaoOrder xialiaoOrder = xialiaoOrderDao.getXialiaoOrderByOrderId(id);
            DateTime mid = DateTime.Now;
            Console.WriteLine("read date cost time in  xialiaoOrderFrm:" + (mid - begin));

            fillXialiaoOrderFrm(xialiaoOrder);

        }



        private void textBoxCompany_TextChanged(object sender, EventArgs e)
        {
            if (!textBoxCompany.Text.ToString().Equals(""))
            {
                //由于往来单位还没有实现，暂时这样初始化company对象
                comboBoxOrderSN.Items.Clear();
                LinkedList<Order> orders = xialiaoOrderDao.getAllOrders((Company)textBoxCompany.Tag);
                foreach (Order order in orders)
                {
                    //int index = 
                    comboBoxOrderSN.Items.Add(order);
                    //comboBoxOrderSN.Items[index].
                }

            }
        }

        private void fillXialiaoOrderOnly(XialiaoOrder xialiaoOrder)
        {
            xialiaoOrderId = xialiaoOrder.Id;
            orderID = xialiaoOrder.Order.Id; // 因为下料订单无法修改，这里只保存了销售订单id
            textBoxSN.Text = xialiaoOrder.Sn;
            textBoxCompany.Tag = xialiaoOrder.Order.Company;
            textBoxCompany.Text = xialiaoOrder.Order.Company.Name;
            comboBoxGetStyle.SelectedIndex = comboBoxGetStyle.FindString(xialiaoOrder.Order.WayOfPayment); // 如果是任意填写的会咋样？
            if (-1 == comboBoxGetStyle.SelectedIndex)
                comboBoxGetStyle.Text = xialiaoOrder.Order.WayOfPayment;

            textBoxPhone.Text = xialiaoOrder.Order.Phone;
            label1CreateDate.Text = xialiaoOrder.CreateDate.ToShortDateString();
            label1Operator.Text = xialiaoOrder.Operatr.Username;
            label1Operator.Tag = xialiaoOrder.Operatr;

            textBoxTotalPackage.Text = xialiaoOrder.TotalPackages.ToString();
            textBoxPayment.Text = xialiaoOrder.Payment.ToString();
            textBoxProcessing.Text = xialiaoOrder.ProcessingCharges.ToString();
            textBoxTotalCost.Text = xialiaoOrder.TotalCost.ToString();
        }

        private void fillXialiaoOrderItemRow(XialiaoOrderItem xialiaoItem, DataGridViewRow row)
        {
            OrderItem item = xialiaoItem.OrderItem;

            row.Cells["ColumnXialiaoItemID"].Value = xialiaoItem.Id;
            row.Cells["ColumnItemID"].Value = xialiaoItem.OrderItem.Id;
            row.Cells["ColumnCategoryStone"].Tag = item.CategoryOfStone;
            row.Cells["ColumnCategoryStone"].Value = item.CategoryOfStone.Name;
            row.Cells["ColumnProductName"].Tag = item.ProductName;
            row.Cells["ColumnProductName"].Value = item.ProductName.Name;

            row.Cells["ColumnLength"].Value = item.Length;
            row.Cells["ColumnWidth"].Value = item.Width;
            row.Cells["ColumnThickness"].Value = item.Thickness;
            row.Cells["ColumnPackage"].Value = xialiaoItem.OriginalPackage; //
            row.Cells["ColumnRemainPackage"].Value = xialiaoItem.RemainPackage; //
            row.Cells["ColumnUsePackage"].Value = xialiaoItem.UsePackage; //
            row.Cells["ColumnUnit"].Value = item.Unit;
            row.Cells["ColumnNumber"].Value = item.Number;
            row.Cells["ColumnUnitPrice"].Value = item.UnitPrice;
            row.Cells["ColumnCost"].Value = item.Cost;

            row.Cells["ColumnDiagram1"].Value = null == item.WorkingDiagram1 ? null : item.WorkingDiagram1.Image;
            row.Cells["ColumnDiagram1"].Tag = item.WorkingDiagram1;
            row.Cells["ColumnName1"].Tag = item.WorkingName1;
            row.Cells["ColumnName1"].Value = (null == item.WorkingName1 ? "" : item.WorkingName1.Name);
            row.Cells["ColumnNumber1"].Value = xialiaoItem.WorkingNumber1; //


            row.Cells["ColumnDiagram2"].Value = null == item.WorkingDiagram2 ? null : item.WorkingDiagram2.Image;
            row.Cells["ColumnDiagram2"].Tag = item.WorkingDiagram2;
            row.Cells["ColumnName2"].Tag = item.WorkingName2;
            row.Cells["ColumnName2"].Value = (null == item.WorkingName2 ? "" : item.WorkingName2.Name);
            row.Cells["ColumnNumber2"].Value = xialiaoItem.WorkingNumber2; //

            row.Cells["ColumnDiagram3"].Value = null == item.WorkingDiagram3 ? null : item.WorkingDiagram3.Image;
            row.Cells["ColumnDiagram3"].Tag = item.WorkingDiagram3;
            row.Cells["ColumnName3"].Tag = item.WorkingName3;
            row.Cells["ColumnName3"].Value = (null == item.WorkingName3 ? "" : item.WorkingName3.Name);
            row.Cells["ColumnNumber3"].Value = xialiaoItem.WorkingNumber3; //
        }


        private void fillXialiaoOrderStatsRow(XialiaoOrderStats xialiaoStats, DataGridViewRow row)
        {

            OrderStats stats = xialiaoStats.OrderStats;

            row.Cells["ColumnXialiaoStatsID"].Value = xialiaoStats.Id;

            row.Cells["ColumnStatsID"].Value = stats.Id;
            row.Cells["ColumnProcessingName"].Tag = stats.TypeOfProcess;
            row.Cells["ColumnProcessingName"].Value = stats.TypeOfProcess.Name;
            row.Cells["ColumnThicknessStats"].Value = stats.ThicknessStats;
            row.Cells["ColumnProcessingDiagram"].Tag = stats.Image;
            row.Cells["ColumnProcessingDiagram"].Value = Util.getThumbnailImage(stats.Image);
            row.Cells["ColumnDWG"].Tag = stats.Dwg;
            row.Cells["ColumnUnitStats"].Value = stats.Unit;
            row.Cells["ColumnNumberStats"].Value = xialiaoStats.TotalNumber; //
            row.Cells["ColumnUnitPriceStats"].Value = stats.UnitPrice;
            row.Cells["ColumnCostStats"].Value = xialiaoStats.AmountOfMoney; //
        }

        private void dataGridViewItemStats_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridViewItemStats.Columns[e.ColumnIndex].Name;
            if ("ColumnProcessingDiagram".Equals(columnName))
            {
                Image image = (Image)dataGridViewItemStats.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                if (null != image)
                {
                    ShowImageFrm showFrm = new ShowImageFrm(image);
                    showFrm.Size = new Size(image.Width + 50, image.Height + 50);
                    showFrm.ShowDialog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCompany.Tag = new Company(1);
            textBoxCompany.Text = "微软有限公司";
        }

    }
}
