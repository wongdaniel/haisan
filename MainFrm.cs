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

namespace haisan
{
    public partial class MainFrm : Form
    {
        public User user;
        private static UserDao userDao = UserDaoImpl.getInstance();
        private static LogDao logDao = LogDaoImpl.getInstance();

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            currentUserLabel.Text = user.Username;
            currentTimeLabel.Text = DateTime.Now.ToShortDateString();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("close main\n");
           
        }

        private void 商品类别TToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string title = getTitleFromMenuItem(sender);

            CategoryFrm cateFrm = new CategoryFrm();
            cateFrm.MdiParent = this;
            cateFrm.setTable("categoryOfProduct");
            cateFrm.Text = title;
            cateFrm.Show();

            logDao.saveLog(user, title);
        }

        private void 往来单位类型UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            
            CategoryFrm cateFrm = new CategoryFrm();
            cateFrm.MdiParent = this;
            cateFrm.setTable("categoryOfUnit");
            cateFrm.Text = title;
            cateFrm.Show();

            logDao.saveLog(user, title);
        }

        private void 商品资料OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);

            ProductFrm docFrm = new ProductFrm();
            docFrm.MdiParent = this;
            docFrm.Text = title;
            docFrm.Show();

            logDao.saveLog(user, title);
        }

        private string getTitleFromMenuItem(object sender)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            return item.Text.Substring(0, item.Text.IndexOf('('));
        }
    }
}
