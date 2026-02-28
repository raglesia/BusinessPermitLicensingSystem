namespace BusinessPermitLicensingSystem.Forms
{
    partial class AuditTrail
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
            dtAudit = new DataGridView();
            button1 = new Button();
            radioUsers = new RadioButton();
            radioProfiling = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dtAudit).BeginInit();
            SuspendLayout();
            // 
            // dtAudit
            // 
            dtAudit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtAudit.Location = new Point(12, 130);
            dtAudit.Name = "dtAudit";
            dtAudit.RowHeadersWidth = 62;
            dtAudit.Size = new Size(1573, 291);
            dtAudit.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(12, 445);
            button1.Name = "button1";
            button1.Size = new Size(147, 34);
            button1.TabIndex = 1;
            button1.Text = "Main Menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // radioUsers
            // 
            radioUsers.AutoSize = true;
            radioUsers.Location = new Point(27, 80);
            radioUsers.Name = "radioUsers";
            radioUsers.Size = new Size(128, 29);
            radioUsers.TabIndex = 2;
            radioUsers.TabStop = true;
            radioUsers.Text = "Users Audit";
            radioUsers.UseVisualStyleBackColor = true;
            radioUsers.CheckedChanged += radioUsers_CheckedChanged;
            // 
            // radioProfiling
            // 
            radioProfiling.AutoSize = true;
            radioProfiling.Location = new Point(190, 80);
            radioProfiling.Name = "radioProfiling";
            radioProfiling.Size = new Size(151, 29);
            radioProfiling.TabIndex = 3;
            radioProfiling.TabStop = true;
            radioProfiling.Text = "Profiling Audit";
            radioProfiling.UseVisualStyleBackColor = true;
            radioProfiling.CheckedChanged += radioProfiling_CheckedChanged;
            // 
            // AuditTrail
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1597, 541);
            Controls.Add(radioProfiling);
            Controls.Add(radioUsers);
            Controls.Add(button1);
            Controls.Add(dtAudit);
            Name = "AuditTrail";
            Text = "AuditTrail";
            ((System.ComponentModel.ISupportInitialize)dtAudit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dtAudit;
        private Button button1;
        private RadioButton radioUsers;
        private RadioButton radioProfiling;
    }
}