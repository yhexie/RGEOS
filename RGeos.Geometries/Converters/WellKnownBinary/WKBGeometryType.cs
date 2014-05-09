namespace RGeos.Converters.WellKnownBinary
{
    /// <summary>
    /// Enumeration to determine geometrytype in Well-known Binary
    /// </summary>
    internal enum WKBGeometryType : uint
    {
        wkbPoint = 1,
        wkbLineString = 2,
        wkbPolygon = 3,
        wkbMultiPoint = 4,
        wkbMultiLineString = 5,
        wkbMultiPolygon = 6,
        wkbGeometryCollection = 7
    }
}