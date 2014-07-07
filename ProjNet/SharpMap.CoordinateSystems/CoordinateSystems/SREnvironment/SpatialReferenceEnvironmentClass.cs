using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    public class SpatialReferenceEnvironmentClass : ISpatialReferenceFactory
    {
        public IEllipsoid CreateSpheroid(int spheroidType)
        {
            IEllipsoid ellipsoid = null;
            switch (spheroidType)
            {
                case (int)RgSRSpheroidType.RgSRSpheroid_WGS1984:
                    ellipsoid = new Ellipsoid(6378137, 0, 298.257223563, true, LinearUnit.Metre, "WGS 84", "EPSG", 7030, "WGS84", "", "Inverse flattening derived from four defining parameters (semi-major axis; C20 = -484.16685*10e-6; earth's angular velocity w = 7292115e11 rad/sec; gravitational constant GM = 3986005e8 m*m*m/s/s).");
                    break;
                case (int)RgSRSpheroidType.RgSRSpheroid_Krasovsky1940:
                    ellipsoid = new Ellipsoid(6378245, 0, 298.30000000000001, true, LinearUnit.Metre, "Krasovsky_1940", "EPSG", 7024, "Krasovsky_1940", "", "");
                    break;
                case (int)RgSRSpheroid2Type.RgSRSpheroid_Xian1980:
                    ellipsoid = new Ellipsoid(6378140, 0, 298.25700000000001, true, LinearUnit.Metre, "Xian_1980", "EPSG", 7049, "Xian_1980", "", "");
                    break;
            }
            return ellipsoid;
        }

        public IHorizontalDatum CreateDatum(int datumType)
        {
            IHorizontalDatum datum = null;
            switch (datumType)
            {
                case (int)RgSRDatumType.RgSRDatum_WGS1984:
                    IEllipsoid ellipsoid = this.CreateSpheroid((int)RgSRSpheroidType.RgSRSpheroid_WGS1984);
                    datum = new HorizontalDatum(ellipsoid, null, DatumType.HD_Geocentric, "World Geodetic System 1984", "EPSG", 6326, String.Empty, "EPSG's WGS 84 datum has been the then current realisation. No distinction is made between the original WGS 84 frame, WGS 84 (G730), WGS 84 (G873) and WGS 84 (G1150). Since 1997, WGS 84 has been maintained within 10cm of the then current ITRF.", String.Empty);
                    break;
                case (int)RgSRDatumType.RgSRDatum_Beijing1954:
                    IEllipsoid ellipsoid2 = this.CreateSpheroid((int)RgSRSpheroidType.RgSRSpheroid_Krasovsky1940);
                    datum = new HorizontalDatum(ellipsoid2, null, DatumType.HD_Geocentric, "D_Beijing1954", "EPSG", 6214, String.Empty, "", String.Empty);
                    break;
                case (int)RgSRDatumType.RgSRDatum_Xian1980:
                    IEllipsoid ellipsoid3 = this.CreateSpheroid((int)RgSRSpheroid2Type.RgSRSpheroid_Xian1980);
                    datum = new HorizontalDatum(ellipsoid3, null, DatumType.HD_Geocentric, "D_Xian1980", "EPSG", 6610, String.Empty, "", String.Empty);
                    break;
            }
            return datum;
        }

        public IGeographicCoordinateSystem CreateGeographicCoordinateSystem(int gcsType)
        {
            IGeographicCoordinateSystem gcs = null;
            switch (gcsType)
            {

                case (int)RgSRGeoCSType.RgSRGeoCS_WGS1984:// WGS 1984. 
                    List<AxisInfo> axes = new List<AxisInfo>(2);
                    axes.Add(new AxisInfo("Lon", AxisOrientationEnum.East));
                    axes.Add(new AxisInfo("Lat", AxisOrientationEnum.North));
                    IHorizontalDatum datum = this.CreateDatum((int)RgSRDatumType.RgSRDatum_WGS1984);
                    gcs = new GeographicCoordinateSystem(CoordinateSystems.AngularUnit.Degrees,
                        datum, CoordinateSystems.PrimeMeridian.Greenwich, axes,
                        "WGS 84", "EPSG", 4326, String.Empty, string.Empty, string.Empty);
                    break;
                case (int)RgSRGeoCSType.RgSRGeoCS_Beijing1954:
                    List<AxisInfo> axes2 = new List<AxisInfo>(2);
                    axes2.Add(new AxisInfo("Lon", AxisOrientationEnum.East));
                    axes2.Add(new AxisInfo("Lat", AxisOrientationEnum.North));
                    IHorizontalDatum datum2 = this.CreateDatum((int)RgSRDatumType.RgSRDatum_Beijing1954);
                    gcs = new GeographicCoordinateSystem(CoordinateSystems.AngularUnit.Degrees,
                        datum2, CoordinateSystems.PrimeMeridian.Greenwich, axes2,
                        "WGS 84", "EPSG", 4214, String.Empty, string.Empty, string.Empty);
                    break;
                case (int)RgSRGeoCS3Type.RgSRGeoCS_Xian1980://Xian80. 
                    List<AxisInfo> axes3 = new List<AxisInfo>(2);
                    axes3.Add(new AxisInfo("Lon", AxisOrientationEnum.East));
                    axes3.Add(new AxisInfo("Lat", AxisOrientationEnum.North));
                    IHorizontalDatum datum3 = this.CreateDatum((int)RgSRDatumType.RgSRDatum_Xian1980);
                    gcs = new GeographicCoordinateSystem(CoordinateSystems.AngularUnit.Degrees,
                        datum3, CoordinateSystems.PrimeMeridian.Greenwich, axes3,
                        "WGS 84", "EPSG", 4610, String.Empty, string.Empty, string.Empty);
                    break;

            }
            return gcs;
        }

        public IProjectedCoordinateSystem CreateProjectedCoordinateSystem(int pcsType)
        {
            throw new NotImplementedException();
        }
        public IProjection CreateProjection(int projectionType)
        {
            switch (projectionType)
            {
                case (int)RgSRProjectionType.RgSRProjection_Albers:
                    break;
                case (int)RgSRProjectionType.RgSRProjection_GaussKruger:
                    break;
                case (int)RgSRProjectionType.RgSRProjection_Mercator:
                    break;
                case (int)RgSRProjectionType.RgSRProjection_TransverseMercator:
                    break;
                case (int)RgSRProjectionType.RgSRProjection_LambertConformalConic:
                    break;
            }
            return null;
        }
    }
}
