using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Core;
using RGeos.Controls;
using RGeos.Carto;

namespace RGeos.Plugins
{
    public class UnSelectCommand : RBaseCommand
    {
        public override string Name { get; set; }
        private RgMapControl mMapCtrl = null;
        public override void OnCreate(HookHelper hook)
        {
            Name = "取消选择";
            mMapCtrl = hook.MapControl as RgMapControl;
        }

        public override void OnClick()
        {
            IMap mMap = mMapCtrl.Map;
            ISelection mSelection = (mMap as Map).Selection;
            for (int i = 0; i < mSelection.SelectedFeatures.Count; i++)
            {
                Feature feat = mSelection.SelectedFeatures[i];
                feat.IsSelected = false;
            }
            mMapCtrl.Refresh();
        }

        public override void OnMouseMove(int x, int y)
        {

        }

        public override void OnMouseDown(int x, int y, System.Windows.Forms.MouseEventArgs e)
        {

        }

        public override void OnMouseUp(int x, int y)
        {

        }
    }
}
