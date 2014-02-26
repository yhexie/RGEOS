using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.PluginEngine;
using System.Drawing;
using RGeos.Geometry;
namespace RGeos.Plugins
{
    class DrawPointTool : RBaseCommand
    {
        UcMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        IScreenDisplay mScreenDisplay = null;
        public override string Name { get; set; }

        public override void OnCreate(HookHelper hook)
        {
            Name = "绘制点";
            mMapCtrl = hook.MapControl;
            mScreenDisplay = mMapCtrl.mScreenDisplay;
        }

        public override void OnClick()
        {

        }

        public override void OnMouseMove(int x, int y)
        {


        }
        int n = 0;
        public override void OnMouseDown(int x, int y)
        {

            RPoint P1 = new RPoint(x, y, 0);
            PointF p2 = new PointF((float)P1.X, (float)P1.Y);
            mScreenDisplay.DrawPoint(Pens.Red, P1);

        }
        public override void OnMouseUp(int x, int y)
        {
            Color colr = Color.Red;

            //Brush brush = new SolidBrush(colr);
            //Pen pen = new Pen(brush,1.0f);


        }
    }
}
