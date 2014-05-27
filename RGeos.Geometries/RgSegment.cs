namespace RGeos.Geometries
{
    /// <summary>
    /// 直线段
    /// </summary>
    public class RgSegment : IGeometry
    {
        public Point3D P0;
        public Point3D P1;

        public ProjNet.CoordinateSystems.ICoordinateSystem SpatialReference
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int Dimension
        {
            get { throw new System.NotImplementedException(); }
        }

        public Geometry Envelope()
        {
            throw new System.NotImplementedException();
        }

        public BoundingBox GetBoundingBox()
        {
            throw new System.NotImplementedException();
        }

        public string AsText()
        {
            throw new System.NotImplementedException();
        }

        public byte[] AsBinary()
        {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSimple()
        {
            throw new System.NotImplementedException();
        }

        public Geometry Boundary()
        {
            throw new System.NotImplementedException();
        }

        public bool Relate(Geometry other, string intersectionPattern)
        {
            throw new System.NotImplementedException();
        }

        public bool Equals(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Disjoint(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Intersects(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Touches(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Crosses(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Within(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public bool Overlaps(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public double Distance(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public Geometry Buffer(double d)
        {
            throw new System.NotImplementedException();
        }

        public Geometry ConvexHull()
        {
            throw new System.NotImplementedException();
        }

        public Geometry Intersection(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public Geometry Union(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public Geometry Difference(Geometry geom)
        {
            throw new System.NotImplementedException();
        }

        public Geometry SymDifference(Geometry geom)
        {
            throw new System.NotImplementedException();
        }
    }
}
