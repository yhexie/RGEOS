using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.Carto;

namespace RGeos.Desktop
{
    public partial class frmNewFeatureClass : Form
    {
        public frmNewFeatureClass()
        {
            InitializeComponent();
            this.comboBox1.Items.Add("Point");
            this.comboBox1.Items.Add("Polyline");
            this.comboBox1.Items.Add("Polygon");
            this.comboBox1.SelectedIndex = 0;
        }
        string strFeatureName;

        public string FeatureName
        {
            get { return strFeatureName; }
            set { strFeatureName = value; }
        }
        RgEnumShapeType mShapeType;

        public RgEnumShapeType ShapeType
        {
            get { return mShapeType; }
            set { mShapeType = value; }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            strFeatureName = this.textBox1.Text.Trim();
            int idx = comboBox1.SelectedIndex;
            if (idx == 0)
            {
                mShapeType = RgEnumShapeType.RgPoint;
            }
            else if (idx == 1)
            {
                mShapeType = RgEnumShapeType.RgLineString;
            }
            else if (idx == 2)
            {
                mShapeType = RgEnumShapeType.RgPolygon;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
