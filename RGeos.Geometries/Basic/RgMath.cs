using System;
using RGeos.Geometries;

namespace RGeos.Geometries
{
    public class RgMath
    {
        public static double SMALL_NUM = 0.0000000001; // anything that avoids division overflow
        /// <summary>
        ///  dot product (3D) which allows vector operations in arguments
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double dot(Vector3d u, Vector3d v)
        {
            return ((u).X * (v).X + (u).Y * (v).Y + (u).Z * (v).Z);
        }
        /// <summary>
        /// 2D数量积，点乘
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double dot2(Vector3d u, Vector3d v)
        {
            return ((u).X * (v).X + (u).Y * (v).Y);
        }
        /// <summary>
        /// 2D矢量叉积，定义为(0,0),P1,P2和P1P2包围四边形的带符号面积
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double perp(Vector3d u, Vector3d v)
        {
            return ((u).X * (v).Y - (u).Y * (v).X);
        }
        /// <summary>
        /// 向量的模
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double norm(Vector3d v)
        {
            return Math.Sqrt(dot(v, v));  // norm = length of vector
        }
        /// <summary>
        /// 两点间距离
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double d(Vector3d u, Vector3d v)
        {
            return norm(u - v);       // distance = norm of difference
        }

        public static double d(RgPoint P1, RgPoint P2)
        {
            return GetDistance(P1, P2);       // distance = norm of difference
        }

        // 判断点P2在直线P0P1的左边还是在右边，还是在直线上
        //isLeft(): tests if a point is Left|On|Right of an infinite line.
        //    Input:  three points P0, P1, and P2
        //    Return: >0 for P2 left of the line through P0 and P1
        //            =0 for P2 on the line
        //            <0 for P2 right of the line
        public static int isLeft(RgPoint P0, RgPoint P1, RgPoint P2)
        {
            double l = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y));
            return (int)l;
        }
        /// <summary>
        /// 获取由两个点所形成的向量的象限角度
        /// </summary>
        /// <param name="preCoord">第一个点的坐标</param>
        /// <param name="nextCoord">第二个点的坐标</param>
        /// <returns></returns>
        public static double GetQuadrantAngle(RgPoint preCoord, RgPoint nextCoord)
        {
            return GetQuadrantAngle(nextCoord.X - preCoord.X, nextCoord.Y - preCoord.Y);
        }
        /// <summary>
        /// 由增量X和增量Y所形成的向量的象限角度
        /// 区别方位角：方位角以正北方向顺时针
        /// |                    |
        /// |  /                 |b /
        /// | / a                |^/
        /// |/_)____象限角       |/______方位角
        /// </summary>
        /// <param name="x">增量X</param>
        /// <param name="y">增量Y</param>
        /// <returns>象限角</returns>
        public static double GetQuadrantAngle(double x, double y)
        {
            double theta = Math.Atan(y / x);
            if (x > 0 && y == 0) return 0;
            if (x == 0 && y > 0) return Math.PI / 2;
            if (x < 0 && y == 0) return Math.PI;
            if (x == 0 && y < 0) return 3 * Math.PI / 2;

            if (x > 0 && y > 0) return theta;
            if (x > 0 && y < 0) return Math.PI * 2 + theta;
            if (x < 0 && y > 0) return theta + Math.PI;
            if (x < 0 && y < 0) return theta + Math.PI;
            return theta;
        }
        /// <summary>
        /// 获取由相邻的三个点A-B-C所形成的两个向量之间的夹角
        /// 向量AB，BC形成的夹角
        /// </summary>
        /// <param name="preCoord">第一个点</param>
        /// <param name="midCoord">中间点</param>
        /// <param name="nextCoord">第三个点</param>
        /// <returns></returns>
        public static double GetIncludedAngle(RgPoint preCoord, RgPoint midCoord, RgPoint nextCoord)
        {
            double innerProduct = (midCoord.X - preCoord.X) * (nextCoord.X - midCoord.X) + (midCoord.Y - preCoord.Y) * (nextCoord.Y - midCoord.Y);
            double mode1 = Math.Sqrt(Math.Pow((midCoord.X - preCoord.X), 2.0) + Math.Pow((midCoord.Y - preCoord.Y), 2.0));
            double mode2 = Math.Sqrt(Math.Pow((nextCoord.X - midCoord.X), 2.0) + Math.Pow((nextCoord.Y - midCoord.Y), 2.0));
            return Math.Acos(innerProduct / (mode1 * mode2));
        }
        /// <summary>
        /// 获取由两个点所形成的向量的模(长度)
        /// </summary>
        /// <param name="preCoord">第一个点</param>
        /// <param name="nextCoord">第二个点</param>
        /// <returns>由两个点所形成的向量的模(长度)</returns>
        public static double GetDistance(RgPoint preCoord, RgPoint nextCoord)
        {
            return Math.Sqrt(Math.Pow((nextCoord.X - preCoord.X), 2) + Math.Pow((nextCoord.Y - preCoord.Y), 2));
        }
    }
}
