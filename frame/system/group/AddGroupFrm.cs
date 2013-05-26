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

namespace haisan.frame.system.group
{
    public partial class AddGroupFrm : Form
    {
        GroupDao groupDao = GroupDaoImpl.getInstance();
        private int groupId = -1;

        public AddGroupFrm()
        {
            InitializeComponent();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            labelErrStatus.Text = "";

            if ("".Equals(textBoxName.Text))
            {
                labelErrStatus.Text = "用户组名，不能为空！";
                return;
            }

            MessageLocal msg = groupDao.saveOrUpdateGroup(new Group(groupId, textBoxName.Text));
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

        public void setGroupId(int groupId)
        {
            this.groupId = groupId;
        }

        public void setGroupName(string name)
        {
            textBoxName.Text = name;
        }
    }
}
