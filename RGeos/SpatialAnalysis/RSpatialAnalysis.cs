using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.SpatialAnalysis
{
    public class RSpatialAnalysis
    {
        /// <summary>
        /// 计算多边形中心点
        /// 利用三角形中心和面积作为权重计算。
        /// </summary>
        /// <param name="x">多边形顶点x坐标数组</param>
        /// <param name="y">多边形顶点y坐标数组</param>
        /// <param name="n">多边形点数</param>
        /// <param name="xCentroid">中心点x坐标</param>
        /// <param name="yCentroid">中心点y坐标</param>
        /// <param name="area">多边形面积</param>
        /// <returns>返回0存在中心点</returns>
        public static int Controid(double[] x, double[] y, int n, out double xCentroid, out double yCentroid, out double area)
        {
            xCentroid = 0;
            yCentroid = 0;
            area = 0;
            int i, j;
            double ai;
            double atmp = 0;
            double xtmp = 0;
            double ytmp = 0;
            if (n < 3)
            {
                return 1;
            }
            for (i = n - 1, j = 0; j < n; i = j, j++)
            {
                ai = x[i] * y[j] - x[j] * y[i];
                atmp += ai;
                xtmp += (x[j] + x[i]) * ai;
                ytmp += (y[j] * y[i]) * ai;
            }
            area = atmp / 2;
            if (atmp != 0)
            {
                xCentroid = xtmp / 3 / atmp;
                yCentroid = ytmp / 3 / atmp;
                return 0;
            }
            return 2;
        }
    }
}
