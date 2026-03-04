namespace BusinessPermitLicensingSystem.Forms
{
    partial class DashboardForm
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            lblUsername = new Label();
            lblPenaltyNotice = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 15);
            button1.Name = "button1";
            button1.Size = new Size(318, 80);
            button1.TabIndex = 0;
            button1.Text = "Stall Owners List";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(336, 101);
            button2.Name = "button2";
            button2.Size = new Size(318, 80);
            button2.TabIndex = 1;
            button2.Text = "Reports";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(12, 101);
            button3.Name = "button3";
            button3.Size = new Size(318, 80);
            button3.TabIndex = 2;
            button3.Text = "Billing";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(336, 15);
            button4.Name = "button4";
            button4.Size = new Size(318, 80);
            button4.TabIndex = 3;
            button4.Text = "Profiling";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(336, 187);
            button5.Name = "button5";
            button5.Size = new Size(318, 80);
            button5.TabIndex = 4;
            button5.Text = "Audit Logs";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(12, 187);
            button6.Name = "button6";
            button6.Size = new Size(318, 80);
            button6.TabIndex = 5;
            button6.Text = "Exit Program";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(12, 596);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 25);
            lblUsername.TabIndex = 6;
            // 
            // lblPenaltyNotice
            // 
            lblPenaltyNotice.AutoSize = true;
            lblPenaltyNotice.ForeColor = Color.DarkOrange;
            lblPenaltyNotice.Location = new Point(736, 43);
            lblPenaltyNotice.Name = "lblPenaltyNotice";
            lblPenaltyNotice.Size = new Size(59, 25);
            lblPenaltyNotice.TabIndex = 7;
            lblPenaltyNotice.Text = "label1";
            lblPenaltyNotice.Visible = false;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1272, 630);
            Controls.Add(lblPenaltyNotice);
            Controls.Add(lblUsername);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            Load += DashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label lblUsername;
        private Label lblPenaltyNotice;
    }
}