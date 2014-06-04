using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.Controls;
using RGeos.PluginEngine;
using RGeos.Core;
using RgPoint = RGeos.Geometries.RgPoint;
using RGeos.Plugins;
using RGeos.Geometries;
using RGeos.Carto;

namespace RGeos.Desktop
{
    public partial class MainFrm : Form
    {
        RgMapControl mMapControl = null;
        Timer mTimer = new Timer();
        public MainFrm()
        {
            InitializeComponent();
            mMapControl = new RgMapControl();
            mMapControl.Dock = DockStyle.Fill;

            mMapControl.SetCenter(new RgPoint(0, 0));//设置基准点
            mMapControl.BackColor = Color.Black;
            this.panel1.Controls.Add(mMapControl);

            mTimer.Interval = 100;
            mTimer.Tick += new EventHandler(mTimer_Tick);
            mMapControl.MouseMove += new MouseEventHandler(mMapControl_MouseMove);
            mTimer.Start();
        }
        public const double MillmeteresPerInch = 25.4;
        void mTimer_Tick(object sender, EventArgs e)
        {
            labCoordinate.Text = string.Format("{0}, {1}毫米", x, y);
        }
        double x, y;
        void mMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            RgPoint pt = mMapControl.ScreenDisplay.DisplayTransformation.ToUnit(new PointF(e.X, e.Y));
            x = Math.Round(pt.X * MillmeteresPerInch, 3);
            y = Math.Round(pt.Y * MillmeteresPerInch, 3);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }

        private void tspPan_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new PanTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void tspDrawLine_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawPolylineTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void tspDrawPolygon_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawPolygonTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void tspNewLayer_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            IMapControl2 mapCtrl = mHook.MapControl as IMapControl2;
            RGeos.Carto.IMap map = mapCtrl.Map;
            FrmNewFeatureClass newFeatCls = new FrmNewFeatureClass();
            if (newFeatCls.ShowDialog() == DialogResult.OK)
            {
                RGeos.Carto.ILayer layer = new RGeos.Carto.FetureLayer();
                layer.Name = newFeatCls.FeatureName;
                (layer as RGeos.Carto.FetureLayer).ShapeType = newFeatCls.ShapeType;
                map.AddLayer(layer);
                mapCtrl.Refresh();
            }
        }
        private void tspSelect_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new SelectTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void tspClear_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new UnSelectCommand();
            pCmd.OnCreate(mHook);
            pCmd.OnClick();
        }

        private void tspDrawPoint_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawPointTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void tspLayerInfo_Click(object sender, EventArgs e)
        {
            FrmLayers frmLyrs = new FrmLayers(mMapControl.Map as Map);
            frmLyrs.ShowDialog();
        }

        private void tspAbout_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }

        private void tspSnap_Click(object sender, EventArgs e)
        {
            if (tspSnap.Checked == true)
            {
                tspSnap.Checked = false;
                mMapControl.RunningSnapsEnabled = false;
            }
            else
            {
                tspSnap.Checked = true;
                mMapControl.RunningSnapsEnabled = true;
            }
        }

        private void tspImportXY_Click(object sender, EventArgs e)
        {
            FrmImportXY frmImportXY = new FrmImportXY();
            frmImportXY.StartPosition = FormStartPosition.CenterParent;
            frmImportXY.ShowDialog();
        }

        private void tspCreateGrid_Click(object sender, EventArgs e)
        {
            FrmGridLineOrPoint frmGridLineOrPoint = new FrmGridLineOrPoint();
            frmGridLineOrPoint.StartPosition = FormStartPosition.CenterParent;
            frmGridLineOrPoint.Show(this);
        }






    }
}
