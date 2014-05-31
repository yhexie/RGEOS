using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Geometries;
using RGeos.Display;
using System.Drawing;

namespace RGeos.Carto
{
    /// <summary>
    /// 捕捉点抽象类
    /// </summary>
    class SnapPointBase : ISnapPoint
    {
        protected RgPoint m_snappoint;

        protected IGeometry m_owner;
        public IGeometry Owner
        {
            get { return m_owner; }
        }
        public SnapPointBase(IGeometry owner, RgPoint snappoint)
        {
            m_owner = owner;
            m_snappoint = snappoint;

        }
        #region ISnapPoint Members
        public virtual RgPoint SnapPoint
        {
            get { return m_snappoint; }
        }

        public virtual void Draw(IScreenDisplay canvas)
        {
            DrawPoint(canvas, Pens.Gold, null);
        }
        #endregion

        protected void DrawPoint(IScreenDisplay canvas, Pen pen, Brush fillBrush)
        {
            IScreenDisplayDraw canvasDraw = canvas as IScreenDisplayDraw;
            Graphics dc = Graphics.FromHwnd(canvas.Handle);

            PointF m_point1 = canvas.DisplayTransformation.ToScreen(m_snappoint);
            dc.DrawRectangle(pen, m_point1.X - 3, m_point1.Y - 3, 6, 6);
            Rectangle screenrect = new Rectangle((int)m_point1.X - 3, (int)m_point1.Y - 3, 6, 6);
            screenrect.X++;
            screenrect.Y++;
            screenrect.Width--;
            screenrect.Height--;
            if (fillBrush != null)
                dc.FillRectangle(fillBrush, screenrect);
            dc.Dispose();
            dc = null;
        }
    }
}
