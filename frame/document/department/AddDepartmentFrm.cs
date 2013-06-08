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

namespace haisan.frame.document.department
{
    public partial class AddDepartmentFrm : Form
    {
        DepartmentDao departmentDao = DepartmentDaoImpl.getInstance();
        private int departmentId = -1;

        public AddDepartmentFrm()
        {
            InitializeComponent();
        }

        private void buttonSaveAdd_Click(object sender, EventArgs e)
        {
            if(saveDepartment())
                clearFieds();
        }

        private void buttonSaveQuit_Click(object sender, EventArgs e)
        {
            if (saveDepartment())
                this.Close();
        }

        private bool saveDepartment()
        {
            Department department = new Department(departmentId);
            if (!checkParameter())
                return false;
            fillField(department);

            this.Enabled = false;
            MessageLocal msg = departmentDao.saveOrUpdateDepartment(department);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return false;
            }

            departmentId = -1;
            this.Enabled = true;

            return true;
        }

        private bool checkParameter()
        {
            bool success = true;
            string errorMsg = "";
            labelErrorStatus.Text = "";

            if ("".Equals(textBoxName.Text))
            {
                errorMsg += "【部门名称】 ";
                success = false;
            }

            if (!success)
            {
                labelErrorStatus.Text = errorMsg + " 不能为空";
            }

            return success;
        }

        public void fillField()
        {
            Department department = departmentDao.getDepartmentById(departmentId);
            if (null == department) return;

            textBoxName.Text = department.Name;
            textBoxDescription.Text = department.Description;
        }

        private void fillField(Department department)
        {
            department.Name = textBoxName.Text;
            department.Description = textBoxDescription.Text;
        }

        private void clearFieds()
        {
            textBoxName.Text = "";
            textBoxDescription.Text = "";
        }

        public void setId(int departmentId)
        {
            this.departmentId = departmentId;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
