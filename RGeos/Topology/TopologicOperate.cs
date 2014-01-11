using System;
using RGeos.Geometry;
using RGeos.Basic;

namespace RGeos.Topology
{
    /// <summary>
    /// 拓扑运算：求交点
    /// </summary>
    public class TopologicOperate
    {
        // perp product (2D)
        // intersect2D_2Segments(): the intersection of 2 finite 2D segments
        //    Input:  two finite segments S1 and S2
        //    Output: *I0 = intersect point (when it exists)
        //            *I1 = endpoint of intersect segment [I0,I1] (when it exists)
        //    Return: 0=disjoint (no intersect)
        //            1=intersect in unique point I0
        //            2=overlap in segment from I0 to I1
        public int Intersect2D_Segments(RSegment S1, RSegment S2, out  RPoint I0, out RPoint I1)
        {
            Vector3d u = S1.P1 - S1.P0;
            Vector3d v = S2.P1 - S2.P0;
            Vector3d w = S1.P0 - S2.P0;
            double D = RMath.perp(u, v);

            // test if they are parallel (includes either being a point)平行
            if (Math.Abs(D) < RMath.SMALL_NUM)
            {
                // S1 and S2 are parallel
                if (RMath.perp(u, w) != 0 || RMath.perp(v, w) != 0)
                {
                    I0 = null;
                    I1 = null;
                    return 0;                   // they are NOT collinear不共线
                }
                // they are collinear or degenerate两线段共线
                // check if they are degenerate points
                double du = RMath.dot2(u, u);
                double dv = RMath.dot2(v, v);
                if (du == 0 && dv == 0)
                {           // both segments are points
                    if (S1.P0 != S2.P0)         // they are distinct points不同的点
                    {
                        I0 = null;
                        I1 = null;
                        return 0;
                    }
                    I0 = S1.P0;
                    I1 = null;// they are the same point同一点
                    return 1;
                }
                if (du == 0)
                {                    // S1 is a single point
                    if (TopologicRelationship.InSegment(S1.P0, S2) == 0)  // but is not in S2
                    {
                        I0 = null;
                        I1 = null;
                        return 0;
                    }
                    I0 = S1.P0;

                    I1 = null;
                    return 1;
                }
                if (dv == 0)
                {                    // S2 a single point
                    if (TopologicRelationship.InSegment(S2.P0, S1) == 0)  // but is not in S1
                    {
                        I0 = null;
                        I1 = null;
                        return 0;
                    }
                    I0 = S2.P0;

                    I1 = null;
                    return 1;
                }
                // they are collinear segments - get overlap (or not)
                double t0, t1;                   // endpoints of S1 in eqn for S2
                Vector3d w2 = S1.P1 - S2.P0;
                if (v.X != 0)
                {
                    t0 = w.X / v.X;
                    t1 = w2.X / v.X;
                }
                else
                {
                    t0 = w.Y / v.Y;
                    t1 = w2.Y / v.Y;
                }
                if (t0 > t1)
                {                  // must have t0 smaller than t1
                    double t = t0; t0 = t1; t1 = t;    // swap if not
                }
                if (t0 > 1 || t1 < 0)
                {
                    I0 = null;
                    I1 = null;
                    return 0;     // NO overlap
                }
                t0 = t0 < 0 ? 0 : t0;              // clip to min 0
                t1 = t1 > 1 ? 1 : t1;              // clip to max 1
                if (t0 == t1)
                {                 // intersect is a point
                    I0 = S2.P0 + t0 * v;

                    I1 = null;
                    return 1;
                }

                // they overlap in a valid subsegment
                I0 = S2.P0 + t0 * v;
                I1 = S2.P0 + t1 * v;
                return 2;
            }

            // the segments are skew and may intersect in a point
            // get the intersect parameter for S1
            double sI = RMath.perp(v, w) / D;
            if (sI < 0 || sI > 1)
            {// no intersect with S1
                I0 = null;
                I1 = null;
                return 0;
            }

            // get the intersect parameter for S2
            double tI = RMath.perp(u, w) / D;
            if (tI < 0 || tI > 1)
            {// no intersect with S2
                I0 = null;
                I1 = null;
                return 0;
            }

            I0 = S1.P0 + sI * u;               // compute S1 intersect point
            {
                I1 = null;
                return 1;
            }
        }

