namespace RGeos.Geometries
{
    /// <summary>
    /// A Curve is a one-dimensional geometric object usually stored as a sequence of points,
    /// with the subtype of Curve specifying the form of the interpolation between points.
    /// </summary>
    public abstract class Curve : Geometry
    {
        /// <summary>
        ///  The inherent dimension of this Geometry object, which must be less than or equal to the coordinate dimension.
        /// </summary>
        public override int Dimension
        {
            get { return 1; }
        }

        /// <summary>
        /// The length of this Curve in its associated spatial reference.
        /// </summary>
        public abstract double Length { get; }

        /// <summary>
        /// The start point of this Curve.
        /// </summary>
        public abstract RgPoint StartPoint { get; }

        /// <summary>
        /// The end point of this Curve.
        /// </summary>
        public abstract RgPoint EndPoint { get; }

        /// <summary>
        /// Returns true if this Curve is closed (StartPoint = EndPoint).
        /// </summary>
        public bool IsClosed
        {
            get { return (StartPoint.Equals(EndPoint)); }
        }

        /// <summary>
        /// true if this Curve is closed (StartPoint = EndPoint) and
        /// this Curve is simple (does not pass through the same point more than once).
        /// </summary>
        public abstract bool IsRing { get; }

        /// <summary>
        /// The position of a point on the line, parameterised by length.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public abstract RgPoint Value(double t);

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.Curve;
            }
        }
    }
}