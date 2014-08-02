using RGeos.Controls;
using RGeos.Display;
using RgPoint = RGeos.Geometries.RgPoint;
using RGeos.Carto;
using RGeos.Core;
using RGeos.Desktop.ControlCommands;
using System.Drawing;

namespace RGeos.Plugins
{
    public class PickUpPointTool : RBaseCommand, IPickUpPoint
    {
        private string mName = string.Empty;
        public override string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }
        RgMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        IScreenDisplay mScreenDisplay = null;
        public override void OnCreate(HookHelper hook)
        {
            Name = "拾取点";
            mMapCtrl = hook.MapControl as RgMapControl;
            mScreenDisplay = mMapCtrl.ScreenDisplay;
        }

        public override void OnClick()
        {

        }

        public override void OnMouseMove(int x, int y)
        {

        }
        RgPoint mPickedPoint = null;
        public override void OnMouseDown(int x, int y, System.Windows.Forms.MouseEventArgs e)
        {
            RgPoint P1 = mScreenDisplay.DisplayTransformation.ToUnit(new System.Drawing.PointF(x, y));
           
            Graphics g = Graphics.FromHwnd(mMapCtrl.Handle);
            mScreenDisplay.StartDrawing(g, 1);
            IScreenDisplayDraw mScreenDisplayDraw = mScreenDisplay as IScreenDisplayDraw;
            mScreenDisplayDraw.DrawPoint(P1, new System.Drawing.Pen(System.Drawing.Color.Yellow), new System.Drawing.SolidBrush(System.Drawing.Color.Yellow));
            mScreenDisplay.FinishDrawing();
            g.Dispose();
            if (PickUpFinishedEventHandler != null)
            {
                PickUpFinishedEventHandler(P1);
            }
            //mMapCtrl.Refresh();
            mPickedPoint = P1;
        }

        public override void OnMouseUp(int x, int y)
        {

        }

        public RgPoint Point
        {
            get
            {
                return mPickedPoint;
            }
            set
            {
                mPickedPoint = value;
            }
        }


        public event PickUpFinished PickUpFinishedEventHandler;
    }
}
