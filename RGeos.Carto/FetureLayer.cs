using System.Collections.Generic;
using RGeos.Geometries;
using RGeos.Display;
using System.Drawing;

namespace RGeos.Carto
{
    public class Feature
    {
        public IGeometry Shape;
        public bool IsSelected;
        public Feature(IGeometry geometry, bool isSelected)
        {
            Shape = geometry;
            IsSelected = isSelected;
        }
    }
    public class FetureLayer : Layer
    {
        public RgEnumShapeType ShapeType;
        public ISymbol Symbol;
        public List<Feature> mGeometries = new List<Feature>();

        public void AddFeature(IGeometry feat)
        {
            mGeometries.Add(new Feature(feat, false));
        }

        public override void Draw(IScreenDisplay display)
        {
            IScreenDisplayDraw displayDraw = display as IScreenDisplayDraw;
            if (ShapeType == RgEnumShapeType.RgPoint)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    Feature feat = mGeometries[i];
                    RGeos.Geometries.RgPoint pt = feat.Shape as RGeos.Geometries.RgPoint;
                    if (pt != null)
                    {
                        if (feat.IsSelected == false)
                        {
                            displayDraw.DrawPoint(pt, new Pen(Color.Red), new SolidBrush(Color.Red));
                        }
                        else
                        {
                            displayDraw.DrawPoint(pt, new Pen(Color.LightBlue), new SolidBrush(Color.LightBlue));
                        }
                    }

                }
            }
            if (ShapeType == RgEnumShapeType.RgLineString)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    Feature feat = mGeometries[i];
                    LineString pt = feat.Shape as LineString;
                    if (pt != null)
                    {
                        if (feat.IsSelected == false)
                        {
                            displayDraw.DrawLineString(pt, new Pen(Color.Red));
                        }
                        else
                        {
                            displayDraw.DrawLineString(pt, new Pen(Color.LightBlue));
                        }
                        // display as r
                    }
                }
            }
            if (ShapeType == RgEnumShapeType.RgMultiLineString)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    Feature feat = mGeometries[i];
                    MultiLineString pt = feat.Shape as MultiLineString;
                    if (pt != null)
                    {
                        if (feat.IsSelected == false)
                        {
                            displayDraw.DrawMultiLineString(pt, new Pen(Color.Red));
                        }
                        else
                        {
                            displayDraw.DrawMultiLineString(pt, new Pen(Color.LightBlue));
                        }
                    }

                }
            }
            if (ShapeType == RgEnumShapeType.RgPolygon)
            {
                for (int i = 0; i < mGeometries.Count; i++)
                {
                    Feature feat = mGeometries[i];
                    Polygon polygon = feat.Shape as Polygon;
                    if (polygon != null)
                    {
                        Pen mPen = new Pen(Color.Red);
                        Brush brush = new SolidBrush(Color.Blue);
                        if (feat.IsSelected == false)
                        {
                            displayDraw.DrawPolygon(polygon, brush, mPen, false);
                        }
                        else
                        {
                            displayDraw.DrawPolygon(polygon, brush, new Pen(Color.LightBlue), false);
                        }
                    }

                }
            }
        }

        public List<Feature> GetHitObjects(BoundingBox selectionBox, bool anyPoint)
        {
            List<Feature> selected = new List<Feature>();

            if (this.Visible == false)
                return null;
            foreach (Feature drawobject in mGeometries)
            {
                if (ShapeType == RgEnumShapeType.RgPoint)
                {
                    RgPoint pt = drawobject.Shape as RgPoint;
                    if (selectionBox.Contains(pt))
                    {
                        selected.Add(drawobject);
                        // drawobject.IsSelected = true;
                    }
                }
                else if (ShapeType == RgEnumShapeType.RgLineString)
                {
                    LineString lineString = drawobject.Shape as LineString;
                    if (lineString.ObjectInRectangle(selectionBox, false))
                    {
                        selected.Add(drawobject);
                        // drawobject.IsSelected = true;
                    }
                }
            }
            return selected;
        }

        public List<Feature> GetHitObjects(RgPoint point, double tolerance)
        {
            List<Feature> selected = new List<Feature>();

            if (this.Visible == false)
                return null;
            foreach (Feature drawobject in mGeometries)
            {
                if (ShapeType == RgEnumShapeType.RgPoint)
                {
                    RgPoint pt = drawobject.Shape as RgPoint;
                    if (RDistanceMeasure.Dist_Point_to_Point(pt, point) < tolerance)
                    {
                        selected.Add(drawobject);
                        // drawobject.IsSelected = true;
                    }
                }
                else if (ShapeType == RgEnumShapeType.RgLineString)
                {
                    LineString lineString = drawobject.Shape as LineString;
                    if (lineString.PointInObject(point))
                    {
                        selected.Add(drawobject);
                        //drawobject.IsSelected = true;
                    }
                }
                else if (ShapeType == RgEnumShapeType.RgPolygon)
                {
                    Polygon polygon = drawobject.Shape as Polygon;
                    if (!polygon.IsEmpty())
                    {
                        if (Geometries.RgTopologicRelationship.IsInPolygon(point, polygon))
                        {
                            selected.Add(drawobject);
                            //drawobject.IsSelected = true;
                        }
                    }
                }

            }
            return selected;
        }
    }
}
