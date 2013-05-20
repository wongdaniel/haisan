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
using haisan.util;
using haisan.frame.document.category;

namespace haisan.frame.document.product
{
    public partial class AddProductFrm : Form
    {

        ProductDao productDao = ProductDaoImpl.getInstance();
        int productId = -1;
        int provider = -1;
        int category = -1;

        public AddProductFrm()
        {
            InitializeComponent();

        }

        // 这样使用Enable比较糟糕，不知道是否有拦截器！
        private void buttonSaveQuit_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            Product pro = new Product();
            if (0 != wrapProduct(pro))
            {
                this.Enabled = true;
                return;
            }

            MessageLocal msg = productDao.saveOrUpdateProduct(pro);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            this.Close();

        }

        private void buttonSaveAdd_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Product pro = new Product();
            if (0 != wrapProduct(pro))
            {
                this.Enabled = true;
                return;
            }

            MessageLocal msg = productDao.saveOrUpdateProduct(pro);
            if (!msg.IsSucess)
            {
                MessageBox.Show( msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            if (!checkBoxIsCopy.Checked)
                clearFieds();

            productId = -1;
            this.Enabled = true;
        }

        int wrapProduct(Product pro)
        {
            int result = 0;
            recoverTextBoxColor();

            if (0 != (result = verificateFields()))
                return result;

            pro.Id = productId;
            pro.Name = textBoxName.Text;
            pro.Code = textBoxCode.Text;
            pro.Spec = textBoxSpec.Text;
            pro.Category.Id = category;
            pro.Provider.Id = provider;
            pro.Address = textBoxAddress.Text;
            pro.Unit = textBoxUnit.Text;
            pro.Purchase_price = decimal.Parse(textBoxPurchase.Text.ToString());
            pro.Sale_price = decimal.Parse(textBoxSale.Text.ToString());
            pro.Wholesale_price = decimal.Parse(textBoxWholeSale.Text.ToString());

            pro.Description = textBoxDescription.Text.ToString();
            pro.Image = null; // skip image function

            return result;
        }

        private void textBoxPurchase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Util.isAcceptByDecimal(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
          
        }

        private void textBoxSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Util.isAcceptByDecimal(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBoxWholeSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Util.isAcceptByDecimal(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void recoverTextBoxColor()
        {
            labelErrorMsg.Text = "";
            textBoxPurchase.ForeColor = Color.Black;
            textBoxPurchase.ForeColor = Color.Black;
            textBoxWholeSale.ForeColor = Color.Black;
        }

        private void clearFieds()
        {
            textBoxName.Text = "";
            textBoxCode.Text = "";
            textBoxSpec.Text = "";
            category = -1;
            provider = -1;
            textBoxAddress.Text = "";
            textBoxUnit.Text = "";

            textBoxPurchase.Text = "0";
            textBoxSale.Text = "0";
            textBoxWholeSale.Text = "0";
            textBoxDescription.Text = "0";
            pictureBoxImage.Image = null;
        }

        public void fillField()
        {
            Product pro = productDao.getProductById(productId);
            if (null == pro)
            {
                this.Close();
            }

            textBoxName.Text = pro.Name;
            textBoxCode.Text = pro.Code;
            textBoxSpec.Text = pro.Spec;
            textBoxCategory.Text = pro.Category.Name;
            category = pro.Category.Id;
            textBoxProvider.Text = pro.Provider.Name;
            provider = pro.Provider.Id;
            textBoxAddress.Text = pro.Address;
            textBoxUnit.Text = pro.Unit;

            textBoxPurchase.Text = pro.Purchase_price.ToString();
            textBoxSale.Text = pro.Sale_price.ToString();
            textBoxWholeSale.Text = pro.Wholesale_price.ToString();
            textBoxDescription.Text = pro.Description;
            pictureBoxImage.Image = null; // skip image
        }

        public void setCategory(string categoryName, int categoryId)
        {
            textBoxCategory.Text = categoryName;
            category = categoryId;
        }

        public void setProductId(int id)
        {
            this.productId = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategoryFrm categoryFrm =  new CategoryFrm(this);
            categoryFrm.setTable("categoryOfProduct");
            categoryFrm.Text = "商品类别";
            categoryFrm.ShowDialog();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxCode.Text =  Code.GetFirstPYLetter(textBoxName.Text.ToString());
        }

        private int verificateFields()
        {
            int result = 0;
            provider = 1; // 跳过对provider的判断

            string errorMsg = "";
            if ("".Equals(textBoxName.Text))
            {
                errorMsg += "【商品名称】 ";
                result = -1;
            }

            if(-1 == category)
            {
                errorMsg += "【商品类型】 ";
                result = -1;
            }

            if(-1 == provider)
            {
                errorMsg += "【供应商】 ";
                result = -1;
            }

            if(-1 == result)
            {
                labelErrorMsg.Text = errorMsg + "不能为空！";
            }

            if (!Util.isDecimal(textBoxPurchase.Text.ToString()))
            {
                textBoxPurchase.ForeColor = Color.Red;
                result = -1;
            }

            if (!Util.isDecimal(textBoxSale.Text.ToString()))
            {
                textBoxPurchase.ForeColor = Color.Red;
                result = -1;
            }

            if (!Util.isDecimal(textBoxWholeSale.Text.ToString()))
            {
                textBoxWholeSale.ForeColor = Color.Red;
                result = -1;
            }

            return result;
        }

        private void buttonCancelQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
