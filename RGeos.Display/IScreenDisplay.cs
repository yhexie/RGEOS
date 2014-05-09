using System;
using System.Drawing;
using System.Windows.Forms;
using RGeos.Core.PluginEngine;
using RgPoint = RGeos.Geometries.Point;

namespace RGeos.Display
{
    public interface IScreenDisplay
    {
        object NewObject { get; set; }
        IntPtr Handle { get; set; }//关联窗体的句柄
        bool IsCacheDirty { get; set; }

        IDisplayTransformation DisplayTransformation { get; }

        void StartDrawing(Graphics dc);
        void FinishDrawing();

        void StartRecording();
        void StopRecording();
        void PanMoveTo(PointF mouseLocation);
        void PanStart(PointF mouseLocation);
        Rectangle PanStop();
        void SetSymbol(ISymbol symbol);

    }
    public class ScreenDisplay : IScreenDisplay
    {
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

        public ScreenDisplay(IntPtr handle)
        {
            Handle = handle;//
            mDisplayTransformation = new DisplayTransformation();
            Control mapCtrl = Control.FromHandle(Handle);
            mDisplayTransformation.PanOffset = new PointF(0, 0);
            mDisplayTransformation.DragOffset = new PointF(0, 0);
            mDisplayTransformation.DeviceFrame = mapCtrl.Bounds;
        }
        private bool eCommandTypePan = false;
        private PointF m_MouseDownPoint;
        private PointF m_MouseMoveToPoint;

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

        public void StartDrawing(Graphics dc)
        {
            mDc = dc;
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

        public void FinishDrawing()
        {
            mBitMapGc.Dispose();
            mDc.DrawImage(mStaticImage, mClipRectangle, mClipRectangle, GraphicsUnit.Pixel);
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
        ISymbol mSymbol;
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


    }
}
