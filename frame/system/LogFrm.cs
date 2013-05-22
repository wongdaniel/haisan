using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.dao;

namespace haisan.frame.system
{
    public partial class LogFrm : Form
    {
        BaseDao baseDao = BaseDaoImpl.getInstance();
        LogDao logDao = LogDaoImpl.getInstance();

        public LogFrm()
        {
            InitializeComponent();
        }

        private void LogFrm_Load(object sender, EventArgs e)
        {
            initComboxOperator();
            dateTimePickerBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            buttonQuery_Click(null, null);
            LogListHeadText();
        }

        private void initComboxOperator()
        {
            int count = 0;
            DataSet dataset = baseDao.getAllEntities("tb_user");
            if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
            {
                int i = 0;
                comboBoxOperator.Items.Add("");
                for (i = 0; i < count; i++)
                {
                    comboBoxOperator.Items.Add(dataset.Tables[0].Rows[i]["username"].ToString());
                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            string oper = "";
            if (null != comboBoxOperator.SelectedItem)
                oper = comboBoxOperator.SelectedItem.ToString();
            dataGridViewLog.DataSource = logDao.getLog(textBoxModule.Text, oper,
                dateTimePickerBegin.Value.Date, dateTimePickerEnd.Value.Date).Tables[0].DefaultView;

        }


        //设置DataGridView标题
        private void LogListHeadText()
        {
            int i = 0;
            dataGridViewLog.Columns[i++].HeaderText = "日志ID";
            dataGridViewLog.Columns[i++].HeaderText = "操作员";
            dataGridViewLog.Columns[i].Width += 42;
            dataGridViewLog.Columns[i++].HeaderText = "操作时间";
            dataGridViewLog.Columns[i++].HeaderText = "操作模块";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            string oper = "";
            if (null != comboBoxOperator.SelectedItem)
                oper = comboBoxOperator.SelectedItem.ToString();
            logDao.delLog(textBoxModule.Text, oper,
                dateTimePickerBegin.Value.Date, dateTimePickerEnd.Value.Date);
            buttonQuery_Click(null, null);
            this.Enabled = true;
        }
    }
}
