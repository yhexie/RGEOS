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
    public partial class Form1 : Form
    {
        UcMapControl mMapControl = null;
        public Form1()
        {
            InitializeComponent();
            mMapControl = new UcMapControl();
            mMapControl.Dock = DockStyle.Fill;
          
            mMapControl.SetCenter(new RPoint(0, 0, 0));
            this.Controls.Add(mMapControl);

            HookHelper mHook = HookHelper.Instance();
            mHook.MapControl = mMapControl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand pCmd = new DrawPolylineTool();
            pCmd.OnCreate(mHook);
            mHook.MapControl.CurrentTool = pCmd as ITool;
        }

    }
}
