using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RGeos.Carto;
using RGeos.Display;

namespace RGeos.Desktop
{
    public class SelectionRectangle
    {
        private PointF m_point1;
        private PointF m_point2;
        public SelectionRectangle(PointF mousedownpoint)
        {
            m_point1 = mousedownpoint;
            m_point2 = PointF.Empty;
        }
        public void Reset()
        {
            m_point2 = PointF.Empty;
        }
        public void SetMousePoint(ScreenDisplay display, Graphics dc, PointF mousepoint)
        {
            if (m_point2 != PointF.Empty)
            {
                float dx = m_point2.X - m_point1.X;
                float dy = m_point2.Y - m_point1.Y;
                Rectangle invalidaterect = ScreenRect();
                invalidaterect.Inflate(2, 2);
                display.RepaintStatic(invalidaterect);

            }
            m_point2 = mousepoint;
            Rectangle rect2 = ScreenRect();
            dc.DrawRectangle(Pens.Yellow, rect2);
            //XorGdi.DrawRectangle(dc, PenStyles.PS_DOT, 1, GetColor(), m_point1, m_point2);
            //float dx2 = m_point2.X - m_point1.X;
            //float dy2 = m_point2.Y - m_point1.Y;
            //dc.DrawRectangle(Pens.Yellow, m_point1.X, m_point1.Y, dx2, dy2);
            //XorGdi.DrawRectangle(dc, PenStyles.PS_DOT, 1, GetColor(), m_point1, m_point2);
        }
        public Rectangle ScreenRect()
        {
            float x = Math.Min(m_point1.X, m_point2.X);
            float y = Math.Min(m_point1.Y, m_point2.Y);
            float w = Math.Abs(m_point1.X - m_point2.X);
            float h = Math.Abs(m_point1.Y - m_point2.Y);
            if (m_point2 == PointF.Empty)
                return Rectangle.Empty;
            if (w < 4 || h < 4) // if no selection was made return empty rectangle (giving a 4 pixel threshold)
                return Rectangle.Empty;
            return new Rectangle((int)x, (int)y, (int)w, (int)h);
        }
        public bool AnyPoint()
        {
            return (m_point1.X > m_point2.X);
        }
        private Color GetColor()
        {
            if (AnyPoint())
                return Color.Blue;
            return Color.Green;
        }
    }
}
