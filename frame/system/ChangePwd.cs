using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.util;
using haisan.dao;

namespace haisan.frame.system
{
    public partial class ChangePwd : Form
    {
        UserDao userDao = UserDaoImpl.getInstance();

        public ChangePwd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBoxOldPwd.Text.Equals(Parameter.user.Password))
            {
                MessageBox.Show("原密码输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxOldPwd.Text = "";
                return;
            }

            if (!textBoxNewPwd.Text.Equals(textBoxConfirmPwd.Text))
            {
                MessageBox.Show("新密码两次输入不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxNewPwd.Text = "";
                textBoxConfirmPwd.Text = "";
                return;
            }

            Parameter.user.Password = textBoxConfirmPwd.Text;
            MessageLocal msg = userDao.updatePwd(Parameter.user);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Parameter.user = null;

                LoginFrm loginFrm = new LoginFrm();
                loginFrm.setIsOpenMain(false);
                loginFrm.ShowDialog();
                if (null == Parameter.user)
                {
                    this.Close();
                    return;
                }
            }

            // 重新加载用户权限
            this.Close();

        }

        
    }
}
