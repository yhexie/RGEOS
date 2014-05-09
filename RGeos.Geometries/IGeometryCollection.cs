namespace RGeos.Geometries
{
    /// <summary>
    /// Interface for a GeometryCollection. A GeometryCollection is a collection of 1 or more geometries.
    /// </summary>
    public interface IGeometryCollection : IGeometry
    {
        /// <summary>
        /// Returns the number of geometries in the collection.
        /// </summary>
        int NumGeometries { get; }

        /// <summary>
        /// Returns an indexed geometry in the collection
        /// </summary>
        /// <param name="N">Geometry index</param>
        /// <returns>Geometry at index N</returns>
        Geometry Geometry(int N);
    }
}