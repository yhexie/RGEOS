using System;
using RGeos.Geometries;

namespace RGeos.Geometries
{
    public class RDistanceMeasure
    {
        //===================================================================
        // 两点间距离
        public static double Dist_Point_to_Point(RgPoint ptFrom, RgPoint ptTo)
        {
            return RgMath.GetDistance(ptFrom, ptTo);
        }
        //===================================================================
        // 点到直线距离（垂直距离）
        // dist_Point_to_Line(): get the distance of a point to a line
        //     Input:  a Point P and a Line L (in any dimension)
        //     Return: the shortest distance from P to L
        public static double dist_Point_to_Line(Point3D P, RgLine L)
        {
            Vector3d v = L.P1 - L.P0;
            Vector3d w = P - L.P0;
            double c1 = RgMath.dot(w, v);
            double c2 = RgMath.dot(v, v);
            double b = c1 / c2;
            Point3D Pb = L.P0 + b * v;
            return RgMath.d(P, Pb);
        }
        //===================================================================
        // 点到线段距离
        //dist_Point_to_Segment(): get the distance of a point to a segment
        //     Input:  a Point P and a Segment S (in any dimension)
        //     Return: the shortest distance from P to S，返回到线段的最短距离
        public static double dist_Point_to_Segment(Point3D P, RgSegment S)
        {
            Vector3d v = S.P1 - S.P0;
            Vector3d w = P - S.P0;
            double c1 = RgMath.dot(w, v);
            if (c1 <= 0)
                return RgMath.d(P, S.P0);
            double c2 = RgMath.dot(v, v);
            if (c2 <= c1)
                return RgMath.d(P, S.P1);
            double b = c1 / c2;
            Point3D Pb = S.P0 + b * v;
            return RgMath.d(P, Pb);
        }
        //===================================================================
        //3D空间两线段间的最短距离
        //dist3D_Segment_to_Segment():
        //    Input:  two 3D line segments S1 and S2
        //    Return: the shortest distance between S1 and S2
        public static double Dist3D_Segment_to_Segment(RgSegment S1, RgSegment S2)
        {
            Vector3d u = S1.P1 - S1.P0;
            Vector3d v = S2.P1 - S2.P0;
            Vector3d w = S1.P0 - S2.P0;
            double a = RgMath.dot(u, u);        // always >= 0
            double b = RgMath.dot(u, v);
            double c = RgMath.dot(v, v);        // always >= 0
            double d = RgMath.dot(u, w);
            double e = RgMath.dot(v, w);
            double D = a * c - b * b;       // always >= 0
            double sc, sN, sD = D;      // sc = sN / sD, default sD = D >= 0
            double tc, tN, tD = D;      // tc = tN / tD, default tD = D >= 0

            // compute the line parameters of the two closest points
            if (D < RgMath.SMALL_NUM)
            { // the lines are almost parallel
                sN = 0.0;        // force using point P0 on segment S1
                sD = 1.0;        // to prevent possible division by 0.0 later
                tN = e;
                tD = c;
            }
            else
            {                // get the closest points on the infinite lines
                sN = (b * e - c * d);
                tN = (a * e - b * d);
                if (sN < 0.0)
                {       // sc < 0 => the s=0 edge is visible
                    sN = 0.0;
                    tN = e;
                    tD = c;
                }
                else if (sN > sD)
                {  // sc > 1 => the s=1 edge is visible
                    sN = sD;
                    tN = e + b;
                    tD = c;
                }
            }

            if (tN < 0.0)
            {           // tc < 0 => the t=0 edge is visible
                tN = 0.0;
                // recompute sc for this edge
                if (-d < 0.0)
                    sN = 0.0;
                else if (-d > a)
                    sN = sD;
                else
                {
                    sN = -d;
                    sD = a;
                }
            }
            else if (tN > tD)
            {      // tc > 1 => the t=1 edge is visible
                tN = tD;
                // recompute sc for this edge
                if ((-d + b) < 0.0)
                    sN = 0;
                else if ((-d + b) > a)
                    sN = sD;
                else
                {
                    sN = (-d + b);
                    sD = a;
                }
            }
            // finally do the division to get sc and tc
            sc = (Math.Abs(sN) < RgMath.SMALL_NUM ? 0.0 : sN / sD);
            tc = (Math.Abs(tN) < RgMath.SMALL_NUM ? 0.0 : tN / tD);

            // get the difference of the two closest points
            Vector3d dP = w + (sc * u) - (tc * v);  // = S1(sc) - S2(tc)

            return RgMath.norm(dP);   // return the closest distance
        }

        // ===================================================================
        //点到平面距离
        //dist_Point_to_Plane(): get distance (and perp base) from a point to a plane
        //    Input:  P  = a 3D point
        //            PL = a  plane with point V0 and normal n
        //    Output: *B = base point on PL of perpendicular from P
        //    Return: the distance from P to the plane PL
        public static double dist_Point_to_Plane(Point3D P, RgPlane PL, Point3D B)
        {
            double sb, sn, sd;
            sn = -RgMath.dot(PL.V, (P - PL.P0));
            sd = RgMath.dot(PL.V, PL.V);
            sb = sn / sd;
            B = P + sb * PL.V;
            return RgMath.d(P, B);
        }
        //===================================================================
    }
}
