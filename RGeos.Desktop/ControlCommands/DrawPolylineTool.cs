using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Controls;
using RGeos.PluginEngine;
using RGeos.Core.PluginEngine;
using RGeos.Display;
using System.Drawing;
using System.Windows.Forms;
using RGeos.Geometries;
using RgPoint = RGeos.Geometries.Point;
namespace RGeos.Desktop
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
                double xmin = Math.Min(lastPoint1.X, lastPoint2.X);
                double ymin = Math.Min(lastPoint1.Y, lastPoint2.Y);
                double w = Math.Abs(lastPoint1.X - lastPoint2.X);
                double h = Math.Abs(lastPoint1.Y - lastPoint2.Y);
                Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                invalidaterect.Inflate(2, 2);
                //擦除上次的范围
                (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);
                lastPoint2 = new PointF(x, y);
                RgPoint p1 = mScreenDisplay.DisplayTransformation.ToUnit(new PointF((float)lastPoint1.X, (float)lastPoint1.Y));
                RgPoint p2 = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                mScreenDisplay.StartDrawing(mMapCtrl.CreateGraphics(), 1);
                mScreenDisplayDraw.DrawLine(p1, p2, Pens.Blue);
                mScreenDisplay.FinishDrawing();
            }
        }
        PointF lastPoint1 = PointF.Empty;
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
                    lastPoint1 = new PointF(x, y);
                    lastPoint2 = new PointF(x, y);
                }
                else
                {
                    RgPoint ptNext = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(x, y));
                    line.Vertices.Add(ptNext);
                    //mScreenDisplay.StartDrawing(mMapCtrl.CreateGraphics(),1);
                    //mScreenDisplayDraw.DrawLineString(line, Pens.Blue);
                    //mScreenDisplay.FinishDrawing();
                    lastPoint1 = new PointF(x, y);
                    lastPoint2 = new PointF(x, y);
                    mScreenDisplay.NewObject = line;
                    mMapCtrl.Refresh();
                }
            }
            else
            {
                n = 0;
                mScreenDisplay.NewObject = null;
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
