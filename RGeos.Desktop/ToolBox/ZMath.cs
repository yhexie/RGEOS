/*
 * 由SharpDevelop创建。
 * 用户： Charles
 * 日期: 2013/12/19
 * 时间: 9:32
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using AppSurveryTools.SevenParams;
using RGeos.Geometries;

namespace SurveyingCal
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class ZMath
    {
        public ZMath()
        {
        }

        //克拉索夫斯基椭球体
        //  长半径a=6378245 m
        // 短半径b=6356863.0187730473 m
        // c=6399698.9017827110
        // 扁率=1/298.3
        // 第一偏心率e2=0.006693421622966
        // 第二偏心率e'2=0.006738525414683
        public const double a54 = 6378245;
        public const double b54 = 6356863.0187730473;
        public const double c54 = 6399698.9017827110;
        public const double α54 = 1 / 298.3;
        public const double e254 = 0.006693421622966;
        public const double e2254 = 0.006738525414683;

        //1975年国际椭球体
        //  长半径a=6378140 m
        // 短半径b=6356755.2881575287 m
        // c=6399596.6519880105
        // 扁率=1/298.257
        // 第一偏心率e2=0.006694384999588
        // 第二偏心率e'2=0.006739501819473
        public const double a80 = 6378140;
        public const double b80 = 6356755.2881575287;
        public const double c80 = 6399596.6519880105;
        public const double α80 = 1 / 298.257;
        public const double e280 = 0.006694384999588;
        public const double e2280 = 0.006739501819473;

        //WGS-84椭球体
        //  长半径a=6378137 m
        // 短半径b=6356752.3142 m
        // c=6399593.6258
        // 扁率=1/298.257223563
        // 第一偏心率e2=0.00669437999013
        // 第二偏心率e'2=0.00673949674227
        public const double a84 = 6378137;
        public const double b84 = 6356752.3142;
        public const double c84 = 6399593.6258;
        public const double α84 = 1 / 298.257223563;
        public const double e284 = 0.00669437999013;
        public const double e2284 = 0.00673949674227;

        //2000中国大地坐标系（CGCS 2000）
        //  长半径a=6378137 m
        // 短半径b=6356752.3141 m
        // c=6399593.6259
        // 扁率=1/298.257222101
        // 第一偏心率e2=0.00669438002290
        // 第二偏心率e'2=0.00673949677548
        public const double a2000 = 6378137;
        public const double b2000 = 6356752.3141;
        public const double c2000 = 6399593.6259;
        public const double α2000 = 1 / 298.257222101;
        public const double e22000 = 0.00669438002290;
        public const double e222000 = 0.00673949677548;

        public const double pi = 3.1415926535897932384626433832795;
        public const double _2pi = 6.283185307179586476925286766559;

        public const double TORAD = pi / 180;//度转弧度
        public const double TOD = 180 / pi;// 弧度转度

        //把弧度规范化到0-360内
        public static double To_2PI(double rad)
        {
            int f = rad >= 0 ? 0 : 1;
            int n = (int)(rad / _2pi);

            return rad - n * _2pi + f * _2pi;
        }

        //将度分秒(dd.mmss)转成弧度
        public static double DMSToRAD(double dms)
        {
            int d, m, f; double s;
            f = dms >= 0 ? 1 : -1;
            dms += f * 0.0000001;//0.001秒 ＝ 4.8481368110953599358991410235795e-9弧度

            d = (int)dms;

            dms = (dms - d) * 100.0;
            m = (int)dms;

            s = (dms - m) * 100.0;

            return (d + m / 60.0 + s / 3600.0) * TORAD - f * 4.8481368110953599358991410235795e-9;
        }

        //将度分秒(ddd.mmss)化为度
        public static double DMStoDegree(double dms)
        {
            int d, m, f; double s;
            f = dms >= 0 ? 1 : -1;
            dms += f * 0.0000001;//0.001秒 ＝ 4.8481368110953599358991410235795e-9弧度  ＝2.7777777777777777777777777777778e-7度

            d = (int)dms;

            dms = (dms - d) * 100.0;
            m = (int)dms;

            s = (dms - m) * 100.0;

            return (d + m / 60.0 + s / 3600.0) - f * 2.7777777777777777777777777777778e-7;
        }

        //弧度转成度分秒(dd.mmss)
        public static double RADtoDMS(double rad)
        {
            int f = rad >= 0 ? 1 : -1;				// 符号 ＋ －
            rad = (rad + f * 4.8481368110953599358991410235795e-9) * TOD;//加0.001秒（用弧度表示），化为度

            int d = (int)rad;

            rad = (rad - d) * 60.0;
            int m = (int)rad;

            double s = (rad - m) * 60.0;

            return d + m / 100.0 + s / 10000.0 - f * 0.0000001;
        }

        //已知两点坐标计算边长
        public static double Side(double dx, double dy)
        {

            double S = Math.Sqrt(dx * dx + dy * dy);
            return S;

        }

        //已知两点坐标计算坐标方位角，用于计算起始边和终边坐标方位角
        public static double Azimuth(double x0, double y0, double x1, double y1)
        {
            double dx = x1 - x0;
            double dy = y1 - y0;
            return Math.Atan2(dy, dx) + (dy > 0 ? 0 : 1) * 2 * pi;
        }

        public static double DAzimuth(double dy, double dx)
        {


            return Math.Atan2(dy, dx) + (dy > 0 ? 0 : 1) * 2 * pi;
        }


        #region (x,y)to(B,L)高斯投影反算

        //计算系数a
        public static void Cal_a(double a, double e2, out double a0, out double a2,
                                   out double a4, out double a6, out double a8)
        {
            double m0 = a * (1 - e2);
            double m2 = 3 * e2 * m0 / 2;
            double m4 = 5 * e2 * m2 / 4;
            double m6 = 7 * e2 * m4 / 6;
            double m8 = 9 * e2 * m6 / 8;

            a0 = m0 + (m2 / 2) + (3 * m4 / 8) + (5 * m6 / 16) + (35 * m8 / 128);
            a2 = (m2 / 2) + (m4 / 2) + (15 * m6 / 32) + (7 * m8 / 16);
            a4 = (m4 / 8) + (3 * m6 / 16) + (7 * m8 / 32);
            a6 = (m6 / 32) + (m8 / 16);
            a8 = m8 / 128;
        }

        //迭代法求Bf
        public static double Bf(double x, double a0, double a2, double a4, double a6, double a8)
        {
            double Bf1 = x / a0;
            double Bf2 = (x + a2 * Math.Sin(2 * Bf1) / 2 - a4 * Math.Sin(4 * Bf1) / 4 + a6 * Math.Sin(6 * Bf1) / 6 - a8 * Math.Sin(8 * Bf1) / 8) / a0;
            while (Math.Abs(Bf2 - Bf1) > 4.8481368110953599358991410235795e-10)//0.0001秒
            {
                Bf1 = Bf2;
                Bf2 = (x + a2 * Math.Sin(2 * Bf1) / 2 - a4 * Math.Sin(4 * Bf1) / 4 + a6 * Math.Sin(6 * Bf1) / 6 - a8 * Math.Sin(8 * Bf1) / 8) / a0;
            }
            return Bf2;
        }
      

        //求B,L
        public static void Cal_BL(double Bf, double y, double L0, double a, double e2, double e22, out double B, out double L)//返回弧度
        {
            //y = y - 500000;
            double tf = Math.Tan(Bf);
            double tf2 = tf * tf;
            double tf4 = tf2 * tf2;
            double nf2 = e22 * Math.Cos(Bf) * Math.Cos(Bf);
            double nf4 = nf2 * nf2;
            double nf6 = nf2 * nf4;
            double Vf2 = 1 + nf2;
            double Nf = a / Math.Sqrt(1 - e2 * Math.Sin(Bf) * Math.Sin(Bf));
            double yNf2 = (y * y) / (Nf * Nf);
            double yNf3 = (y / Nf) * yNf2;
            double yNf4 = yNf2 * yNf2;
            double yNf5 = (y / Nf) * yNf4;
            double yNf6 = yNf2 * yNf4;
            double yNf7 = (y / Nf) * yNf6;
            B = Bf - Vf2 * tf * (yNf2 - (5 + 3 * tf2 + nf2 - 9 * nf2 * tf2) * yNf4 / 12 + (61 + 90 * tf2 + 45 * tf4 + 107 * nf2 + 162 * nf2 * tf2 + 45 * nf2 * tf4) * yNf6 / 360) / 2;
            double l = (y / Nf - (1 + 2 * tf2 + nf2) * yNf3 / 6 + (5 + 28 * tf2 + 24 * tf4 + 6 * nf2 + 8 * nf2 * tf2 - 3 * nf4 + 16 * nf4 * tf - 4 * nf6) * yNf5 / 120 - (61 + 662 * tf2 + 1320 * tf4 + 720 * tf2 * tf4 + 107 * nf2 + 440 * tf2 * nf2 + 336 * nf2 * tf4) * yNf7 / 5040) / Math.Cos(Bf);
            //B = RADtoDMS(B);
            //L = L0 + RADtoDMS(l);
            L = L0 + l;
        }

        //根据(x,y)求子午线收敛角(精确到0.001秒)
        public static double Cal_gamaxy(double Bf, double y, double a, double e2, double e22)
        {
            double gama = 0;
            double tf = Math.Tan(Bf);
            double tf2 = tf * tf;
            double tf4 = tf2 * tf2;
            double nf2 = e22 * Math.Cos(Bf) * Math.Cos(Bf);
            double nf4 = nf2 * nf2;
            double Nf = a / Math.Sqrt(1 - e2 * Math.Sin(Bf) * Math.Sin(Bf));
            double Nf3 = Nf * Nf * Nf;
            double Nf5 = Nf3 * Nf * Nf;
            gama = y * tf / Nf - y * y * y * tf * (1 + tf2 - nf2 - 2 * nf4) / (3 * Nf3) + y * y * y * y * y * tf * (2 + 5 * tf2 + 3 * tf4) / (15 * Nf5);
            return gama;
        }

        //根据(B,L)求子午线收敛角
        public static double Cal_gamaBL(double B, double L, double L0, double e22)//B、L、L0单位均为弧度
        {
            double gama = 0;
            double t = Math.Tan(B);
            double n2 = e22 * Math.Cos(B) * Math.Cos(B);
            double l = L - L0;//弧度
            gama = l * Math.Sin(B) * (1 + l * l * Math.Cos(B) * Math.Cos(B) * (1 + 3 * n2 + 2 * n2 * n2) / 3 + l * l * l * l * Math.Cos(B) * Math.Cos(B) * Math.Cos(B) * Math.Cos(B) * (2 - t * t) / 15);
            return gama;
        }
        #endregion

        #region (B,L)to(x,y)高斯投影正算
        //求x,y
        public static void Cal_xy(double B, double L, double L0, double a0, double a2, double a4, double a6, double a8,
                                    double a, double e2, double e22, out double x, out double y)
        {
            //L0=DMSToRAD(L0);
            //L=DMSToRAD(L);
            //B=DMSToRAD(B);
            double l = Math.Abs(L - L0);
            double sinB2 = Math.Sin(B) * Math.Sin(B);
            double sinB4 = sinB2 * sinB2;
            double cosB = Math.Cos(B);
            double cosB2 = cosB * cosB;
            double cosB3 = cosB * cosB2;
            double cosB4 = cosB * cosB3;
            double cosB5 = cosB * cosB4;
            double cosB6 = cosB2 * cosB4;
            double t = Math.Tan(B);
            double t2 = t * t;
            double t4 = t2 * t2;
            double n2 = e22 * cosB2;
            double N = a / Math.Sqrt(1 - e2 * sinB2);
            double l2 = l * l;
            double l3 = l * l2;
            double l4 = l2 * l2;
            double l5 = l * l4;
            double l6 = l2 * l4;
            double X = a0 * B - Math.Sin(B) * cosB * ((a2 - a4 + a6) + (2 * a4 - 16 * a6 / 3) * sinB2 + 16 * a6 * sinB4 / 3);

            x = X + N * t * cosB2 * l2 / 2 + N * t * (5 - t2 + 9 * n2 + 4 * n2 * n2) * cosB4 * l4 / 24 + N * t * (61 - 58 * t2 + t4 + 270 * n2 - 330 * n2 * t2) * cosB6 * l6 / 720;
            y = N * cosB * l + N * cosB3 * (1 - t2 + n2) * l3 / 6 + N * cosB5 * (5 - 18 * t2 + t4 + 14 * n2 - 58 * n2 * t2) * l5 / 120;
            //点在中央子午线以东为正，以西为负
            if (L - L0 < 0)
                y = -y;
        }
        #endregion

        #region (B,L,H)to(X,Y,Z)
        public static void BLHtoXYZ(double a, double e2, double B, double L, double H, out double X, out double Y, out double Z)
        {
            B = DMSToRAD(B);
            L = DMSToRAD(L);
            double N = a / Math.Sqrt(1 - e2 * Math.Sin(B) * Math.Sin(B));
            X = (N + H) * Math.Cos(B) * Math.Cos(L);
            Y = (N + H) * Math.Cos(B) * Math.Sin(L);
            Z = (N * (1 - e2) + H) * Math.Sin(B);
        }
        #endregion

        #region (X,Y,Z)to(B,L,H)
        public static void XYZtoBLH(double a, double b, double e2, double X, double Y, double Z,
                               out double B, out double L, out double H)
        {
            L = Math.Atan2(Y, X);
            double N0 = a;
            double H0 = Math.Sqrt(X * X + Y * Y + Z * Z) - Math.Sqrt(a * b);
            double B0 = Math.Atan2(Z * (N0 + H0), Math.Sqrt(X * X + Y * Y) * (N0 + H0 - e2 * N0));
            double Ni = a / Math.Sqrt(1 - e2 * Math.Sin(B0) * Math.Sin(B0));
            double Hi = Math.Sqrt(X * X + Y * Y) / Math.Cos(B0) - Ni;
            double Bi = Math.Atan2(Z * (Ni + Hi), Math.Sqrt(X * X + Y * Y) * (Ni + Hi - e2 * Ni));
            do
            {
                N0 = Ni;
                H0 = Hi;
                B0 = Bi;
                Ni = a / Math.Sqrt(1 - e2 * Math.Sin(B0) * Math.Sin(B0));
                Hi = Math.Sqrt(X * X + Y * Y) / Math.Cos(B0) - Ni;
                Bi = Math.Atan2(Z * (Ni + Hi), Math.Sqrt(X * X + Y * Y) * (Ni + Hi - e2 * Ni));
            }
            while (Bi - B0 < 4.8e-10 && Hi - H0 < 1e-6);
            B = Bi;
            H = Hi;
        }
        #endregion

        #region (x,y)to(x,y)//换带计算
        public static void xytoxy(double a, double e2, double e22, double L01, double L02, double x1, double y1, out double x2, out double y2)
        {
            double a0 = 0, a2 = 0, a4 = 0, a6 = 0, a8 = 0;
            double BF = 0, B = 0, L = 0;
            Cal_a(a, e2, out a0, out a2, out a4, out a6, out a8);
            BF = Bf(x1, a0, a2, a4, a6, a8);
            Cal_BL(BF, y1, L01, a, e2, e22, out B, out L);
            Cal_xy(B, L, L02, a0, a2, a4, a6, a8, a, e2, e22, out x2, out y2);

        }
        #endregion

        #region 计算四参数
        public static void Cal4Parameter(List<PointPair> cp, out double a, out double b, out double c, out double d, out double k, out double w)
        {
            int n = cp.Count;
            double[] B = new double[8 * n];//误差方程系数阵B
            double[] l = new double[2 * n];//常数项l
            for (int i = 0; i < n; i++)
            {
                B[8 * i] = 1;
                B[8 * i + 1] = 0;
                B[8 * i + 2] = cp[i].X1;
                B[8 * i + 3] = cp[i].Y1;
                B[8 * i + 4] = 0;
                B[8 * i + 5] = 1;
                B[8 * i + 6] = cp[i].Y1;
                B[8 * i + 7] = -cp[i].X1;
            }
            for (int i = 0; i < n; i++)
            {
                l[2 * i] = cp[i].X2;
                l[2 * i + 1] = cp[i].Y2;
            }
            //构造矩阵,权阵P=E
            Matrix MB = new Matrix(2 * n, 4, B);
            Matrix Ml = new Matrix(2 * n, 1, l);
            Matrix MBT = MB.Transpose();//BT
            Matrix MBTB = MBT.Multiply(MB);//BT*B
            Matrix _MBTB = MBTB;
            _MBTB.InvertSsgj();//求逆
            Matrix MBTMl = MBT.Multiply(Ml);

            Matrix x = _MBTB.Multiply(MBTMl);//x=btb逆*btl
            a = x.GetElement(0, 0);
            b = x.GetElement(1, 0);
            c = x.GetElement(2, 0);
            d = x.GetElement(3, 0);
            w = To_2PI(Math.Atan2(-d, c));
            k = c / Math.Cos(w);
            w = RADtoDMS(w);
        }
        #endregion

        #region 计算六参数（仿射变换）
        public static void Cal6Parameter(List<PointPair> cp, out double dx, out double dy, out double A1, out double A2, out double B1, out double B2, out double kx, out double ky, out double ex, out double ey)
        {
            int n = cp.Count;
            double[] B = new double[12 * n];
            double[] l = new double[2 * n];
            for (int i = 0; i < n; i++)
            {
                B[12 * i] = 1; B[12 * i + 1] = 0; B[12 * i + 2] = cp[i].X1; B[12 * i + 3] = -cp[i].Y1; B[12 * i + 4] = 0; B[12 * i + 5] = 0;
                B[12 * i + 6] = 0; B[12 * i + 7] = 1; B[12 * i + 8] = 0; B[12 * i + 9] = 0; B[12 * i + 10] = cp[i].X1; B[12 * i + 11] = cp[i].Y1;
            }
            for (int i = 0; i < n; i++)
            {
                l[2 * i] = cp[i].X2;
                l[2 * i + 1] = cp[i].Y2;
            }
            //构造矩阵，权阵P=E
            Matrix MB = new Matrix(2 * n, 6, B);
            Matrix Ml = new Matrix(2 * n, 1, l);
            Matrix MBT = MB.Transpose();
            Matrix MBTB = MBT.Multiply(MB);
            Matrix _MBTB = MBTB;
            _MBTB.InvertSsgj();//求逆
            Matrix MBTMl = MBT.Multiply(Ml);

            Matrix x = _MBTB.Multiply(MBTMl);//x=btb逆*btl
            dx = x.GetElement(0, 0);
            dy = x.GetElement(1, 0);
            A1 = x.GetElement(2, 0);
            A2 = x.GetElement(3, 0);
            B1 = x.GetElement(4, 0);
            B2 = x.GetElement(5, 0);
            ex = Math.Atan2(B1, A1);
            ey = Math.Atan2(A2, B2);
            kx = A1 / Math.Cos(ex);
            ky = B2 / Math.Cos(ey);
        }
        #endregion

        #region 计算七参数，根据空间直角坐标(X,Y,Z)
        public static void Cal7Parameter(List<PointPair> cp, out double dx, out double dy, out double dz, out double k,
            out double ex, out double ey, out double ez)
        {
            int n = cp.Count;
            double[] B = new double[21 * n];//误差方程系数阵B
            double[] l = new double[3 * n];//常数项l
            for (int i = 0; i < n; i++)
            {
                B[21 * i] = 1; B[21 * i + 1] = 0; B[21 * i + 2] = 0; B[21 * i + 3] = cp[i].X1; B[21 * i + 4] = 0; B[21 * i + 5] = -cp[i].Z1; B[21 * i + 6] = cp[i].Y1;
                B[21 * i + 7] = 0; B[21 * i + 8] = 1; B[21 * i + 9] = 0; B[21 * i + 10] = cp[i].Y1; B[21 * i + 11] = cp[i].Z1; B[21 * i + 12] = 0; B[21 * i + 13] = -cp[i].X1;
                B[21 * i + 14] = 0; B[21 * i + 15] = 0; B[21 * i + 16] = 1; B[21 * i + 17] = cp[i].Z1; B[21 * i + 18] = -cp[i].Y1; B[21 * i + 19] = cp[i].X1; B[21 * i + 20] = 0;
            }
            for (int i = 0; i < n; i++)
            {
                l[3 * i] = cp[i].X2 - cp[i].X1;
                l[3 * i + 1] = cp[i].Y2 - cp[i].Y1;
                l[3 * i + 2] = cp[i].Z2 - cp[i].Z1;
            }
            //构造矩阵，权阵P=E
            Matrix MB = new Matrix(3 * n, 7, B);
            Matrix Ml = new Matrix(3 * n, 1, l);
            Matrix MBT = MB.Transpose();
            Matrix MBTB = MBT.Multiply(MB);//BT*B
            Matrix _MBTB = MBTB;
            _MBTB.InvertSsgj();//求逆
            Matrix MBTMl = MBT.Multiply(Ml);

            Matrix x = _MBTB.Multiply(MBTMl);//x=btb逆*btl    
            dx = x.GetElement(0, 0);
            dy = x.GetElement(1, 0);
            dz = x.GetElement(2, 0);
            k = x.GetElement(3, 0);
            ex = RADtoDMS(x.GetElement(4, 0));
            ey = RADtoDMS(x.GetElement(5, 0));
            ez = RADtoDMS(x.GetElement(6, 0));
        }
        #endregion

        //#region 坐标转换
        //public static void CoordinatesConverse(string type,double x1,double y1,double z1,out double x2,out double y2,out double z2)
        //{

        //}
        //#endregion
    }
}
