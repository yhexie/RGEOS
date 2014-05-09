namespace RGeos.Converters.WellKnownBinary
{
    /// <summary>
    /// Specifies the specific binary encoding (NDR or XDR) used for a geometry byte stream
    /// </summary>
    public enum WkbByteOrder : byte
    {
        /// <summary>
        /// XDR (Big Endian) Encoding of Numeric Types
        /// </summary>
        /// <remarks>
        /// <para>The XDR representation of an Unsigned Integer is Big Endian (most significant byte first).</para>
        /// <para>The XDR representation of a Double is Big Endian (sign bit is first byte).</para>
        /// </remarks>
        Xdr = 0,
        /// <summary>
        /// NDR (Little Endian) Encoding of Numeric Types
        /// </summary>
        /// <remarks>
        /// <para>The NDR representation of an Unsigned Integer is Little Endian (least significant byte first).</para>
        /// <para>The NDR representation of a Double is Little Endian (sign bit is last byte).</para>
        /// </remarks>
        Ndr = 1
    }
}