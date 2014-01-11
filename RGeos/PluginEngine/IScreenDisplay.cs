using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RGeos.Geometry;
using System.Windows.Forms;

namespace RGeos.PluginEngine
{
    public interface IScreenDisplay
    {
        void DrawCache();
        void DrawMultipoint();
        void DrawPoint();
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
    public class ScreenDisplay : IScreenDisplay
    {
        public IntPtr Handle { get; set; }
        private Graphics gc = null;
        private bool m_staticDirty = true;

        public bool IsCacheDirty
        {
            get { return m_staticDirty; }
            set { m_staticDirty = value; }
        }
        public void DrawMultipoint()
        {
            throw new NotImplementedException();
        }

        public void DrawPoint()
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon()
        {
            throw new NotImplementedException();
        }
        public void UpdateWindow()
        {
            m_staticImage=null;
            m_staticDirty = true;
        }
        public void DrawPolyline(Pen vPen, PointF pt1, PointF pt2)
        {
            gc = Graphics.FromHwnd(Handle);
            gc.DrawLine(vPen, pt1, pt2);
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
    }
}
