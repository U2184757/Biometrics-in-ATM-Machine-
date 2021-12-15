﻿namespace ATM_Verification_System
{
    partial class FingerprintAmount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerprintAmount));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWithDraw = new System.Windows.Forms.Button();
            this.lblAmount = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.lblAccountLabel = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnWithDraw);
            this.groupBox1.Controls.Add(this.lblAmount);
            this.groupBox1.Controls.Add(this.tbAmount);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(166, 116);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(321, 153);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Amount Withdraw";
            // 
            // btnWithDraw
            // 
            this.btnWithDraw.BackColor = System.Drawing.Color.Firebrick;
            this.btnWithDraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWithDraw.Location = new System.Drawing.Point(190, 90);
            this.btnWithDraw.Name = "btnWithDraw";
            this.btnWithDraw.Size = new System.Drawing.Size(99, 29);
            this.btnWithDraw.TabIndex = 12;
            this.btnWithDraw.Text = "Withdraw";
            this.btnWithDraw.UseVisualStyleBackColor = false;
            this.btnWithDraw.Click += new System.EventHandler(this.btnWithDraw_Click);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.White;
            this.lblAmount.Location = new System.Drawing.Point(32, 46);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(59, 16);
            this.lblAmount.TabIndex = 11;
            this.lblAmount.Text = "Amount";
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(115, 40);
            this.tbAmount.Multiline = true;
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(175, 30);
            this.tbAmount.TabIndex = 10;
            // 
            // lblAccountLabel
            // 
            this.lblAccountLabel.AutoSize = true;
            this.lblAccountLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAccountLabel.Location = new System.Drawing.Point(47, 28);
            this.lblAccountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAccountLabel.Name = "lblAccountLabel";
            this.lblAccountLabel.Size = new System.Drawing.Size(69, 15);
            this.lblAccountLabel.TabIndex = 15;
            this.lblAccountLabel.Text = "Account #";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.BackColor = System.Drawing.Color.Transparent;
            this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.ForeColor = System.Drawing.Color.White;
            this.lblAccount.Location = new System.Drawing.Point(116, 28);
            this.lblAccount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(79, 15);
            this.lblAccount.TabIndex = 14;
            this.lblAccount.Text = "Account No";
            // 
            // FingerprintAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(664, 374);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblAccountLabel);
            this.Controls.Add(this.lblAccount);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FingerprintAmount";
            this.Text = "Amount Withdraw";
            this.Load += new System.EventHandler(this.FingerprintAmount_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWithDraw;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.Label lblAccountLabel;
        private System.Windows.Forms.Label lblAccount;
    }
}