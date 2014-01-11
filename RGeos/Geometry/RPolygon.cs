using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometry
{

    public class RPolygon : RGeometry
    {

    }
    /// <summary>
    /// 2D凸多边形
    /// a Polygon is given by:
    ///        int n = number of vertex points
    ///        Point* V[] = an array of points with V[n]=V[0], V[n+1]=V[1]
    /// </summary>
    public class RConvexPolygon : RGeometry, IRPointCollection
    {
        public int n;
        public List<RPoint> PointCollection { get; set; }
    }
    /// <summary>
    /// 2D任意多边形
    /// </summary>
    public class RGerneralPolygon : RGeometry
    {
        public int NumOfRings { get; set; }
        public List<RRing> RingList;

        public RGerneralPolygon()
        {
            RingList = new List<RRing>();
            RRing ring = new RRing();
            RingList.Add(ring);

        }
        public RRing GetRing(int Index)
        {
            if (Index < RingList.Count)
            {
                return RingList[Index];
            }
            return null;
        }
        public void AddRing(RRing ring)
        {
            RingList.Add(ring);
        }
    }
}
