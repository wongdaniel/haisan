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
using haisan.frame.system.user;
using haisan.frame.system.group;
using haisan.frame.document.plain;
using haisan.frame.document.typeOfProcess;
using haisan.frame.pdm.purchase;
using haisan.frame.document.image;

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
            docFrm.initPermission(Parameter.user);
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

            ChangePwdFrm cpFrm = new ChangePwdFrm();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            UserFrm userFrm = new UserFrm();
            userFrm.Text = title;
            userFrm.ShowDialog();
        }

        private void 操作员及权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            GroupFrm grpFrm = new GroupFrm();
            grpFrm.Text = title;
            grpFrm.ShowDialog();
        }

        public void initPermission(User user)
        {
           disableAllComponent();
           Dictionary<Module, Permission> permissions = user.Group.Permissions;

           checkPermissionOfMenuItem(permissions, Parameter.MODULE_RELOGIN, 重新登录LToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_CHGPWD, 更换口令KToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_USER_MGR, toolStripMenuItem1);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_GROUP_MGR, 操作员及权限ToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_CONFIGURE, 账套参数ToolStripMenuItem);

           checkPermissionOfMenuItem(permissions, Parameter.MODULE_DOC, ProductOToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_PROVIDER, companyPToolStripMenuItem);
           checkPermissionOfMenuItem(permissions,Parameter.MODULE_EMPLOYEE, 员工资料RToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_STOREHOUSE, 仓库资料SToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_DEPARMENT, 部门资料QToolStripMenuItem);

           checkPermissionOfMenuItem(permissions, Parameter.MODULE_CATE_PROD, 商品类别TToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_CATE_PRD, 往来单位类型UToolStripMenuItem);
           checkPermissionOfMenuItem(permissions, Parameter.MODULE_CATE_STONE, toolStripMenuItem2);
           checkPermissionOfMenuItem(permissions, Parameter.MDOULE_NAME_PROD, toolStripMenuItem3);
        }

        public void disableAllComponent()
        {
            Console.WriteLine("Close all Component");
            重新登录LToolStripMenuItem.Enabled = false;
            更换口令KToolStripMenuItem.Enabled = false;
            toolStripMenuItem1.Enabled = false;
            操作员及权限ToolStripMenuItem.Enabled = false;
            账套参数ToolStripMenuItem.Enabled = false;

            ProductOToolStripMenuItem.Enabled = false;
            companyPToolStripMenuItem.Enabled = false;
            员工资料RToolStripMenuItem.Enabled = false;
            仓库资料SToolStripMenuItem.Enabled = false;
            部门资料QToolStripMenuItem.Enabled = false;

            商品类别TToolStripMenuItem.Enabled = false;
            往来单位类型UToolStripMenuItem.Enabled = false;
            toolStripMenuItem2.Enabled = false;
            toolStripMenuItem3.Enabled = false;
        }

        private void checkPermissionOfMenuItem(Dictionary<Module, Permission> permissions,
    String module, ToolStripMenuItem menuItem)
        {
            Permission per = permissions[new Module(module)];
            if (Parameter.CHECKED == per.Run)
            {
                Console.WriteLine("Open permission : " + menuItem.Text);
                menuItem.Enabled = true;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            PlainFrm plainFrm = new PlainFrm();
            plainFrm.MdiParent = this;
            plainFrm.setTableName("tb_category_stone");
            plainFrm.Text = title;
            plainFrm.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            PlainFrm plainFrm = new PlainFrm();
            plainFrm.MdiParent = this;
            plainFrm.setTableName("tb_product_name");
            plainFrm.Text = title;
            plainFrm.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            TypeOfProcessFrm typeFrm = new TypeOfProcessFrm();
            typeFrm.MdiParent = this;
            typeFrm.Text = title;
            typeFrm.Show();
        }

        private void 进货订单KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            PurchaseOrderFrm purFrm = new PurchaseOrderFrm();
            purFrm.MdiParent = this;
            purFrm.Text = title;
            purFrm.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string title = getTitleFromMenuItem(sender);
            logDao.saveLog(Parameter.user, title);

            ImageFrm imageFrm = new ImageFrm();
            imageFrm.MdiParent = this;
            imageFrm.Text = title;
            imageFrm.Show();
        }
    }
}
