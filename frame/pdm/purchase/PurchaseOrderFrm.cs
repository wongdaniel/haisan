﻿using System;
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

namespace haisan.frame.pdm.purchase
{
    public partial class PurchaseOrderFrm : Form
    {
        private BaseDao baseDao = BaseDaoImpl.getInstance();
        private PurchaseOrderDao purchaseOrderDao = PurchaseOrderDaoImpl.getInstance();

        private static string[] units = { Parameter.SQUARE_METER, Parameter.STERE, Parameter.METER, Parameter.PACKAGE };
        private static readonly string regexIncludeDigital = "^(ColumnName[1-3]|ColumnDiagram[1-3])$";

        private static readonly int ILLEGAEL_ORDERID = -1;
        private static readonly int NUMBER_OF_PROCESS = 3;

        private int orderID = ILLEGAEL_ORDERID;
        private int currentRow = -1;
        private int currentColumn = -1;

        private string fileName = "";

        public PurchaseOrderFrm()
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

        private void dataGridViewItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (-1 == e.RowIndex) return;

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
            else if (-1 != columnName.IndexOf("ColumnDiagram"))
            {
                if(checkParameterZeroBeforeClickNameXOrDiagramX(dataGridViewItem.Rows[e.RowIndex]))
                {
                    Util.showInformation("请先初始化长、宽、厚、件数");
                    return;
                }

                BrowseImage broImage = new BrowseImage(dataGridViewItem.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                broImage.Text = "浏览图片";
                broImage.ShowDialog();

            }
            else if (-1 != columnName.IndexOf("ColumnName"))
            {

                if (checkParameterZeroBeforeClickNameXOrDiagramX(dataGridViewItem.Rows[e.RowIndex]))
                {
                    Util.showInformation("请先初始化长、宽、厚、件数");
                    return;
                }

                TypeOfProcessFrm typeOfProcessFrm = new TypeOfProcessFrm(this, e.ColumnIndex, e.RowIndex);
                typeOfProcessFrm.Text = "加工类型";
                typeOfProcessFrm.ShowDialog();
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
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            initField();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, "tb_order_item", dataGridViewItem);
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

            initFirstRowOfDataGridView();
        }

