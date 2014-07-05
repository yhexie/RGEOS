using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    interface ISpatialReferenceFactory
    {
        public IEllipsoid CreateSpheroid(int spheroidType);
        public IDatum CreateDatum(int datumType);
        public IGeographicCoordinateSystem CreateGeographicCoordinateSystem(int gcsType);
        public IProjectedCoordinateSystem CreateProjectedCoordinateSystem(int pcsType);
    }
}
