using System.Drawing;
using RGeos.Geometries;

namespace RGeos.Core.PluginEngine
{
    // DisplayTransformation object to convert coordinates between map units and device units.
    //默认单位：屏幕是像素和地图是英寸
    public interface IDisplayTransformation : ITransformation
    {
        BoundingBox VisibleBounds { get; set; }
        Rectangle DeviceFrame { get; set; }

        double PixelWidth { get; }
        double PixelHeight { get; }

        PointF PanOffset { get; set; }
        PointF DragOffset { get; set; }
        float Zoom { get; set; }
        PointF ToScreen(RgPoint pt);
        RgPoint ToUnit(PointF screenpoint);
    }
    public delegate void DeviceFrameUpdatedEventHander();
    public class DisplayTransformation : IDisplayTransformation
    {
        private Rectangle mDeviceFrame;
        private BoundingBox mVisibleBounds = null;
        public BoundingBox VisibleBounds
        {
            get
            {
                if (mDeviceFrame != Rectangle.Empty)
                {
                    RgPoint lowLeft = ToUnit(new PointF(mDeviceFrame.Left, mDeviceFrame.Bottom));
                    RgPoint upRight = ToUnit(new PointF(mDeviceFrame.Right, mDeviceFrame.Top));
                    mVisibleBounds = new BoundingBox(lowLeft, upRight);
                    return mVisibleBounds;
                }
                return null;
            }
            set
            {
                mVisibleBounds = value;
            }
        }
        /// <summary>
        /// 一个象素在当前缩放比例下代表的地图尺寸
        /// </summary>
        public double PixelSize
        {
            get { return mVisibleBounds.Width / mDeviceFrame.Width; }
            //get { return Zoom / mDeviceFrame.Width; }
        }

        /// <summary>
        /// Returns the width of a pixel in world coordinate units.
        /// </summary>
        public double PixelWidth
        {
            get { return PixelSize; }
        }

        public double PixelHeight
        {
            get { return PixelSize; }
        }

        public DeviceFrameUpdatedEventHander DeviceFrameUpdated;
        //设备的可见范围.
        //The DeviceFrame is normally the full extent of the device with the origin equal to (0, 0).  
        public Rectangle DeviceFrame
        {
            get { return mDeviceFrame; }
            set
            {
                if (mDeviceFrame != value)
                {
                    mDeviceFrame = value;
                    if (DeviceFrameUpdated != null)
                    {
                        DeviceFrameUpdated();
                    }
                }
            }
        }
        //分辨率：Resolution of the device in dots (pixels) per inch.
        public static float m_screenResolution = 96;

        public float Zoom { get; set; }

        private PointF m_panOffset = new PointF(25, -25);

        public PointF PanOffset
        {
            get { return m_panOffset; }
            set { m_panOffset = value; }
        }
        private PointF m_dragOffset = new PointF(0, 0);

        public PointF DragOffset
        {
            get { return m_dragOffset; }
            set { m_dragOffset = value; }
        }
        public DisplayTransformation()
        {
            Zoom = 1.0f;
        }
        public PointF ToScreen(RgPoint pt)
        {
            PointF transformedPoint = new PointF((float)pt.X, (float)pt.Y);
            transformedPoint.Y = ScreenHeight() - transformedPoint.Y;//将Unit坐标系转换为屏幕坐标系，Y轴反向，此时Y坐标为屏幕坐标系坐标
            transformedPoint.Y *= m_screenResolution * Zoom;//相对于屏幕原点放大
            transformedPoint.X *= m_screenResolution * Zoom;

            transformedPoint.X += m_panOffset.X + m_dragOffset.X;
            transformedPoint.Y += m_panOffset.Y + m_dragOffset.Y;
            return transformedPoint;
        }

        private float ScreenHeight()
        {
            return (float)(ToUnit(this.mDeviceFrame.Height));
        }

        public RgPoint ToUnit(PointF screenpoint)
        {
            float panoffsetX = m_panOffset.X + m_dragOffset.X;
            float panoffsetY = m_panOffset.Y + m_dragOffset.Y;
            float xpos = (screenpoint.X - panoffsetX) / (m_screenResolution * Zoom);
            float ypos = ScreenHeight() - ((screenpoint.Y - panoffsetY)) / (m_screenResolution * Zoom);
            return new RgPoint(xpos, ypos);
        }

        //将屏幕距离计算为Zoom等级下的地图距离
        public double ToUnit(float screenvalue)
        {
            return (double)screenvalue / (double)(m_screenResolution * Zoom);
        }
    }
}
