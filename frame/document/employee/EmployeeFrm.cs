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

namespace haisan.frame.document.employee
{
    public partial class EmployeeFrm : Form
    {
        EmployeeDao employeeDao = EmployeeDaoImpl.getInstance();
        BaseDao baseDao = BaseDaoImpl.getInstance();
        private readonly string TABLE_NAME = "tb_employee";

        public EmployeeFrm()
        {
            InitializeComponent();
        }

        private void EmployeeFrm_Load(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            refreshDataGridView();
            listHeadText();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, TABLE_NAME, dataGridViewEmployee);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void refreshDataGridView()
        {
            dataGridViewEmployee.DataSource = employeeDao.getAllEmployee(textBoxQuery.Text).Tables[0].DefaultView;
        }

        //设置DataGridView标题
        private void listHeadText()
        {
            int i = 0;
            dataGridViewEmployee.Columns[i++].HeaderText = "员工ID";
            dataGridViewEmployee.Columns[i++].HeaderText = "员工姓名";
            dataGridViewEmployee.Columns[i++].HeaderText = "姓名简码";
            dataGridViewEmployee.Columns[i++].HeaderText = "性别";
            dataGridViewEmployee.Columns[i++].HeaderText = "出生日期";
            dataGridViewEmployee.Columns[i++].HeaderText = "身份证";
            dataGridViewEmployee.Columns[i++].HeaderText = "电话";
            dataGridViewEmployee.Columns[i++].HeaderText = "入职日期";
            dataGridViewEmployee.Columns[i++].HeaderText = "合同日期";
            dataGridViewEmployee.Columns[i++].HeaderText = "部门名称";
            dataGridViewEmployee.Columns[i++].HeaderText = "职位";
            dataGridViewEmployee.Columns[i++].HeaderText = "地址";
            dataGridViewEmployee.Columns[i++].HeaderText = "是否停用";
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            refreshDataGridView();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEmployeeFrm addFrm = new AddEmployeeFrm();
            addFrm.Text = "添加员工信息";
            addFrm.ShowDialog();

            refreshDataGridView();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewEmployee.CurrentRow) return;
            int index = dataGridViewEmployee.CurrentRow.Index;
            AddEmployeeFrm addFrm = new AddEmployeeFrm();
            Console.WriteLine("修改ID[" + dataGridViewEmployee.Rows[index].Cells["Id"].Value.ToString() + "]的员工信息");
            addFrm.setEmployeeId(int.Parse(dataGridViewEmployee.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.fillField();
            addFrm.Text = "修改员工信息";
            addFrm.ShowDialog();

            refreshDataGridView();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewEmployee.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewEmployee.SelectedRows)
            {
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (dataGridViewEmployee.SelectedRows.Count > 0)
                ids.Remove(ids.Length - 1, 1);

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的员工信息!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities(TABLE_NAME, ids.ToString());
                this.Enabled = true;
            }

            refreshDataGridView();
        }

        private void buttonSaveTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, TABLE_NAME, dataGridViewEmployee);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void EmployeeFrm_Resize(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
        }

        private void dataGridViewEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            buttonModify_Click(null, null);
        }

    }
}
