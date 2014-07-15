using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppSurveryTools
{
    //角度类
    public class Angle
    {
        double mDegree;

        public double Degree
        {
            get { return mDegree; }
            set { mDegree = value; }
        }
        public Angle()
        {
        }
        //通过一个double类型数据构造
        public Angle(double degree)
        {
            mDegree = degree;
        }
        //通过度分秒构造
        public Angle(int dd,int mm,double ss)
        {
            if (true)
            {
                
            }
            mDegree = dd + mm / 60 + ss / 3600;
        }
        //通过度分秒构造
        public Angle(string strDFM)
        {
            if (strDFM.Contains("°") || strDFM.Contains("'") || strDFM.Contains("\""))
            {
                double value = 0;
                string[] valueList = strDFM.Split(new char[] { '°', '\'', '"' });
                double fen = Convert.ToDouble(valueList[1]) / 60;
                double miao = Convert.ToDouble(valueList[2]) / 3600;
                value = Convert.ToDouble(valueList[0]) + fen + miao;
                mDegree = value;
            }
            else
            {
                throw new ArgumentException("不是有效的度分秒格式，DD°MM'SS\"");
            }
        }
        public Radian ToRadian()
        {
            Radian rad = new Radian();
            rad.Rad = SurveryMath.Degree2Rad(mDegree);
            return rad;
        }
        /// <summary>
        ///将度、分、秒转为度
        /// </summary>
        ///  <param name="<number>"<度、分、秒></param>
        public double GetDoubleFormDFM(string number)
        {
            double value = 0;
            string[] valueList = number.Split(new char[] { '°', '\'', '"' });
            double fen = Convert.ToDouble(valueList[1]) / 60;
            double miao = Convert.ToDouble(valueList[2]) / 3600;
            value = Convert.ToDouble(valueList[0]) + fen + miao;
            return value;
        }
    }
}
