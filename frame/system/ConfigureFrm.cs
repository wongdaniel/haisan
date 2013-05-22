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

namespace haisan.frame.system
{
    public partial class ConfigureFrm : Form
    {
        ConfigureDao configureDao = ConfigureDaoImpl.getInstance();

        public ConfigureFrm()
        {
            InitializeComponent();
            readFields();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFields();
            this.Close();
        }

        private void saveFields()
        {
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_NAME, textBoxCompanyName.Text);
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_REPRESENT, textBoxRepresent.Text);
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_BANK, textBoxBank.Text);
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_BANK_ACCOUNT, textBoxAccount.Text);
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_PHONE, textBoxPhone.Text);
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_POSTAL_CODE, textBoxPostalCode.Text);
            configureDao.saveOrUpdateValueByName(Parameter.COMPANY_ADDRESS, textBoxAddress.Text);
        }

        private void readFields()
        {
            textBoxCompanyName.Text = configureDao.getValueByName(Parameter.COMPANY_NAME);
            textBoxRepresent.Text = configureDao.getValueByName(Parameter.COMPANY_REPRESENT);
            textBoxBank.Text = configureDao.getValueByName(Parameter.COMPANY_BANK);
            textBoxAccount.Text = configureDao.getValueByName(Parameter.COMPANY_BANK_ACCOUNT);
            textBoxPhone.Text = configureDao.getValueByName(Parameter.COMPANY_PHONE);
            textBoxPostalCode.Text = configureDao.getValueByName(Parameter.COMPANY_POSTAL_CODE);
            textBoxAddress.Text = configureDao.getValueByName(Parameter.COMPANY_ADDRESS);
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
