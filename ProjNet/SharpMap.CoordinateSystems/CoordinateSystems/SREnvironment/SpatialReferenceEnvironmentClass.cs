using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    class SpatialReferenceEnvironmentClass : ISpatialReferenceFactory
    {
        public IEllipsoid CreateSpheroid(int spheroidType)
        {
            switch (spheroidType)
            {
                case (int)RgSRSpheroidType.RgSRSpheroid_WGS1984:
                    break;
                case (int)RgSRSpheroidType.RgSRSpheroid_Krasovsky1940:
                    break;
                case (int)RgSRSpheroidType.RgSRSpheroid_Everest1975:
                    break;
            }
            return null;
        }

        public IDatum CreateDatum(int datumType)
        {
            throw new NotImplementedException();
        }

        public IGeographicCoordinateSystem CreateGeographicCoordinateSystem(int gcsType)
        {
            throw new NotImplementedException();
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
