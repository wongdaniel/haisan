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

namespace haisan.frame.system.group
{
    public partial class GroupFrm : Form
    {
        BaseDao baseDao = BaseDaoImpl.getInstance();

        public GroupFrm()
        {
            InitializeComponent();
        }

        private void GroupFrm_Load(object sender, EventArgs e)
        {
            dataGridViewGroup.DataSource = baseDao.getAllEntities("tb_group").Tables[0].DefaultView;
            GroupListHeadText();
        }

        //设置DataGridView标题
        private void GroupListHeadText()
        {
            int i = 0;
            dataGridViewGroup.Columns[i].Width += 100;
            dataGridViewGroup.Columns[i++].HeaderText = "用户组ID";
            dataGridViewGroup.Columns[i].Width += 100;
            dataGridViewGroup.Columns[i++].HeaderText = "用户组名";

        }

        private void 新增NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGroupFrm addFrm = new AddGroupFrm();
            addFrm.Text = "新增用户组";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void 修改MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewGroup.CurrentRow) return;
            int index = dataGridViewGroup.CurrentRow.Index;
            if (Parameter.SUPER_ADMISTRATOR.Equals(dataGridViewGroup.Rows[index].Cells["name"].Value.ToString()))
            {
                MessageBox.Show(Parameter.SUPER_ADMISTRATOR + "不允许修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AddGroupFrm addFrm = new AddGroupFrm();
            addFrm.setGroupId(int.Parse(dataGridViewGroup.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.setGroupName(dataGridViewGroup.Rows[index].Cells["name"].Value.ToString());
            addFrm.Text = "修改用户组信息";
            addFrm.ShowDialog();
            refreshDataGridView();
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewGroup.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ids = constructIds(dataGridViewGroup); //当所选择的行，只包括Parameter.SUPER_ADMISTRATOR, 将返回空
            if ("".Equals(ids)) 
                return;

            DialogResult dr = MessageBox.Show("将删除ID为[" + ids.ToString() + "]的用户组!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities("tb_group", ids);
                this.Enabled = true;
                refreshDataGridView();
            }

        }

        private string constructIds(DataGridView dataGridViewGroup)
        {
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewGroup.SelectedRows)
            {
                if (Parameter.SUPER_ADMISTRATOR.Equals(row.Cells["name"].Value)) 
                    continue;
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (ids.Length > 0)
                ids.Remove(ids.Length - 1, 1);

            return ids.ToString();
        }

        public void refreshDataGridView()
        {
            dataGridViewGroup.DataSource = baseDao.getAllEntities("tb_group").Tables[0].DefaultView;
        }

        private void 授权PToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (null == dataGridViewGroup.CurrentRow) return;
            int index = dataGridViewGroup.CurrentRow.Index;
            if (Parameter.SUPER_ADMISTRATOR.Equals(dataGridViewGroup.Rows[index].Cells["name"].Value.ToString()))
            {
                MessageBox.Show(Parameter.SUPER_ADMISTRATOR + "不允许修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PermisssionFrm perFrm = new PermisssionFrm();
            perFrm.setGroupId(int.Parse(dataGridViewGroup.Rows[index].Cells["id"].Value.ToString()));
            perFrm.Text = "修改用户组【" + dataGridViewGroup.Rows[index].Cells["name"].Value.ToString() + "】的权限";
            perFrm.ShowDialog();
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
