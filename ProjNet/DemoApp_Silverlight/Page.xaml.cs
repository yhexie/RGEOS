using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace ProjNet.Silverlight.TestApplication
{
	public partial class Page : UserControl
	{
		public Page()
		{
			InitializeComponent();
		}

		private void map_MouseMove(object sender, MouseEventArgs e)
		{
			Point p = e.GetPosition(map);
			double scale = 360 / map.ActualWidth;
			Point pGeo = new Point(p.X * scale - 180, 90 - p.Y * scale);
			locationLong.Text = string.Format("{0}", pGeo.X);
			locationLat.Text = string.Format("{0}", pGeo.Y);

			//Transform to UTM
			CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
			ICoordinateSystem wgs84geo = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
			int zone = (int)Math.Ceiling((pGeo.X + 180) / 6);
			ICoordinateSystem utm = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WGS84_UTM(zone, pGeo.Y > 0);
			ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84geo, utm);
			Point pUtm = trans.MathTransform.Transform(pGeo);
			locationX.Text = string.Format("N{0}", pUtm.Y);
			locationY.Text = string.Format("E{0}", pUtm.X);
			locationZone.Text = string.Format("Zone {0}{1}", zone, pGeo.Y > 0 ? 'N' : 'S');
		}
	}
}
