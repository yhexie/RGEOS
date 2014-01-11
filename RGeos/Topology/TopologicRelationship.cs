using RGeos.Geometry;

namespace RGeos.Topology
{
    /// <summary>
    /// 拓扑关系：相交、相离
    /// </summary>
    public class TopologicRelationship
    {
        /// <summary>
        /// 点在直线上
        /// </summary>
        /// <param name="rPt"></param>
        /// <param name="rLine"></param>
        /// <returns></returns>
        public static bool IsInLine(RPoint rPt, RLine rLine)
        {
            bool flag = false;
            return flag;
        }
        /// <summary>
        /// 点是否在共线的线段上
        /// 1 = P is inside S;
        /// 0 = P is not inside S
        /// </returns> 
        /// </summary>
        /// <param name="P">a point P</param>
        /// <param name="S">a collinear segment S</param>
        /// <returns></returns>
        public static int InSegment(RPoint P, RSegment S)
        {
            if (S.P0.X != S.P1.X)
            {    // S is not vertical
                if (S.P0.X <= P.X && P.X <= S.P1.X)
                    return 1;
                if (S.P0.X >= P.X && P.X >= S.P1.X)
                    return 1;
            }
            else
            {    // S is vertical, so test y coordinate
                if (S.P0.Y <= P.Y && P.Y <= S.P1.Y)
                    return 1;
                if (S.P0.Y >= P.Y && P.Y >= S.P1.Y)
                    return 1;
            }
            return 0;
        }
        /// <summary>
        /// 点是否在线段上
        /// </summary>
        /// <param name="P">任意的点</param>
        /// <param name="S">任意线段</param>
        /// <returns></returns>
        public static int In2D_Point_Segment(RPoint P, RSegment S)
        {

            return 0;
        }

        /// <summary>
        /// 点在多边形中
        /// </summary>
        /// <param name="rPt"></param>
        /// <param name="rLine"></param>
        /// <returns></returns>
        public static bool IsInPolygon(RPoint rPt, RPolygon rPolygon)
        {
            bool flag = false;
            return flag;
        }
    }
}
