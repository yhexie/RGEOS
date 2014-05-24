using RGeos.Controls;
using RGeos.Display;
using RgPoint = RGeos.Geometries.Point;
using RGeos.Carto;
using RGeos.Core;

namespace RGeos.Plugins
{
    class DrawPointTool : RBaseCommand
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
            Name = "绘制点";
            mMapCtrl = hook.MapControl as RgMapControl;
            mScreenDisplay = mMapCtrl.ScreenDisplay;
        }

        public override void OnClick()
        {

        }

        public override void OnMouseMove(int x, int y)
        {

        }

        public override void OnMouseDown(int x, int y, System.Windows.Forms.MouseEventArgs e)
        {
            RgPoint P1 = mScreenDisplay.DisplayTransformation.ToUnit(new System.Drawing.PointF(x, y));
            Carto.FetureLayer featurelyr = mMapCtrl.Map.CurrentLayer as Carto.FetureLayer;
            if (featurelyr != null && featurelyr.ShapeType == RgEnumShapeType.RgPoint)
            {
                featurelyr.mGeometries.Add(P1);
            }
            mMapCtrl.Refresh();
        }

        public override void OnMouseUp(int x, int y)
        {

        }
    }
}
