namespace haisan.frame.document.product
{
    partial class ProductFrm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonSaveTable = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewCategory = new System.Windows.Forms.TreeView();
            this.dataGridViewProd = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.报表打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出到ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProd)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonQuery);
            this.panel1.Controls.Add(this.textBoxQuery);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(672, 73);
            this.panel1.TabIndex = 0;
            // 
            // buttonQuery
            // 
            this.buttonQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonQuery.Location = new System.Drawing.Point(304, 44);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(75, 23);
            this.buttonQuery.TabIndex = 3;
            this.buttonQuery.Text = "查  询";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Location = new System.Drawing.Point(135, 45);
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.Size = new System.Drawing.Size(144, 21);
            this.textBoxQuery.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(5, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "商品名称/简码/规格：";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.Location = new System.Drawing.Point(258, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(76, 16);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "商品资料";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelStatus);
            this.panel2.Controls.Add(this.buttonImport);
            this.panel2.Controls.Add(this.buttonSaveTable);
            this.panel2.Controls.Add(this.buttonPrint);
            this.panel2.Controls.Add(this.buttonDel);
            this.panel2.Controls.Add(this.buttonModify);
            this.panel2.Controls.Add(this.buttonNew);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 368);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(672, 38);
            this.panel2.TabIndex = 1;
            this.panel2.UseWaitCursor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(593, 14);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(59, 12);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "共x条记录";
            this.labelStatus.UseWaitCursor = true;
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(426, 8);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(79, 23);
            this.buttonImport.TabIndex = 5;
            this.buttonImport.Text = "从Excel导入";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.UseWaitCursor = true;
            // 
            // buttonSaveTable
            // 
            this.buttonSaveTable.Location = new System.Drawing.Point(325, 8);
            this.buttonSaveTable.Name = "buttonSaveTable";
            this.buttonSaveTable.Size = new System.Drawing.Size(79, 23);
            this.buttonSaveTable.TabIndex = 4;
            this.buttonSaveTable.Text = "表格保存";
            this.buttonSaveTable.UseVisualStyleBackColor = true;
            this.buttonSaveTable.UseWaitCursor = true;
            this.buttonSaveTable.Click += new System.EventHandler(this.buttonSaveTable_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(248, 8);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(57, 23);
            this.buttonPrint.TabIndex = 3;
            this.buttonPrint.Text = "打印";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.UseWaitCursor = true;
            this.buttonPrint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonPrint_MouseDown);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(167, 8);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(57, 23);
            this.buttonDel.TabIndex = 2;
            this.buttonDel.Text = "删除";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.UseWaitCursor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(87, 8);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(57, 23);
            this.buttonModify.TabIndex = 1;
            this.buttonModify.Text = "修改";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.UseWaitCursor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(11, 8);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(57, 23);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "新增";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.UseWaitCursor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 73);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewCategory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewProd);
            this.splitContainer1.Size = new System.Drawing.Size(672, 295);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeViewCategory
            // 
            this.treeViewCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCategory.Location = new System.Drawing.Point(0, 0);
            this.treeViewCategory.Name = "treeViewCategory";
            this.treeViewCategory.Size = new System.Drawing.Size(163, 295);
            this.treeViewCategory.TabIndex = 0;
            this.treeViewCategory.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewCategory_BeforeSelect);
            this.treeViewCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCategory_AfterSelect);
            this.treeViewCategory.Leave += new System.EventHandler(this.treeViewCategory_Leave);
            // 
            // dataGridViewProd
            // 
            this.dataGridViewProd.AllowUserToOrderColumns = true;
            this.dataGridViewProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProd.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewProd.Name = "dataGridViewProd";
            this.dataGridViewProd.RowTemplate.Height = 23;
            this.dataGridViewProd.Size = new System.Drawing.Size(505, 295);
            this.dataGridViewProd.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.报表打印ToolStripMenuItem,
            this.报表预览ToolStripMenuItem,
            this.导出到ExcelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 70);
            // 
            // 报表打印ToolStripMenuItem
            // 
            this.报表打印ToolStripMenuItem.Name = "报表打印ToolStripMenuItem";
            this.报表打印ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.报表打印ToolStripMenuItem.Text = "报表打印";
            this.报表打印ToolStripMenuItem.Click += new System.EventHandler(this.报表打印ToolStripMenuItem_Click);
            // 
            // 报表预览ToolStripMenuItem
            // 
            this.报表预览ToolStripMenuItem.Name = "报表预览ToolStripMenuItem";
            this.报表预览ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.报表预览ToolStripMenuItem.Text = "报表预览";
            this.报表预览ToolStripMenuItem.Click += new System.EventHandler(this.报表预览ToolStripMenuItem_Click);
            // 
            // 导出到ExcelToolStripMenuItem
            // 
            this.导出到ExcelToolStripMenuItem.Name = "导出到ExcelToolStripMenuItem";
            this.导出到ExcelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.导出到ExcelToolStripMenuItem.Text = "导出到Excel";
            // 
            // ProductFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 406);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ProductFrm";
            this.Text = "商品资料";
            this.Load += new System.EventHandler(this.ProductFrm_Load);
            this.SizeChanged += new System.EventHandler(this.ProductFrm_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProd)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonSaveTable;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewCategory;
        private System.Windows.Forms.DataGridView dataGridViewProd;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 报表打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出到ExcelToolStripMenuItem;
    }
}