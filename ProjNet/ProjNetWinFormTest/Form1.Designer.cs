namespace ProjNetWinFormTest
{
    partial class FrmUTM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUTM));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.locationLong = new System.Windows.Forms.TextBox();
            this.locationLat = new System.Windows.Forms.TextBox();
            this.locationX = new System.Windows.Forms.TextBox();
            this.locationY = new System.Windows.Forms.TextBox();
            this.locationZone = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(733, 345);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // locationLong
            // 
            this.locationLong.Location = new System.Drawing.Point(35, 12);
            this.locationLong.Name = "locationLong";
            this.locationLong.Size = new System.Drawing.Size(157, 21);
            this.locationLong.TabIndex = 1;
            // 
            // locationLat
            // 
            this.locationLat.Location = new System.Drawing.Point(271, 12);
            this.locationLat.Name = "locationLat";
            this.locationLat.Size = new System.Drawing.Size(154, 21);
            this.locationLat.TabIndex = 1;
            // 
            // locationX
            // 
            this.locationX.Location = new System.Drawing.Point(35, 39);
            this.locationX.Name = "locationX";
            this.locationX.Size = new System.Drawing.Size(157, 21);
            this.locationX.TabIndex = 1;
            // 
            // locationY
            // 
            this.locationY.Location = new System.Drawing.Point(271, 39);
            this.locationY.Name = "locationY";
            this.locationY.Size = new System.Drawing.Size(154, 21);
            this.locationY.TabIndex = 1;
            // 
            // locationZone
            // 
            this.locationZone.Location = new System.Drawing.Point(510, 39);
            this.locationZone.Name = "locationZone";
            this.locationZone.Size = new System.Drawing.Size(154, 21);
            this.locationZone.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.locationZone);
            this.splitContainer1.Panel2.Controls.Add(this.locationLong);
            this.splitContainer1.Panel2.Controls.Add(this.locationY);
            this.splitContainer1.Panel2.Controls.Add(this.locationLat);
            this.splitContainer1.Panel2.Controls.Add(this.locationX);
            this.splitContainer1.Size = new System.Drawing.Size(733, 419);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.TabIndex = 2;
            // 
            // FrmUTM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 419);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmUTM";
            this.Text = "UTM投影测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox locationLong;
        private System.Windows.Forms.TextBox locationLat;
        private System.Windows.Forms.TextBox locationX;
        private System.Windows.Forms.TextBox locationY;
        private System.Windows.Forms.TextBox locationZone;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

