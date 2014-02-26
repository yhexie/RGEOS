using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometry
{
    public class REnvelope
    {
        public RPoint LowLeft { get; set; }
        public RPoint TopRight { get; set; }
        public double Lower { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Height
        {
            get
            {
                return (double)(Top - Lower);
            }
        }
        public double Width
        {
            get
            {
                return (double)(Right - Left);
            }
        }
        public REnvelope()
        {
        }
        public REnvelope(double low, double top, double left, double right)
        {
            Left = left;
            Top = top;
            Right = right;
            Lower = low;
        }
        public REnvelope Union(REnvelope pEnv)
        {
            return null;
        }
    }
}
