using System;

namespace RGeos.Geometries
{
    /// <summary>
    ///点是0-dimensional几何对象，代表2D空间的位置。
    /// </summary>
    [Serializable]
    public class RgPoint : Geometry, IComparable<RgPoint>
    {
        private bool _IsEmpty = false;
        private double _X;
        private double _Y;

        /// <summary>
        /// 构造Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public RgPoint(double x, double y)
        {
            _X = x;
            _Y = y;
        }

        /// <summary>
        /// Initializes a new empty Point
        /// </summary>
        public RgPoint()
            : this(0, 0)
        {
            _IsEmpty = true;
        }

        /// <summary>
        /// Create a new point by a douuble[] array
        /// </summary>
        /// <param name="point"></param>
        public RgPoint(double[] point)
        {
            if (point.Length < 2)
                throw new Exception("Only 2 dimensions are supported for points");

            _X = point[0];
            _Y = point[1];
        }
        public RgPoint(Vector3d v)
        {
            _X = v.X;
            _Y = v.Y;
        }
        /// <summary>
        /// Sets whether this object is empty
        /// </summary>
        protected bool SetIsEmpty
        {
            set { _IsEmpty = value; }
        }

        /// <summary>
        /// 点的X坐标
        /// </summary>
        public double X
        {
            get
            {
                if (!_IsEmpty)
                    return _X;
                else throw new ApplicationException("Point is empty");
            }
            set
            {
                _X = value;
                _IsEmpty = false;
            }
        }

        /// <summary>
        /// 点的Y坐标
        /// </summary>
        public double Y
        {
            get
            {
                if (!_IsEmpty)
                    return _Y;
                else throw new ApplicationException("Point is empty");
            }
            set
            {
                _Y = value;
                _IsEmpty = false;
            }
        }

        /// <summary>
        /// Returns part of coordinate. Index 0 = X, Index 1 = Y
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual double this[uint index]
        {
            get
            {
                if (_IsEmpty)
                    throw new ApplicationException("Point is empty");
                else if (index == 0)
                    return X;
                else if
                    (index == 1)
                    return Y;
                else
                    throw (new Exception("Point index out of bounds"));
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else
                    throw (new Exception("Point index out of bounds"));
                _IsEmpty = false;
            }
        }

        /// <summary>
        /// Returns the number of ordinates for this point
        /// </summary>
        public virtual int NumOrdinates
        {
            get { return 2; }
        }

        #region IComparable<Point> Members

        /// <summary>
        /// Comparator used for ordering point first by ascending X, then by ascending Y.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual int CompareTo(RgPoint other)
        {
            if (X < other.X || X == other.X && Y < other.Y)
                return -1;
            else if (X > other.X || X == other.X && Y > other.Y)
                return 1;
            else // (this.X == other.X && this.Y == other.Y)
                return 0;
        }

        #endregion

        /// <summary>
        /// 输出为2-dimensional double array
        /// </summary>
        /// <returns></returns>
        public double[] ToDoubleArray()
        {
            return new double[2] { _X, _Y };
        }

        /// <summary>
        /// Returns a point based on degrees, minutes and seconds notation.
        /// For western or southern coordinates, add minus '-' in front of all longitude and/or latitude values
        /// </summary>
        /// <param name="longDegrees">Longitude degrees</param>
        /// <param name="longMinutes">Longitude minutes</param>
        /// <param name="longSeconds">Longitude seconds</param>
        /// <param name="latDegrees">Latitude degrees</param>
        /// <param name="latMinutes">Latitude minutes</param>
        /// <param name="latSeconds">Latitude seconds</param>
        /// <returns>Point</returns>
        public static RgPoint FromDMS(double longDegrees, double longMinutes, double longSeconds,
                                    double latDegrees, double latMinutes, double latSeconds)
        {
            return new RgPoint(longDegrees + longMinutes / 60 + longSeconds / 3600,
                             latDegrees + latMinutes / 60 + latSeconds / 3600);
        }

        /// <summary>
        /// Returns a 2D <see cref="RgPoint"/> instance from this <see cref="Point3D"/>
        /// </summary>
        /// <returns><see cref="RgPoint"/></returns>
        public RgPoint AsPoint()
        {
            return new RgPoint(_X, _Y);
        }

        /// <summary>
        /// This method must be overridden using 'public new [derived_data_type] Clone()'
        /// </summary>
        /// <returns>Clone</returns>
        public new RgPoint Clone()
        {
            return new RgPoint(X, Y);
        }

        #region Operators

        /// <summary>
        /// Vector + Vector
        /// </summary>
        /// <param name="v1">Vector</param>
        /// <param name="v2">Vector</param>
        /// <returns></returns>
        public static RgPoint operator +(RgPoint v1, RgPoint v2)
        {
            return new RgPoint(v1.X + v2.X, v1.Y + v2.Y);
        }


        /// <summary>
        /// Vector - Vector
        /// </summary>
        /// <param name="v1">Vector</param>
        /// <param name="v2">Vector</param>
        /// <returns>Cross product</returns>
        public static RgPoint operator -(RgPoint v1, RgPoint v2)
        {
            return new RgPoint(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Vector * Scalar
        /// </summary>
        /// <param name="m">Vector</param>
        /// <param name="d">Scalar (double)</param>
        /// <returns></returns>
        public static RgPoint operator *(RgPoint m, double d)
        {
            return new RgPoint(m.X * d, m.Y * d);
        }
        /// <summary>
        /// Vector * Scalar
        /// </summary>
        /// <param name="m">Vector</param>
        /// <param name="d">Scalar (double)</param>
        /// <returns></returns>
        public static RgPoint operator /(RgPoint m, double d)
        {
            return new RgPoint(m.X / d, m.Y / d);
        }

        #endregion

        #region "Inherited methods from abstract class Geometry"

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.Point;
            }
        }

