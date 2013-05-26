namespace haisan.frame.system.group
{
    partial class AddGroupFrm
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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonSure = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelErrStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(25, 36);
            this.labelName.Name = "label1";
            this.labelName.Size = new System.Drawing.Size(65, 12);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "用户组名：";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(107, 30);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(152, 21);
            this.textBoxName.TabIndex = 1;
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(27, 124);
            this.buttonSure.Name = "buttonSure";
            this.buttonSure.Size = new System.Drawing.Size(75, 23);
            this.buttonSure.TabIndex = 2;
            this.buttonSure.Text = "确  定";
            this.buttonSure.UseVisualStyleBackColor = true;
            this.buttonSure.Click += new System.EventHandler(this.buttonSure_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(170, 124);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取  消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelErrStatus
            // 
            this.labelErrStatus.AutoSize = true;
            this.labelErrStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelErrStatus.ForeColor = System.Drawing.Color.Red;
            this.labelErrStatus.Location = new System.Drawing.Point(27, 78);
            this.labelErrStatus.Name = "labelErrStatus";
            this.labelErrStatus.Size = new System.Drawing.Size(0, 12);
            this.labelErrStatus.TabIndex = 4;
            // 
            // AddGroupFrm
            // 
            this.AcceptButton = this.buttonSure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 165);
            this.Controls.Add(this.labelErrStatus);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSure);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Name = "AddGroupFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelErrStatus;
    }
}