using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppSurveryTools
{
    //弧度类
    public class Radian
    {
        double rad;
      
        public Radian()
        {
        }
        public Radian(double r)
        {
            // TODO: Complete member initialization
            this.rad = r;
        }
        public Radian(Radian radian)
        {
            // TODO: Complete member initialization
            this.rad = radian.Rad;
        }
        public double Rad
        {
            get { return rad; }
            set { rad = value; }
        }
        public Angle ToAngle()
        {
            Angle angle = new Angle();
            angle.Degree = SurveryMath.Rad2Degree(rad);
            return angle;
        }
    }
}
