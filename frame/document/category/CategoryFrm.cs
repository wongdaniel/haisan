using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.domain;
using haisan.dao;
using haisan.frame.document.product;

namespace haisan.frame.document.category
{
    public partial class CategoryFrm : Form
    {
        private CategoryDao categoryDao = CategoryDaoImpl.getInstance();
        private TreeNode clicked = null;
        private string table = null;
        private AddProductFrm addProductFrm = null;

        public CategoryFrm()
        {
            InitializeComponent();
        }

        public CategoryFrm(AddProductFrm addProductFrm)
        {
            InitializeComponent();
            this.addProductFrm = addProductFrm;
        }

        private void CategoryOfProduct_Load(object sender, EventArgs e)
        {
            listViewCategory.View = View.Details;
            listViewCategory.GridLines = true;
            this.categoryListHeadText();

            refreshTreeView();
    
        }

        //设置DataGridView标题
        private void categoryListHeadText()
        {
            listViewCategory.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listViewCategory.Columns.Add("名称", -2, HorizontalAlignment.Left);

        }

        private void treeViewCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            clicked = e.Node;
            refreshListView(e.Node);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddCategoryFrm addFrm = new AddCategoryFrm();
            addFrm.setCategoryTree(clicked);
            addFrm.Text = "添加" + this.Text;
            addFrm.setCategoryFrm(this);
            addFrm.setTable(table);
            addFrm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null != clicked && 0 != ((Category)clicked.Tag).Id)
            {
                ModifyCategoryFrm modFrm = new ModifyCategoryFrm();
                modFrm.setCategoryTree(clicked);
                modFrm.Text = "修改" + this.Text;
                modFrm.setCategoryFrm(this);
                modFrm.setTable(table);
                modFrm.Show();
            }

        }

        public void refreshListView(TreeNode node)
        {
            listViewCategory.Items.Clear();
            foreach (TreeNode cur in node.Nodes)
            {
                string[] item = new string[2] { ((Category)cur.Tag).Id.ToString(), cur.Text };
                listViewCategory.Items.Add(new ListViewItem(item));
            }
        }

        public void refreshTreeView()
        {
            treeViewCategory.Nodes.Clear();

            TreeNode all = treeViewCategory.Nodes.Add("全部");
            Category root = new Category(0, "全部");
            all.Tag = root;
            categoryDao.fillSubCategory(all, table);
            all.Expand();

            refreshListView(all);
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (null != clicked && 0 != ((Category)clicked.Tag).Id) // 根目录不允许删除
            {
                DialogResult result = MessageBox.Show("删除后将无法恢复，确认删除？", "警 告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    categoryDao.deleteCategoryRecursive((Category)clicked.Tag, table);
                    treeViewCategory.Nodes.Remove(clicked);
                }
            }

        }

        public void setTable(string table)
        {
            this.table = table;
        }

        private void toolStripButtonSure_Click(object sender, EventArgs e)
        {
            fillAddProductFrm();
            this.Close();
        }

        private void treeViewCategory_DoubleClick(object sender, EventArgs e)
        {
            fillAddProductFrm();
            this.Close();
        }

        private void fillAddProductFrm()
        {
            // 添加产品时，选择类型
            if (null != addProductFrm)
            {
                if (null != clicked && 0 != ((Category)clicked.Tag).Id)
                {
                    addProductFrm.setCategory(clicked.Text.ToString(), ((Category)clicked.Tag).Id);
                }
            }
        }


    }
}
