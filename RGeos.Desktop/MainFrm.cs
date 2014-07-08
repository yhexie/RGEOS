using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.Controls;
using RGeos.Core;
using RgPoint = RGeos.Geometries.RgPoint;
using RGeos.Plugins;
using RGeos.Geometries;
using RGeos.Carto;
using RGeos.Display;

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
            mMapControl.Units = RgeosUnits.RgMillimeters;
            mMapControl.BackColor = Color.Black;
            this.panel1.Controls.Add(mMapControl);
            mMapControl.SetCenter(new RgPoint(0, 0));//设置基准点
            mTimer.Interval = 100;
            mTimer.Tick += new EventHandler(mTimer_Tick);
            mMapControl.MouseMove += new MouseEventHandler(mMapControl_MouseMove);
            mTimer.Start();
        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            switch (mMapControl.Units)
            {
                case RgeosUnits.RgMeters:
                    labCoordinate.Text = string.Format("{0}, {1}米", x, y);
                    break;
                case RgeosUnits.RgInches:
                    labCoordinate.Text = string.Format("{0}, {1}英寸", x, y);
                    break;
                case RgeosUnits.RgKilometers:
                    labCoordinate.Text = string.Format("{0}, {1}千米", x, y);
                    break;
                case RgeosUnits.RgCentimeters:
                    labCoordinate.Text = string.Format("{0}, {1}厘米", x, y);
                    break;
                case RgeosUnits.RgMillimeters:
                    labCoordinate.Text = string.Format("{0}, {1}毫米", x, y);
                    break;
                default:
                    labCoordinate.Text = string.Format("{0}, {1}未知", x, y);
                    break;
            }

        }
        double x, y;
        void mMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            RgPoint pt = mMapControl.ScreenDisplay.DisplayTransformation.ToUnit(new PointF(e.X, e.Y));
            x = Math.Round(pt.X, 3);
            y = Math.Round(pt.Y, 3);
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

        private void tspOpenRaster_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "打开影像数据...";
            dlg.Filter = "Img格式(*.img)|*.img|GeoTiff格式(*.tif)|*.tif";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filename = dlg.FileName;
                RasterLayer layer = new RasterLayer("VirtualRasterTable", filename);
                RgPoint center = (layer.Envelope.BottomLeft + layer.Envelope.TopRight) / 2;
                mMapControl.SetCenter(center);

                (mMapControl.ScreenDisplay as ScreenDisplay).UpdateWindow();
                mMapControl.Map.AddLayer(layer);
            }
        }

        private void tspCoordTrans_Click(object sender, EventArgs e)
        {
            FrmGaussProj frmGauss = new FrmGaussProj();
            frmGauss.StartPosition = FormStartPosition.CenterParent;
            frmGauss.ShowDialog();
        }
    }
}
