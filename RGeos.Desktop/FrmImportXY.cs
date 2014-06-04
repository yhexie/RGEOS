using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RGeos.Desktop
{
    public partial class FrmImportXY : Form
    {
        public FrmImportXY()
        {
            InitializeComponent();
        }
        List<RgFileFormat> formats = null;
        private void FrmImportXY_Load(object sender, EventArgs e)
        {
            txtFormat.Enabled = false;
            RgFileFormat format1 = new RgFileFormat("格式一(*.txt)", "ID,X,Y");
            RgFileFormat format2 = new RgFileFormat("格式二(*.txt)", "ID,X,Y,Z");
            formats = new List<RgFileFormat>();
            formats.Add(format1);
            formats.Add(format2);
            for (int i = 0; i < formats.Count; i++)
            {
                RgFileFormat format0 = formats[i];
                listFormatName.Items.Add(format0.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFormatName.SelectedIndex > -1)
            {
                txtFormat.Text = string.Format("格式字符串：{0}", formats[listFormatName.SelectedIndex].FormatString);
            }

        }
       

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
