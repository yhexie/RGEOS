using System;

namespace RGeos.Geometries
{
    /// <summary>
    /// 相交检测类
    /// </summary>
    public class HitUtil
    {
        public static bool LineIntersectWithRect(RgPoint lp1, RgPoint lp2, BoundingBox r)
        {
            if (r.Contains(lp1))
                return true;
            if (r.Contains(lp2))
                return true;

            // the rectangle bottom is top in world units and top is bottom!, confused?
            // check left
            RgPoint p3 = new RgPoint(r.Left, r.Top);
            RgPoint p4 = new RgPoint(r.Left, r.Bottom);
            if (LinesIntersect(lp1, lp2, p3, p4))
                return true;
            // check bottom
            p4.Y = r.Top;
            p4.X = r.Right;
            if (LinesIntersect(lp1, lp2, p3, p4))
                return true;
            // check right
            p3.X = r.Right;
            p3.Y = r.Top;
            p4.X = r.Right;
            p4.Y = r.Bottom;
            if (LinesIntersect(lp1, lp2, p3, p4))
                return true;
            return false;
        }
        public static bool LinesIntersect(RgPoint lp1, RgPoint lp2, RgPoint lp3, RgPoint lp4)
        {
            double x = 0;
            double y = 0;
            return LinesIntersect(lp1, lp2, lp3, lp4, ref x, ref y, false, false, false);
        }
        private static bool LinesIntersect(RgPoint lp1, RgPoint lp2, RgPoint lp3, RgPoint lp4, ref double x, ref double y,
            bool returnpoint,
            bool extendA,
            bool extendB)
        {
            // http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/
            // line a is given by P1 and P2, point of intersect for line a (Pa) and b (Pb)
            // Pa = P1 + ua ( P2 - P1 )
            // Pb = P3 + ub ( P4 - P3 )

            // ua(x) = ub(x) and ua(y) = ub (y)
            // x1 + ua (x2 - x1) = x3 + ub (x4 - x3)
            // y1 + ua (y2 - y1) = y3 + ub (y4 - y3)

            // ua = ((x4-x3)(y1-y3) - (y4-y3)(x1-x3)) / ((x4-x3)(x2-x1) - (x4-x3)(y2-y1))
            // ub = ((x2-x1)(y1-y3) - (y2-y1)(x1-x3)) / ((y4-y3)(x2-x1) - (x4-x3)(y2-y1))

            // intersect point x = x1 + ua (x2 - x1)
            // intersect point y = y1 + ua (y2 - y1) 
            double x1 = lp1.X;
            double x2 = lp2.X;
            double x3 = lp3.X;
            double x4 = lp4.X;
            double y1 = lp1.Y;
            double y2 = lp2.Y;
            double y3 = lp3.Y;
            double y4 = lp4.Y;

            double denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            if (denominator == 0) // lines are parallel
                return false;
            double numerator_ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3));
            double numerator_ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3));
            double ua = numerator_ua / denominator;
            double ub = numerator_ub / denominator;
            // if a line is not extended then ua (or ub) must be between 0 and 1
            if (extendA == false)
            {
                if (ua < 0 || ua > 1)
                    return false;
            }
            if (extendB == false)
            {
                if (ub < 0 || ub > 1)
                    return false;
            }
            if (extendA || extendB) // no need to chck range of ua and ub if check is one on lines 
            {
                x = x1 + ua * (x2 - x1);
                y = y1 + ua * (y2 - y1);
                return true;
            }
            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                if (returnpoint)
                {
                    x = x1 + ua * (x2 - x1);
                    y = y1 + ua * (y2 - y1);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 点与线相交
        /// </summary>
        /// <param name="linepoint1"></param>
        /// <param name="linepoint2"></param>
        /// <param name="testpoint"></param>
        /// <param name="halflinewidth">容差</param>
        /// <returns></returns>
        public static bool IsPointInLine(RgPoint linepoint1, RgPoint linepoint2, RgPoint testpoint, float halflinewidth)
        {
            RgPoint p1 = linepoint1;
            RgPoint p2 = linepoint2;
            RgPoint p3 = testpoint;

            // check bounding rect, this is faster than creating a new rectangle and call r.Contains
            double lineLeftPoint = Math.Min(p1.X, p2.X) - halflinewidth;
            double lineRightPoint = Math.Max(p1.X, p2.X) + halflinewidth;
            if (testpoint.X < lineLeftPoint || testpoint.X > lineRightPoint)
                return false;

            double lineBottomPoint = Math.Min(p1.Y, p2.Y) - halflinewidth;
            double lineTopPoint = Math.Max(p1.Y, p2.Y) + halflinewidth;
            if (testpoint.Y < lineBottomPoint || testpoint.Y > lineTopPoint)
                return false;

            // then check if it hits the endpoint
            if (CircleHitPoint(p1, halflinewidth, p3))
                return true;
            if (CircleHitPoint(p2, halflinewidth, p3))
                return true;

            if (p1.Y == p2.Y) // line is horizontal
            {
                double min = Math.Min(p1.X, p2.X) - halflinewidth;
                double max = Math.Max(p1.X, p2.X) + halflinewidth;
                if (p3.X >= min && p3.X <= max)
                    return true;
                return false;
            }
            if (p1.X == p2.X) // line is vertical
            {
                double min = Math.Min(p1.Y, p2.Y) - halflinewidth;
                double max = Math.Max(p1.Y, p2.Y) + halflinewidth;
                if (p3.Y >= min && p3.Y <= max)
                    return true;
                return false;
            }

            // using COS law
            // a^2 = b^2 + c^2 - 2bc COS A
            // A = ACOS ((a^2 - b^2 - c^2) / (-2bc))
            double xdiff = Math.Abs(p2.X - p3.X);
            double ydiff = Math.Abs(p2.Y - p3.Y);
            double aSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double a = Math.Sqrt(aSquare);

            xdiff = Math.Abs(p1.X - p2.X);
            ydiff = Math.Abs(p1.Y - p2.Y);
            double bSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double b = Math.Sqrt(bSquare);

            xdiff = Math.Abs(p1.X - p3.X);
            ydiff = Math.Abs(p1.Y - p3.Y);
            double cSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double c = Math.Sqrt(cSquare);
            double A = Math.Acos(((aSquare - bSquare - cSquare) / (-2 * b * c)));

            // once we have A we can find the height (distance from the line)
            // SIN(A) = (h / c)
            // h = SIN(A) * c;
            double h = Math.Sin(A) * c;

            // now if height is smaller than half linewidth, the hitpoint is within the line
            return h <= halflinewidth;
        }
        /// <summary>
        /// 圆包含点
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="hitpoint"></param>
        /// <returns></returns>
        public static bool CircleHitPoint(RgPoint center, float radius, RgPoint hitpoint)
        {
            // check bounding rect, this is faster than creating a new rectangle and call r.Contains
            double leftPoint = center.X - radius;
            double rightPoint = center.X + radius;
            if (hitpoint.X < leftPoint || hitpoint.X > rightPoint)
                return false;

            double bottomPoint = center.Y - radius;
            double topPoint = center.Y + radius;
            if (hitpoint.Y < bottomPoint || hitpoint.Y > topPoint)
                return false;

            return true;
        }
    }
}
