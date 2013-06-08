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
using haisan.frame.document.employee;
using haisan.domain;

namespace haisan.frame.document.department
{
    public partial class DepartmentFrm : Form
    {

        BaseDao baseDao = BaseDaoImpl.getInstance();
        DepartmentDao departmentDao = DepartmentDaoImpl.getInstance();

        private readonly string TB_NAME = "tb_department";
        private AddEmployeeFrm addEmployeeFrm = null;

        public DepartmentFrm()
        {
            InitializeComponent();
        }

        public DepartmentFrm(AddEmployeeFrm addEmployeeFrm)
        {
            InitializeComponent();
            this.addEmployeeFrm = addEmployeeFrm;
        }

        private void DepartmentFrm_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            departmentListHeadText();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, TB_NAME, dataGridViewDepartment);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void refreshDataGridView()
        {
            dataGridViewDepartment.DataSource = baseDao.getAllEntities(TB_NAME).Tables[0].DefaultView;
        }

        //设置DataGridView标题
        private void departmentListHeadText()
        {
            int i = 0;
            dataGridViewDepartment.Columns[i++].HeaderText = "部门ID";
            dataGridViewDepartment.Columns[i++].HeaderText = "部门名";
            dataGridViewDepartment.Columns[i++].HeaderText = "描述";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddDepartmentFrm addFrm = new AddDepartmentFrm();
            addFrm.Text = "添加部门信息";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewDepartment.CurrentRow) return;
            int index = dataGridViewDepartment.CurrentRow.Index;
            AddDepartmentFrm addFrm = new AddDepartmentFrm();
            addFrm.setId(int.Parse(dataGridViewDepartment.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.fillField();
            addFrm.Text = "修改部门信息";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewDepartment.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewDepartment.SelectedRows)
            {
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (dataGridViewDepartment.SelectedRows.Count > 0)
                ids.Remove(ids.Length - 1, 1);

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的仓库信息!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            string cantDelete = "";

            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                foreach (DataGridViewRow row in dataGridViewDepartment.SelectedRows)
                {
                    MessageLocal msg = baseDao.deleteEntity("del_departmentById", "tb_department", int.Parse(row.Cells["id"].Value.ToString()));
                   if (!msg.IsSucess)
                   {
                       cantDelete += row.Cells["id"].Value.ToString() + ",";
                   }
                }
                this.Enabled = true;
            }

            if(!"".Equals(cantDelete))
                MessageBox.Show("由于存在外键引用，ID为[" + cantDelete + "]的部门信息无法删除!", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshDataGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, TB_NAME, dataGridViewDepartment);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dataGridViewDepartment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (null != addEmployeeFrm)
            {
                Department department = new Department(int.Parse(dataGridViewDepartment.Rows[e.RowIndex].Cells["id"].Value.ToString()),
                    dataGridViewDepartment.Rows[e.RowIndex].Cells["name"].Value.ToString());

                addEmployeeFrm.setDepartment(department);
                this.Close();
            }
        }



    }
}
