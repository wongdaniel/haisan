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

namespace haisan.frame.pdm.purchase
{
    public partial class PurchaseOrderFrm : Form
    {
        private BaseDao baseDao = BaseDaoImpl.getInstance();
        private static string[] units = { Parameter.SQUARE_METER, Parameter.STERE, Parameter.METER, Parameter.PACKAGE };
        private static readonly string regexIncludeDigital = "^(ColumnName[1-3]|ColumnDiagram[1-3])$";

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
                BrowseImage broImage = new BrowseImage(dataGridViewItem.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                broImage.Text = "浏览图片";
                broImage.ShowDialog();

            }
            else if (-1 != columnName.IndexOf("ColumnName"))
            {
                TypeOfProcessFrm typeOfProcessFrm = new TypeOfProcessFrm(dataGridViewItem.Rows[e.RowIndex].Cells[e.ColumnIndex]);
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
            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, "tb_order_item", dataGridViewItem);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // 用以验证当前输入的值，是否合法。
        private void dataGridViewItem_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dataGridViewItem.Rows[e.RowIndex].ErrorText = "";
            if (dataGridViewItem.Rows[e.RowIndex].IsNewRow)
                return;

            string columnName = dataGridViewItem.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("ColumnLength") || columnName.Equals("ColumnWidth"))
            {
                if (!Util.isLengthOrWidth(e.FormattedValue.ToString()))
                {
                    dataGridViewItem.Rows[e.RowIndex].ErrorText = "格式非法！,输入格式: 123(123)";
                    e.Cancel = true;
                    return;
                }
            }
            else if (columnName.Equals("ColumnThickness") || columnName.Equals("ColumnPackage"))
            {
                if(!Util.isDigital(e.FormattedValue.ToString()))
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

            Regex regex = new Regex(regexIncludeDigital);
            if (regex.IsMatch(columnName))
            {
              //  Console.WriteLine("触发" + columnName);
                refreshColumnNumberX(sender, e, columnName);
            }else if(columnName.Equals("ColumnNumber") || columnName.Equals("ColumnUnitPrice")){
             //   Console.WriteLine("触发" + columnName);
                refreshColumnCost(sender, e, columnName);
            }else if (columnName.Equals("ColumnPackage") || columnName.Equals("ColumnLength")|| columnName.Equals("ColumnWidth")
                || columnName.Equals("ColumnThickness") || columnName.Equals("ColumnUnit")){
             //   Console.WriteLine("触发" + columnName);
                refreshColumnNumber(sender, e, columnName);
            }

        }

        //刷新ColumnNumber[1-3]
        private void refreshColumnNumberX(object sender, DataGridViewCellEventArgs e, string columnName)
        {
            int index = int.Parse(columnName.Substring(columnName.Length - 1, 1));
            string name = "ColumnName" + index;
            string diagram = "ColumnDiagram" + index;
            string number = "ColumnNumber" + index;

             if(dataGridViewItem.Rows[e.RowIndex].Cells[name].Tag == null 
                 || null == dataGridViewItem.Rows[e.RowIndex].Cells[diagram].Tag){
                 dataGridViewItem.Rows[e.RowIndex].Cells[number].Value = 0;
             }else{
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
                         package = int.Parse(getFormattedValue(e, "ColumnWidth"));

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

        private string getFormattedValue(DataGridViewCellEventArgs e, string columnName)
        {
            return dataGridViewItem.Rows[e.RowIndex].Cells[columnName].FormattedValue.ToString();
        }

        private decimal computeNumber(string unit, int length, int width, int thickness)
        {
            if (unit.Equals(Parameter.SQUARE_METER))
            {
                return ((decimal)length / 1000) * ((decimal)width / 1000); 
            }else if(unit.Equals(Parameter.STERE))
            {
                return ((decimal)length / 1000) * ((decimal)width / 1000) * ((decimal)thickness / 1000);
            }else if((unit.Equals(Parameter.METER)))
            {
                return (decimal)length;
            }else {
                return 0;
            }
        }

    }
}