        /// <summary>
        ///  The inherent dimension of this Geometry object, which must be less than or equal to the coordinate dimension.
        /// </summary>
        public override int Dimension
        {
            get { return 0; }
        }

        /// <summary>
        /// Checks whether this instance is spatially equal to the Point 'o'
        /// </summary>
        /// <param name="p">Point to compare to</param>
        /// <returns></returns>
        public virtual bool Equals(RgPoint p)
        {
            return p != null && p.X == _X && p.Y == _Y && _IsEmpty == p.IsEmpty();
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="GetHashCode"/> is suitable for use 
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current <see cref="GetHashCode"/>.</returns>
        public override int GetHashCode()
        {
            return _X.GetHashCode() ^ _Y.GetHashCode() ^ _IsEmpty.GetHashCode();
        }

        /// <summary>
        /// If true, then this Geometry represents the empty point set, ? for the coordinate space. 
        /// </summary>
        /// <returns>Returns 'true' if this Geometry is the empty geometry</returns>
        public override bool IsEmpty()
        {
            return _IsEmpty;
        }

        /// <summary>
        /// Returns 'true' if this Geometry has no anomalous geometric points, such as self
        /// intersection or self tangency. The description of each instantiable geometric class will include the specific
        /// conditions that cause an instance of that class to be classified as not simple.
        /// </summary>
        /// <returns>true if the geometry is simple</returns>
        public override bool IsSimple()
        {
            return true;
        }

        /// <summary>
        /// The boundary of a point is the empty set.
        /// </summary>
        /// <returns>null</returns>
        public override Geometry Boundary()
        {
            return null;
        }

        /// <summary>
        /// Returns the distance between this geometry instance and another geometry, as
        /// measured in the spatial reference system of this instance.
        /// </summary>
        /// <param name="geom"></param>
        /// <returns></returns>
        public override double Distance(Geometry geom)
        {
            if (geom.GetType() == typeof(RgPoint))
            {
                RgPoint p = geom as RgPoint;
                return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));
            }
            else
                throw new Exception("The method or operation is not implemented for this geometry type.");
        }

        /// <summary>
        /// Returns the distance between this point and a <see cref="BoundingBox"/>
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public double Distance(BoundingBox box)
        {
            return box.Distance(this);
        }

        /// <summary>
        /// Returns a geometry that represents all points whose distance from this Geometry
        /// is less than or equal to distance. Calculations are in the Spatial Reference
        /// System of this Geometry.
        /// </summary>
        /// <param name="d">Buffer distance</param>
        /// <returns>Buffer around geometry</returns>
        public override Geometry Buffer(double d)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Geometry桼eturns a geometry that represents the convex hull of this Geometry.
        /// </summary>
        /// <returns>The convex hull</returns>
        public override Geometry ConvexHull()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a geometry that represents the point set intersection of this Geometry
        /// with anotherGeometry.
        /// </summary>
        /// <param name="geom">Geometry to intersect with</param>
        /// <returns>Returns a geometry that represents the point set intersection of this Geometry with anotherGeometry.</returns>
        public override Geometry Intersection(Geometry geom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a geometry that represents the point set union of this Geometry with anotherGeometry.
        /// </summary>
        /// <param name="geom">Geometry to union with</param>
        /// <returns>Unioned geometry</returns>
        public override Geometry Union(Geometry geom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a geometry that represents the point set difference of this Geometry with anotherGeometry.
        /// </summary>
        /// <param name="geom">Geometry to compare to</param>
        /// <returns>Geometry</returns>
        public override Geometry Difference(Geometry geom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a geometry that represents the point set symmetric difference of this Geometry with anotherGeometry.
        /// </summary>
        /// <param name="geom">Geometry to compare to</param>
        /// <returns>Geometry</returns>
        public override Geometry SymDifference(Geometry geom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The minimum bounding box for this Geometry.
        /// </summary>
        /// <returns></returns>
        public override BoundingBox GetBoundingBox()
        {
            return new BoundingBox(X, Y, X, Y);
        }

        /// <summary>
        /// Checks whether this point touches a <see cref="BoundingBox"/>
        /// </summary>
        /// <param name="box">box</param>
        /// <returns>true if they touch</returns>
        public bool Touches(BoundingBox box)
        {
            return box.Touches(this);
        }

        /// <summary>
        /// Checks whether this point touches another <see cref="Geometry"/>
        /// </summary>
        /// <param name="geom">Geometry</param>
        /// <returns>true if they touch</returns>
        public override bool Touches(Geometry geom)
        {
            if (geom is RgPoint && Equals(geom)) return true;
            throw new NotImplementedException("Touches not implemented for this feature type");
        }

        /// <summary>
        /// Checks whether this point intersects a <see cref="BoundingBox"/>
        /// </summary>
        /// <param name="box">Box</param>
        /// <returns>True if they intersect</returns>
        public bool Intersects(BoundingBox box)
        {
            return box.Contains(this);
        }

        /// <summary>
        /// Returns true if this instance contains 'geom'
        /// </summary>
        /// <param name="geom">Geometry</param>
        /// <returns>True if geom is contained by this instance</returns>
        public override bool Contains(Geometry geom)
        {
            return false;
        }

        #endregion
    }
}