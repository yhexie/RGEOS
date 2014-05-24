using System.Windows.Forms;
using System.Drawing;
using RGeos.Controls;
using RGeos.Core;
using RGeos.Display;
namespace RGeos.Plugins
{
    public class PanTool : RBaseCommand
    {
        Cursor mCursor = null;
        RgMapControl mMapCtrl = null;
        public IDisplayFeedback DrawPhase = null;
        public override string Name { get; set; }
        public override void OnCreate(HookHelper hook)
        {
            Name = "漫游";
            mMapCtrl = hook.MapControl as RgMapControl;
            mScreenDisplay = mMapCtrl.ScreenDisplay;
        }
        RGeos.Display.IScreenDisplay mScreenDisplay = null;

        public override void OnClick()
        {
            mCursor = mMapCtrl.Cursor;
        }
        public override void OnMouseMove(int x, int y)
        {
            mScreenDisplay.PanMoveTo(new PointF(x, y));
            mMapCtrl.Refresh();
        }

        public override void OnMouseDown(int x, int y, MouseEventArgs e)
        {
            mScreenDisplay.PanStart(new PointF(x, y));
            mMapCtrl.Cursor = Cursors.Hand;
        }
        public override void OnMouseUp(int x, int y)
        {
            mScreenDisplay.PanStop();
            mMapCtrl.Refresh();
            mMapCtrl.Cursor = mCursor;
        }
    }
}
