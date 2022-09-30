namespace SJLibTest
{
    partial class FmLogIn
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
        public void InitializeComponent()
        {
            this.SJLogIn1 = new SJCommonLib.SJLogIn();
            this.SuspendLayout();
            // 
            // SJLogIn1
            // 
            this.SJLogIn1.AuthorityLevel = 0;
            this.SJLogIn1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SJLogIn1.Location = new System.Drawing.Point(1, 2);
            this.SJLogIn1.Name = "SJLogIn1";
            this.SJLogIn1.Size = new System.Drawing.Size(336, 561);
            this.SJLogIn1.TabIndex = 0;
            // 
            // FmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 562);
            this.Controls.Add(this.SJLogIn1);
            this.Name = "FmLogIn";
            this.Text = "FmLogIn";
            this.ResumeLayout(false);

        }

        #endregion

        public SJCommonLib.SJLogIn SJLogIn1;
    }
}