        /// <summary>
        /// 计算两条直线的交点，p1与p2构成一条直线，p3与p4构成一条直线
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static Vector3d Intersect2D_Lines(Vector3d p1, Vector3d p2, Vector3d p3, Vector3d p4)
        {
            Vector3d pointXY = null;
            if (p1.X != p2.X)
            {
                if (p3.X != p4.X)
                {
                    double k1 = Derivative(p1, p2);
                    double b1 = p1.Y - k1 * p1.X;

                    double k2 = Derivative(p3, p4);
                    double b2 = p3.Y - k2 * p3.X;

                    if (k1 == k2)
                    {
                        throw new Exception("两条直线平行或重合，无法计算交点。");
                    }
                    else
                    {
                        double x = (b2 - b1) / (k1 - k2);
                        double y = b1 + k1 * x;

                        pointXY = new Vector3d(x, y, 0);
                    }

                }
                else
                {
                    double k1 = Derivative(p1, p2);
                    double b1 = p1.Y - k1 * p1.X;

                    double x = p3.X;
                    double y = b1 + k1 * x;

                    pointXY = new Vector3d(x, y, 0);
                }
            }
            else if (p1.X == p2.X)
            {
                if (p3.X != p4.X)
                {
                    double k2 = Derivative(p3, p4);
                    double b2 = p3.Y - k2 * p3.X;

                    double x = p1.X;
                    double y = b2 + k2 * x;

                    pointXY = new Vector3d(x, y, 0);

                }
                else
                {
                    throw new Exception("两条直线平行于Y轴，无法计算交点。");
                }
            }

            return pointXY;
        }
        /// <summary>
        /// 计算两个点的斜率
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <returns></returns>
        public static double Derivative(Vector3d point0, Vector3d point1)
        {
            double k = double.PositiveInfinity;
            if (point0.X != point1.X)
            {
                k = (point1.Y - point0.Y) / (point1.X - point0.X);
            }
            return k;
        }

        /// <summary>
        /// 角平分线上点
        /// V0-V1-V2，角V0V1V2
        /// </summary>
        /// <param name="V0">起点</param>
        /// <param name="V1">中间点</param>
        /// <param name="V2">终点</param>
        /// <param name="dLength">角平分线上的截距</param>
        /// <returns></returns>
        public RPoint Bisector(Vector3d V0, Vector3d V1, Vector3d V2, double dLength)
        {
            RPoint pt1 = new RPoint(V0);
            RPoint pt2 = new RPoint(V1);
            RPoint pt3 = new RPoint(V2);
            //通过象限角来求中平分线
            double dJ2 = RMath.GetQuadrantAngle(V2.X - V1.X, V2.Y - V1.Y);//第二条线的象限角
            double dJ1 = RMath.GetQuadrantAngle(V1.X - V0.X, V1.Y - V0.Y);//第一条线的象限角
            double dJ = 0.0;//中分线的象限角
            int bLeft = RMath.isLeft(pt1, pt2, pt3);
            double dJZ = RMath.GetIncludedAngle(pt1, pt2, pt3);//计算夹角
            dJZ = Math.PI - dJZ;
            if (bLeft > 0)
            {
                dJ = dJ2 + dJZ / 2;
                if (dJ < 0)
                    dJ = 2 * Math.PI + dJ;
                if (dJ > 2 * Math.PI)
                    dJ = dJ - 2 * Math.PI;
            }
            else
            {
                dJ = dJ2 - dJZ / 2;
                if (dJ < 0)
                    dJ = 2 * Math.PI + dJ;
                if (dJ > 2 * Math.PI)
                    dJ = dJ - 2 * Math.PI;
            }
            double dx = dLength * Math.Cos(dJ);
            double dy = dLength * Math.Sin(dJ);
            RPoint res = new RPoint();
            res.X = pt2.X + dx;
            res.Y = pt2.Y + dy;
            res.Z = 0;
            return res;

        }
    }
}
