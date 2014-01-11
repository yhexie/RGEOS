using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.PluginEngine;
using System.Drawing;
namespace RGeos.Plugins
{
    public class DrawPolylineTool : RBaseCommand
    {
        UcMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        public override string Name { get; set; }
        public override void OnCreate(HookHelper hook)
        {
            Name = "绘制多义线";
            mMapCtrl = hook.MapControl;
            mScreenDisplay = mMapCtrl.mScreenDisplay;
        }
        IScreenDisplay mScreenDisplay = null;
        public override void OnClick()
        {

        }
        int n = 0;
        PointF p1;
        PointF Point0;
        public override void OnMouseMove(int x, int y)
        {

            if (n < 1)
            {

            }
            else
            {
                double xmin = Math.Min(p1.X, Point0.X);
                double ymin = Math.Min(p1.Y, Point0.Y);
                double w = Math.Abs(p1.X - Point0.X);
                double h = Math.Abs(p1.Y - Point0.Y);
                Rectangle invalidaterect = new Rectangle((int)xmin, (int)ymin, (int)w, (int)h);
                invalidaterect.Inflate(2, 2);

                (mScreenDisplay as ScreenDisplay).RepaintStatic(invalidaterect);
                PointF p2 = new PointF(x, y);

                mScreenDisplay.DrawPolyline(Pens.Blue, p1, p2);
                Point0 = p2;
            }
        }
        ///// <summary>
        ///// 在无效区域绘制对象
        ///// </summary>
        ///// <param name="obj"></param>
        //void RepaintObject(IDrawObject obj)
        //{
        //    if (obj == null)
        //        return;
        //    CanvasWrapper dc = new CanvasWrapper(this, Graphics.FromHwnd(Handle), ClientRectangle);
        //    RectangleF invalidaterect = ScreenUtils.ConvertRect(ScreenUtils.ToScreenNormalized(dc, obj.GetBoundingRect(dc)));
        //    obj.Draw(dc, invalidaterect);
        //    dc.Graphics.Dispose();
        //    dc.Dispose();
        //}
        public override void OnMouseDown(int x, int y)
        {
            n++;
            if (n <= 1)
            {
                p1 = new PointF(x, y);
                Point0 = new PointF(x, y);
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
