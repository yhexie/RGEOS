using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
namespace RGeos.Desktop
{
    public partial class FrmGaussProj : Form
    {
        public FrmGaussProj()
        {
            InitializeComponent();
        }

        private void FrmGaussProj_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("北京1954");
            comboBox1.Items.Add("西安1980");
            comboBox1.Items.Add("国家2000");
            comboBox1.Items.Add("WGS 1984");
            comboBox1.SelectedIndex = 0;

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string str = txtLon.Text.Trim();
            if (str == string.Empty)
            {
                errorProvider1.SetError(txtLon, "请输入经度！");
            }
            double lon = 0;
            try
            {
                lon = Convert.ToDouble(str);
            }
            catch
            {
                errorProvider1.SetError(txtLon, "请输入度为单位的经度！");
            }
            string str2 = txtLat.Text.Trim();
            if (str2 == string.Empty)
            {
                errorProvider1.SetError(txtLat, "请输入纬度！");
            }
            double lat = 0;
            try
            {
                lat = Convert.ToDouble(str2);
            }
            catch
            {
                errorProvider1.SetError(txtLat, "请输入度为单位的纬度！");
            }

            string str3 = txtCentre.Text.Trim();
            if (str3 == string.Empty)
            {
                errorProvider1.SetError(txtLat, "请输入中央经线！");
            }
            double longitude0 = 0;
            try
            {
                longitude0 = Convert.ToDouble(str3);
            }
            catch
            {
                errorProvider1.SetError(txtCentre, "请输入度为单位的中央经线！");
            }
            List<ProjectionParameter> pInfo = new List<ProjectionParameter>();
            pInfo.Add(new ProjectionParameter("latitude_of_origin", 0));
            pInfo.Add(new ProjectionParameter("central_meridian", longitude0));
            pInfo.Add(new ProjectionParameter("scale_factor", 1));
            pInfo.Add(new ProjectionParameter("false_easting", 500000));
            pInfo.Add(new ProjectionParameter("false_northing", 0));

            Projection proj = new Projection("Gauss_Kruger", pInfo, "", "EPSG", 0, String.Empty, String.Empty, String.Empty);
            System.Collections.Generic.List<AxisInfo> axes = new List<AxisInfo>();
            axes.Add(new AxisInfo("East", AxisOrientationEnum.East));
            axes.Add(new AxisInfo("North", AxisOrientationEnum.North));
            ISpatialReferenceFactory srf = new SpatialReferenceEnvironmentClass();
            IHorizontalDatum dataum = srf.CreateDatum((int)RgSRDatumType.RgSRDatum_Beijing1954);
            ICoordinateSystem gauss = new ProjectedCoordinateSystem(dataum, coord, ProjNet.CoordinateSystems.LinearUnit.Metre, proj, axes, "CustomGauss", "EPSG", 0, String.Empty, "", string.Empty);
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(coord, gauss);

            double[] pGauss = trans.MathTransform.Transform(new double[] { lon, lat });
            txtX.Text = string.Format("{0}", pGauss[0]);
            txtY.Text = string.Format("{0}", pGauss[1]);
        }
        IGeographicCoordinateSystem coord = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISpatialReferenceFactory srf = new SpatialReferenceEnvironmentClass();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    coord = srf.CreateGeographicCoordinateSystem((int)RgSRGeoCSType.RgSRGeoCS_Beijing1954);
                    break;
                case 1:
                    coord = srf.CreateGeographicCoordinateSystem((int)RgSRGeoCS3Type.RgSRGeoCS_Xian1980);
                    break;
                case 2:
                    coord = srf.CreateGeographicCoordinateSystem((int)RgSRGeoCSType.RgSRGeoCS_Clarke1880);
                    break;
                case 3:
                    coord = srf.CreateGeographicCoordinateSystem((int)RgSRGeoCSType.RgSRGeoCS_WGS1984);
                    break;
            }
        }

        private void txtLon_Leave(object sender, EventArgs e)
        {

        }

        private void btnInvCalculate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string str = txtY.Text.Trim();
            if (str == string.Empty)
            {
                errorProvider1.SetError(txtY, "请输入高斯平面Y坐标！");
            }
            double dgaussY = 0;
            try
            {
                dgaussY = Convert.ToDouble(str);
            }
            catch
            {
                errorProvider1.SetError(txtY, "请输入m为单位的高斯平面Y坐标！");
            }
            string str2 = txtX.Text.Trim();
            if (str2 == string.Empty)
            {
                errorProvider1.SetError(txtX, "请输入高斯平面X坐标！");
            }
            double dgaussX = 0;
            try
            {
                dgaussX = Convert.ToDouble(str2);
            }
            catch
            {
                errorProvider1.SetError(txtX, "请输入m为单位的高斯平面X坐标！");
            }
            string str3 = txtCentre.Text.Trim();
            if (str3 == string.Empty)
            {
                errorProvider1.SetError(txtLat, "请输入中央经线！");
            }
            double longitude0 = 0;
            try
            {
                longitude0 = Convert.ToDouble(str3);
            }
            catch
            {
                errorProvider1.SetError(txtCentre, "请输入度为单位的中央经线！");
            }
            List<ProjectionParameter> pInfo = new List<ProjectionParameter>();
            pInfo.Add(new ProjectionParameter("latitude_of_origin", 0));
            pInfo.Add(new ProjectionParameter("central_meridian", longitude0));
            pInfo.Add(new ProjectionParameter("scale_factor", 1));
            pInfo.Add(new ProjectionParameter("false_easting", 500000));
            pInfo.Add(new ProjectionParameter("false_northing", 0));

            Projection proj = new Projection("Gauss_Kruger", pInfo, "", "EPSG", 0, String.Empty, String.Empty, String.Empty);
            System.Collections.Generic.List<AxisInfo> axes = new List<AxisInfo>();
            axes.Add(new AxisInfo("East", AxisOrientationEnum.East));
            axes.Add(new AxisInfo("North", AxisOrientationEnum.North));
            ISpatialReferenceFactory srf = new SpatialReferenceEnvironmentClass();
            IHorizontalDatum dataum = srf.CreateDatum((int)RgSRDatumType.RgSRDatum_Beijing1954);
            ICoordinateSystem gauss = new ProjectedCoordinateSystem(dataum, coord, ProjNet.CoordinateSystems.LinearUnit.Metre, proj, axes, "CustomGauss", "EPSG", 0, String.Empty, "", string.Empty);
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(coord, gauss);
            trans.MathTransform.Invert();//启用反算
            double[] pGaussBL = trans.MathTransform.Transform(new double[] { dgaussY, dgaussX });

            txtLon.Text = string.Format("{0}", pGaussBL[0]);
            txtLat.Text = string.Format("{0}", pGaussBL[1]);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