        private void initFirstRowOfDataGridView()
        {

            disableCellValueChanged();

            if (dataGridViewItem.Rows.Count > 0)
            {
                dataGridViewItem.Rows[0].Cells["ColumnLength"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnWidth"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnThickness"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnPackage"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnNumber"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnUnitPrice"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnCost"].Value = "0";

                dataGridViewItem.Rows[0].Cells["ColumnNumber1"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnNumber2"].Value = "0";
                dataGridViewItem.Rows[0].Cells["ColumnNumber3"].Value = "0";

            }

            enableCellValueChanged();
            
        }
        

        // 用以验证当前输入的值，是否合法。
        private void dataGridViewItem_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Console.WriteLine("135 of purFrm 触发：dataGridViewItem_CellValidating");
            dataGridViewItem.Rows[e.RowIndex].ErrorText = "";
            if (dataGridViewItem.Rows[e.RowIndex].IsNewRow)
                return;

            string columnName = dataGridViewItem.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("ColumnLength") || columnName.Equals("ColumnWidth"))
            {
                if (!Util.isLengthOrWidth(e.FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "格式非法，数字需大于0！,输入格式: 123(123)";
                    e.Cancel = true;
                    return;
                }
            }
            else if (columnName.Equals("ColumnThickness"))
            {
                if (!Util.isDigitalGreaterZero(e.FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "格式非法！只能输入大于0数字";
                    e.Cancel = true;
                    return;
                }

                // 在加工统计表里，去掉旧的厚度统计
                Console.WriteLine("delete the old thickness");
                string thickness = dataGridViewItem.Rows[e.RowIndex].Cells["ColumnThickness"].Value.ToString();
                if (Util.getDecimalValue(thickness) > 0)
                {
                    if (e.FormattedValue.ToString().Equals(thickness))
                        return;
                    deleteProcessStats(dataGridViewItem.Rows[e.RowIndex], thickness, false);
                }

            }else if(columnName.Equals("ColumnPackage"))
            {
                if (!Util.isDigitalGreaterZero(e.FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "格式非法！只能输入大于0的数字";
                    e.Cancel = true;
                    return;
                }
            }
            else if (columnName.Equals("ColumnUnitPrice"))
            {
                if (!Util.isDecimal(e.FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "格式非法！只能输入小数";
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

            Regex regex = new Regex(regexIncludeDigital);
            if (regex.IsMatch(columnName))
            {
         //         Console.WriteLine("regex match");
                refreshColumnNumberX(sender, e, columnName);
            }
            else if (columnName.Equals("ColumnNumber") || columnName.Equals("ColumnUnitPrice"))
            {
           //        Console.WriteLine("更新价格");
                refreshColumnCost(sender, e, columnName);
                refreshTotalFiled();
            }
            else if (columnName.Equals("ColumnPackage") || columnName.Equals("ColumnLength") || columnName.Equals("ColumnWidth")
               || columnName.Equals("ColumnThickness") || columnName.Equals("ColumnUnit"))
            {
         //       Console.WriteLine("将刷新，数量，以及数量1,数量2，数量3");
                if(columnName.Equals("ColumnPackage"))
                    refreshTotalFiled();

                refreshColumnNumber(sender, e, columnName);
                if (columnName.Equals("ColumnPackage") || columnName.Equals("ColumnLength") || columnName.Equals("ColumnWidth"))
                {
                    refreshColumnNumberX(sender, e, "ColumnDiagram1");
                    refreshColumnNumberX(sender, e, "ColumnDiagram2");
                    refreshColumnNumberX(sender, e, "ColumnDiagram3");
                }

                if (columnName.Equals("ColumnThickness"))
                {
                    // 在加工统计表里，加上新的厚度统计
                    if (Util.getDecimalValue(dataGridViewItem.Rows[e.RowIndex].Cells["ColumnThickness"].Value.ToString()) > 0)
                        deleteProcessStats(dataGridViewItem.Rows[e.RowIndex],
                            dataGridViewItem.Rows[e.RowIndex].Cells["ColumnThickness"].Value.ToString(), true);
                }
            }
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
                    dataGridViewItem.Rows[e.RowIndex].Cells[number].Value = getFormattedValue(e, "ColumnPackage");
                }
                else if (Parameter.METER.Equals(typePro.Unit))
                {
                    ProcessingImage proImage = (ProcessingImage)dataGridViewItem.Rows[e.RowIndex].Cells[diagram].Tag;
                    int length = 0, width = 0, package = 0;

                    if (Util.isLengthOrWidth(getFormattedValue(e, "ColumnLength")))
                        length = Util.getValueOfLWT2(getFormattedValue(e, "ColumnLength"));

                    if (Util.isLengthOrWidth(getFormattedValue(e, "ColumnWidth")))
                        width = Util.getValueOfLWT2(getFormattedValue(e, "ColumnWidth"));

                    if (Util.isDigital(getFormattedValue(e, "ColumnPackage")))
                        package = int.Parse(getFormattedValue(e, "ColumnPackage"));

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
                dataGridViewItem.Rows[e.RowIndex].Cells["ColumnNumber"].Value = getFormattedValue(e, "ColumnPackage");
            }
            else
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
                totalPackage += Util.getIntValue(row.Cells["ColumnPackage"].FormattedValue.ToString());
                totalPayment += Util.getDecimalValue(row.Cells["ColumnCost"].FormattedValue.ToString());
            }

            textBoxTotalNumber.Text = totalNumber.ToString();
            textBoxTotalPackage.Text = totalPackage.ToString();
            textBoxPayment.Text = totalPayment.ToString();

            textBoxTotalCost.Text = (decimal.Parse(textBoxProcessing.Text) + decimal.Parse(textBoxPayment.Text)).ToString();
        }

        private void PurchaseOrderFrm_Resize(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
        }



        private Order getOrder()
        {
            Order order = constructOrder();
            Console.WriteLine("dataGridViewItem.Rows.Count:" + dataGridViewItem.Rows.Count);

            int i = 0;
            foreach (DataGridViewRow row in dataGridViewItem.Rows)
            {
                if (row.IsNewRow) continue;
                i++;
                OrderItem item = constructOrderItem(order, row);
                order.OrderItems.AddLast(item);
            }
            Console.WriteLine("i:" + i);

            Console.WriteLine("dataGridViewItemStats.Rows.Count: " + dataGridViewItemStats.Rows.Count);
            foreach (DataGridViewRow row in dataGridViewItemStats.Rows)
            {
                if (row.IsNewRow) continue;

                OrderStats stats = constructOrderStats(order, row);
                order.OrderStats.AddLast(stats);
            }

            return order;
        }

        private Order constructOrder()
        {
            Order order = new Order();

            order.Id = orderID;
            order.Sn = textBoxSN.Text.ToString();
            order.Company = new Company(int.Parse(textBoxCompany.Text.ToString())); // 先暂时这样写，以后需要用textbox上的tag直接复制。
            if (null != comboBoxGetStyle.SelectedItem)
                order.WayOfPayment = comboBoxGetStyle.SelectedItem.ToString();
            else
                order.WayOfPayment = comboBoxGetStyle.Text.ToString();
            order.Phone = textBoxPhone.Text.ToString();
            order.CreateDate = DateTime.Now;
            order.Operatr = (User)label1Operator.Tag == null ? Parameter.user : (User)label1Operator.Tag;
            order.TotalNumber = Util.getDecimalValue(textBoxTotalNumber.Text.ToString());
            order.TotalPackages = Util.getIntValue(textBoxTotalPackage.Text.ToString());
            order.Payment = Util.getDecimalValue(textBoxPayment.Text.ToString());
            order.ProcessingCharges = Util.getDecimalValue(textBoxProcessing.Text.ToString());
            order.TotalCost = Util.getDecimalValue(textBoxTotalCost.Text.ToString());
            order.AdvancesReceived = Util.getDecimalValue(textBoxAdvanceReceived.Text.ToString());
            return order;
        }

        private bool checkParameterForOrder()
        {
            bool success = true;
            if ("".Equals(textBoxCompany.Text))
            {
                MessageBox.Show("客户名，不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                success = false;
            }

            foreach (DataGridViewRow row in dataGridViewItem.Rows)
            {
                if (row.IsNewRow) continue;

                row.ErrorText = "";
                if ((null == row.Cells["ColumnCategoryStone"].Value) || (null == row.Cells["ColumnProductName"].Value))
                {
                    row.ErrorText = "石材名称或产品名称不能为空";
                    success = false;
                }

            }

            return success;
        }

        public void fillPurchaseOrderFrm(Order order)
        {
            fillOrderOnly(order);

            int index = 0;

            disableCellValueValidating();

            dataGridViewItem.Rows.Clear();
            dataGridViewItemStats.Rows.Clear();

            enableCellValueValidating();

            foreach (OrderItem item in order.OrderItems)
            {
                index = dataGridViewItem.Rows.Add();
                fillOrderItemRow(item, dataGridViewItem.Rows[index]);
            }
           
            foreach (OrderStats stats in order.OrderStats)
            {
                index = dataGridViewItemStats.Rows.Add();
                fillOrderStatsRow(stats, dataGridViewItemStats.Rows[index]);
            }
        }

        private void fillOrderOnly(Order order)
        {
            orderID = order.Id;
            textBoxSN.Text = order.Sn;
            textBoxCompany.Tag = order.Company;
            textBoxCompany.Text = order.Company.Name;
            comboBoxGetStyle.SelectedIndex = comboBoxGetStyle.FindString(order.WayOfPayment); // 如果是任意填写的会咋样？
            if (-1 == comboBoxGetStyle.SelectedIndex)
                comboBoxGetStyle.Text = order.WayOfPayment;
            Console.WriteLine("479 of PurchaseOrderFrm: comboBoxGetStyle.SelectedIndex" + comboBoxGetStyle.SelectedIndex);

            textBoxPhone.Text = order.Phone;
            label1CreateDate.Text = order.CreateDate.ToShortDateString();
            label1Operator.Text = order.Operatr.Username;
            label1Operator.Tag = order.Operatr;

            textBoxTotalNumber.Text = order.TotalNumber.ToString();
            textBoxTotalPackage.Text = order.TotalPackages.ToString();
            textBoxPayment.Text = order.Payment.ToString();
            textBoxProcessing.Text = order.ProcessingCharges.ToString();
            textBoxTotalCost.Text = order.TotalCost.ToString();
            textBoxAdvanceReceived.Text = order.AdvancesReceived.ToString();
        }

        private OrderItem constructOrderItem(Order order, DataGridViewRow row)
        {
            OrderItem item = new OrderItem();
            item.Id = Util.getIntValue(row.Cells["ColumnItemID"].FormattedValue.ToString());
            item.Order = order;
            item.CategoryOfStone = (CategoryOfStone)row.Cells["ColumnCategoryStone"].Tag;
            item.ProductName = (ProductName)row.Cells["ColumnProductName"].Tag;
            item.Length = row.Cells["ColumnLength"].Value.ToString();
            item.Width = row.Cells["ColumnWidth"].Value.ToString();
            item.Thickness = row.Cells["ColumnThickness"].Value.ToString();
            item.Package = int.Parse(row.Cells["ColumnPackage"].Value.ToString());
            item.Unit = row.Cells["ColumnUnit"].FormattedValue.ToString();
            item.Number = Util.getDecimalValue(row.Cells["ColumnNumber"].Value.ToString());
            item.UnitPrice = Util.getDecimalValue(row.Cells["ColumnUnitPrice"].Value.ToString());
            item.Cost = Util.getDecimalValue(row.Cells["ColumnCost"].Value.ToString());
            item.WorkingDiagram1 = (ProcessingImage)row.Cells["ColumnDiagram1"].Tag;
            item.WorkingName1 = (TypeOfProcess)row.Cells["ColumnName1"].Tag;
            item.WorkingNumber1 = Util.getDecimalValue(row.Cells["ColumnNumber1"].Value.ToString());
            item.WorkingDiagram2 = (ProcessingImage)row.Cells["ColumnDiagram2"].Tag;
            item.WorkingName2 = (TypeOfProcess)row.Cells["ColumnName2"].Tag;
            item.WorkingNumber2 = Util.getDecimalValue(row.Cells["ColumnNumber2"].Value.ToString());
            item.WorkingDiagram3 = (ProcessingImage)row.Cells["ColumnDiagram3"].Tag;
            item.WorkingName3 = (TypeOfProcess)row.Cells["ColumnName3"].Tag;
            item.WorkingNumber3 = Util.getDecimalValue(row.Cells["ColumnNumber3"].Value.ToString());
            return item;
        }

        private void fillOrderItemRow(OrderItem item, DataGridViewRow row)
        {
            row.Cells["ColumnItemID"].Value = item.Id;
            row.Cells["ColumnCategoryStone"].Tag = item.CategoryOfStone;
            row.Cells["ColumnCategoryStone"].Value = item.CategoryOfStone.Name;
            row.Cells["ColumnProductName"].Tag = item.ProductName;
            row.Cells["ColumnProductName"].Value = item.ProductName.Name;

            row.Cells["ColumnLength"].Value = item.Length;
            row.Cells["ColumnWidth"].Value = item.Width;
            row.Cells["ColumnThickness"].Value = item.Thickness;
            row.Cells["ColumnPackage"].Value = item.Package;
            row.Cells["ColumnUnit"].Value = item.Unit;
            row.Cells["ColumnNumber"].Value = item.Number;
            row.Cells["ColumnUnitPrice"].Value = item.UnitPrice;
            row.Cells["ColumnCost"].Value = item.Cost;

            row.Cells["ColumnDiagram1"].Value = null == item.WorkingDiagram1 ? null : item.WorkingDiagram1.Image;
            row.Cells["ColumnDiagram1"].Tag = item.WorkingDiagram1;
            row.Cells["ColumnName1"].Tag = item.WorkingName1;
            row.Cells["ColumnName1"].Value = (null == item.WorkingName1 ? "" : item.WorkingName1.Name);
            row.Cells["ColumnNumber1"].Value = item.WorkingNumber1;


            row.Cells["ColumnDiagram2"].Value = null == item.WorkingDiagram2 ? null : item.WorkingDiagram2.Image;
            row.Cells["ColumnDiagram2"].Tag = item.WorkingDiagram2;
            row.Cells["ColumnName2"].Tag = item.WorkingName2;
            row.Cells["ColumnName2"].Value = (null == item.WorkingName2 ? "" : item.WorkingName2.Name);
            row.Cells["ColumnNumber2"].Value = item.WorkingNumber2;

            row.Cells["ColumnDiagram3"].Value = null == item.WorkingDiagram3 ? null : item.WorkingDiagram3.Image;
            row.Cells["ColumnDiagram3"].Tag = item.WorkingDiagram3;
            row.Cells["ColumnName3"].Tag = item.WorkingName3;
            row.Cells["ColumnName3"].Value = (null == item.WorkingName3 ? "" : item.WorkingName3.Name);
            row.Cells["ColumnNumber3"].Value = item.WorkingNumber3;
        }

        private OrderStats constructOrderStats(Order order, DataGridViewRow row)
        {
            OrderStats stats = new OrderStats();

            stats.Id = Util.getIntValue(row.Cells["ColumnStatsID"].Value.ToString());
            stats.Order = order;
            stats.TypeOfProcess = (TypeOfProcess)row.Cells["ColumnProcessingName"].Tag;
            stats.ThicknessStats = row.Cells["ColumnThicknessStats"].Value.ToString();
            stats.Image = (Image)row.Cells["ColumnProcessingDiagram"].Tag;
            stats.Dwg = (byte[])row.Cells["ColumnDWG"].Tag;
            stats.Unit = row.Cells["ColumnUnitStats"].Value.ToString();
            stats.TotalNumber = Util.getDecimalValue(row.Cells["ColumnNumberStats"].Value.ToString());
            stats.UnitPrice = Util.getDecimalValue(row.Cells["ColumnUnitPriceStats"].Value.ToString());
            stats.AmountOfMoney = Util.getDecimalValue(row.Cells["ColumnCostStats"].Value.ToString());

            return stats;
        }

        private void fillOrderStatsRow(OrderStats stats, DataGridViewRow row)
        {

            row.Cells["ColumnStatsID"].Value = stats.Id;
            row.Cells["ColumnProcessingName"].Tag = stats.TypeOfProcess;
            row.Cells["ColumnProcessingName"].Value = stats.TypeOfProcess.Name;
            row.Cells["ColumnThicknessStats"].Value = stats.ThicknessStats;
            row.Cells["ColumnProcessingDiagram"].Tag = stats.Image;
            row.Cells["ColumnProcessingDiagram"].Value = Util.getThumbnailImage(stats.Image);
            row.Cells["ColumnDWG"].Tag = stats.Dwg;
            row.Cells["ColumnUnitStats"].Value = stats.Unit;
            row.Cells["ColumnNumberStats"].Value = stats.TotalNumber;
            row.Cells["ColumnUnitPriceStats"].Value = stats.UnitPrice;
            row.Cells["ColumnCostStats"].Value = stats.AmountOfMoney;
        }

        private void 保存订单NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            if (!checkParameterForOrder())
            {
                this.Enabled = true;
                return;
            }

            Order order = getOrder();
            MessageLocal msg = purchaseOrderDao.saveOrUpdatePurchaseOrder(order);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("526 purchase msg.Message: " + msg.Message);
            }
            else
            {
                textBoxSN.Text = order.Sn;
                orderID = order.Id;

                int i = 0;
                foreach (OrderItem item in order.OrderItems)
                {
                    dataGridViewItem.Rows[i++].Cells["ColumnItemID"].Value = item.Id;
                }
                i = 0;
                foreach (OrderStats stats in order.OrderStats)
                {
                    dataGridViewItemStats.Rows[i++].Cells["ColumnStatsID"].Value = stats.Id;
                }
            }
            this.Enabled = true;
        }

        private void 查询订单QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryPurchaseOrderFrm queryFrm = new QueryPurchaseOrderFrm(this);
            queryFrm.Text = "查询订单";
            queryFrm.ShowDialog();
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
            initFirstRowOfDataGridView();
            this.Enabled = true;
        }

        private void clearAllField()
        {

            orderID = ILLEGAEL_ORDERID;
            textBoxSN.Text = "";
            textBoxCompany.Tag = null;
            textBoxCompany.Text = "";
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
            textBoxAdvanceReceived.Text = "0";

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

        private void dataGridViewItemStats_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentColumn = e.ColumnIndex;
            currentRow = e.RowIndex;
        }

        private void dataGridViewItemStats_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            currentColumn = -1;
            currentRow = -1;
        }

