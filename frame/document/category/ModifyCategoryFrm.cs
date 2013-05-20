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

namespace haisan.frame.document.category
{
    public partial class ModifyCategoryFrm : Form
    {
        private TreeNode categoryNode;
        private CategoryDao categoryDao = CategoryDaoImpl.getInstance();
        private CategoryFrm categoryFrm;
        private string table = null;

        public ModifyCategoryFrm()
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

            MessageLocal msg = categoryDao.updateCategoryByName(categoryName.Text, (Category)categoryNode.Tag, table);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message + " 系统将刷新所有类型！");
                categoryFrm.refreshTreeView();
                this.Close();
                return;
            }
            
            categoryNode.Text = categoryName.Text;
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

        private void ModifyCategoryOfProductFrm_Load(object sender, EventArgs e)
        {
            categoryName.Text = categoryNode.Text;
        }

        public void setTable(string table)
        {
            this.table = table;
        }
    }
}
