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

namespace haisan.frame.system.user
{
    public partial class UserFrm : Form
    {
        UserDao userDao = UserDaoImpl.getInstance();
        BaseDao baseDao = BaseDaoImpl.getInstance();
        DataSet dataset = null;

        public UserFrm()
        {
            InitializeComponent();
        }

        private void UserFrm_Load(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            dataGridViewUser.DataSource = userDao.getAllUser(textBoxQuery.Text).Tables[0].DefaultView;
            ProductListHeadText();

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, "tb_user", dataGridViewUser);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void UserFrm_SizeChanged(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
        }

        //设置DataGridView标题
        private void ProductListHeadText()
        {
            int i = 0;
            dataGridViewUser.Columns[i++].HeaderText = "用户ID";
            dataGridViewUser.Columns[i++].HeaderText = "用户名";
            dataGridViewUser.Columns[i++].HeaderText = "密码";
            dataGridViewUser.Columns[i++].HeaderText = "组名";
            dataGridViewUser.Columns[i++].HeaderText = "真实姓名";
            dataGridViewUser.Columns[i++].HeaderText = "邮箱";
            dataGridViewUser.Columns[i++].HeaderText = "电话";
            dataGridViewUser.Columns[i++].HeaderText = "备注";
            dataGridViewUser.Columns[i++].HeaderText = "是否被锁定";
            dataGridViewUser.Columns[i++].HeaderText = "上次登陆时间";
            dataGridViewUser.Columns[i++].HeaderText = "是否在线";
            dataGridViewUser.Columns[i++].HeaderText = "创建时间";
        }

        private void buttonSaveTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageLocal msg = baseDao.saveOrUpdateDataGridView(Parameter.user, "tb_user", dataGridViewUser);
            this.Enabled = true;

            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddUserFrm addFrm = new AddUserFrm();
            addFrm.Text = "添加用户信息";
            addFrm.ShowDialog();

            refreshDataGridView();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewUser.CurrentRow) return;
            int index = dataGridViewUser.CurrentRow.Index;
            AddUserFrm addFrm = new AddUserFrm();
            addFrm.setUserId(int.Parse(dataGridViewUser.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.fillField();
            addFrm.Text = "修改用户信息";
            addFrm.ShowDialog();

            refreshDataGridView();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewUser.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewUser.SelectedRows)
            {
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (dataGridViewUser.SelectedRows.Count > 0)
                ids.Remove(ids.Length - 1, 1);

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的用户信息!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities("tb_user", ids.ToString());
                this.Enabled = true;
            }

            refreshDataGridView();
        }

        private void refreshDataGridView()
        {
            dataset = userDao.getAllUser(textBoxQuery.Text);
            dataGridViewUser.DataSource = dataset.Tables[0].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewUser.DataSource = userDao.getAllUser(textBoxQuery.Text).Tables[0].DefaultView;
        }
    }
}
