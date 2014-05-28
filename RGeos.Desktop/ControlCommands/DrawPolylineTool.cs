using System;
using RGeos.Controls;
using RGeos.Display;
using System.Drawing;
using System.Windows.Forms;
using RGeos.Geometries;
using RgPoint = RGeos.Geometries.RgPoint;
using RGeos.Carto;
using RGeos.Core;

namespace RGeos.Plugins
{
    class DrawPolylineTool : RBaseCommand
    {
        RgMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        public override string Name { get; set; }
        public override void OnCreate(HookHelper hook)
        {
            Name = "绘制折线";
            mMapCtrl = hook.MapControl as RgMapControl;
            mScreenDisplay = mMapCtrl.ScreenDisplay;
            mScreenDisplayDraw = mScreenDisplay as IScreenDisplayDraw;
        }
        IScreenDisplay mScreenDisplay = null;
        IScreenDisplayDraw mScreenDisplayDraw = null;
        LineString line = new LineString();
        public override void OnClick()
        {

        }
        int n = 0;
        public override void OnMouseMove(int x, int y)
        {

            if (n < 1)
            {

            }
            else
            {
                lastPoint1 = mScreenDisplay.DisplayTransformation.ToScreen(lastPoint1Unit);
                double xmin = Math.Min(lastPoint1.X, lastPoint2.X);
                double ymin = Math.Min(lastPoint1.Y, lastPoint2.Y);
                double w = Math.Abs(lastPoint1.X - lastPoint2.X);
                double h = Math.Abs(lastPoint1.Y - lastPoint2.Y);
                Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                invalidaterect.Inflate(2, 2);
                //擦除上次的范围
                (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);
                lastPoint2 = new PointF(x, y);
                RgPoint p1 = lastPoint1Unit;
                RgPoint p2 = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                mScreenDisplay.StartDrawing(mMapCtrl.CreateGraphics(), 1);
                mScreenDisplayDraw.DrawLine(p1, p2, Pens.Blue);
                mScreenDisplay.FinishDrawing();
            }
        }
        PointF lastPoint1 = PointF.Empty;
        RgPoint lastPoint1Unit = null;
        PointF lastPoint2 = PointF.Empty;
        public override void OnMouseDown(int x, int y, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                n++;
                if (n <= 1)
                {
                    line = new LineString();
                    RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                    line.Vertices.Add(pt);
                    lastPoint1Unit = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                    lastPoint2 = new PointF(x, y);
                }
                else
                {
                    RgPoint ptNext = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                    line.Vertices.Add(ptNext);
                    lastPoint1Unit = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));

                    lastPoint2 = new PointF(x, y);
                    mScreenDisplay.NewObject = line;
                    mMapCtrl.Refresh();
                }
            }
            else
            {
                Carto.FetureLayer featurelyr = mMapCtrl.Map.CurrentLayer as Carto.FetureLayer;
                if (featurelyr != null && featurelyr.ShapeType == RgEnumShapeType.RgLineString)
                {
                    featurelyr.AddFeature(line);
                }
                n = 0;
                mScreenDisplay.NewObject = null;
                mMapCtrl.Refresh();
            }
        }
        public override void OnMouseUp(int x, int y)
        {
            Color colr = Color.Red;
        }
    }
}
