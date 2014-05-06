using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.PluginEngine;
using System.Drawing;
using RGeos.Geometry;
namespace RGeos.Plugins
{
    public class DrawLineTool : RBaseCommand
    {
        UcMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        IScreenDisplay mScreenDisplay = null;
        RLine line = new RLine();
        public override string Name { get; set; }
        public override void OnCreate(HookHelper hook)
        {
            Name = "绘制线段";
            mMapCtrl = hook.MapControl;
            mScreenDisplay = mMapCtrl.mScreenDisplay;
        }

        public override void OnClick()
        {

        }

        public override void OnMouseMove(int x, int y)
        {

            if (n < 1)
            {

            }
            else
            {
                double xmin = line.Envelop.Left;
                double ymin = line.Envelop.Lower;
                double w = line.Envelop.Width;
                double h = line.Envelop.Height;
                Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                invalidaterect.Inflate(2, 2);

                (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);
                PointF p1 = new PointF((float)line.P0.X, (float)line.P0.Y);
                PointF p2 = new PointF(x, y);

                mScreenDisplay.DrawPolyline(Pens.Blue, p1, p2);
                line.P1 = new RPoint(x, y, 0);
            }
        }
        int n = 0;
        public override void OnMouseDown(int x, int y, MouseEventArgs e)
        {
            n++;
            if (n <= 1)
            {
                line = new RLine();
                line.P0 = new RPoint(x, y, 0);
                line.P1 = new RPoint(x, y, 0);
            }
            else
            {
                line.P1 = new RPoint(x, y, 0);
                PointF p1 = new PointF((float)line.P0.X, (float)line.P0.Y);
                PointF p2 = new PointF((float)line.P1.X, (float)line.P1.Y);
                mScreenDisplay.DrawPolyline(Pens.Blue, p1, p2);
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
