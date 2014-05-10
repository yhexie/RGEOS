using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Display;
using RGeos.Geometries;
using System.Drawing;

namespace RGeos.Carto
{
    public abstract class Layer : ILayer
    {
        public string Name { get; set; }
        public string AliasName { get; set; }
        public bool Visible { get; set; }
        public virtual void Draw(IScreenDisplay display)
        {
        }


    }
    public class FetureLayer : Layer
    {
        public int ShapeType;
        public ISymbol2 Symbol;
        List<IGeometry> mGeometries = new List<IGeometry>();
        public override void Draw(IScreenDisplay display)
        {
            IScreenDisplayDraw displayDraw = display as IScreenDisplayDraw;
            if (ShapeType == 0)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    //RgPoint pt = mGeometries[i] as RPoint;
                    //if (pt != null)
                    //{
                    //    display.DrawPoint(new Pen(Color.Red), pt);
                    //}

                }
            }
            if (ShapeType == 1)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    MultiLineString pt = mGeometries[i] as MultiLineString;
                    if (pt != null)
                    {
                        displayDraw.DrawMultiLineString(pt, new Pen(Color.Red));
                    }

                }
            }
            if (ShapeType == 2)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    Polygon pt = mGeometries[i] as Polygon;
                    if (pt != null)
                    {
                        Pen mPen = new Pen(Color.Red);
                        Brush brush = new SolidBrush(Color.Blue);
                        displayDraw.DrawPolygon(pt, brush, mPen, false);
                    }

                }
            }
        }
    }
}
