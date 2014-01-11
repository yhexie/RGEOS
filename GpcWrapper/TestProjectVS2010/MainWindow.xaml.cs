using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GpcTest
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		System.Drawing.FontFamily fontFamily = new System.Drawing.FontFamily("Times New Roman");
		float flatness = 1.0F;

		public MainWindow()
		{
			InitializeComponent();
			panel.Paint += new System.Windows.Forms.PaintEventHandler(panel_Paint);
		}

		void panel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			GraphicsPath p = new GraphicsPath();

			// draw A
            p.AddString("A", fontFamily, 0, 250, new System.Drawing.Point(0, 0), new StringFormat());
			p.Flatten(new System.Drawing.Drawing2D.Matrix(), flatness);
			e.Graphics.FillPath(System.Drawing.Brushes.DarkGray, p);
			e.Graphics.DrawPath(new System.Drawing.Pen(System.Drawing.Color.Black, 2.0F), p);
			// draw B
            p.AddString("B", fontFamily, 0, 250, new System.Drawing.Point(0, 250), new StringFormat());
			p.Flatten(new System.Drawing.Drawing2D.Matrix(), flatness);
			e.Graphics.FillPath(System.Drawing.Brushes.DarkGray, p);
			e.Graphics.DrawPath(new System.Drawing.Pen(System.Drawing.Color.Black, 2.0F), p);

			// create polygonA
			p = new GraphicsPath();
            p.AddString("A", fontFamily, 0, 250, new System.Drawing.Point(0, 0), new StringFormat());
			p.Flatten(new System.Drawing.Drawing2D.Matrix(), flatness);
			GpcWrapper.Polygon polygonA = new GpcWrapper.Polygon(p);

			// create polygonB
			p = new GraphicsPath();
            p.AddString("B", fontFamily, 0, 250, new System.Drawing.Point(0, 0), new StringFormat());
			p.Flatten(new System.Drawing.Drawing2D.Matrix(), flatness);
			GpcWrapper.Polygon polygonB = new GpcWrapper.Polygon(p);

			// Save and Load
			polygonA.Save("A.plg", true);
			GpcWrapper.Polygon loadedPolygon = GpcWrapper.Polygon.FromFile("A.plg", true);
            polygonB.Save("B.plg", true);
            GpcWrapper.Polygon loadedPolygon2 = GpcWrapper.Polygon.FromFile("B.plg", true);

			// create Tristrip
			GpcWrapper.Tristrip tristrip = loadedPolygon.ToTristrip();
            GpcWrapper.Tristrip tristrip2 = loadedPolygon2.ToTristrip();
			for (int i = 0; i < tristrip.NofStrips; i++)
			{
				GpcWrapper.VertexList vertexList = tristrip.Strip[i];
				GraphicsPath path = vertexList.TristripToGraphicsPath();
				System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
				m.Translate(600, 0);
				path.Transform(m);
				e.Graphics.FillPath(System.Drawing.Brushes.DarkGray, path);
				e.Graphics.DrawPath(Pens.Black, path);
			}
            for (int i = 0; i < tristrip2.NofStrips; i++)
            {
                GpcWrapper.VertexList vertexList = tristrip2.Strip[i];
                GraphicsPath path = vertexList.TristripToGraphicsPath();
                System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
                m.Translate(600, 250);
                path.Transform(m);
                e.Graphics.FillPath(System.Drawing.Brushes.DarkGray, path);
                e.Graphics.DrawPath(Pens.Black, path);
            }
			PointF[] upperLeftCorner = new PointF[] { new PointF(200, 0), new PointF(200, 250), new PointF(400, 0), new PointF(400, 250) };

			int position = 0;
            int iName=0;
			foreach (GpcWrapper.GpcOperation operation in Enum.GetValues(typeof(GpcWrapper.GpcOperation)))
			{
                iName++;
				GpcWrapper.Polygon polygon = polygonA.Clip(operation, polygonB);
                polygon.Save(iName.ToString()+".plg", true);
				GraphicsPath path = polygon.ToGraphicsPath();
				System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
				m.Translate(upperLeftCorner[position].X, upperLeftCorner[position].Y);
				path.Transform(m);
				e.Graphics.FillPath(System.Drawing.Brushes.DarkGray, path);
				e.Graphics.DrawPath(Pens.Black, path);
				position++;
			}
		}
	}
}
