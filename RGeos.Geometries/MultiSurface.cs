namespace RGeos.Geometries
{
    /// <summary>
    /// A MultiSurface is a two-dimensional geometric collection whose elements are surfaces. The interiors of any
    /// two surfaces in a MultiSurface may not intersect. The boundaries of any two elements in a MultiSurface may
    /// intersect at most at a finite number of points.
    /// </summary>
    /// <remarks>
    /// MultiSurface is a non-instantiable class in this specification, it defines a set of methods for its subclasses and
    /// is included for reasons of extensibility. The instantiable subclass of MultiSurface is MultiPolygon,
    /// corresponding to a collection of Polygons.
    /// </remarks>
    public abstract class MultiSurface : GeometryCollection
    {
        /// <summary>
        /// The area of this Surface, as measured in the spatial reference system of this Surface.
        /// </summary>
        public abstract double Area { get; }

        /// <summary>
        /// The mathematical centroid for this Surface as a Point.
        /// The result is not guaranteed to be on this Surface.
        /// </summary>
        public abstract RgPoint Centroid { get; }

        /// <summary>
        /// A point guaranteed to be on this Surface.
        /// </summary>
        public abstract RgPoint PointOnSurface { get; }

        /// <summary>
        ///  The inherent dimension of this Geometry object, which must be less than or equal to the coordinate dimension.
        /// </summary>
        public override int Dimension
        {
            get { return 2; }
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiSurface;
            }
        }

    }
}