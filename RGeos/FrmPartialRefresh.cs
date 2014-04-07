using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.PluginEngine;
using RGeos.Geometry;
using RGeos.Plugins;

namespace RGeos
{
    public partial class FrmPartialRefresh : Form
    {
        UcMapControl mMapControl = null;
        public FrmPartialRefresh()
        {
            InitializeComponent();
            mMapControl = new UcMapControl();
            mMapControl.Dock = DockStyle.Fill;

            mMapControl.SetCenter(new RPoint(0, 0, 0));//设置基准点
            this.Controls.Add(mMapControl);

            HookHelper mHook = HookHelper.Instance();
            mHook.MapControl = mMapControl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float dx, dy;
            Graphics g = this.CreateGraphics();
            try
            {
                dx = g.DpiX;
                dy = g.DpiY;
            }
            finally
            {
                g.Dispose();
            }
            MessageBox.Show(string.Format("{0}/{1}", dx, dy));
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawPolylineTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawLineTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void btnDrawPoint_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawPointTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void btnDrawPolygon_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawMultiPolygonTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

        private void btnAddlayer_Click(object sender, EventArgs e)
        {

        }

        private void FrmPartialRefresh_Load(object sender, EventArgs e)
        {

        }

    }
}
