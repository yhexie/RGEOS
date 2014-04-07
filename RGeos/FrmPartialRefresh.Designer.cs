namespace RGeos
{
    partial class FrmPartialRefresh
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPartialRefresh));
            this.btnDrawPolyline = new System.Windows.Forms.Button();
            this.btnDrawLine = new System.Windows.Forms.Button();
            this.btnDrawPoint = new System.Windows.Forms.Button();
            this.btnDrawPolygon = new System.Windows.Forms.Button();
            this.btnAddlayer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDrawPolyline
            // 
            this.btnDrawPolyline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawPolyline.Location = new System.Drawing.Point(486, 81);
            this.btnDrawPolyline.Name = "btnDrawPolyline";
            this.btnDrawPolyline.Size = new System.Drawing.Size(75, 23);
            this.btnDrawPolyline.TabIndex = 0;
            this.btnDrawPolyline.Text = "绘制折线";
            this.btnDrawPolyline.UseVisualStyleBackColor = true;
            this.btnDrawPolyline.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawLine.Location = new System.Drawing.Point(486, 52);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(75, 23);
            this.btnDrawLine.TabIndex = 0;
            this.btnDrawLine.Text = "绘制线段";
            this.btnDrawLine.UseVisualStyleBackColor = true;
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // btnDrawPoint
            // 
            this.btnDrawPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawPoint.Location = new System.Drawing.Point(486, 23);
            this.btnDrawPoint.Name = "btnDrawPoint";
            this.btnDrawPoint.Size = new System.Drawing.Size(75, 23);
            this.btnDrawPoint.TabIndex = 0;
            this.btnDrawPoint.Text = "绘制点";
            this.btnDrawPoint.UseVisualStyleBackColor = true;
            this.btnDrawPoint.Click += new System.EventHandler(this.btnDrawPoint_Click);
            // 
            // btnDrawPolygon
            // 
            this.btnDrawPolygon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawPolygon.Location = new System.Drawing.Point(486, 110);
            this.btnDrawPolygon.Name = "btnDrawPolygon";
            this.btnDrawPolygon.Size = new System.Drawing.Size(75, 23);
            this.btnDrawPolygon.TabIndex = 0;
            this.btnDrawPolygon.Text = "绘制多边形";
            this.btnDrawPolygon.UseVisualStyleBackColor = true;
            this.btnDrawPolygon.Click += new System.EventHandler(this.btnDrawPolygon_Click);
            // 
            // btnAddlayer
            // 
            this.btnAddlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddlayer.Location = new System.Drawing.Point(486, 139);
            this.btnAddlayer.Name = "btnAddlayer";
            this.btnAddlayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddlayer.TabIndex = 0;
            this.btnAddlayer.Text = "添加图层";
            this.btnAddlayer.UseVisualStyleBackColor = true;
            this.btnAddlayer.Click += new System.EventHandler(this.btnAddlayer_Click);
            // 
            // FrmPartialRefresh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 373);
            this.Controls.Add(this.btnDrawPoint);
            this.Controls.Add(this.btnDrawLine);
            this.Controls.Add(this.btnAddlayer);
            this.Controls.Add(this.btnDrawPolygon);
            this.Controls.Add(this.btnDrawPolyline);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPartialRefresh";
            this.Text = "局部刷新测试";
            this.Load += new System.EventHandler(this.FrmPartialRefresh_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDrawPolyline;
        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.Button btnDrawPoint;
        private System.Windows.Forms.Button btnDrawPolygon;
        private System.Windows.Forms.Button btnAddlayer;

    }
}

