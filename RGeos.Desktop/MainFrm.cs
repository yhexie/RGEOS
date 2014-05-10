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
using RGeos.Core.PluginEngine;
using RgPoint = RGeos.Geometries.Point;
using RGeos.Plugins;

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
    }
}
