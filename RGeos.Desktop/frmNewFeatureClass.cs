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
    public partial class FrmNewFeatureClass : Form
    {
        public FrmNewFeatureClass()
        {
            InitializeComponent();
            this.cmbShapeType.Items.Add("Point");
            this.cmbShapeType.Items.Add("Polyline");
            this.cmbShapeType.Items.Add("Polygon");
            this.cmbShapeType.SelectedIndex = 0;
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
            strFeatureName = this.txtLayerName.Text.Trim();
            if (strFeatureName.Trim()==string.Empty)
            {
                MessageBox.Show("图层名称不能为空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            int idx = cmbShapeType.SelectedIndex;
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
