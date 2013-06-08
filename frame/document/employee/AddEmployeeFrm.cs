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
using haisan.frame.document.department;

namespace haisan.frame.document.employee
{
    public partial class AddEmployeeFrm : Form
    {
        EmployeeDao employeeDao = EmployeeDaoImpl.getInstance();
        private int employeeId = -1;

        public AddEmployeeFrm()
        {
            InitializeComponent();
            initComboBoxSex();
        }

        private void buttonSaveAdd_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee(employeeId);
            if (!checkParameter())
                return;
            fillEmployee(employee);

            this.Enabled = false;
            MessageLocal msg = employeeDao.saveOrUpdateEmployee(employee);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            if (!checkBoxIsCopy.Checked)
                clearFieds();

            employeeId = -1;
            this.Enabled = true;
        }

        private bool checkParameter()
        {
            bool success = true;
            string errorMsg = "";
            labelErrorStatus.Text = "";

            if ("".Equals(textBoxName.Text))
            {
                errorMsg += "【员工姓名】 ";
                success = false;
            }

            if ("".Equals(textBoxDepartment.Text))
            {
                errorMsg += "【所属部门】 ";
                success = false;
            }

            if (!success)
            {
                labelErrorStatus.Text = errorMsg + " 不能为空";
            }

            return success;
        }

        private void fillEmployee(Employee employee)
        {
            employee.Name = textBoxName.Text;
            employee.Code = textBoxName.Text;
            employee.Sex = getSex(comboBoxSex.Text);
            employee.Birthday = dateTimePickerBirthday.Value.Date;
            employee.IdCard = textBoxIdCard.Text;
            employee.Phone = textBoxPhone.Text;
            employee.JoinDate = dateTimePickerJoinDate.Value.Date;
            employee.ContractDate = dateTimePickerContractDate.Value.Date;
            employee.Department = (Department)textBoxDepartment.Tag;
            employee.Position = textBoxPosition.Text;
            employee.Address = textBoxAddress.Text;
            employee.IsLocked = checkBoxIsLock.Checked;
        }

        public void fillField()
        {
            Employee employee = employeeDao.getEmployeeById(employeeId);
            if (null == employee) return;

            textBoxName.Text = employee.Name;
            textBoxCode.Text = employee.Code;
            comboBoxSex.SelectedIndex = comboBoxSex.FindString(getSex(employee.Sex));
            dateTimePickerBirthday.Value = employee.Birthday;
            textBoxIdCard.Text = employee.IdCard;
            textBoxPhone.Text = employee.Phone;
            dateTimePickerJoinDate.Value = employee.JoinDate;
            dateTimePickerContractDate.Value = employee.ContractDate;
            textBoxDepartment.Tag = employee.Department;
            textBoxDepartment.Text = employee.Department.Name;
            textBoxPosition.Text = employee.Position;
            textBoxAddress.Text = employee.Address;
            checkBoxIsLock.Checked = employee.IsLocked;

        }

        private void clearFieds()
        {

            textBoxName.Text = "";
            textBoxName.Text = "";
            comboBoxSex.SelectedIndex = 0;
            dateTimePickerBirthday.Value = DateTime.Now;
            textBoxIdCard.Text = "";
            textBoxPhone.Text = "";
            dateTimePickerJoinDate.Value = DateTime.Now;
            dateTimePickerContractDate.Value = DateTime.Now;
            textBoxDepartment.Tag = null;
            textBoxDepartment.Text = "";
            textBoxPosition.Text = "";
            textBoxAddress.Text = "";

            checkBoxIsLock.Checked = false;
        }

        private bool getSex(string sex)
        {
            if (Parameter.MAN.Equals(sex))
                return true;
            return false;
        }

        private string getSex(bool sex)
        {
            if (sex) return Parameter.MAN;
            return Parameter.WONMAN;
        }

        private void buttonSaveQuit_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee(employeeId);
            if (!checkParameter())
                return;
            fillEmployee(employee);

            this.Enabled = false;
            MessageLocal msg = employeeDao.saveOrUpdateEmployee(employee);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            employeeId = -1;
            this.Enabled = true;

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DepartmentFrm departmentFrm = new DepartmentFrm(this);
            departmentFrm.Text = "选择部门";
            departmentFrm.StartPosition = FormStartPosition.CenterScreen;
            departmentFrm.ShowDialog();
        }

        public void setDepartment(Department department)
        {
            textBoxDepartment.Tag = department;
            textBoxDepartment.Text = department.Name;
        }

        private void AddEmployeeFrm_Load(object sender, EventArgs e)
        {

        }

        private void initComboBoxSex()
        {
            comboBoxSex.Items.Add(Parameter.MAN);
            comboBoxSex.Items.Add(Parameter.WONMAN);
            comboBoxSex.SelectedIndex = 0;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxCode.Text = Code.GetFirstPYLetter(textBoxName.Text.ToString());
        }

        public void setEmployeeId(int id)
        {
            employeeId = id;
        }

    }
}
