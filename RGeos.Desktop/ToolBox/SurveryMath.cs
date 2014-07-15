using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppSurveryTools
{
    class SurveryMath
    {
        /// <summary>
        /// 将角度转换为度分秒
        /// </summary>
        /// <param name="angle">double</param>
        /// <returns>string</returns>
        public static string Degree2Ddmmss(double degree)
        {
            string ddmmss = string.Empty;
            int dd=(int)degree;
            int mm=(int)((degree-dd)*60);
            int ss = (int)(((degree - dd) * 60 - mm) * 60);
            if (true)
            {
                
            }
            ddmmss = string.Format("{0}°{1}'{2}\"",dd,mm,ss);
            return ddmmss;
        }
        /// <summary>
        /// 度分秒转换到角度
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="mm"></param>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static double Ddmmss2Degree(int dd, int mm, int ss)
        {
            double degree = 0;
            double m = (double)mm / 60;
            double s = (double)ss / 3600;
            degree = dd + m + s;
            return degree;
        }
        //角度转换成弧度
        public static double Degree2Rad(double degree)
        {
            double rad = 0;
            rad = degree / 180 * Math.PI;
            return rad;
        }
        //角度转换成弧度
        public static double Rad2Degree(double rad)
        {
            double degree = 0;
            degree = rad * 180 / Math.PI;
            return degree;
        }
    }
}
