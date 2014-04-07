using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Core.PluginEngine;
using System.Drawing;
using RGeos.Geometry;

namespace RGeos.PluginEngine
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
        public ISymbol Symbol;
        List<Geometry.RGeometry> mGeometries = new List<Geometry.RGeometry>();
        public override void Draw(IScreenDisplay display)
        {
            if (ShapeType == 0)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    RPoint pt = mGeometries[i] as RPoint;
                    if (pt != null)
                    {
                        display.DrawPoint(new Pen(Color.Red), pt);
                    }

                }
            }
            if (ShapeType == 1)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    RPolyline pt = mGeometries[i] as RPolyline;
                    if (pt != null)
                    {
                        display.DrawPolyline(new Pen(Color.Red), pt);
                    }

                }
            }
            if (ShapeType == 2)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    RPolygon pt = mGeometries[i] as RPolygon;
                    if (pt != null)
                    {
                        display.DrawPolygon (new Pen(Color.Red), pt);
                    }

                }
            }
        }
    }
}
