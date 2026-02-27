namespace BusinessPermitLicensingSystem.Forms
{
    partial class AccountCreationForm
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
            txtFullName = new TextBox();
            txtuname = new TextBox();
            txtpass = new TextBox();
            txtconpass = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnCreate = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(405, 125);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(284, 31);
            txtFullName.TabIndex = 0;
            // 
            // txtuname
            // 
            txtuname.Location = new Point(405, 178);
            txtuname.Name = "txtuname";
            txtuname.Size = new Size(284, 31);
            txtuname.TabIndex = 1;
            // 
            // txtpass
            // 
            txtpass.Location = new Point(405, 228);
            txtpass.Name = "txtpass";
            txtpass.Size = new Size(284, 31);
            txtpass.TabIndex = 2;
            txtpass.UseSystemPasswordChar = true;
            // 
            // txtconpass
            // 
            txtconpass.Location = new Point(405, 277);
            txtconpass.Name = "txtconpass";
            txtconpass.Size = new Size(284, 31);
            txtconpass.TabIndex = 3;
            txtconpass.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(296, 125);
            label1.Name = "label1";
            label1.Size = new Size(95, 25);
            label1.TabIndex = 4;
            label1.Text = "Full Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(296, 178);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 5;
            label2.Text = "Username: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(296, 228);
            label3.Name = "label3";
            label3.Size = new Size(96, 25);
            label3.TabIndex = 6;
            label3.Text = "Password: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(231, 283);
            label4.Name = "label4";
            label4.Size = new Size(160, 25);
            label4.TabIndex = 7;
            label4.Text = "Confirm Password:";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(306, 343);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(165, 44);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Create Account";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(503, 343);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(165, 44);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(458, 403);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 25);
            lblMessage.TabIndex = 10;
            // 
            // AccountCreationForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 450);
            Controls.Add(lblMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtconpass);
            Controls.Add(txtpass);
            Controls.Add(txtuname);
            Controls.Add(txtFullName);
            Name = "AccountCreationForm";
            Text = "AccountCreationForm";
            Load += AccountCreationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFullName;
        private TextBox txtuname;
        private TextBox txtpass;
        private TextBox txtconpass;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnCreate;
        private Button btnCancel;
        private Label lblMessage;
    }
}