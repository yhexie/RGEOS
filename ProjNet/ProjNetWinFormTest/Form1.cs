using System;
using System.Drawing;
using System.Windows.Forms;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
namespace ProjNetWinFormTest
{
    public partial class FrmUTM : Form
    {
        public FrmUTM()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X,e.Y);
            double scale = (double)360 / pictureBox1.Width;
            Point pGeo = new Point((int)(p.X * scale - 180),(int)( 90 - p.Y * scale));
            locationLong.Text = string.Format("{0}", pGeo.X);
            locationLat.Text = string.Format("{0}", pGeo.Y);

            //Transform to UTM
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            ICoordinateSystem wgs84geo = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
            int zone = (int)Math.Ceiling((double)(pGeo.X + 180) / 6);
            ICoordinateSystem utm = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WGS84_UTM(zone, pGeo.Y > 0);
            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84geo, utm);

           double[]  pUtm = trans.MathTransform.Transform(new double[]{pGeo.X,pGeo.Y});
            locationX.Text = string.Format("N{0}", pUtm[1]);
            locationY.Text = string.Format("E{0}", pUtm[0]);
            locationZone.Text = string.Format("Zone {0}{1}", zone, pGeo.Y > 0 ? 'N' : 'S');
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
