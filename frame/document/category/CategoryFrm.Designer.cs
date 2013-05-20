namespace haisan.frame.document.category
{
    partial class CategoryFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryFrm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonModify = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSure = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewCategory = new System.Windows.Forms.TreeView();
            this.listViewCategory = new System.Windows.Forms.ListView();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripButtonModify,
            this.toolStripButtonDel,
            this.toolStripButtonSure});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(476, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonSave.Text = "新增";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonModify
            // 
            this.toolStripButtonModify.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonModify.Image")));
            this.toolStripButtonModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonModify.Name = "toolStripButtonModify";
            this.toolStripButtonModify.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonModify.Text = "修改";
            this.toolStripButtonModify.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButtonDel
            // 
            this.toolStripButtonDel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDel.Image")));
            this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDel.Name = "toolStripButtonDel";
            this.toolStripButtonDel.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonDel.Text = "删除";
            this.toolStripButtonDel.Click += new System.EventHandler(this.toolStripButtonDel_Click);
            // 
            // toolStripButtonSure
            // 
            this.toolStripButtonSure.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSure.Image")));
            this.toolStripButtonSure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSure.Name = "toolStripButtonSure";
            this.toolStripButtonSure.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonSure.Text = "确定";
            this.toolStripButtonSure.Click += new System.EventHandler(this.toolStripButtonSure_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 269);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(476, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(212, 17);
            this.toolStripStatusLabel1.Text = "说明：双击所选类别或按确定选定退出";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewCategory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewCategory);
            this.splitContainer1.Size = new System.Drawing.Size(476, 244);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeViewCategory
            // 
            this.treeViewCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCategory.Location = new System.Drawing.Point(0, 0);
            this.treeViewCategory.Name = "treeViewCategory";
            this.treeViewCategory.Size = new System.Drawing.Size(145, 244);
            this.treeViewCategory.TabIndex = 0;
            this.treeViewCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCategory_AfterSelect);
            this.treeViewCategory.DoubleClick += new System.EventHandler(this.treeViewCategory_DoubleClick);
            // 
            // listViewCategory
            // 
            this.listViewCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCategory.Location = new System.Drawing.Point(0, 0);
            this.listViewCategory.Name = "listViewCategory";
            this.listViewCategory.Size = new System.Drawing.Size(327, 244);
            this.listViewCategory.TabIndex = 0;
            this.listViewCategory.UseCompatibleStateImageBehavior = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(123, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // CategoryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 291);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategoryFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "商品类别";
            this.Load += new System.EventHandler(this.CategoryOfProduct_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonModify;
        private System.Windows.Forms.ToolStripButton toolStripButtonDel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TreeView treeViewCategory;
        private System.Windows.Forms.ListView listViewCategory;
        private System.Windows.Forms.ToolStripButton toolStripButtonSure;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}