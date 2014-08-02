namespace RGeos.Desktop
{
    partial class FrmGridLineOrPoint
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.txtCols = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRowHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRowWidth = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPickUpPoint = new System.Windows.Forms.Button();
            this.chkGridLine = new System.Windows.Forms.CheckBox();
            this.chkGridPoint = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(255, 138);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "创建";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "基点坐标";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtY, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(74, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(307, 28);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(3, 3);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(147, 21);
            this.txtX.TabIndex = 4;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(156, 3);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(148, 21);
            this.txtY.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "行  宽";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "行列数";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.41463F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.58537F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel2.Controls.Add(this.txtRows, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtCols, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(74, 55);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(307, 28);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // txtRows
            // 
            this.txtRows.Location = new System.Drawing.Point(3, 3);
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(137, 21);
            this.txtRows.TabIndex = 4;
            // 
            // txtCols
            // 
            this.txtCols.Location = new System.Drawing.Point(164, 3);
            this.txtCols.Name = "txtCols";
            this.txtCols.Size = new System.Drawing.Size(137, 21);
            this.txtCols.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(148, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(8, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "*";
            // 
            // txtRowHeight
            // 
            this.txtRowHeight.Location = new System.Drawing.Point(77, 93);
            this.txtRowHeight.Name = "txtRowHeight";
            this.txtRowHeight.Size = new System.Drawing.Size(112, 21);
            this.txtRowHeight.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "列  宽";
            // 
            // txtRowWidth
            // 
            this.txtRowWidth.Location = new System.Drawing.Point(241, 93);
            this.txtRowWidth.Name = "txtRowWidth";
            this.txtRowWidth.Size = new System.Drawing.Size(137, 21);
            this.txtRowWidth.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(336, 138);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPickUpPoint
            // 
            this.btnPickUpPoint.Location = new System.Drawing.Point(384, 18);
            this.btnPickUpPoint.Name = "btnPickUpPoint";
            this.btnPickUpPoint.Size = new System.Drawing.Size(41, 23);
            this.btnPickUpPoint.TabIndex = 7;
            this.btnPickUpPoint.Text = "拾取";
            this.btnPickUpPoint.UseVisualStyleBackColor = true;
            this.btnPickUpPoint.Click += new System.EventHandler(this.btnPickUpPoint_Click);
            // 
            // chkGridLine
            // 
            this.chkGridLine.AutoSize = true;
            this.chkGridLine.Location = new System.Drawing.Point(30, 18);
            this.chkGridLine.Name = "chkGridLine";
            this.chkGridLine.Size = new System.Drawing.Size(48, 16);
            this.chkGridLine.TabIndex = 8;
            this.chkGridLine.Text = "格网";
            this.chkGridLine.UseVisualStyleBackColor = true;
            // 
            // chkGridPoint
            // 
            this.chkGridPoint.AutoSize = true;
            this.chkGridPoint.Location = new System.Drawing.Point(122, 18);
            this.chkGridPoint.Name = "chkGridPoint";
            this.chkGridPoint.Size = new System.Drawing.Size(60, 16);
            this.chkGridPoint.TabIndex = 8;
            this.chkGridPoint.Text = "格网点";
            this.chkGridPoint.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkGridPoint);
            this.groupBox1.Controls.Add(this.chkGridLine);
            this.groupBox1.Location = new System.Drawing.Point(17, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 49);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // FrmGridLineOrPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 193);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPickUpPoint);
            this.Controls.Add(this.txtRowWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRowHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmGridLineOrPoint";
            this.Text = "生成格网(点)";
            this.Load += new System.EventHandler(this.FrmGridLineOrPoint_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.TextBox txtCols;
        private System.Windows.Forms.TextBox txtRowHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRowWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPickUpPoint;
        private System.Windows.Forms.CheckBox chkGridLine;
        private System.Windows.Forms.CheckBox chkGridPoint;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}