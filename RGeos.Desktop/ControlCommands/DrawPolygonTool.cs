using System.Collections.Generic;
using System.Windows.Forms;
using RGeos.PluginEngine;
using System.Drawing;
using RGeos.Display;
using RGeos.Controls;
using RGeos.Geometries;
using System;
using RGeos.Carto;
using RGeos.Core;
namespace RGeos.Plugins
{
    public class DrawPolygonTool : RBaseCommand
    {
        RgMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        IScreenDisplay mScreenDisplay = null;
        IScreenDisplayDraw mScreenDisplayDraw = null;
        Polygon polygon = new Polygon();
        Polygon tempPolygon = new Polygon();
        public override string Name { get; set; }
        public override void OnCreate(HookHelper hook)
        {
            Name = "绘制多边形";
            mMapCtrl = hook.MapControl as RgMapControl;
            mScreenDisplay = mMapCtrl.ScreenDisplay;
            mScreenDisplayDraw = mScreenDisplay as IScreenDisplayDraw;
        }

        public override void OnClick()
        {

        }
        IList<RGeos.Geometries.RgPoint> tmpVertices;
        int j = 0;
        public override void OnMouseMove(int x, int y)
        {
            if (n == 1)
            {

            }
            if (n == 2)
            {
                j++;
                tmpVertices = new List<RGeos.Geometries.RgPoint>();
                RGeos.Geometries.RgPoint P0 = vertices[0];
                RGeos.Geometries.RgPoint P1 = vertices[1];
                RGeos.Geometries.RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                RGeos.Geometries.RgPoint P2 = pt;
                tmpVertices.Add(P0);
                tmpVertices.Add(P1);
                tmpVertices.Add(P2);
                LinearRing tmpLine = new LinearRing(tmpVertices);
                tempPolygon.ExteriorRing = tmpLine;

                if (j == 1)
                {
                    BoundingBox box = tempPolygon.GetBoundingBox();
                    PointF lowLeft = mScreenDisplay.DisplayTransformation.ToScreen(box.TopLeft);
                    PointF topRight = mScreenDisplay.DisplayTransformation.ToScreen(box.BottomRight);
                    double xmin = lowLeft.X;
                    double ymin = lowLeft.Y;
                    double w = Math.Abs(topRight.X - lowLeft.X);
                    double h = Math.Abs(topRight.Y - lowLeft.Y);
                    Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                    invalidaterect.Inflate(2, 2);
                    (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);
                    j = 0;
                }
                SolidBrush brush = new SolidBrush(Color.Blue);
                Pen pen = new Pen(brush);
                mScreenDisplay.StartDrawing(mMapCtrl.CreateGraphics(), 1);
                mScreenDisplayDraw.DrawPolygon(tempPolygon, brush, pen, false);
                mScreenDisplay.FinishDrawing();
            }
            else if (n > 2)
            {
                BoundingBox box = tempPolygon.GetBoundingBox();
                PointF lowLeft = mScreenDisplay.DisplayTransformation.ToScreen(box.TopLeft);
                PointF topRight = mScreenDisplay.DisplayTransformation.ToScreen(box.BottomRight);
                double xmin = lowLeft.X;
                double ymin = lowLeft.Y;
                double w = Math.Abs(topRight.X - lowLeft.X);
                double h = Math.Abs(topRight.Y - lowLeft.Y);
                Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                invalidaterect.Inflate(2, 2);

                (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);

                tmpVertices = new List<RGeos.Geometries.RgPoint>();
                RGeos.Geometries.RgPoint P0 = vertices[0];
                RGeos.Geometries.RgPoint P1 = vertices[n - 1];
                RGeos.Geometries.RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                RGeos.Geometries.RgPoint P2 = pt;
                tmpVertices.Add(P0);
                tmpVertices.Add(P1);
                tmpVertices.Add(P2);
                LinearRing tmpLine = new LinearRing(tmpVertices);
                tempPolygon.ExteriorRing = tmpLine;
                SolidBrush brush = new SolidBrush(Color.Blue);
                SolidBrush brush2 = new SolidBrush(Color.Pink);
                Pen pen = new Pen(brush2);
                mScreenDisplay.StartDrawing(mMapCtrl.CreateGraphics(), 1);
                mScreenDisplayDraw.DrawPolygon(tempPolygon, brush, pen, false);
                mScreenDisplay.FinishDrawing();
            }
        }
        int n = 0;
        LinearRing line = null;
        IList<RGeos.Geometries.RgPoint> vertices = new List<RGeos.Geometries.RgPoint>();
        public override void OnMouseDown(int x, int y, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                n++;
                if (n == 1)
                {
                    RGeos.Geometries.RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                    RGeos.Geometries.RgPoint P0 = new RGeos.Geometries.RgPoint(pt.X, pt.Y);
                    RGeos.Geometries.RgPoint P1 = new RGeos.Geometries.RgPoint(pt.X, pt.Y);
                    RGeos.Geometries.RgPoint P2 = new RGeos.Geometries.RgPoint(pt.X, pt.Y);
                    vertices.Add(P0);
                    vertices.Add(P1);
                    vertices.Add(P2);
                    line = new LinearRing(vertices);
                }
                else if (n == 2)
                {

                    PointF p2 = new PointF(x, y);
                    RGeos.Geometries.RgPoint pt1 = vertices[0];
                    RGeos.Geometries.RgPoint pt2 = mScreenDisplay.DisplayTransformation.ToUnit(p2);
                    vertices[1] = pt2;
                    mScreenDisplay.StartDrawing(mMapCtrl.CreateGraphics(), 1);
                    mScreenDisplayDraw.DrawLine(pt1, pt2, Pens.Blue);
                    mScreenDisplay.FinishDrawing();
                    //n = 0;
                }
                else if (n == 3)
                {
                    PointF p1 = new PointF(x, y);
                    RGeos.Geometries.RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(p1);
                    vertices[2] = pt;
                    polygon.ExteriorRing = line;
                    if (line != null)
                    {
                        SolidBrush brush = new SolidBrush(Color.Blue);
                        Pen pen = new Pen(brush);
                        mScreenDisplay.NewObject = polygon;
                        mMapCtrl.Refresh();
                    }
                }
                else
                {
                    PointF p1 = new PointF(x, y);
                    RGeos.Geometries.RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(p1);
                    RGeos.Geometries.RgPoint P4 = pt;
                    vertices.Add(P4);
                    SolidBrush brush = new SolidBrush(Color.Blue);
                    Pen pen = new Pen(brush);
                    mScreenDisplay.NewObject = polygon;
                    mMapCtrl.Refresh();
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                Carto.FetureLayer featurelyr = mMapCtrl.Map.CurrentLayer as Carto.FetureLayer;
                if (featurelyr != null && featurelyr.ShapeType == RgEnumShapeType.RgPolygon)
                {
                    featurelyr.mGeometries.Add(polygon);
                }
               
                vertices = new List<RGeos.Geometries.RgPoint>();
                polygon = new Polygon();
                tempPolygon = new Polygon();
                mScreenDisplay.NewObject = null;
                n = 0;
                mMapCtrl.Refresh();
            }
        }
        public override void OnMouseUp(int x, int y)
        {
            Color colr = Color.Red;
        }
    }
}
