namespace haisan.frame.document.product
{
    partial class AddProductFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSpec = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxProvider = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCategory = new System.Windows.Forms.TextBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelErrorMsg = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSale = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxWholeSale = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxPurchase = new System.Windows.Forms.TextBox();
            this.buttonSaveAdd = new System.Windows.Forms.Button();
            this.buttonSaveQuit = new System.Windows.Forms.Button();
            this.buttonCancelQuit = new System.Windows.Forms.Button();
            this.checkBoxIsCopy = new System.Windows.Forms.CheckBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品名称：";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(98, 34);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 21);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "商品简码：";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(289, 36);
            this.textBoxCode.MaxLength = 50;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.Size = new System.Drawing.Size(100, 21);
            this.textBoxCode.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "规格：";
            // 
            // textBoxSpec
            // 
            this.textBoxSpec.Location = new System.Drawing.Point(459, 36);
            this.textBoxSpec.MaxLength = 20;
            this.textBoxSpec.Name = "textBoxSpec";
            this.textBoxSpec.Size = new System.Drawing.Size(100, 21);
            this.textBoxSpec.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "供应商：";
            // 
            // textBoxProvider
            // 
            this.textBoxProvider.Location = new System.Drawing.Point(290, 77);
            this.textBoxProvider.Name = "textBoxProvider";
            this.textBoxProvider.ReadOnly = true;
            this.textBoxProvider.Size = new System.Drawing.Size(100, 21);
            this.textBoxProvider.TabIndex = 7;
            this.textBoxProvider.Text = "开发中...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(410, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "产地：";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(459, 76);
            this.textBoxAddress.MaxLength = 20;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(100, 21);
            this.textBoxAddress.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "计量单位：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "商品类型：";
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.Location = new System.Drawing.Point(98, 76);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.ReadOnly = true;
            this.textBoxCategory.Size = new System.Drawing.Size(100, 21);
            this.textBoxCategory.TabIndex = 12;
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.Location = new System.Drawing.Point(98, 117);
            this.textBoxUnit.MaxLength = 10;
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(100, 21);
            this.textBoxUnit.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelErrorMsg);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 141);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // labelErrorMsg
            // 
            this.labelErrorMsg.AutoSize = true;
            this.labelErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMsg.Location = new System.Drawing.Point(216, 110);
            this.labelErrorMsg.Name = "labelErrorMsg";
            this.labelErrorMsg.Size = new System.Drawing.Size(0, 12);
            this.labelErrorMsg.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::haisan.Properties.Resources.未命名1;
            this.button1.Location = new System.Drawing.Point(191, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "进货参考价格：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(319, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "销售参考价格：";
            // 
            // textBoxSale
            // 
            this.textBoxSale.Location = new System.Drawing.Point(414, 184);
            this.textBoxSale.Name = "textBoxSale";
            this.textBoxSale.Size = new System.Drawing.Size(100, 21);
            this.textBoxSale.TabIndex = 18;
            this.textBoxSale.Text = "0";
            this.textBoxSale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSale_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 229);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "批发参考价格：";
            // 
            // textBoxWholeSale
            // 
            this.textBoxWholeSale.Location = new System.Drawing.Point(132, 226);
            this.textBoxWholeSale.Name = "textBoxWholeSale";
            this.textBoxWholeSale.Size = new System.Drawing.Size(100, 21);
            this.textBoxWholeSale.TabIndex = 20;
            this.textBoxWholeSale.Text = "0";
            this.textBoxWholeSale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxWholeSale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWholeSale_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 266);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "备注：";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(83, 265);
            this.textBoxDescription.MaxLength = 100;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(149, 55);
            this.textBoxDescription.TabIndex = 22;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxPurchase);
            this.groupBox2.Location = new System.Drawing.Point(12, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(564, 169);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其它信息";
            // 
            // textBoxPurchase
            // 
            this.textBoxPurchase.Location = new System.Drawing.Point(120, 21);
            this.textBoxPurchase.Name = "textBoxPurchase";
            this.textBoxPurchase.Size = new System.Drawing.Size(100, 21);
            this.textBoxPurchase.TabIndex = 1;
            this.textBoxPurchase.Text = "0";
            this.textBoxPurchase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxPurchase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPurchase_KeyPress);
            // 
            // buttonSaveAdd
            // 
            this.buttonSaveAdd.Location = new System.Drawing.Point(263, 351);
            this.buttonSaveAdd.Name = "buttonSaveAdd";
            this.buttonSaveAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAdd.TabIndex = 25;
            this.buttonSaveAdd.Text = "保存增加";
            this.buttonSaveAdd.UseVisualStyleBackColor = true;
            this.buttonSaveAdd.Click += new System.EventHandler(this.buttonSaveAdd_Click);
            // 
            // buttonSaveQuit
            // 
            this.buttonSaveQuit.Location = new System.Drawing.Point(366, 351);
            this.buttonSaveQuit.Name = "buttonSaveQuit";
            this.buttonSaveQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveQuit.TabIndex = 26;
            this.buttonSaveQuit.Text = "保存退出";
            this.buttonSaveQuit.UseVisualStyleBackColor = true;
            this.buttonSaveQuit.Click += new System.EventHandler(this.buttonSaveQuit_Click);
            // 
            // buttonCancelQuit
            // 
            this.buttonCancelQuit.Location = new System.Drawing.Point(473, 351);
            this.buttonCancelQuit.Name = "buttonCancelQuit";
            this.buttonCancelQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelQuit.TabIndex = 27;
            this.buttonCancelQuit.Text = "取消退出";
            this.buttonCancelQuit.UseVisualStyleBackColor = true;
            this.buttonCancelQuit.Click += new System.EventHandler(this.buttonCancelQuit_Click);
            // 
            // checkBoxIsCopy
            // 
            this.checkBoxIsCopy.AutoSize = true;
            this.checkBoxIsCopy.Location = new System.Drawing.Point(12, 355);
            this.checkBoxIsCopy.Name = "checkBoxIsCopy";
            this.checkBoxIsCopy.Size = new System.Drawing.Size(144, 16);
            this.checkBoxIsCopy.TabIndex = 28;
            this.checkBoxIsCopy.Text = "新增时复制上一条记录";
            this.checkBoxIsCopy.UseVisualStyleBackColor = true;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Image = global::haisan.Properties.Resources.page_not_found;
            this.pictureBoxImage.Location = new System.Drawing.Point(321, 229);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(193, 91);
            this.pictureBoxImage.TabIndex = 23;
            this.pictureBoxImage.TabStop = false;
            // 
            // AddProductFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 383);
            this.Controls.Add(this.checkBoxIsCopy);
            this.Controls.Add(this.buttonCancelQuit);
            this.Controls.Add(this.buttonSaveQuit);
            this.Controls.Add(this.buttonSaveAdd);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxWholeSale);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxSale);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxUnit);
            this.Controls.Add(this.textBoxCategory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxProvider);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSpec);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "AddProductFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "商品信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSpec;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCategory;
        private System.Windows.Forms.TextBox textBoxUnit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSale;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxWholeSale;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSaveAdd;
        private System.Windows.Forms.Button buttonSaveQuit;
        private System.Windows.Forms.Button buttonCancelQuit;
        private System.Windows.Forms.CheckBox checkBoxIsCopy;
        private System.Windows.Forms.TextBox textBoxPurchase;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelErrorMsg;
    }
}