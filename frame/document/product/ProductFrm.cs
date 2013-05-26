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

// 因为产品的数据量比较大，删除和修改是否对datagridView全部刷新，未定！！
namespace haisan.frame.document.product
{
    public partial class ProductFrm : Form
    {
        private ProductDao productDao = ProductDaoImpl.getInstance();
        private BaseDao baseDao = BaseDaoImpl.getInstance();
        private CategoryDao categoryDao = CategoryDaoImpl.getInstance();

        private DataSet dataset = null;

        private int categoryId = 0;
 //       private TreeNode selectedNode = null;

        public ProductFrm()
        {
            InitializeComponent();
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            refreshDataGridView();
        }

        private void ProductFrm_Load(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;

            if (!buttonQuery.Enabled)
                return;
            DateTime begin = DateTime.Now;
            //fill the category of product to tree view
            refreshTreeView();

            refreshDataGridView();
            ProductListHeadText();
            DateTime mid = DateTime.Now;
            Console.WriteLine("read date cost time:" + (mid - begin));

            MessageLocal msg = baseDao.fillDataGridView(Parameter.user, "tb_product", dataGridViewProd);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime end = DateTime.Now;
            Console.WriteLine("read date cost time:" + (end - mid));
        }

        //设置DataGridView标题
        private void ProductListHeadText()
        {
            int i = 0;
            dataGridViewProd.Columns[i++].HeaderText = "商品ID";
            dataGridViewProd.Columns[i++].HeaderText = "商品名称";
            dataGridViewProd.Columns[i++].HeaderText = "商品简码";
            dataGridViewProd.Columns[i++].HeaderText = "商品规格";
            dataGridViewProd.Columns[i++].HeaderText = "供应商";
            dataGridViewProd.Columns[i++].HeaderText = "商品类型";
            dataGridViewProd.Columns[i++].HeaderText = "产地";
            dataGridViewProd.Columns[i++].HeaderText = "计量单位";
            dataGridViewProd.Columns[i++].HeaderText = "进货参考价";
            dataGridViewProd.Columns[i++].HeaderText = "销售参考价";
            dataGridViewProd.Columns[i++].HeaderText = "批发参考价";
            dataGridViewProd.Columns[i++].HeaderText = "备注";
        }

        private void ProductFrm_SizeChanged(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
        }

        private void buttonSaveTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
           MessageLocal msg =  baseDao.saveOrUpdateDataGridView(Parameter.user, "tb_product", dataGridViewProd);
           this.Enabled = true;

           if (!msg.IsSucess)
           {
               MessageBox.Show(msg.Message, "错误",MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
           }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            AddProductFrm addFrm = new AddProductFrm();
            addFrm.Text = "添加商品信息";
            addFrm.ShowDialog();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (null == dataGridViewProd.CurrentRow) return;
            int index = dataGridViewProd.CurrentRow.Index;
            AddProductFrm addFrm = new AddProductFrm();
            addFrm.setProductId(int.Parse(dataGridViewProd.Rows[index].Cells["Id"].Value.ToString()));
            addFrm.fillField();
            addFrm.Text = "修改商品信息";
            addFrm.ShowDialog();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (0 == dataGridViewProd.SelectedRows.Count)
            {
                MessageBox.Show("请先选择要删除的数据！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewProd.SelectedRows)
            {
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (dataGridViewProd.SelectedRows.Count > 0)
                ids.Remove(ids.Length - 1, 1);

            DialogResult dr =  MessageBox.Show("将删除ID为["+ ids.ToString() + "]的商品资料!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                this.Enabled = false;
                baseDao.deleteEntities("tb_product", ids.ToString());
                this.Enabled = true;
            }
        }

        private void refreshTreeView()
        {
            treeViewCategory.Nodes.Clear();

            TreeNode all = treeViewCategory.Nodes.Add("全部");
            Category root = new Category(0, "全部");
            all.Tag = root;
            categoryDao.fillSubCategory(all, "categoryOfProduct");
            all.Expand();
        }

        private void refreshDataGridView()
        {
            dataset = productDao.getAllProduct(textBoxQuery.Text, categoryId);
            dataGridViewProd.DataSource = dataset.Tables[0].DefaultView;
            labelStatus.Text = "共" + dataset.Tables[1].Rows[0]["total"].ToString() + "条记录";
            Console.WriteLine("product:" + labelStatus.Text);
        }

        private void treeViewCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            categoryId = ((Category)e.Node.Tag).Id;
            if(buttonQuery.Enabled )
                buttonQuery_Click(null, null);
        }

        private void treeViewCategory_Leave(object sender, EventArgs e)
        {
            if (treeViewCategory.SelectedNode != null)
            {
                treeViewCategory.SelectedNode.BackColor = Color.DodgerBlue;
                treeViewCategory.SelectedNode.ForeColor = Color.White;
            }
        }

        private void treeViewCategory_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (treeViewCategory.SelectedNode != null)
            {
                treeViewCategory.SelectedNode.BackColor = Color.Empty;
                treeViewCategory.SelectedNode.ForeColor = Color.Black;
            }
        }

        private void buttonPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show((Button)sender, new Point(e.X, e.Y));
            }
        }

        private void 报表打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 报表预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrystalReportProduct ccrp = new CrystalReportProduct();
            ccrp.SetDataSource(dataset.Tables[0]);
            CReprotFrm crfrm = new CReprotFrm(ccrp);
            crfrm.Show();
        }

        public void initPermission(User user)
        {
            disableAllComponent();
            Dictionary<Module, Permission> permissions = user.Group.Permissions;
            checkPermissionOfProudct(permissions, Parameter.MODULE_DOC);
        }


        private void checkPermissionOfProudct(Dictionary<Module, Permission> permissions, String module)
        {
            Permission per = permissions[new Module(module)];
            if (Parameter.CHECKED == per.Query)
            {
                Console.WriteLine("Open permission of ProductFrm : " + buttonQuery.Text);
                buttonQuery.Enabled = true;
            }

            if (Parameter.CHECKED == per.Add)
            {
                Console.WriteLine("Open permission of ProductFrm : " + buttonNew.Text);
                buttonNew.Enabled = true;
                buttonImport.Enabled = true;
            }

            if (Parameter.CHECKED == per.Modify)
            {
                Console.WriteLine("Open permission of ProductFrm : " + buttonModify.Text);
                buttonModify.Enabled = true;
            }

            if (Parameter.CHECKED == per.Delete)
            {
                Console.WriteLine("Open permission of ProductFrm : " + buttonDel.Text);
                buttonDel.Enabled = true;
            }

            if (Parameter.CHECKED == per.SaveTable)
            {
                Console.WriteLine("Open permission of ProductFrm : " + buttonSaveTable.Text);
                buttonSaveTable.Enabled = true;
            }

            if (Parameter.CHECKED == per.Print)
            {
                Console.WriteLine("Open permission of ProductFrm : " + buttonPrint.Text);
                buttonPrint.Enabled = true;
            }
        }

        private void disableAllComponent()
        {
            buttonQuery.Enabled = false;
            buttonNew.Enabled = false;
            buttonModify.Enabled = false;
            buttonDel.Enabled = false;
            buttonSaveTable.Enabled = false;
            buttonPrint.Enabled = false;
            buttonImport.Enabled = false;
        }
    }
}
