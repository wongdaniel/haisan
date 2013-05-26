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

namespace haisan.frame.document.plain
{
    public partial class AddPlainFrm : Form
    {
        PlainDao plainDao = PlainDaoImpl.getInstance();
        private int plainId = -1;
        private string tableName = "";

        public AddPlainFrm()
        {
            InitializeComponent();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            labelErrStatus.Text = "";

            if ("".Equals(textBoxName.Text))
            {
                labelErrStatus.Text =  "【" + labelName.Text + "】，不能为空！";
                return;
            }

            MessageLocal msg = plainDao.saveOrUpdatePlain(new Plain(plainId, textBoxName.Text), tableName);
            if (!msg.IsSucess)
            {
                labelErrStatus.Text = msg.Message;
                return;
            }
            this.Close();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setPlainId(int plainId)
        {
            this.plainId = plainId;
        }

        public void setPlainName(string name)
        {
            textBoxName.Text = name;
        }

        public void setLabelName(string name)
        {
            labelName.Text = name;
        }

        public void setTableName(string name)
        {
            tableName = name;
        }
    }
}
