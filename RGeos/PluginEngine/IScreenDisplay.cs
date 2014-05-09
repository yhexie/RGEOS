using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RGeos.Geometry;
using System.Windows.Forms;
using RGeos.Core.PluginEngine;
using SharpMap.Geometries;
using System.Drawing.Drawing2D;
using SharpMap.Utilities;

namespace RGeos.PluginEngine
{
   
    public interface IScreenDisplayOld
    {
        object NewObject { get; set; }
        IDisplayTransformationOld DisplayTransformation { get; set; }
        void DrawPoint(Pen vPen, RPoint pt0);
        void DrawPolygon(Pen vPen, RPolygon polyline);
        void DrawPolyline(Pen vPen, RPolyline polyline);


        void DrawLineString(Graphics g, LineString line, Pen pen, Map map);
        void DrawPolygon(Polygon pol, Brush brush, Pen pen, bool clip);
        void DrawPolygon2(Graphics g,Polygon pol, Brush brush, Pen pen, bool clip);

        void DrawCache();
        void DrawMultipoint();
        void DrawPolygon();
        void DrawPolyline(Pen vPen, PointF pt1, PointF pt2);
        void DrawRectangle();
        void DrawText();

        void StartRecording();
        void StartDrawing(UcMapControl mapCtrl);
        void FinishDrawing(Graphics dc);
        void StopRecording();

        IntPtr Handle { get; set; }
        bool IsCacheDirty { get; set; }
    }
    public class ScreenDisplayOld : IScreenDisplayOld
    {
        public IntPtr Handle { get; set; }
        private Graphics gc = null;
        private bool m_staticDirty = true;
        public object NewObject { get; set; }
        private const float extremeValueLimit = 1E+8f;
        private const float nearZero = 1E-30f; // 1/Infinity
        public bool IsCacheDirty
        {
            get { return m_staticDirty; }
            set { m_staticDirty = value; }
        }
        public void DrawMultipoint()
        {
            throw new NotImplementedException();
        }

        public void DrawMultiLineString(Graphics g, MultiLineString lines, Pen pen, Map map)
        {
            for (int i = 0; i < lines.LineStrings.Count; i++)
                DrawLineString(g, lines.LineStrings[i], pen, map);
        }

        /// <summary>
        /// Renders a LineString to the map.
        public void DrawLineString(Graphics g, LineString line, Pen pen, Map map)
        {
            if (line.Vertices.Count > 1)
            {
                GraphicsPath gp = new GraphicsPath();
                PointF[] v = new PointF[line.Vertices.Count];
                for (int i = 0; i < line.Vertices.Count; i++)
                    v[i] = Transform2.WorldtoMap(line.Vertices[i], map);
                gp.AddLines(LimitValues(v, extremeValueLimit));
                g.DrawPath(pen, gp);
            }

        }

        public static PointF[] TransformLineString(LineString line)
        {
            PointF[] v = new PointF[line.Vertices.Count];
            for (int i = 0; i < line.Vertices.Count; i++)
                v[i] = new PointF((float)line.Vertices[i].X, (float)line.Vertices[i].Y);
            return v;
        }

