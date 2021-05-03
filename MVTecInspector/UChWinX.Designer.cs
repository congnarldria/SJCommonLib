
namespace CommonInspector
{
    partial class UChWinX
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
            this.hWinX = new HalconDotNet.HWindowControl();
            this.cmsEditWondow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMeasureDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMeasureStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMeasureEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMeasureStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.bsWindowInfo = new System.Windows.Forms.BindingSource(this.components);
            this.cmsEditWondow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsWindowInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // hWinX
            // 
            this.hWinX.BackColor = System.Drawing.Color.Black;
            this.hWinX.BorderColor = System.Drawing.Color.Black;
            this.hWinX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWinX.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWinX.Location = new System.Drawing.Point(0, 0);
            this.hWinX.Name = "hWinX";
            this.hWinX.Size = new System.Drawing.Size(672, 548);
            this.hWinX.TabIndex = 0;
            this.hWinX.WindowSize = new System.Drawing.Size(672, 548);
            // 
            // cmsEditWondow
            // 
            this.cmsEditWondow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdd,
            this.tsmEdit,
            this.tsmCopy,
            this.tsmPaste,
            this.tsmDelete,
            this.tsmMeasureDistance,
            this.tsmSave,
            this.tsmLoadImage});
            this.cmsEditWondow.Name = "cmsEditWondow";
            this.cmsEditWondow.Size = new System.Drawing.Size(123, 180);
            // 
            // tsmAdd
            // 
            this.tsmAdd.Name = "tsmAdd";
            this.tsmAdd.Size = new System.Drawing.Size(122, 22);
            this.tsmAdd.Text = "新增";
            // 
            // tsmEdit
            // 
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(122, 22);
            this.tsmEdit.Text = "編輯";
            // 
            // tsmCopy
            // 
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.Size = new System.Drawing.Size(122, 22);
            this.tsmCopy.Text = "複製";
            // 
            // tsmPaste
            // 
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.Size = new System.Drawing.Size(122, 22);
            this.tsmPaste.Text = "貼上";
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(122, 22);
            this.tsmDelete.Text = "刪除";
            // 
            // tsmMeasureDistance
            // 
            this.tsmMeasureDistance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmMeasureStart,
            this.tsmMeasureEnd,
            this.tsmMeasureStop});
            this.tsmMeasureDistance.Name = "tsmMeasureDistance";
            this.tsmMeasureDistance.Size = new System.Drawing.Size(122, 22);
            this.tsmMeasureDistance.Text = "距離量測";
            // 
            // tsmMeasureStart
            // 
            this.tsmMeasureStart.Name = "tsmMeasureStart";
            this.tsmMeasureStart.Size = new System.Drawing.Size(122, 22);
            this.tsmMeasureStart.Text = "量測起點";
            // 
            // tsmMeasureEnd
            // 
            this.tsmMeasureEnd.Name = "tsmMeasureEnd";
            this.tsmMeasureEnd.Size = new System.Drawing.Size(122, 22);
            this.tsmMeasureEnd.Text = "量測終點";
            // 
            // tsmMeasureStop
            // 
            this.tsmMeasureStop.Name = "tsmMeasureStop";
            this.tsmMeasureStop.Size = new System.Drawing.Size(122, 22);
            this.tsmMeasureStop.Text = "停止量測";
            // 
            // tsmSave
            // 
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.Size = new System.Drawing.Size(122, 22);
            this.tsmSave.Text = "存圖";
            // 
            // tsmLoadImage
            // 
            this.tsmLoadImage.Name = "tsmLoadImage";
            this.tsmLoadImage.Size = new System.Drawing.Size(122, 22);
            this.tsmLoadImage.Text = "讀圖";
            // 
            // UChWinX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hWinX);
            this.Name = "UChWinX";
            this.Size = new System.Drawing.Size(672, 548);
            this.cmsEditWondow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsWindowInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HWindowControl hWinX;
        private System.Windows.Forms.ContextMenuStrip cmsEditWondow;
        private System.Windows.Forms.ToolStripMenuItem tsmAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmMeasureDistance;
        private System.Windows.Forms.ToolStripMenuItem tsmMeasureStart;
        private System.Windows.Forms.ToolStripMenuItem tsmMeasureEnd;
        private System.Windows.Forms.ToolStripMenuItem tsmMeasureStop;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmLoadImage;
        private System.Windows.Forms.BindingSource bsWindowInfo;
    }
}
