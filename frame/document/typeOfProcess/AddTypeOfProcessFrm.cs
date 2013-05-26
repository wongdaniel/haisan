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

namespace haisan.frame.document.typeOfProcess
{
    public partial class AddTypeOfProcessFrm : Form
    {

        TypeOfProcessDao typeOfProcessDao = TypeOfProcessDaoImpl.getInstance();
 
        private int typeOfProcessId = -1;

        public AddTypeOfProcessFrm()
        {
            InitializeComponent();
            fillUnits();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            labelErrStatus.Text = "";

            if ("".Equals(textBoxName.Text) || (null == comboBoxUnit.SelectedItem && "".Equals(comboBoxUnit.Text)))
            {
                if("".Equals(textBoxName.Text))
                    labelErrStatus.Text += "【加工名称】 ";
                if (null == comboBoxUnit.SelectedItem && "".Equals(comboBoxUnit.Text))
                    labelErrStatus.Text += "【单位】 ";
                labelErrStatus.Text += "，不能为空！";
                return;
            }


            TypeOfProcess typeOfProcess = new TypeOfProcess();
            typeOfProcess.Id = typeOfProcessId;
            typeOfProcess.Name = textBoxName.Text;

            if (null != comboBoxUnit.SelectedItem)
                typeOfProcess.Unit = comboBoxUnit.SelectedItem.ToString();
            else
            {
                if (!"".Equals(comboBoxUnit.Text))
                    typeOfProcess.Unit = comboBoxUnit.Text;
            }

            MessageLocal msg = typeOfProcessDao.saveOrUpdateTypeOfProcess(typeOfProcess);
            if (!msg.IsSucess)
            {
                labelErrStatus.Text = msg.Message;
                return;
            }
            this.Close();
        }

        public void SetTypeOfProcessId(int id)
        {
            typeOfProcessId = id;
        }

        public void setName(string name)
        {
            textBoxName.Text = name;
        }

        public void setUnit(string unit)
        {
            comboBoxUnit.SelectedIndex = comboBoxUnit.FindString(unit);

            if (-1 == comboBoxUnit.SelectedIndex)
                comboBoxUnit.SelectedItem = "unit";
        }

        private void fillUnits()
        {
            comboBoxUnit.Items.Add("m");
            comboBoxUnit.Items.Add("件");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
