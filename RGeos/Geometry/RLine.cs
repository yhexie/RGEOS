using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometry
{
    /// <summary>
    /// 直线
    /// </summary>
    public class RLine : RGeometry
    {
        public RPoint P0;
        public RPoint P1;
        public REnvelope Envelop
        {
            get
            {
                if (P0 != null && P1 != null)
                {
                    double left = Math.Min(P1.X, P0.X);
                    double right = Math.Max(P0.X, P1.X);
                    double lower = Math.Min(P0.Y, P1.Y);
                    double top = Math.Max(P0.Y, P1.Y);
                    REnvelope env = new REnvelope(lower, top, left, right);
                    return env;
                }
                return null;
            }
        }
    }
}
