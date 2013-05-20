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

namespace haisan.frame.document.category{
    public partial class AddCategoryFrm : Form
    {
        private TreeNode categoryNode;
        private CategoryDao categoryDao = CategoryDaoImpl.getInstance();
        private CategoryFrm categoryFrm;
        private string table = null;

        public AddCategoryFrm()
        {
            InitializeComponent();
            
        }

        public void setCategoryTree(TreeNode treeNode)
        {
            categoryNode = treeNode;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            messageLabel.Text = "";
            if ("".Equals(categoryName.Text))
            {
                messageLabel.Text = "类型名不能为空";
                return;
            }
            MessageLocal msg = categoryDao.saveCategory(categoryName.Text, (Category)categoryNode.Tag, table);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message + " 系统将刷新所有类型！");
                categoryFrm.refreshTreeView();
                this.Close();
                return;
            }

            // 再读一次的原因是，为了取得新创建的category的ID， 偷懒没有在saveCategory读取
            Category cop = categoryDao.getCategoryByName(categoryName.Text, table);
            if (null != cop)
            {
                TreeNode newNode = new TreeNode(categoryName.Text);
                newNode.Tag = cop;
                categoryNode.Nodes.Add(newNode);
                categoryFrm.refreshListView(categoryNode);
            }else
            {
                MessageBox.Show("该类型【" + categoryName.Text + "】被删除，系统将刷新所有类型！");
                categoryFrm.refreshTreeView();
            }
            this.Close();
        }

        public void setCategoryFrm(CategoryFrm categoryFrm)
        {
            this.categoryFrm = categoryFrm;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setTable(string table)
        {
            this.table = table;
        }
    }
}
