namespace KioskUpdater
{
    partial class Form1
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
            this.btnSyncDonation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSyncDonation
            // 
            this.btnSyncDonation.Location = new System.Drawing.Point(285, 150);
            this.btnSyncDonation.Name = "btnSyncDonation";
            this.btnSyncDonation.Size = new System.Drawing.Size(198, 94);
            this.btnSyncDonation.TabIndex = 0;
            this.btnSyncDonation.Text = "Sync Donation";
            this.btnSyncDonation.UseVisualStyleBackColor = true;
            this.btnSyncDonation.Click += new System.EventHandler(this.btnSyncDonation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSyncDonation);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSyncDonation;
    }
}