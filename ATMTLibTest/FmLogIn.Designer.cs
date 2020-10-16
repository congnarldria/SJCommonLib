namespace ATMTLibTest
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
            this.atmtLogIn1 = new ATMTCommonLib.ATMTLogIn();
            this.SuspendLayout();
            // 
            // atmtLogIn1
            // 
            this.atmtLogIn1.AuthorityLevel = 0;
            this.atmtLogIn1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.atmtLogIn1.Location = new System.Drawing.Point(1, 2);
            this.atmtLogIn1.Name = "atmtLogIn1";
            this.atmtLogIn1.Size = new System.Drawing.Size(336, 375);
            this.atmtLogIn1.TabIndex = 0;
            // 
            // FmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 381);
            this.Controls.Add(this.atmtLogIn1);
            this.Name = "FmLogIn";
            this.Text = "FmLogIn";
            this.ResumeLayout(false);

        }

        #endregion

        public ATMTCommonLib.ATMTLogIn atmtLogIn1;
    }
}