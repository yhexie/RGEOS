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
using RgPoint = RGeos.Geometries.Point;
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
            this.panel1.Controls.Add(mMapControl);

            HookHelper mHook = HookHelper.Instance();
            mHook.MapControl = mMapControl as IMapControl;
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
            //RGeos.Carto.ILayer layer = new RGeos.Carto.FetureLayer();
            //(layer as RGeos.Carto.FetureLayer).ShapeType = RgEnumShapeType.RgLineString;
            //LineString line = new LineString();
            //RgPoint pt = new RgPoint(0, 0);
            //RgPoint pt1 = new RgPoint(10 / MillmeteresPerInch, 0);
            //line.Vertices.Add(pt);
            //line.Vertices.Add(pt1);
            //(layer as RGeos.Carto.FetureLayer).mGeometries.Add(line);
            //LineString line1 = new LineString();
            //RgPoint pt11 = new RgPoint(0, 10 / MillmeteresPerInch);
            //RgPoint pt12 = new RgPoint(0, 0);
            //line1.Vertices.Add(pt11);
            //line1.Vertices.Add(pt12);
            //(layer as RGeos.Carto.FetureLayer).mGeometries.Add(line1);
            //map.AddLayer(layer);

            //RGeos.Carto.ILayer layerPolygon = new RGeos.Carto.FetureLayer();
            //(layerPolygon as RGeos.Carto.FetureLayer).ShapeType = RgEnumShapeType.RgPolygon;
            //map.AddLayer(layerPolygon);

            //RGeos.Carto.ILayer layerPolygon = new RGeos.Carto.FetureLayer();
            //(layerPolygon as RGeos.Carto.FetureLayer).ShapeType = RgEnumShapeType.RgPoint;
            //map.AddLayer(layerPolygon);
            //mapCtrl.Refresh();
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


    }
}
