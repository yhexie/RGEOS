using System;
using System.Drawing;
using System.Windows.Forms;
using RGeos.Core.PluginEngine;
using RgPoint = RGeos.Geometries.Point;
using RGeos.Geometries;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace RGeos.Display
{
    public interface IScreenDisplay
    {
        object NewObject { get; set; }
        IntPtr Handle { get; set; }//关联窗体的句柄
        bool IsCacheDirty { get; set; }

        IDisplayTransformation DisplayTransformation { get; }

        void StartDrawing(Graphics dc, int drawBuffer);
        void FinishDrawing();

        void StartRecording();
        void StopRecording();
        void PanMoveTo(PointF mouseLocation);
        void PanStart(PointF mouseLocation);
        Rectangle PanStop();
        void SetSymbol(ISymbol symbol);

    }

    public interface IScreenDisplayDraw
    {
        void DrawMultiLineString(MultiLineString lines, Pen pen);
        void DrawPolygon(Polygon pol, Brush brush, Pen pen, bool clip);
        void DrawLineString(LineString line, Pen pen);
        void DrawLine(RGeos.Geometries.Point p1, RGeos.Geometries.Point p2, Pen pen);
    }

    public class ScreenDisplay : IScreenDisplay, IScreenDisplayDraw
    {
        #region IScreenDisplay
        //关联设备（Control）的句柄
        public IntPtr Handle { get; set; }
        private bool m_staticDirty = true;
        public object NewObject { get; set; }
        private const float extremeValueLimit = 1E+8f;
        private const float nearZero = 1E-30f; //值=1/Infinity
        public bool IsCacheDirty
        {
            get { return m_staticDirty; }
            set { m_staticDirty = value; }
        }

        private IDisplayTransformation mDisplayTransformation;
        public IDisplayTransformation DisplayTransformation
        {
            get
            {
                return mDisplayTransformation;
            }
        }

        private bool eCommandTypePan = false;
        private PointF m_MouseDownPoint;
        private PointF m_MouseMoveToPoint;
        private ISymbol mSymbol;

        public ScreenDisplay(IntPtr handle)
        {
            Handle = handle;//
            mDisplayTransformation = new DisplayTransformation();
            Control mapCtrl = Control.FromHandle(Handle);
            mDisplayTransformation.PanOffset = new PointF(0, 0);
            mDisplayTransformation.DragOffset = new PointF(0, 0);
            mDisplayTransformation.DeviceFrame = mapCtrl.Bounds;
        }

        public void PanStart(PointF mouseLocation)
        {
            eCommandTypePan = true;
            m_MouseDownPoint = mouseLocation;
        }

        public void PanMoveTo(PointF mouseLocation)
        {
            if (eCommandTypePan == true)
            {
                m_MouseMoveToPoint = mouseLocation;
                float x = -(m_MouseDownPoint.X - m_MouseMoveToPoint.X);
                float y = -(m_MouseDownPoint.Y - m_MouseMoveToPoint.Y);
                mDisplayTransformation.DragOffset = new PointF(x, y);
            }
        }

        public Rectangle PanStop()
        {
            if (eCommandTypePan == true)
            {
                float x = mDisplayTransformation.PanOffset.X;
                float y = mDisplayTransformation.PanOffset.Y;
                x += mDisplayTransformation.DragOffset.X;
                y += mDisplayTransformation.DragOffset.Y;
                mDisplayTransformation.PanOffset = new PointF(x, y);
                mDisplayTransformation.DragOffset = new PointF(0, 0);
                eCommandTypePan = false;
            }
            return Rectangle.Empty;
        }

        public void UpdateWindow()
        {
            mStaticImage = null;
            m_staticDirty = true;
        }

        //背景图片，缓冲图片
        private Bitmap mStaticImage = null;
        private Rectangle mClipRectangle;
        private Graphics mBitMapGc = null;
        private Graphics mDc = null;
        private int DrawBufferFlag = 0;//是否绘制到缓冲的图片上
        private bool IsStartDrawing;
        public void StartDrawing(Graphics dc, int drawBuffer)
        {
            mDc = dc;
            DrawBufferFlag = drawBuffer;
            IsStartDrawing = true;
            if (DrawBufferFlag == 0)
            {
                Control mapCtrl = Control.FromHandle(Handle);
                if (mStaticImage == null)
                {
                    mClipRectangle = mapCtrl.ClientRectangle;
                    mStaticImage = new Bitmap(mClipRectangle.Width, mClipRectangle.Height);
                    m_staticDirty = true;
                }
                System.Drawing.Drawing2D.SmoothingMode m_smoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                //绘制在背景图片上
                mBitMapGc = Graphics.FromImage(mStaticImage);
                mBitMapGc.SmoothingMode = m_smoothingMode;
                mBitMapGc.Clear(Color.White);
                //m_model.BackgroundLayer.Draw(dcStatic, r);
                //if (m_model.GridLayer.Enabled)
                //    m_model.GridLayer.Draw(dcStatic, r);
                //绘制十字丝
                RgPoint rCenterPoint = new RgPoint(0, 0);
                PointF nullPoint = DisplayTransformation.ToScreen(rCenterPoint);
                mBitMapGc.DrawLine(Pens.Blue, nullPoint.X - 10, nullPoint.Y, nullPoint.X + 10, nullPoint.Y);
                mBitMapGc.DrawLine(Pens.Blue, nullPoint.X, nullPoint.Y - 10, nullPoint.X, nullPoint.Y + 10);

            }
        }

        public void FinishDrawing()
        {
            if (IsStartDrawing && DrawBufferFlag == 0)
            {
                mBitMapGc.Dispose();
                mDc.DrawImage(mStaticImage, mClipRectangle, mClipRectangle, GraphicsUnit.Pixel);
            }

        }

        public void StartRecording()
        {

        }

        public void StopRecording()
        {

        }

        public void DrawCache()
        {

        }

        /// <summary>
        /// 局部刷新，绘制图片Bitmap无效区域
        /// </summary>
        /// <param name="r"></param>
        public void RepaintStatic(Rectangle r)
        {
            if (mStaticImage == null)
                return;
            Graphics dc = Graphics.FromHwnd(Handle);
            if (r.X < 0) r.X = 0;
            if (r.X > mStaticImage.Width) r.X = 0;
            if (r.Y < 0) r.Y = 0;
            if (r.Y > mStaticImage.Height) r.Y = 0;

            if (r.Width > mStaticImage.Width || r.Width < 0)
                r.Width = mStaticImage.Width;
            if (r.Height > mStaticImage.Height || r.Height < 0)
                r.Height = mStaticImage.Height;
            dc.DrawImage(mStaticImage, r, r, GraphicsUnit.Pixel);
            dc.Dispose();
        }

        public void SetSymbol(ISymbol symbol)
        {
            mSymbol = symbol;
        }

        public void DrawRectangle()
        {
            throw new NotImplementedException();
        }

        public void DrawText()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IScreenDisplayDraw
        public void DrawMultiLineString(MultiLineString lines, Pen pen)
        {
            if (IsStartDrawing == false)
            {
                return;
            }
            for (int i = 0; i < lines.LineStrings.Count; i++)
                DrawLineString(lines.LineStrings[i], pen);
        }

        public void DrawLineString(LineString line, Pen pen)
        {
            if (IsStartDrawing == false)
            {
                return;
            }
            if (DrawBufferFlag == 0)
            {
                if (line.Vertices.Count > 1)
                {
                    GraphicsPath gp = new GraphicsPath();
                    PointF[] v = new PointF[line.Vertices.Count];
                    for (int i = 0; i < line.Vertices.Count; i++)
                        v[i] = mDisplayTransformation.ToScreen(line.Vertices[i]);
                    gp.AddLines(LimitValues(v, extremeValueLimit));
                    if (mBitMapGc != null)
                    {
                        mBitMapGc.DrawPath(pen, gp);
                    }
                }
            }
            else
            {
                if (line.Vertices.Count > 1)
                {
                    GraphicsPath gp = new GraphicsPath();
                    PointF[] v = new PointF[line.Vertices.Count];
                    for (int i = 0; i < line.Vertices.Count; i++)
                        v[i] = mDisplayTransformation.ToScreen(line.Vertices[i]);
                    gp.AddLines(LimitValues(v, extremeValueLimit));
                    if (mDc!=null)
                    {
                        mDc.DrawPath(pen, gp);
                    }                    
                }
            }
        }

        public void DrawLine(RGeos.Geometries.Point p1, RGeos.Geometries.Point p2, Pen pen)
        {
            if (IsStartDrawing == false)
            {
                return;
            }
            if (DrawBufferFlag == 0)
            {
                PointF v1 = mDisplayTransformation.ToScreen(p1);
                PointF v2 = mDisplayTransformation.ToScreen(p2);
                if (mBitMapGc != null)
                {
                    mBitMapGc.DrawLine(pen, v1, v2);
                }
            }
            else
            {
                PointF v1 = mDisplayTransformation.ToScreen(p1);
                PointF v2 = mDisplayTransformation.ToScreen(p2);
                mDc.DrawLine(pen, v1, v2);
            }
        }

        public void DrawPolygon(Polygon pol, Brush brush, Pen pen, bool clip)
        {
            if (IsStartDrawing == false)
            {
                return;
            }
            if (pol.ExteriorRing == null)
                return;
            if (pol.ExteriorRing.Vertices.Count > 2)
            {
                //Use a graphics path instead of DrawPolygon. DrawPolygon has a problem with several interior holes
                GraphicsPath gp = new GraphicsPath();
                int width = this.DisplayTransformation.DeviceFrame.Width;
                int height = this.DisplayTransformation.DeviceFrame.Height;
                //Add the exterior polygon
                PointF[] pts = TransformLineString(pol.ExteriorRing);
                if (!clip)
                    gp.AddPolygon(LimitValues(pts, extremeValueLimit));
                else
                    DrawPolygonClipped(gp, pts, (int)width, (int)height);

                //Add the interior polygons (holes)

                for (int i = 0; i < pol.InteriorRings.Count; i++)
                {
                    PointF[] pts1 = TransformLineString(pol.InteriorRings[i]);
                    if (!clip)
                        gp.AddPolygon(LimitValues(pts1, extremeValueLimit));
                    else
                        DrawPolygonClipped(gp, pts1, (int)width, (int)height);
                }

                if (DrawBufferFlag == 0)
                { // Only render inside of polygon if the brush isn't null or isn't transparent
                    if (brush != null && brush != Brushes.Transparent)
                    {
                        if (mBitMapGc != null)
                        {
                            mBitMapGc.FillPath(brush, gp);
                        }

                    }
                    // Create an outline if a pen style is available
                    if (pen != null)
                    {
                        if (mBitMapGc != null)
                        {
                            mBitMapGc.DrawPath(pen, gp);
                        }
                    }
                }
                else
                {
                    if (brush != null && brush != Brushes.Transparent)
                        mDc.FillPath(brush, gp);
                    // Create an outline if a pen style is available
                    if (pen != null)
                        mDc.DrawPath(pen, gp);
                }
            }
        }

        #endregion

        #region 私有的
        private PointF[] TransformLineString(LineString line)
        {
            PointF[] v = new PointF[line.Vertices.Count];
            for (int i = 0; i < line.Vertices.Count; i++)
                v[i] = mDisplayTransformation.ToScreen(line.Vertices[i]);
            return v;
        }

        private PointF[] LimitValues(PointF[] vertices, float limit)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].X = Math.Max(-limit, Math.Min(limit, vertices[i].X));
                vertices[i].Y = Math.Max(-limit, Math.Min(limit, vertices[i].Y));
            }
            return vertices;
        }

        private enum ClipState
        {
            Within,
            Outside,
            Intersecting
        } ;

        private static PointF[] clipPolygon(PointF[] vertices, int width, int height)
        {
            float deltax, deltay, xin, xout, yin, yout;
            float tinx, tiny, toutx, touty, tin1, tin2, tout;
            float x1, y1, x2, y2;

            List<PointF> line = new List<PointF>();
            if (vertices.Length <= 1) /* nothing to clip */
                return vertices;

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                x1 = vertices[i].X;
                y1 = vertices[i].Y;
                x2 = vertices[i + 1].X;
                y2 = vertices[i + 1].Y;

                deltax = x2 - x1;
                if (deltax == 0)
                {
                    // bump off of the vertical
                    deltax = (x1 > 0) ? -nearZero : nearZero;
                }
                deltay = y2 - y1;
                if (deltay == 0)
                {
                    // bump off of the horizontal
                    deltay = (y1 > 0) ? -nearZero : nearZero;
                }

                if (deltax > 0)
                {
                    //  points to right
                    xin = 0;
                    xout = width;
                }
                else
                {
                    xin = width;
                    xout = 0;
                }

                if (deltay > 0)
                {
                    //  points up
                    yin = 0;
                    yout = height;
                }
                else
                {
                    yin = height;
                    yout = 0;
                }

                tinx = (xin - x1) / deltax;
                tiny = (yin - y1) / deltay;

                if (tinx < tiny)
                {
                    // hits x first
                    tin1 = tinx;
                    tin2 = tiny;
                }
                else
                {
                    // hits y first
                    tin1 = tiny;
                    tin2 = tinx;
                }

                if (1 >= tin1)
                {
                    if (0 < tin1)
                        line.Add(new PointF(xin, yin));

                    if (1 >= tin2)
                    {
                        toutx = (xout - x1) / deltax;
                        touty = (yout - y1) / deltay;

                        tout = (toutx < touty) ? toutx : touty;

                        if (0 < tin2 || 0 < tout)
                        {
                            if (tin2 <= tout)
                            {
                                if (0 < tin2)
                                {
                                    if (tinx > tiny)
                                        line.Add(new PointF(xin, y1 + tinx * deltay));
                                    else
                                        line.Add(new PointF(x1 + tiny * deltax, yin));
                                }

                                if (1 > tout)
                                {
                                    if (toutx < touty)
                                        line.Add(new PointF(xout, y1 + toutx * deltay));
                                    else
                                        line.Add(new PointF(x1 + touty * deltax, yout));
                                }
                                else
                                    line.Add(new PointF(x2, y2));
                            }
                            else
                            {
                                if (tinx > tiny)
                                    line.Add(new PointF(xin, yout));
                                else
                                    line.Add(new PointF(xout, yin));
                            }
                        }
                    }
                }
            }
            if (line.Count > 0)
                line.Add(new PointF(line[0].X, line[0].Y));

            return line.ToArray();
        }

        private void DrawPolygonClipped(GraphicsPath gp, PointF[] polygon, int width, int height)
        {
            ClipState clipState = DetermineClipState(polygon, width, height);
            if (clipState == ClipState.Within)
            {
                gp.AddPolygon(polygon);
            }
            else if (clipState == ClipState.Intersecting)
            {
                PointF[] clippedPolygon = clipPolygon(polygon, width, height);
                if (clippedPolygon.Length > 2)
                    gp.AddPolygon(clippedPolygon);
            }
        }

        private ClipState DetermineClipState(PointF[] vertices, int width, int height)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                minX = Math.Min(minX, vertices[i].X);
                minY = Math.Min(minY, vertices[i].Y);
                maxX = Math.Max(maxX, vertices[i].X);
                maxY = Math.Max(maxY, vertices[i].Y);
            }

            if (maxX < 0) return ClipState.Outside;
            if (maxY < 0) return ClipState.Outside;
            if (minX > width) return ClipState.Outside;
            if (minY > height) return ClipState.Outside;
            if (minX > 0 && maxX < width && minY > 0 && maxY < height) return ClipState.Within;
            return ClipState.Intersecting;
        }
        #endregion
    }
}
