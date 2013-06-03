namespace haisan.frame.document.warehouse
{
    partial class WarehouseFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridViewWarehouse = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehouse)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.buttonDel);
            this.panel1.Controls.Add(this.buttonModify);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 252);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 41);
            this.panel1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(248, 10);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "表格保存";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(170, 10);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(60, 23);
            this.buttonDel.TabIndex = 6;
            this.buttonDel.Text = "删除";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(89, 10);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(60, 23);
            this.buttonModify.TabIndex = 5;
            this.buttonModify.Text = "修改";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 10);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(61, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "新增";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridViewWarehouse
            // 
            this.dataGridViewWarehouse.AllowUserToAddRows = false;
            this.dataGridViewWarehouse.AllowUserToDeleteRows = false;
            this.dataGridViewWarehouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewWarehouse.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewWarehouse.Name = "dataGridViewWarehouse";
            this.dataGridViewWarehouse.ReadOnly = true;
            this.dataGridViewWarehouse.RowTemplate.Height = 23;
            this.dataGridViewWarehouse.Size = new System.Drawing.Size(475, 252);
            this.dataGridViewWarehouse.TabIndex = 1;
            // 
            // WarehouseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 293);
            this.Controls.Add(this.dataGridViewWarehouse);
            this.Controls.Add(this.panel1);
            this.Name = "WarehouseFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "warehouseFrm";
            this.Load += new System.EventHandler(this.warehouseFrm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehouse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridViewWarehouse;
    }
}