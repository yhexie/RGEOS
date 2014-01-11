using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class RRing : IRPointCollection
    {
        /// <summary>
        /// true是内环
        /// </summary>
        public bool OutOrInerialRing;
        public List<RPoint> PointCollection { get; set; }

    }
}
