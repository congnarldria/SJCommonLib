namespace ATMTLibTest
{
    partial class FmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbForBind = new System.Windows.Forms.ComboBox();
            this.bsCombo = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsCombo)).BeginInit();
            this.SuspendLayout();
            // 
            // cbForBind
            // 
            this.cbForBind.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCombo, "DropString", true));
            this.cbForBind.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCombo, "Selected", true));
            this.cbForBind.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsCombo, "DropString", true));
            this.cbForBind.DataSource = this.bsCombo;
            this.cbForBind.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbForBind.FormattingEnabled = true;
            this.cbForBind.Location = new System.Drawing.Point(531, 226);
            this.cbForBind.Name = "cbForBind";
            this.cbForBind.Size = new System.Drawing.Size(223, 29);
            this.cbForBind.TabIndex = 1;
            // 
            // bsCombo
            // 
            this.bsCombo.DataSource = typeof(ATMTLibTest.TComboLList);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(615, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(646, 149);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(131, 22);
            this.textBox1.TabIndex = 3;
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 431);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbForBind);
            this.Name = "FmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsCombo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbForBind;
        private System.Windows.Forms.BindingSource bsCombo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

