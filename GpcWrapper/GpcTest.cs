namespace GpcTest
{
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GpcWrapper;


class Start
{
	public static void Main(string[] args)
	{
		Application.Run(new MyForm());
 	}
}

class MyForm : Form
{
	FontFamily fontFamily = new FontFamily( "Times New Roman" );
	float      flatness   = 1.0F;
	
	public MyForm()
	{
		Size                 = new Size( 850, 550 );
		Text                 = "GpcTest";
	}

	protected override void OnPaint( PaintEventArgs e )
	{
		GraphicsPath p = new GraphicsPath();
		
		// draw A
		p.AddString( "A", fontFamily, 0, 250, new Point( 0, 0 ), new StringFormat() );
		p.Flatten( new Matrix(), flatness );
		e.Graphics.FillPath( Brushes.DarkGray, p );
		e.Graphics.DrawPath( new Pen(Color.Black,2.0F), p );
		// draw B
		p.AddString( "B", fontFamily, 0, 250, new Point( 0, 250 ), new StringFormat() );
		p.Flatten( new Matrix(), flatness );
		e.Graphics.FillPath( Brushes.DarkGray, p );
		e.Graphics.DrawPath( new Pen(Color.Black,2.0F), p );

		// create polygonA
		p = new GraphicsPath();
		p.AddString( "A", fontFamily, 0, 250, new Point( 0, 0 ), new StringFormat() );
		p.Flatten( new Matrix(), flatness );
		Polygon polygonA = new Polygon( p );

		// create polygonB
		p = new GraphicsPath();
		p.AddString( "B", fontFamily, 0, 250, new Point( 0, 0 ), new StringFormat() );
		p.Flatten( new Matrix(), flatness );
		Polygon polygonB = new Polygon( p );
		
		// Save and Load
		polygonA.Save( "A.plg", true );
		Polygon loadedPolygon = Polygon.FromFile( "A.plg", true );

		// create Tristrip
		Tristrip tristrip = loadedPolygon.ToTristrip();
		for ( int i =0 ; i<tristrip.NofStrips ; i++ ) {
			VertexList vertexList = tristrip.Strip[i];
			GraphicsPath path = vertexList.TristripToGraphicsPath();
			Matrix m = new Matrix();
			m.Translate( 600, 0 );
			path.Transform( m );
			e.Graphics.FillPath( Brushes.DarkGray, path );
			e.Graphics.DrawPath( Pens.Black, path );
		}

		PointF[] upperLeftCorner= new PointF[]{ new PointF(200,0), new PointF(200,250), new PointF(400,0), new PointF(400,250) };

		int position = 0;
		foreach ( GpcOperation operation in Enum.GetValues( typeof(GpcOperation)) ) {
			Polygon polygon = polygonA.Clip( operation, polygonB );
			GraphicsPath path = polygon.ToGraphicsPath();
			Matrix m = new Matrix();
			m.Translate( upperLeftCorner[position].X, upperLeftCorner[position].Y );
			path.Transform( m );
			e.Graphics.FillPath( Brushes.DarkGray, path );
			e.Graphics.DrawPath( Pens.Black, path );
			position++;
		}
	}
}

}