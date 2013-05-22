namespace haisan.frame.system
{
    partial class ChangePwd
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
            this.textBoxOldPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNewPwd = new System.Windows.Forms.TextBox();
            this.labelConfirmPwd = new System.Windows.Forms.Label();
            this.textBoxConfirmPwd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原密码：";
            // 
            // textBoxOldPwd
            // 
            this.textBoxOldPwd.Location = new System.Drawing.Point(98, 26);
            this.textBoxOldPwd.MaxLength = 20;
            this.textBoxOldPwd.Name = "textBoxOldPwd";
            this.textBoxOldPwd.Size = new System.Drawing.Size(115, 21);
            this.textBoxOldPwd.TabIndex = 1;
            this.textBoxOldPwd.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "新密码：";
            // 
            // textBoxNewPwd
            // 
            this.textBoxNewPwd.Location = new System.Drawing.Point(98, 64);
            this.textBoxNewPwd.MaxLength = 20;
            this.textBoxNewPwd.Name = "textBoxNewPwd";
            this.textBoxNewPwd.Size = new System.Drawing.Size(115, 21);
            this.textBoxNewPwd.TabIndex = 3;
            this.textBoxNewPwd.UseSystemPasswordChar = true;
            // 
            // labelConfirmPwd
            // 
            this.labelConfirmPwd.AutoSize = true;
            this.labelConfirmPwd.Location = new System.Drawing.Point(29, 107);
            this.labelConfirmPwd.Name = "labelConfirmPwd";
            this.labelConfirmPwd.Size = new System.Drawing.Size(65, 12);
            this.labelConfirmPwd.TabIndex = 4;
            this.labelConfirmPwd.Text = "确认密码：";
            // 
            // textBoxConfirmPwd
            // 
            this.textBoxConfirmPwd.Location = new System.Drawing.Point(98, 103);
            this.textBoxConfirmPwd.MaxLength = 20;
            this.textBoxConfirmPwd.Name = "textBoxConfirmPwd";
            this.textBoxConfirmPwd.Size = new System.Drawing.Size(115, 21);
            this.textBoxConfirmPwd.TabIndex = 5;
            this.textBoxConfirmPwd.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(158, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(14, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 134);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // ChangePwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 177);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxConfirmPwd);
            this.Controls.Add(this.labelConfirmPwd);
            this.Controls.Add(this.textBoxNewPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOldPwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChangePwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangePwd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOldPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNewPwd;
        private System.Windows.Forms.Label labelConfirmPwd;
        private System.Windows.Forms.TextBox textBoxConfirmPwd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}