        public static PointF[] TransformLineString(LineString line, Map map)
        {
            PointF[] v = new PointF[line.Vertices.Count];
            for (int i = 0; i < line.Vertices.Count; i++)
                v[i] = Transform2.WorldtoMap(line.Vertices[i], map);
            return v;
        }
        private static PointF[] LimitValues(PointF[] vertices, float limit)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].X = Math.Max(-limit, Math.Min(limit, vertices[i].X));
                vertices[i].Y = Math.Max(-limit, Math.Min(limit, vertices[i].Y));
            }
            return vertices;
        }

        public void DrawPoint(Pen vPen, RPoint pt0)
        {
            gc = Graphics.FromHwnd(Handle);
            PointF pt = new PointF((float)pt0.X, (float)pt0.Y);
            SizeF size = new SizeF(4f, 4f);
            RectangleF rect = new RectangleF(pt, size);
            gc.DrawEllipse(vPen, rect);
            gc.Dispose();
        }

        public void DrawPolygon()
        {
            throw new NotImplementedException();
        }
        public void DrawPolygon(Pen vPen, RPolygon polygon)
        {
            gc = Graphics.FromHwnd(Handle);
            //for (int i = 0; i < polygon; i++)
            //{

            //    // gc.DrawLine(vPen, pt1, pt2);
            //}
            gc.Dispose();
        }

        private enum ClipState
        {
            Within,
            Outside,
            Intersecting
        } ;

        internal static PointF[] clipPolygon(PointF[] vertices, int width, int height)
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

        public void DrawPolygon(Polygon pol, Brush brush, Pen pen, bool clip)
        {
            gc = Graphics.FromHwnd(Handle);
            if (pol.ExteriorRing == null)
                return;
            if (pol.ExteriorRing.Vertices.Count > 2)
            {
                //Use a graphics path instead of DrawPolygon. DrawPolygon has a problem with several interior holes
                GraphicsPath gp = new GraphicsPath();

                //Add the exterior polygon
                PointF[] pts = TransformLineString(pol.ExteriorRing);
                if (!clip)
                    gp.AddPolygon(LimitValues(pts, extremeValueLimit));
                else
                    DrawPolygonClipped(gp, pts, (int)100, (int)100);

                //Add the interior polygons (holes)

                for (int i = 0; i < pol.InteriorRings.Count; i++)
                {
                    PointF[] pts1 = TransformLineString(pol.InteriorRings[i]);
                    if (!clip)
                        gp.AddPolygon(LimitValues(pts1, extremeValueLimit));
                    else
                        DrawPolygonClipped(gp, pts1, (int)100, (int)100);
                }

                // Only render inside of polygon if the brush isn't null or isn't transparent
                if (brush != null && brush != Brushes.Transparent)
                    gc.FillPath(brush, gp);
                // Create an outline if a pen style is available
                if (pen != null)
                    gc.DrawPath(pen, gp);
                gc.Dispose();
            }
        }

        public void DrawPolygon2(Graphics g,Polygon pol, Brush brush, Pen pen, bool clip)
        {
            if (pol.ExteriorRing == null)
                return;
            if (pol.ExteriorRing.Vertices.Count > 2)
            {
                //Use a graphics path instead of DrawPolygon. DrawPolygon has a problem with several interior holes
                GraphicsPath gp = new GraphicsPath();

                //Add the exterior polygon
                PointF[] pts = TransformLineString(pol.ExteriorRing);
                if (!clip)
                    gp.AddPolygon(LimitValues(pts, extremeValueLimit));
                else
                    DrawPolygonClipped(gp, pts, (int)100, (int)100);

                //Add the interior polygons (holes)

                for (int i = 0; i < pol.InteriorRings.Count; i++)
                {
                    PointF[] pts1 = TransformLineString(pol.InteriorRings[i]);
                    if (!clip)
                        gp.AddPolygon(LimitValues(pts1, extremeValueLimit));
                    else
                        DrawPolygonClipped(gp, pts1, (int)100, (int)100);
                }

                // Only render inside of polygon if the brush isn't null or isn't transparent
                if (brush != null && brush != Brushes.Transparent)
                    g.FillPath(brush, gp);
                // Create an outline if a pen style is available
                if (pen != null)
                    g.DrawPath(pen, gp);
            }
        }


        public void DrawPolygon(Graphics g, Polygon pol, Brush brush, Pen pen, bool clip, Map map)
        {
            if (pol.ExteriorRing == null)
                return;
            if (pol.ExteriorRing.Vertices.Count > 2)
            {
                //Use a graphics path instead of DrawPolygon. DrawPolygon has a problem with several interior holes
                GraphicsPath gp = new GraphicsPath();

                //Add the exterior polygon
                PointF[] pts = TransformLineString(pol.ExteriorRing, map);
                if (!clip)
                    gp.AddPolygon(LimitValues(pts, extremeValueLimit));
                else
                    DrawPolygonClipped(gp, pts, (int)map.Size.Width, (int)map.Size.Height);

                //Add the interior polygons (holes)

                for (int i = 0; i < pol.InteriorRings.Count; i++)
                {
                    PointF[] pts1 = TransformLineString(pol.InteriorRings[i], map);
                    if (!clip)
                        gp.AddPolygon(LimitValues(pts1, extremeValueLimit));
                    else
                        DrawPolygonClipped(gp, pts1, (int)map.Size.Width, (int)map.Size.Height);
                }

                // Only render inside of polygon if the brush isn't null or isn't transparent
                if (brush != null && brush != Brushes.Transparent)
                    g.FillPath(brush, gp);
                // Create an outline if a pen style is available
                if (pen != null)
                    g.DrawPath(pen, gp);
            }
        }

        public void UpdateWindow()
        {
            m_staticImage = null;
            m_staticDirty = true;
        }
        public void DrawPolyline(Pen vPen, PointF pt1, PointF pt2)
        {
            gc = Graphics.FromHwnd(Handle);
            gc.DrawLine(vPen, pt1, pt2);
            gc.Dispose();
        }
        public void DrawPolyline(Pen vPen, RPolyline polyline)
        {
            gc = Graphics.FromHwnd(Handle);
            for (int i = 0; i < polyline.Number; i++)
            {

                // gc.DrawLine(vPen, pt1, pt2);
            }
            gc.Dispose();
        }
        //缓存图片？
        Bitmap m_staticImage = null;
        Rectangle cliprectangle;
        public void StartRecording()
        {

        }
        Graphics BitMapGc = null;
        public void StartDrawing(UcMapControl mapCtrl)
        {
            if (m_staticImage == null)
            {
                cliprectangle = mapCtrl.ClientRectangle;
                m_staticImage = new Bitmap(cliprectangle.Width, cliprectangle.Height);
                //  m_staticImage.Save("D:\\a.png", ImageFormat.Png);
                m_staticDirty = true;
            }
            System.Drawing.Drawing2D.SmoothingMode m_smoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //绘制在背景图片上
            BitMapGc = Graphics.FromImage(m_staticImage);
            BitMapGc.SmoothingMode = m_smoothingMode;
            BitMapGc.Clear(Color.White);
            //m_model.BackgroundLayer.Draw(dcStatic, r);
            //if (m_model.GridLayer.Enabled)
            //    m_model.GridLayer.Draw(dcStatic, r);
            //绘制十字丝
            RPoint rCenterPoint = new RPoint(0, 0, 0);
            PointF nullPoint = Transform.ToScreen(rCenterPoint, mapCtrl);
            BitMapGc.DrawLine(Pens.Blue, nullPoint.X - 10, nullPoint.Y, nullPoint.X + 10, nullPoint.Y);
            BitMapGc.DrawLine(Pens.Blue, nullPoint.X, nullPoint.Y - 10, nullPoint.X, nullPoint.Y + 10);
            if (m_staticDirty)
            {
                m_staticDirty = false;
                List<ILayer> layers = mapCtrl.mMap.Layers;
                for (int layerindex = layers.Count - 1; layerindex >= 0; layerindex--)
                {
                    if (layers[layerindex].Visible)
                        layers[layerindex].Draw(this);
                }

            }
            if (NewObject != null)
            {
                if (NewObject is Polygon)
                {
                    SolidBrush brush = new SolidBrush(Color.Blue);
                    SolidBrush brush2 = new SolidBrush(Color.Pink);
                    Pen pen = new Pen(brush2);
                    DrawPolygon2(BitMapGc,NewObject as Polygon, brush, pen, false);
                }
            }
            BitMapGc.Dispose();
        }

        public void FinishDrawing(Graphics dc)
        {
            //Graphics dc = Graphics.FromHwnd(Handle);
            dc.DrawImage(m_staticImage, cliprectangle, cliprectangle, GraphicsUnit.Pixel);
            // dc.Dispose();

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
            if (m_staticImage == null)
                return;
            Graphics dc = Graphics.FromHwnd(Handle);
            if (r.X < 0) r.X = 0;
            if (r.X > m_staticImage.Width) r.X = 0;
            if (r.Y < 0) r.Y = 0;
            if (r.Y > m_staticImage.Height) r.Y = 0;

            if (r.Width > m_staticImage.Width || r.Width < 0)
                r.Width = m_staticImage.Width;
            if (r.Height > m_staticImage.Height || r.Height < 0)
                r.Height = m_staticImage.Height;
            dc.DrawImage(m_staticImage, r, r, GraphicsUnit.Pixel);
            dc.Dispose();
        }
        public void DrawRectangle()
        {
            throw new NotImplementedException();
        }

        public void DrawText()
        {
            throw new NotImplementedException();
        }

        public IDisplayTransformationOld DisplayTransformation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
