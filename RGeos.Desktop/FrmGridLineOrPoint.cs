using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.Core;
using RGeos.Plugins;
using RGeos.Desktop.ControlCommands;
using RGeos.Carto;
using RGeos.Controls;
using RGeos.Geometries;

namespace RGeos.Desktop
{
    public partial class FrmGridLineOrPoint : Form
    {
        public FrmGridLineOrPoint(IMapControl mapCtrl)
        {
            InitializeComponent();
            mMapCtrl = mapCtrl;
        }
        IMapControl mMapCtrl = null;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int row = 0;
            bool flag = int.TryParse(txtRows.Text, out row);
            int col = 0;
            bool flag1 = int.TryParse(txtCols.Text, out col);

            double rheight, rwidth;
            bool flag3 = double.TryParse(txtRowHeight.Text, out rheight);
            bool flag4 = double.TryParse(txtRowWidth.Text, out rwidth);
            if (!flag)
            {
                MessageBox.Show("请输入正确的行数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRows.Focus();
                return;
            }
            if (!flag1)
            {
                MessageBox.Show("请输入正确的列数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCols.Focus();
                return;
            }
            if (!flag3)
            {
                MessageBox.Show("请输入正确的行高", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRowHeight.Focus();
                return;
            }
            if (!flag4)
            {
                MessageBox.Show("请输入正确的行宽", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRowWidth.Focus();
                return;
            }
            IMapControl2 mapCtrl = mMapCtrl as IMapControl2;
            RGeos.Carto.IMap map = mapCtrl.Map;

            if (chkGridPoint.Checked)
            {
                RGeos.Carto.FetureLayer layer = new RGeos.Carto.FetureLayer();
                layer.Name = "GridPoint";
                layer.ShapeType = RgEnumShapeType.RgPoint;
                map.AddLayer(layer);

                double baseX;
                bool flag5 = double.TryParse(txtX.Text, out baseX);
                double baseY;
                bool flag6 = double.TryParse(txtY.Text, out baseY);

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        RgPoint pt = new RgPoint();
                        pt.X = baseX + i * rwidth;
                        pt.Y = baseY + j * rheight;
                        layer.AddFeature(pt);
                    }
                }
            }
            if (chkGridLine.Checked)
            {
                RGeos.Carto.FetureLayer layer = new RGeos.Carto.FetureLayer();
                layer.Name = "GridLine";
                layer.ShapeType = RgEnumShapeType.RgLineString;
                map.AddLayer(layer);

                double baseX;
                bool flag5 = double.TryParse(txtX.Text, out baseX);
                double baseY;
                bool flag6 = double.TryParse(txtY.Text, out baseY);

                RgPoint ptLowerLeft = new RgPoint();
                ptLowerLeft.X = baseX;
                ptLowerLeft.Y = baseY;
                RgPoint ptLowerRight = new RgPoint();
                ptLowerRight.X = baseX + col * rwidth;
                ptLowerRight.Y = baseY;

                RgPoint ptTopRight = new RgPoint();
                ptTopRight.X = baseX + col * rwidth;
                ptTopRight.Y = baseY + row * rheight;
                RgPoint ptTopLeft = new RgPoint();
                ptTopLeft.X = baseX;
                ptTopLeft.Y = baseY + row * rheight;


                List<RgPoint> ptBounds = new List<RgPoint>();
                ptBounds.Add(ptLowerLeft);
                ptBounds.Add(ptLowerRight);
                ptBounds.Add(ptTopRight);
                ptBounds.Add(ptTopLeft);
                ptBounds.Add(ptLowerLeft);
                LineString lineBounds = new LineString(ptBounds);
                layer.AddFeature(lineBounds);

                for (int i = 1; i < row; i++)
                {
                    RgPoint ptStart = new RgPoint();
                    ptStart.X = baseX;
                    ptStart.Y = baseY + i * rheight;
                    RgPoint ptEnd = new RgPoint();
                    ptEnd.X = baseX + col * rwidth;
                    ptEnd.Y = baseY + i * rheight;
                    List<RgPoint> pts = new List<RgPoint>();
                    pts.Add(ptStart);
                    pts.Add(ptEnd);
                    LineString line = new LineString(pts);
                    layer.AddFeature(line);
                }
                for (int j = 1; j < col; j++)
                {
                    RgPoint ptStart = new RgPoint();
                    ptStart.X = baseX + j * rwidth;
                    ptStart.Y = baseY;
                    RgPoint ptEnd = new RgPoint();
                    ptEnd.X = baseX + j * rwidth;
                    ptEnd.Y = baseY + row * rheight;
                    List<RgPoint> pts = new List<RgPoint>();
                    pts.Add(ptStart);
                    pts.Add(ptEnd);
                    LineString line = new LineString(pts);
                    layer.AddFeature(line);
                }
            }
            mapCtrl.Refresh();

        }
        IPickUpPoint pick = null;
        private void btnPickUpPoint_Click(object sender, EventArgs e)
        {
            HookHelper mHook = HookHelper.Instance();
            ICommand cmd = new PickUpPointTool();
            pick = cmd as IPickUpPoint;
            pick.PickUpFinishedEventHandler += new PickUpFinished(pick_PickUpFinishedEventHandler);
            cmd.OnCreate(mHook);
            mMapCtrl.CurrentTool = cmd as ITool;
        }

        void pick_PickUpFinishedEventHandler(Geometries.RgPoint p1)
        {
            txtX.Text = p1.X.ToString();
            txtY.Text = p1.Y.ToString();
        }

        private void FrmGridLineOrPoint_Load(object sender, EventArgs e)
        {
            txtRows.Text = "1";
            txtCols.Text = "1";
            txtRowHeight.Text = "10";
            txtRowWidth.Text = "10";
        }
    }
}
