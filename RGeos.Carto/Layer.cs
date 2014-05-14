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
        public RgEnumShapeType ShapeType;
        public ISymbol2 Symbol;
        public List<IGeometry> mGeometries = new List<IGeometry>();
        public override void Draw(IScreenDisplay display)
        {
            IScreenDisplayDraw displayDraw = display as IScreenDisplayDraw;
            if (ShapeType == RgEnumShapeType.RgPoint)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    RGeos.Geometries.Point pt = mGeometries[i] as RGeos.Geometries.Point;
                    if (pt != null)
                    {
                        displayDraw.DrawPoint( pt,new Pen(Color.Red));
                    }

                }
            }
            if (ShapeType == RgEnumShapeType.RgLineString)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    LineString pt = mGeometries[i] as LineString;
                    if (pt != null)
                    {
                        displayDraw.DrawLineString(pt, new Pen(Color.Red));
                       // display as r
                    }
                }
            }
            if (ShapeType == RgEnumShapeType.RgMultiLineString)
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
            if (ShapeType == RgEnumShapeType.RgPolygon)
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
