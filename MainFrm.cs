using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using haisan.util;
using haisan.domain;
using haisan.dao;
using haisan.frame;
using haisan.frame.document.category;
using haisan.frame.document.product;
using haisan.frame.system;

namespace haisan
{
    public partial class MainFrm : Form
    {
        private static UserDao userDao = UserDaoImpl.getInstance();
        private static LogDao logDao = LogDaoImpl.getInstance();

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            refreshUserStatus();
        }

        private void refreshUserStatus()
        {
            currentUserLabel.Text = Parameter.user.Username;
            currentTimeLabel.Text = DateTime.Now.ToShortDateString();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("close main\n");
           
        }

        private void 商品类别TToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            CategoryFrm cateFrm = new CategoryFrm();
            cateFrm.MdiParent = this;
            cateFrm.setTable("categoryOfProduct");
            cateFrm.Text = title;
            cateFrm.Show();

            
        }

        private void 往来单位类型UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            CategoryFrm cateFrm = new CategoryFrm();
            cateFrm.MdiParent = this;
            cateFrm.setTable("categoryOfUnit");
            cateFrm.Text = title;
            cateFrm.Show();

            
        }

        private void 商品资料OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            ProductFrm docFrm = new ProductFrm();
            docFrm.MdiParent = this;
            docFrm.Text = title;
            docFrm.Show();

            
        }

        private string getTitleFromMenuItem(object sender)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            return item.Text.Substring(0, item.Text.IndexOf('('));
        }

        private void 系统日志PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            LogFrm logFrm = new LogFrm();
            logFrm.MdiParent = this;
            logFrm.Text = title;
            logFrm.Show();
            
        }

        private void 账套参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            ConfigureFrm conFrm = new ConfigureFrm();
           // conFrm.MdiParent = this;
            conFrm.Text = title;
            conFrm.ShowDialog();
            
        }

        private void 更换口令KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            ChangePwd cpFrm = new ChangePwd();
            // conFrm.MdiParent = this;
            cpFrm.Text = title;
            cpFrm.ShowDialog();

            if (null == Parameter.user)
                this.Close();
            
        }

        private void 重新登录LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            User user = Parameter.user; // 因为在LoginFrm中，如果登陆不成功，将把Parameter.user置为null
            LoginFrm loginFrm = new LoginFrm();
            loginFrm.setIsOpenMain(false);
            loginFrm.Text = title;
            loginFrm.ShowDialog();

            if (null == Parameter.user)
            {
                Parameter.user = user;
                this.Close();
            }
            else
            {
                refreshUserStatus();
            }
        }
    }
}
