using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometry
{
    /// <summary>
    /// 点
    /// </summary>
    public class RPoint : RGeometry
    {
        double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        double z;

        public double Z
        {
            get { return z; }
            set { z = value; }
        }
        public RPoint()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.z = 0.0f;
        }
        public RPoint(double _x, double _y, double _z)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
        }
        public RPoint(Vector3d v)
        {
            this.x = v.X;
            this.y = v.Y;
            this.z = v.Z;
        }
        public void Move(double dx, double dy, double dz)
        {
            this.x += dx;
            this.y += dy;
            this.z += dz;
        }
        /// <summary>
        /// 点相减的一个向量
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Vector3d operator -(RPoint p1, RPoint p2)
        {
            Vector3d Vec = new Vector3d();
            Vec.X = (p1.x - p2.x);
            Vec.Y = (p1.y - p2.y);
            Vec.Z = (p1.z - p2.z);
            return Vec;
        }
        //public static bool operator ==(RPoint p1, RPoint p2)
        //{
        //    return (p1.x == p2.x) && (p1.y == p2.y) && (p1.z == p2.z);
        //}
        //public static bool operator !=(RPoint p1, RPoint p2)
        //{
        //    return (p1.x != p2.x) || (p1.y != p2.y) || (p1.z != p2.z);

        //}
        public static RPoint operator +(RPoint Pts, Vector3d V3)//向量除以数量
        {
            RPoint temp = new RPoint();
            temp.X = Pts.X + V3.X;
            temp.Y = Pts.Y + V3.Y;
            temp.Z = Pts.Z + V3.Z;
            return temp;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
