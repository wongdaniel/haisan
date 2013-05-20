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
using haisan;

namespace haisan
{
    public partial class LoginFrm : Form
    {
        private static UserDao userDao = UserDaoImpl.getInstance();
        private User user;

        public LoginFrm()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
       
            errorLabel.Text = "";
            if ("".Equals(username.Text) || "".Equals(password.Text))
            {
                errorLabel.Text = "用户名或密码不能为空！";
                return;
            }
            
            user = new User(username.Text, password.Text);
            Parameter.user = user;

            haisan.util.MessageLocal msg = userDao.login(user);
            if (!msg.IsSucess)
            {
                errorLabel.Text = msg.Message;
                return;
            }

            MainFrm main = new MainFrm();
            main.user = user;
            this.Visible = false;

            main.ShowDialog();
            this.Close();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
        }

        private void LoginFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            userDao.loginOut(user);
        }
    }
}
