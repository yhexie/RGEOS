using System;

namespace RGeos.Geometries
{
    /// <summary>
    /// 3D向量类
    /// </summary>
    public class Vector3d
    {
        public double[] vector;
        private const double E = 0.0000001f;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// 
        public Vector3d()
        {
            vector = new double[3];
        }
        public Vector3d(double x, double y, double z)
        {
            vector = new double[3] { x, y, z };
        }
        public Vector3d(Vector3d vct)
        {
            vector = new double[3];
            vector[0] = vct.X;
            vector[1] = vct.Y;
            vector[2] = vct.Z;
        }
        #region 属性
        /// <summary>
        /// X向量
        /// </summary>
        public double X
        {
            get { return vector[0]; }
            set { vector[0] = value; }
        }
        /// <summary>
        /// Y向量
        /// </summary>
        public double Y
        {
            get { return vector[1]; }
            set { vector[1] = value; }
        }
        /// <summary>
        /// Z向量
        /// </summary>
        public double Z
        {
            get { return vector[2]; }
            set { vector[2] = value; }
        }
        #endregion

        #region 向量操作
        /// <summary>
        /// /// <summary>
        /// 向量加法+
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3d operator +(Vector3d lhs, Vector3d rhs)//向量加
        {
            Vector3d result = new Vector3d(lhs);
            result.X += rhs.X;
            result.Y += rhs.Y;
            result.Z += rhs.Z;
            return result;
        }
        /// <summary>
        /// 向量减-
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3d operator -(Vector3d lhs, Vector3d rhs)//向量减法
        {
            Vector3d result = new Vector3d(lhs);
            result.X -= rhs.X;
            result.Y -= rhs.Y;
            result.Z -= rhs.Z;
            return result;
        }
        /// <summary>
        /// 向量除
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3d operator /(Vector3d lhs, double rhs)//向量除以数量
        {
            if (rhs != 0)
                return new Vector3d(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
            else
                return new Vector3d(0, 0, 0);
        }
        /// <summary>
        /// 向量数乘*
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3d operator *(double lhs, Vector3d rhs)//左乘数量
        {
            return new Vector3d(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);
        }
        /// <summary>
        /// 向量数乘
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3d operator *(Vector3d lhs, double rhs)//右乘数量
        {
            return new Vector3d(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        /// <summary>
        /// 判断量向量是否相等
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>True 或False</returns>
        public static bool operator ==(Vector3d lhs, Vector3d rhs)
        {
            if (Math.Abs(lhs.X - rhs.X) < E && Math.Abs(lhs.Y - rhs.Y) < E && Math.Abs(lhs.Z - rhs.Z) < E)
                return true;
            else
                return false;
        }
        public static bool operator !=(Vector3d lhs, Vector3d rhs)
        {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "(" + X + "," + Y + "," + Z + ")";
        }
        /// <summary>
        /// 向量叉积，求与两向量垂直的向量
        /// </summary>
        public static Vector3d Cross(Vector3d v1, Vector3d v2)
        {
            Vector3d r = new Vector3d(0, 0, 0);
            r.X = (v1.Y * v2.Z) - (v1.Z * v2.Y);
            r.Y = (v1.Z * v2.X) - (v1.X * v2.Z);
            r.Z = (v1.X * v2.Y) - (v1.Y * v2.X);
            return r;
        }
        /// <summary>
        /// 向量数量积
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static double operator *(Vector3d lhs, Vector3d rhs)//
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }
        /// <summary>
        /// 内积
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double InnerMultiply(Vector3d v1, Vector3d v2)
        {
            double inner = 0.0;
            inner = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            return inner;
        }
        /// <summary>
        /// 求向量长度，向量的模
        /// </summary>
        public static double Magnitude(Vector3d v1)
        {
            return (double)Math.Sqrt((v1.X * v1.X) + (v1.Y * v1.Y) + (v1.Z * v1.Z));
        }
        /// <summary>
        /// 单位化向量
        /// </summary>
        public static Vector3d Normalize(Vector3d v1)
        {
            double magnitude = Magnitude(v1);
            v1 = v1 / magnitude;
            return v1;
        }
        #endregion
    }
}
