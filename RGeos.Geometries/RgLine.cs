using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometries
{
    /// <summary>
    /// 直线
    /// </summary>
    public class RgLine : IGeometry
    {
        public Point3D P0;
        public Point3D P1;
        public BoundingBox Envelop
        {
            get
            {
                if (P0 != null && P1 != null)
                {
                    double left = Math.Min(P1.X, P0.X);
                    double right = Math.Max(P0.X, P1.X);
                    double lower = Math.Min(P0.Y, P1.Y);
                    double top = Math.Max(P0.Y, P1.Y);
                    BoundingBox env = new BoundingBox(lower, top, left, right);
                    return env;
                }
                return null;
            }
        }

        public ProjNet.CoordinateSystems.ICoordinateSystem SpatialReference
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

        public int Dimension
        {
            get { throw new NotImplementedException(); }
        }

        public Geometry Envelope()
        {
            throw new NotImplementedException();
        }

        public BoundingBox GetBoundingBox()
        {
            throw new NotImplementedException();
        }

        public string AsText()
        {
            throw new NotImplementedException();
        }

        public byte[] AsBinary()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public bool IsSimple()
        {
            throw new NotImplementedException();
        }

        public Geometry Boundary()
        {
            throw new NotImplementedException();
        }

        public bool Relate(Geometry other, string intersectionPattern)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Disjoint(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Touches(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Crosses(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Within(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public double Distance(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public Geometry Buffer(double d)
        {
            throw new NotImplementedException();
        }

        public Geometry ConvexHull()
        {
            throw new NotImplementedException();
        }

        public Geometry Intersection(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public Geometry Union(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public Geometry Difference(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public Geometry SymDifference(Geometry geom)
        {
            throw new NotImplementedException();
        }
    }
}
