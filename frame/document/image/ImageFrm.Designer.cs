namespace haisan.frame.document.image
{
    partial class ImageFrm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.保存表格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewImage = new System.Windows.Forms.DataGridView();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存表格ToolStripMenuItem,
            this.退出EToolStripMenuItem,
            this.删除DToolStripMenuItem,
            this.退出EToolStripMenuItem1
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(468, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 保存表格ToolStripMenuItem
            // 
            this.保存表格ToolStripMenuItem.Name = "保存表格ToolStripMenuItem";
            this.保存表格ToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.保存表格ToolStripMenuItem.Text = "保存(&S)";
            this.保存表格ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 退出EToolStripMenuItem
            // 
            this.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem";
            this.退出EToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.退出EToolStripMenuItem.Text = "保存表格(&T)";
            this.退出EToolStripMenuItem.Click += new System.EventHandler(this.保存表格EToolStripMenuItem_Click);
            // 
            // 退出EToolStripMenuItem1
            // 
            this.退出EToolStripMenuItem1.Name = "退出EToolStripMenuItem1";
            this.退出EToolStripMenuItem1.Size = new System.Drawing.Size(59, 21);
            this.退出EToolStripMenuItem1.Text = "退出(&E)";
            this.退出EToolStripMenuItem1.Click += new System.EventHandler(this.退出EToolStripMenuItem1_Click);
            // 
            // dataGridViewImage
            // 
            this.dataGridViewImage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewImage.Location = new System.Drawing.Point(0, 25);
            this.dataGridViewImage.Name = "dataGridViewImage";
            this.dataGridViewImage.RowTemplate.Height = 23;
            this.dataGridViewImage.Size = new System.Drawing.Size(468, 295);
            this.dataGridViewImage.TabIndex = 1;
            this.dataGridViewImage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImage_CellContentClick);
            this.dataGridViewImage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImage_CellDoubleClick);
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // ImageFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 320);
            this.Controls.Add(this.dataGridViewImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ImageFrm";
            this.Text = "ImageFrm";
            this.Load += new System.EventHandler(this.ImageFrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 保存表格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出EToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewImage;
        private System.Windows.Forms.ToolStripMenuItem 退出EToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
    }
}