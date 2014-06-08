using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Display
{
    public class SimpleMarkerSymbol : ISymbol
    {
        public void Draw(Geometries.IGeometry Geometry)
        {
            throw new NotImplementedException();
        }

        public void QueryBoundary(int hDC, Core.ITransformation displayTransform, Geometries.IGeometry Geometry, Geometries.Polygon boundary)
        {
            throw new NotImplementedException();
        }

        public void ResetDC()
        {
            throw new NotImplementedException();
        }

        public RgRasterOpCode ROP2
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetupDC(int hDC, Core.ITransformation Transformation)
        {
            throw new NotImplementedException();
        }
    }
}
