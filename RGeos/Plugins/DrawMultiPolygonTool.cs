using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.PluginEngine;
using System.Drawing;
using RGeos.Geometry;
using SharpMap.Geometries;
namespace RGeos.Plugins
{
    public class DrawMultiPolygonTool : RBaseCommand
    {
        UcMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        IScreenDisplay mScreenDisplay = null;
        Polygon polygon = new Polygon();
        Polygon tempPolygon = new Polygon();
        public override string Name { get; set; }
        public override void OnCreate(HookHelper hook)
        {
            Name = "绘制多边形";
            mMapCtrl = hook.MapControl;
            mScreenDisplay = mMapCtrl.mScreenDisplay;
        }

        public override void OnClick()
        {

        }
        IList<SharpMap.Geometries.Point> tmpVertices;
        int j = 0;
        public override void OnMouseMove(int x, int y)
        {
            if (n == 1)
            {

            }
            if (n == 2)
            {
                j++;
                tmpVertices = new List<SharpMap.Geometries.Point>();
                SharpMap.Geometries.Point P0 = vertices[0];
                SharpMap.Geometries.Point P1 = vertices[1];
                SharpMap.Geometries.Point P2 = new SharpMap.Geometries.Point(x, y);
                tmpVertices.Add(P0);
                tmpVertices.Add(P1);
                tmpVertices.Add(P2);
                LinearRing tmpLine = new LinearRing(tmpVertices);
                tempPolygon.ExteriorRing = tmpLine;

                if (j == 1)
                {
                    BoundingBox box = tempPolygon.GetBoundingBox();
                    double xmin = box.Left;
                    double ymin = box.Bottom;
                    double w = box.Width;
                    double h = box.Height;
                    Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                    invalidaterect.Inflate(2, 2);
                    (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);
                    j = 0;
                }

                //LinearRing tmpLine = new LinearRing(tmpVertices);
                //tempPolygon.ExteriorRing = tmpLine;
                SolidBrush brush = new SolidBrush(Color.Blue);
                Pen pen = new Pen(brush);
                mScreenDisplay.DrawPolygon(tempPolygon, brush, pen, false);
            }
            else if (n > 2)
            {
                BoundingBox box = tempPolygon.GetBoundingBox();
                double xmin = box.Left;
                double ymin = box.Bottom;
                double w = box.Width;
                double h = box.Height;
                Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                invalidaterect.Inflate(2, 2);

                (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);

                tmpVertices = new List<SharpMap.Geometries.Point>();
                SharpMap.Geometries.Point P0 = vertices[0];
                SharpMap.Geometries.Point P1 = vertices[n - 1];
                SharpMap.Geometries.Point P2 = new SharpMap.Geometries.Point(x, y);
                tmpVertices.Add(P0);
                tmpVertices.Add(P1);
                tmpVertices.Add(P2);
                LinearRing tmpLine = new LinearRing(tmpVertices);
                tempPolygon.ExteriorRing = tmpLine;
                SolidBrush brush = new SolidBrush(Color.Blue);
                SolidBrush brush2 = new SolidBrush(Color.Pink);
                Pen pen = new Pen(brush2);
                mScreenDisplay.DrawPolygon(tempPolygon, brush, pen, false);
                //PointF p1 = new PointF((float)line.P0.X, (float)line.P0.Y);
                //PointF p2 = new PointF(x, y);

                //mScreenDisplay.DrawPolygon(Pens.Blue, p1, p2);
                //line.P1 = new RPoint(x, y, 0);
            }
        }
        int n = 0;
        LinearRing line = null;
        IList<SharpMap.Geometries.Point> vertices = new List<SharpMap.Geometries.Point>();
        public override void OnMouseDown(int x, int y, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                n++;
                if (n == 1)
                {
                    SharpMap.Geometries.Point P0 = new SharpMap.Geometries.Point(x, y);
                    SharpMap.Geometries.Point P1 = new SharpMap.Geometries.Point(x, y);
                    SharpMap.Geometries.Point P2 = new SharpMap.Geometries.Point(x, y);
                    vertices.Add(P0);
                    vertices.Add(P1);
                    vertices.Add(P2);
                    line = new LinearRing(vertices);
                }
                else if (n == 2)
                {
                    vertices[1] = new SharpMap.Geometries.Point(x, y);
                    PointF p1 = new PointF((float)vertices[0].X, (float)vertices[0].Y);
                    PointF p2 = new PointF(x, y);
                    mScreenDisplay.DrawPolyline(Pens.Blue, p1, p2);
                    //n = 0;
                }
                else if (n == 3)
                {
                    vertices[2] = new SharpMap.Geometries.Point(x, y);
                    polygon.ExteriorRing = line;
                    if (line != null)
                    {
                        SolidBrush brush = new SolidBrush(Color.Blue);
                        Pen pen = new Pen(brush);
                        //  mScreenDisplay.DrawPolygon(polygon, brush, pen, false);
                        mScreenDisplay.NewObject = polygon;
                    }
                }
                else
                {
                    SharpMap.Geometries.Point P4 = new SharpMap.Geometries.Point(x, y);
                    vertices.Add(P4);
                    SolidBrush brush = new SolidBrush(Color.Blue);
                    Pen pen = new Pen(brush);
                    //    mScreenDisplay.DrawPolygon(polygon, brush, pen, false);
                    mScreenDisplay.NewObject = polygon;

                }
                mMapCtrl.Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                mMapCtrl.Refresh();
                vertices = new List<SharpMap.Geometries.Point>();
                polygon = new Polygon();
                tempPolygon = new Polygon();
                n = 0;
            }
        }
        public override void OnMouseUp(int x, int y)
        {
            Color colr = Color.Red;

            //Brush brush = new SolidBrush(colr);
            //Pen pen = new Pen(brush,1.0f);


        }
    }
}
