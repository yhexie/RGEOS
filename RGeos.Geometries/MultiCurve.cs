namespace RGeos.Geometries
{
    /// <summary>
    /// A MultiCurve is a one-dimensional GeometryCollection whose elements are Curves
    /// </summary>
    public abstract class MultiCurve : GeometryCollection
    {
        /// <summary>
        ///  The inherent dimension of this Geometry object, which must be less than or equal to the coordinate dimension.
        /// </summary>
        public override int Dimension
        {
            get { return 1; }
        }

        /// <summary>
        /// Returns true if this MultiCurve is closed (StartPoint=EndPoint for each curve in this MultiCurve)
        /// </summary>
        public abstract bool IsClosed { get; }

        /// <summary>
        /// The Length of this MultiCurve which is equal to the sum of the lengths of the element Curves.
        /// </summary>
        public abstract double Length { get; }

        /// <summary>
        /// Returns the number of geometries in the collection.
        /// </summary>
        public new abstract int NumGeometries { get; }

        /// <summary>
        /// Returns an indexed geometry in the collection
        /// </summary>
        /// <param name="N">Geometry index</param>
        /// <returns>Geometry at index N</returns>
        public new abstract Geometry Geometry(int N);

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiCurve;
            }
        }

    }
}