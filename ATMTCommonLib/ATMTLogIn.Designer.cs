namespace ATMTCommonLib
{
    partial class ATMTLogIn
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_btnLogOut = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnLogin = new System.Windows.Forms.Button();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAccountList = new System.Windows.Forms.DataGridView();
            this.accountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passWordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刪除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bsAccount = new System.Windows.Forms.BindingSource(this.components);
            this.chkAccountManager = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountList)).BeginInit();
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnLogOut
            // 
            this.m_btnLogOut.BackColor = System.Drawing.Color.SkyBlue;
            this.m_btnLogOut.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.m_btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnLogOut.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.m_btnLogOut.Location = new System.Drawing.Point(169, 122);
            this.m_btnLogOut.Margin = new System.Windows.Forms.Padding(5);
            this.m_btnLogOut.Name = "m_btnLogOut";
            this.m_btnLogOut.Size = new System.Drawing.Size(160, 30);
            this.m_btnLogOut.TabIndex = 18;
            this.m_btnLogOut.Text = "登出 Logout";
            this.m_btnLogOut.UseVisualStyleBackColor = false;
            this.m_btnLogOut.Click += new System.EventHandler(this.m_btnLogOut_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.BackColor = System.Drawing.Color.SkyBlue;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.m_btnCancel.Location = new System.Drawing.Point(3, 85);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(160, 30);
            this.m_btnCancel.TabIndex = 11;
            this.m_btnCancel.Text = "取消 Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnLogin
            // 
            this.m_btnLogin.BackColor = System.Drawing.Color.SkyBlue;
            this.m_btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnLogin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.m_btnLogin.Location = new System.Drawing.Point(170, 85);
            this.m_btnLogin.Margin = new System.Windows.Forms.Padding(5);
            this.m_btnLogin.Name = "m_btnLogin";
            this.m_btnLogin.Size = new System.Drawing.Size(160, 30);
            this.m_btnLogin.TabIndex = 12;
            this.m_btnLogin.Text = "登入 Login";
            this.m_btnLogin.UseVisualStyleBackColor = false;
            this.m_btnLogin.Click += new System.EventHandler(this.m_btnLogin_Click);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPassWord.Location = new System.Drawing.Point(170, 48);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(160, 29);
            this.txtPassWord.TabIndex = 10;
            this.txtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassWord.UseSystemPasswordChar = true;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblStatus.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.m_lblStatus.Location = new System.Drawing.Point(3, 123);
            this.m_lblStatus.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(160, 29);
            this.m_lblStatus.TabIndex = 15;
            this.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 29);
            this.label2.TabIndex = 17;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAccount.Location = new System.Drawing.Point(170, 13);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(160, 29);
            this.txtAccount.TabIndex = 19;
            this.txtAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 29);
            this.label1.TabIndex = 20;
            this.label1.Text = "User";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvAccountList
            // 
            this.dgvAccountList.AutoGenerateColumns = false;
            this.dgvAccountList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accountDataGridViewTextBoxColumn,
            this.passWordDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn});
            this.dgvAccountList.ContextMenuStrip = this.cms;
            this.dgvAccountList.DataSource = this.bsAccount;
            this.dgvAccountList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvAccountList.Location = new System.Drawing.Point(0, 197);
            this.dgvAccountList.Name = "dgvAccountList";
            this.dgvAccountList.RowHeadersVisible = false;
            this.dgvAccountList.RowTemplate.Height = 24;
            this.dgvAccountList.Size = new System.Drawing.Size(332, 167);
            this.dgvAccountList.TabIndex = 21;
            this.dgvAccountList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvAccountList_CellBeginEdit);
            this.dgvAccountList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAccountList_CellFormatting);
            this.dgvAccountList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAccountList_EditingControlShowing);
            // 
            // accountDataGridViewTextBoxColumn
            // 
            this.accountDataGridViewTextBoxColumn.DataPropertyName = "Account";
            this.accountDataGridViewTextBoxColumn.FillWeight = 120F;
            this.accountDataGridViewTextBoxColumn.HeaderText = "Account";
            this.accountDataGridViewTextBoxColumn.Name = "accountDataGridViewTextBoxColumn";
            this.accountDataGridViewTextBoxColumn.Width = 140;
            // 
            // passWordDataGridViewTextBoxColumn
            // 
            this.passWordDataGridViewTextBoxColumn.DataPropertyName = "PassWord";
            this.passWordDataGridViewTextBoxColumn.FillWeight = 120F;
            this.passWordDataGridViewTextBoxColumn.HeaderText = "PassWord";
            this.passWordDataGridViewTextBoxColumn.Name = "passWordDataGridViewTextBoxColumn";
            this.passWordDataGridViewTextBoxColumn.Width = 130;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.FillWeight = 50F;
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            this.levelDataGridViewTextBoxColumn.Width = 50;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刪除ToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(99, 26);
            // 
            // 刪除ToolStripMenuItem
            // 
            this.刪除ToolStripMenuItem.Name = "刪除ToolStripMenuItem";
            this.刪除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.刪除ToolStripMenuItem.Text = "刪除";
            this.刪除ToolStripMenuItem.Click += new System.EventHandler(this.刪除ToolStripMenuItem_Click);
            // 
            // bsAccount
            // 
            this.bsAccount.DataSource = typeof(ATMTCommonLib.TAccount);
            // 
            // chkAccountManager
            // 
            this.chkAccountManager.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAccountManager.BackColor = System.Drawing.Color.SkyBlue;
            this.chkAccountManager.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkAccountManager.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.chkAccountManager.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.chkAccountManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAccountManager.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkAccountManager.Location = new System.Drawing.Point(3, 157);
            this.chkAccountManager.Name = "chkAccountManager";
            this.chkAccountManager.Size = new System.Drawing.Size(326, 29);
            this.chkAccountManager.TabIndex = 51;
            this.chkAccountManager.Text = "帳號管理";
            this.chkAccountManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAccountManager.UseVisualStyleBackColor = false;
            this.chkAccountManager.CheckedChanged += new System.EventHandler(this.chkAccountManager_CheckedChanged);
            // 
            // ATMTLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.chkAccountManager);
            this.Controls.Add(this.dgvAccountList);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnLogOut);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnLogin);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.m_lblStatus);
            this.Controls.Add(this.label2);
            this.Name = "ATMTLogIn";
            this.Size = new System.Drawing.Size(332, 364);
            this.Load += new System.EventHandler(this.ATMTLogIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountList)).EndInit();
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsAccount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnLogOut;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label m_lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAccountList;
        private System.Windows.Forms.BindingSource bsAccount;
        private System.Windows.Forms.CheckBox chkAccountManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passWordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        public System.Windows.Forms.Button m_btnLogin;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem 刪除ToolStripMenuItem;
    }
}
