using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    public interface ISpatialReferenceFactory
    {
        IEllipsoid CreateSpheroid(int spheroidType);
        IDatum CreateDatum(int datumType);
        IGeographicCoordinateSystem CreateGeographicCoordinateSystem(int gcsType);
        IProjection CreateProjection(int projectionType);
        IProjectedCoordinateSystem CreateProjectedCoordinateSystem(int pcsType);
    }
}