        private void dataGridViewItemStats_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (currentRow >= 0
                    && "ColumnProcessingDiagram".Equals(dataGridViewItemStats.Columns[currentColumn].Name))
                {
                    disableMouseEvent();
                    contextMenuStripDiagram.Show(dataGridViewItemStats, new Point(e.X, e.Y));
                }

            }
            else if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("出发鼠标左键！row:[" + currentRow + "] column:[" + currentColumn + "]");
                if (currentRow >= 0
                  && "ColumnProcessingDiagram".Equals(dataGridViewItemStats.Columns[currentColumn].Name))
                {
                    Image image = (Image)dataGridViewItemStats.Rows[currentRow].Cells[currentColumn].Tag;
                    if (null != image)
                    {
                        ShowImageFrm showFrm = new ShowImageFrm(image);
                        showFrm.Size = new Size(image.Width + 50, image.Height + 50);
                        showFrm.ShowDialog();
                    }
                }
            }
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // ofd.InitialDirectory = Parameter.IMG_TEMP;
            ofd.Filter = "Image Files(*.dwg)|*.dwg|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.Title = "加载CAD文件";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                refreshImage(ofd.FileName);
            }
        }

        private void 保存文件SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.InitialDirectory = Parameter.IMG_TEMP;
            sfd.Filter = "Image Files(*.dwg)|*.dwg|All files (*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = "保存CAD模板图片";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)sfd.OpenFile();
                byte[] bytes = (byte[])dataGridViewItemStats.Rows[currentRow].Cells["ColumnDWG"].Tag;
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }

        private void contextMenuStripDiagram_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            
            enableMouseEvent();
        }

        private void disableMouseEvent()
        {
            this.dataGridViewItemStats.CellMouseEnter -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemStats_CellMouseEnter);
            this.dataGridViewItemStats.CellMouseLeave -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemStats_CellMouseLeave);
        }

        private void enableMouseEvent()
        {
            this.dataGridViewItemStats.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemStats_CellMouseLeave);
            this.dataGridViewItemStats.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemStats_CellMouseEnter);
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

        private void dataGridViewItem_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            disableCellValueChanged();

            e.Row.Cells["ColumnLength"].Value = "0";
            e.Row.Cells["ColumnWidth"].Value = "0";
            e.Row.Cells["ColumnThickness"].Value = "0";
            e.Row.Cells["ColumnPackage"].Value = "0";
            e.Row.Cells["ColumnNumber"].Value = "0";
            e.Row.Cells["ColumnUnitPrice"].Value = "0";
            e.Row.Cells["ColumnCost"].Value = "0";

            e.Row.Cells["ColumnNumber1"].Value = "0";
            e.Row.Cells["ColumnNumber2"].Value = "0";
            e.Row.Cells["ColumnNumber3"].Value = "0";

            enableCellValueChanged();
        }

        //在打印日志时发现ToolStripMenuItem的单击事件比contextMenuStrip的关闭事件完。 但这并不能证明，这种假设就一定成立
        // 因为日志有缓冲的因素，还需要查看c#的事件触发链。 用this.Enabled = false; 可以屏蔽鼠标事件。
        private void 编辑文件EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                byte[] bytes = (byte[])dataGridViewItemStats.Rows[currentRow].Cells["ColumnDWG"].Tag;

                fileName = Parameter.USER_HOME + @"\tmp" + DateTime.Now.Ticks + ".dwg";
                if (File.Exists(fileName))
                {
                    MessageBox.Show("文件[" + fileName + "]已存在，无法创建临时文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                StreamWriter sw = File.CreateText(fileName);
                sw.Close();

                FileStream dwgF = new FileStream(fileName, FileMode.Open, FileAccess.Write);
                dwgF.Write(bytes, 0, bytes.Length);
                dwgF.Close();

                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = (Parameter.PATH_AUTOCAD);
                info.Arguments = fileName;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                process1.StartInfo = info;
                process1.Start();
            }
            catch (Exception ex)
            {
                Util.showError(ex.Message);
                this.Enabled = true;
            }
        }

        private void process1_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("955 currentRow:[" + currentRow + "] currentColumn:[" + currentColumn + "]");
            refreshImage(fileName);
            File.Delete(fileName);
            string bakFileName = fileName.Substring(0, fileName.LastIndexOf(".")) + ".bak";
 //           Console.WriteLine("bakFileName:"+ bakFileName);
            if (File.Exists(bakFileName))
                File.Delete(bakFileName);
            this.Enabled = true;
        }

        private void refreshImage(string fileName)
        {
            try
            {
                Image reducedImage = CAD.getThumbnailDWG(fileName);
                dataGridViewItemStats.Rows[currentRow].Cells[currentColumn].Value = Util.getThumbnailImage(reducedImage); ;
                dataGridViewItemStats.Rows[currentRow].Cells[currentColumn].Tag = reducedImage;
                dataGridViewItemStats.Rows[currentRow].Cells["ColumnDWG"].Tag = Util.getBytes(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void deleteProcessStats(DataGridViewRow row, string thickness, bool add)
        {
            for (int i = 1; i <= NUMBER_OF_PROCESS; i++)
            {
                string numberX = "ColumnNumber" + i;
                if (0 == Util.getDecimalValue(row.Cells[numberX].Value.ToString()) || row.Cells["ColumnName" + i].Tag == null ||
                    row.Cells["ColumnDiagram" + i].Value == null)
                {
                    continue;
                }

               // Console.WriteLine("will add[" + add + "] number:[" + Util.getDecimalValue(row.Cells[numberX].Value.ToString()) + "]");
                insertIntoStats((TypeOfProcess)row.Cells["ColumnName" + i].Tag, thickness,
                    add ? Util.getDecimalValue(row.Cells[numberX].Value.ToString()) : -Util.getDecimalValue(row.Cells[numberX].Value.ToString()));
            }
        }

        private bool checkParameterZeroBeforeClickNameXOrDiagramX(DataGridViewRow row)
        {
            return Util.isZeroDecimal(row.Cells["ColumnLength"].Value.ToString()) || Util.isZeroDecimal(row.Cells["ColumnWidth"].Value.ToString()) ||
                Util.isZeroDecimal(row.Cells["ColumnThickness"].Value.ToString()) || Util.isZeroDecimal(row.Cells["ColumnPackage"].Value.ToString());
        }

    }
}
