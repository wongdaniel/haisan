using System.Windows.Forms;
using System.Drawing;
namespace haisan.frame.system.group
{
    partial class PermisssionFrm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewModule = new System.Windows.Forms.TreeView();
            this.dataGridViewPermission = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.全部选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部清除SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.确定CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threeStateQuery = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateAdd = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateModify = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStatePrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateRun = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateSaveTable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.threeStateAnticheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPermission)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewModule);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewPermission);
            this.splitContainer1.Size = new System.Drawing.Size(584, 387);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeViewModule
            // 
            this.treeViewModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewModule.Location = new System.Drawing.Point(0, 0);
            this.treeViewModule.Name = "treeViewModule";
            this.treeViewModule.Size = new System.Drawing.Size(141, 387);
            this.treeViewModule.TabIndex = 0;
            this.treeViewModule.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewModule_AfterSelect);
            // 
            // dataGridViewPermission
            // 
            this.dataGridViewPermission.AllowUserToAddRows = false;
            this.dataGridViewPermission.AllowUserToDeleteRows = false;
            this.dataGridViewPermission.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPermission.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnName,
            this.threeStateQuery,
            this.threeStateAdd,
            this.threeStateModify,
            this.threeStateDelete,
            this.threeStatePrint,
            this.threeStateRun,
            this.threeStateSaveTable,
            this.threeStateCheck,
            this.threeStateAnticheck});
            this.dataGridViewPermission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPermission.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPermission.Name = "dataGridViewPermission";
            this.dataGridViewPermission.RowTemplate.Height = 23;
            this.dataGridViewPermission.Size = new System.Drawing.Size(439, 387);
            this.dataGridViewPermission.TabIndex = 0;
            this.dataGridViewPermission.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPermission_CellContentClick);
            this.dataGridViewPermission.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewPermission_CellFormatting);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部选择ToolStripMenuItem,
            this.全部清除SToolStripMenuItem,
            this.确定CToolStripMenuItem,
            this.取消EToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 全部选择ToolStripMenuItem
            // 
            this.全部选择ToolStripMenuItem.Name = "全部选择ToolStripMenuItem";
            this.全部选择ToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.全部选择ToolStripMenuItem.Text = "全部选择(&S)";
            // 
            // 全部清除SToolStripMenuItem
            // 
            this.全部清除SToolStripMenuItem.Name = "全部清除SToolStripMenuItem";
            this.全部清除SToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.全部清除SToolStripMenuItem.Text = "全部清除(&S)";
            // 
            // 确定CToolStripMenuItem
            // 
            this.确定CToolStripMenuItem.Name = "确定CToolStripMenuItem";
            this.确定CToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.确定CToolStripMenuItem.Text = "确定(&C)";
            this.确定CToolStripMenuItem.Click += new System.EventHandler(this.确定CToolStripMenuItem_Click);
            // 
            // 取消EToolStripMenuItem
            // 
            this.取消EToolStripMenuItem.Name = "取消EToolStripMenuItem";
            this.取消EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.取消EToolStripMenuItem.Text = "取消(&E)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(164, 17);
            this.toolStripStatusLabel1.Text = "说明：修改权限请点击单元格";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "查询";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.Width = 35;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "新增";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 35;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.HeaderText = "修改";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.Width = 35;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.HeaderText = "删除";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Width = 35;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.HeaderText = "打印";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.Width = 35;
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.HeaderText = "运行";
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.Width = 35;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.HeaderText = "保表";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.Width = 35;
            // 
            // dataGridViewCheckBoxColumn8
            // 
            this.dataGridViewCheckBoxColumn8.HeaderText = "审查";
            this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
            this.dataGridViewCheckBoxColumn8.Width = 35;
            // 
            // dataGridViewCheckBoxColumn9
            // 
            this.dataGridViewCheckBoxColumn9.HeaderText = "反审查";
            this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
            this.dataGridViewCheckBoxColumn9.Width = 47;
            // 
            // ColumnID
            // 
            this.ColumnID.HeaderText = "权限ID";
            this.ColumnID.Name = "ColumnID";
            this.ColumnID.Visible = false;
            this.ColumnID.Width = 66;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "模块名";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Width = 66;
            // 
            // threeStateQuery
            // 
            this.threeStateQuery.HeaderText = "查询";
            this.threeStateQuery.Name = "threeStateQuery";
            this.threeStateQuery.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.threeStateQuery.Width = 35;
            // 
            // threeStateAdd
            // 
            this.threeStateAdd.HeaderText = "新增";
            this.threeStateAdd.Name = "threeStateAdd";
            this.threeStateAdd.Width = 35;
            // 
            // threeStateModify
            // 
            this.threeStateModify.HeaderText = "修改";
            this.threeStateModify.Name = "threeStateModify";
            this.threeStateModify.Width = 35;
            // 
            // threeStateDelete
            // 
            this.threeStateDelete.HeaderText = "删除";
            this.threeStateDelete.Name = "threeStateDelete";
            this.threeStateDelete.Width = 35;
            // 
            // threeStatePrint
            // 
            this.threeStatePrint.HeaderText = "打印";
            this.threeStatePrint.Name = "threeStatePrint";
            this.threeStatePrint.Width = 35;
            // 
            // threeStateRun
            // 
            this.threeStateRun.HeaderText = "运行";
            this.threeStateRun.Name = "threeStateRun";
            this.threeStateRun.Width = 35;
            // 
            // threeStateSaveTable
            // 
            this.threeStateSaveTable.HeaderText = "保表";
            this.threeStateSaveTable.Name = "threeStateSaveTable";
            this.threeStateSaveTable.Width = 35;
            // 
            // threeStateCheck
            // 
            this.threeStateCheck.HeaderText = "审查";
            this.threeStateCheck.Name = "threeStateCheck";
            this.threeStateCheck.Width = 35;
            // 
            // threeStateAnticheck
            // 
            this.threeStateAnticheck.HeaderText = "反审查";
            this.threeStateAnticheck.Name = "threeStateAnticheck";
            this.threeStateAnticheck.Width = 47;
            // 
            // PermisssionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "PermisssionFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PermisssionFrm";
            this.Load += new System.EventHandler(this.PermisssionFrm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPermission)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewModule;
        private System.Windows.Forms.DataGridView dataGridViewPermission;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全部选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部清除SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 确定CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消EToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private DataGridViewTextBoxColumn ColumnID;
        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewCheckBoxColumn threeStateQuery;
        private DataGridViewCheckBoxColumn threeStateAdd;
        private DataGridViewCheckBoxColumn threeStateModify;
        private DataGridViewCheckBoxColumn threeStateDelete;
        private DataGridViewCheckBoxColumn threeStatePrint;
        private DataGridViewCheckBoxColumn threeStateRun;
        private DataGridViewCheckBoxColumn threeStateSaveTable;
        private DataGridViewCheckBoxColumn threeStateCheck;
        private DataGridViewCheckBoxColumn threeStateAnticheck;
    }
}