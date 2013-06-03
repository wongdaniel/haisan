namespace haisan.frame.document.warehouse
{
    partial class AddWarehouseFrm
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
            this.checkBoxIsLocked = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSaveAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSaveQuit = new System.Windows.Forms.Button();
            this.labelErrorStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库名：";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(74, 30);
            this.textBoxName.MaxLength = 20;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(125, 21);
            this.textBoxName.TabIndex = 1;
            // 
            // checkBoxIsLocked
            // 
            this.checkBoxIsLocked.AutoSize = true;
            this.checkBoxIsLocked.Location = new System.Drawing.Point(248, 34);
            this.checkBoxIsLocked.Name = "checkBoxIsLocked";
            this.checkBoxIsLocked.Size = new System.Drawing.Size(72, 16);
            this.checkBoxIsLocked.TabIndex = 2;
            this.checkBoxIsLocked.Text = "是否停用";
            this.checkBoxIsLocked.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "描述：";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(74, 67);
            this.textBoxDescription.MaxLength = 50;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(237, 21);
            this.textBoxDescription.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 97);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // buttonSaveAdd
            // 
            this.buttonSaveAdd.Location = new System.Drawing.Point(68, 140);
            this.buttonSaveAdd.Name = "buttonSaveAdd";
            this.buttonSaveAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAdd.TabIndex = 24;
            this.buttonSaveAdd.Text = "保存增加";
            this.buttonSaveAdd.UseVisualStyleBackColor = true;
            this.buttonSaveAdd.Click += new System.EventHandler(this.buttonSaveAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(252, 140);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "取消退出";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSaveQuit
            // 
            this.buttonSaveQuit.Location = new System.Drawing.Point(160, 140);
            this.buttonSaveQuit.Name = "buttonSaveQuit";
            this.buttonSaveQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveQuit.TabIndex = 22;
            this.buttonSaveQuit.Text = "保存退出";
            this.buttonSaveQuit.UseVisualStyleBackColor = true;
            this.buttonSaveQuit.Click += new System.EventHandler(this.buttonSaveQuit_Click);
            // 
            // labelErrorStatus
            // 
            this.labelErrorStatus.AutoSize = true;
            this.labelErrorStatus.ForeColor = System.Drawing.Color.Red;
            this.labelErrorStatus.Location = new System.Drawing.Point(12, 116);
            this.labelErrorStatus.Name = "labelErrorStatus";
            this.labelErrorStatus.Size = new System.Drawing.Size(0, 12);
            this.labelErrorStatus.TabIndex = 25;
            // 
            // AddWarehouseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 178);
            this.Controls.Add(this.labelErrorStatus);
            this.Controls.Add(this.buttonSaveAdd);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSaveQuit);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxIsLocked);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddWarehouseFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddWarehouseFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.CheckBox checkBoxIsLocked;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSaveAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSaveQuit;
        private System.Windows.Forms.Label labelErrorStatus;
    }
}