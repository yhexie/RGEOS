using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.Display;
using RGeos.Geometries;
using RgPoint = RGeos.Geometries.Point;
using System.Diagnostics;
using RGeos.Core;

namespace RGeos.Controls
{
    public partial class RgMapControl : UserControl, IMapControl2
    {
        public RgMapControl()
        {
            mScreenDisplay = new RGeos.Display.ScreenDisplay(Handle);
            mScreenDisplay.DisplayTransformation.Zoom = 1.0f;
            InitializeComponent();
            mMap = new RGeos.Carto.Map();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.MouseDown += new MouseEventHandler(mPanel_MouseDown);
            this.MouseMove += new MouseEventHandler(mPanel_MouseMove);
            this.MouseUp += new MouseEventHandler(mPanel_MouseUp);
            this.Resize += new System.EventHandler(this.UcMapControl_Resize);
        }

        public ITool CurrentTool { get; set; }
        private RGeos.Display.IScreenDisplay mScreenDisplay;

        public RGeos.Display.IScreenDisplay ScreenDisplay
        {
            get { return mScreenDisplay; }
        }
        public BoundingBox mExtent { get; set; }
        private RGeos.Carto.IMap mMap;

        public RGeos.Carto.IMap Map
        {
            get { return mMap; }
        }

        System.Drawing.Drawing2D.SmoothingMode m_smoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = m_smoothingMode;
            if (mScreenDisplay.IsCacheDirty)
            {
                mScreenDisplay.StartRecording();
                mScreenDisplay.StartDrawing(e.Graphics, 0);
                mMap.Draw(mScreenDisplay);
                object newObj = mScreenDisplay.NewObject;
                if (newObj != null)
                {
                   IScreenDisplayDraw drawNew= mScreenDisplay as IScreenDisplayDraw;
                    if (newObj is LineString)
                    {
                        Pen mpen = new Pen(Color.Blue);
                        drawNew.DrawLineString(newObj as LineString, mpen);
                    }
                    else if (newObj is MultiLineString)
                    {
                        Pen mpen = new Pen(Color.Blue);
                        drawNew.DrawMultiLineString(newObj as MultiLineString, mpen);
                    }
                    else if (newObj is Polygon)
                    {
                        Brush brush = new SolidBrush(Color.Blue);
                        drawNew.DrawPolygon(newObj as Polygon,brush, Pens.AliceBlue, false);
                    }
                }

                mScreenDisplay.FinishDrawing();
                mScreenDisplay.StopRecording();
            }
        }


        void mPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null)
            {
                CurrentTool.OnMouseUp(e.X, e.Y);
            }
        }

        void mPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null)
            {
                CurrentTool.OnMouseMove(e.X, e.Y);
            }
            RgPoint pt = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(e.X, e.Y));
            Debug.Write(string.Format("X:{0}mm Y:{1}mm", pt.X * 25.4, pt.Y * 25.4));
        }

        void mPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null)
            {
                CurrentTool.OnMouseDown(e.X, e.Y, e);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            System.Drawing.Point point = this.PointToClient(Control.MousePosition);//放大中心点屏幕坐标
            RgPoint p = mScreenDisplay.DisplayTransformation.ToUnit(point);//对应的当前Zoom下的世界坐标
            float wheeldeltatick = 120;
            float zoomdelta = (1.25f * (Math.Abs(e.Delta) / wheeldeltatick));
            if (e.Delta < 0)
                mScreenDisplay.DisplayTransformation.Zoom = mScreenDisplay.DisplayTransformation.Zoom / zoomdelta;
            else
                mScreenDisplay.DisplayTransformation.Zoom = mScreenDisplay.DisplayTransformation.Zoom * zoomdelta;
            SetCenterScreen(mScreenDisplay.DisplayTransformation.ToScreen(p), false);//放大后，得到同一个世界坐标对应的屏幕坐标
            Invalidate(true);
            base.OnMouseWheel(e);
        }

        RgPoint m_lastCenterPoint;
        private void UcMapControl_Resize(object sender, EventArgs e)
        {
            if (m_lastCenterPoint != null && Width != 0)
                SetCenterScreen(mScreenDisplay.DisplayTransformation.ToScreen(m_lastCenterPoint), false);
            m_lastCenterPoint = CenterPointUnit();
            (mScreenDisplay as RGeos.Display.ScreenDisplay).UpdateWindow();
            Invalidate();
        }
        /// <summary>
        /// 设置画布到屏幕的中心
        /// </summary>
        /// <param name="rPoint">直角坐标系坐标</param>
        public void SetCenter(RgPoint unitPoint)
        {
            //将unitPoint点对应到屏幕上point
            PointF point = mScreenDisplay.DisplayTransformation.ToScreen(unitPoint);
            m_lastCenterPoint = unitPoint;
            //将unitPoint偏移到屏幕中心
            SetCenterScreen(point, false);
        }
        protected void SetCenterScreen(PointF screenPoint, bool setCursor)
        {
            float centerX = ClientRectangle.Width / 2;
            float x = mScreenDisplay.DisplayTransformation.PanOffset.X + centerX - screenPoint.X;
            float centerY = mScreenDisplay.DisplayTransformation.PanOffset.Y + ClientRectangle.Height / 2;
            float y = centerY - screenPoint.Y;
            mScreenDisplay.DisplayTransformation.PanOffset = new PointF(x, y);
            if (setCursor)
                Cursor.Position = this.PointToScreen(new System.Drawing.Point((int)centerX, (int)centerY));
            Invalidate();
        }
        public RgPoint CenterPointUnit()
        {
            RgPoint p1 = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(0, 0));
            RgPoint p2 = mScreenDisplay.DisplayTransformation.ToUnit(new PointF(this.ClientRectangle.Width, this.ClientRectangle.Height));
            RgPoint center = new RgPoint();
            center.X = (p1.X + p2.X) / 2;
            center.Y = (p1.Y + p2.Y) / 2;
            return center;
        }
    }
}
