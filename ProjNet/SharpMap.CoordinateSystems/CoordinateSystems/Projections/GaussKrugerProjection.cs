using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems.Projections
{
    public class GaussKrugerProjection : MapProjection
    {
        private double scale_factor;	/* scale factor				*/
        private double central_meridian;//中央经线
        private double lat_origin;	//纬度起点
        private double e0, e1, e2, e3;	/* eccentricity constants		*/
        private double e, es, esp;		/* eccentricity constants		*/
        private double ml0;		/* small value m			*/
        private double false_northing;// y坐标向北偏移量
        private double false_easting;//x坐标向西偏移量

        public GaussKrugerProjection(List<ProjectionParameter> parameters)
            : this(parameters, false)
        {
        }
        double a;
        double m0, m2, m4, m6, m8, a0, a2, a4, a6, a8;
        // double xx, yy, _x, _y, BB, LL;
        public GaussKrugerProjection(List<ProjectionParameter> parameters, bool inverse)
            : base(parameters, inverse)
        {
            this.Name = "Gauss_Kruger";
            this.Authority = "EPSG";
            this.AuthorityCode = 43006;
            ProjectionParameter par_scale_factor = GetParameter("scale_factor");
            ProjectionParameter par_central_meridian = GetParameter("central_meridian");
            ProjectionParameter par_latitude_of_origin = GetParameter("latitude_of_origin");
            ProjectionParameter par_false_easting = GetParameter("false_easting");
            ProjectionParameter par_false_northing = GetParameter("false_northing");
            //Check for missing parameters
            if (par_scale_factor == null)
                throw new ArgumentException("Missing projection parameter 'scale_factor'");
            if (par_central_meridian == null)
                throw new ArgumentException("Missing projection parameter 'central_meridian'");
            if (par_latitude_of_origin == null)
                throw new ArgumentException("Missing projection parameter 'latitude_of_origin'");
            if (par_false_easting == null)
                throw new ArgumentException("Missing projection parameter 'false_easting'");
            if (par_false_northing == null)
                throw new ArgumentException("Missing projection parameter 'false_northing'");

            scale_factor = par_scale_factor.Value;
            //central_meridian = Degrees2Radians(par_central_meridian.Value);//中央经线
            central_meridian = par_central_meridian.Value;
            lat_origin = Degrees2Radians(par_latitude_of_origin.Value);
            false_easting = par_false_easting.Value * _metersPerUnit;
            false_northing = par_false_northing.Value * _metersPerUnit;

            /* 
             * a = 6378140;  //西安80椭球 IGA75，长轴？
             * e2 = 0.006694384999588;//扁率 alpha=(a-b)/a
             * m0 = a * (1 - e2);//短轴
             */

            a = this._semiMajor;
            e2 = (this._semiMajor - this._semiMinor) / this._semiMajor;
            m0 = this._semiMinor;
            m2 = 3.0 / 2 * e2 * m0;
            m4 = 5.0 / 4 * e2 * m2;
            m6 = 7.0 / 6 * e2 * m4;
            m8 = 9.0 / 8 * e2 * m6;
            a0 = m0 + m2 / 2 + (3.0 / 8.0) * m4 + (5.0 / 16.0) * m6 + (35.0 / 128.0) * m8;
            a2 = m2 / 2 + m4 / 2 + 15.0 / 32 * m6 + 7.0 / 16 * m8;
            a4 = m4 / 8 + 3.0 / 16 * m6 + 7.0 / 32 * m8;
            a6 = m6 / 32 + m8 / 16;
            a8 = m8 / 128;

        }
        public override double[] MetersToDegrees(double[] p)
        {
            double x = p[0] * _metersPerUnit - false_easting;
            double y = p[1] * _metersPerUnit - false_northing;
            double lon, lat;
            GaussNegative(x, y, central_meridian, out lat, out lon);
            if (p.Length < 3)
                return new double[] { lon, lat };
            else
                return new double[] { lon, lat, p[2] };
        }

        public override double[] DegreesToMeters(double[] lonlat)
        {
            double lon = lonlat[0];
            double lat = lonlat[1];
            double x, y;
            GaussPositive(lat, lon, central_meridian, out x, out y);
            if (lonlat.Length < 3)
                return new double[] { x / _metersPerUnit, y / _metersPerUnit };
            else
                return new double[] { x / _metersPerUnit, y / _metersPerUnit, lonlat[2] };
        }

        public override Transformations.IMathTransform Inverse()
        {
            throw new NotImplementedException();
        }
        //高斯正算
        void GaussPositive(double B, double L, double L0, out double xx, out double yy)
        {
            double X, t, N, h2, l, m;
            B = Degrees2Radians(B);
            L = Degrees2Radians(L);
            // int Bdu, Bfen, Ldu, Lfen, Bmiao, Lmiao;
            //Bdu = (int)B;
            //Bfen = (int)(B * 100) % 100;
            //Bmiao = (B - Bdu - Bfen * 0.01) * 10000.0;
            //B = Bdu * PI / 180.0 + (Bfen / 60.0) * PI / 180.0 + Bmiao / 3600.0 * PI / 180.0;
            //Ldu = (int)L;
            //Lfen = (int)(L * 100) % 100;
            //Lmiao = (L - Ldu - Lfen * 0.01) * 10000.0;
            //L = Ldu * PI / 180.0 + (Lfen / 60.0) * PI / 180 + Lmiao / 3600.0 * PI / 180.0;
            l = L - L0 * PI / 180;
            X = a0 * B - Math.Sin(B) * Math.Cos(B) * ((a2 - a4 + a6) + (2 * a4 - 16.0 / 3.0 * a6) * Math.Sin(B) * Math.Sin(B) + 16.0 / 3.0 * a6 * Math.Pow(Math.Sin(B), 4)) + a8 / 8.0 * Math.Sin(8 * B);
            t = Math.Tan(B);
            h2 = e2 / (1 - e2) * Math.Cos(B) * Math.Cos(B);
            N = a / Math.Sqrt(1 - e2 * Math.Sin(B) * Math.Sin(B));
            m = Math.Cos(B) * l;
            xx = X + N * t * ((0.5 + (1.0 / 24.0 * (5 - t * t + 9 * h2 + 4 * h2 * h2) + 1.0 / 720.0 * (61 - 58 * t * t + Math.Pow(t, 4)) * m * m) * m * m) * m * m);
            yy = N * ((1 + (1.0 / 6.0 * (1 - t * t + h2) + 1.0 / 120.0 * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * h2 - 58 * h2 * t * t) * m * m) * m * m) * m);
            yy = yy + false_easting;
        }
        //高斯反算
        void GaussNegative(double x, double y, double L0, out  double BB, out  double LL)
        {
            double Bf, Vf, l, tf, hf2, Nf, Bmiao, Lmiao;
            int Bdu, Bfen, Ldu, Lfen;
            // y = y - 500000;
            Bf = hcfansuan(x);
            Vf = Math.Sqrt(1 + e2 / (1 - e2) * Math.Cos(Bf) * Math.Cos(Bf));
            tf = Math.Tan(Bf);
            hf2 = e2 / (1 - e2) * Math.Cos(Bf) * Math.Cos(Bf);
            Nf = a / Math.Sqrt(1 - e2 * Math.Sin(Bf) * Math.Sin(Bf));
            BB = (Bf - 0.5 * Vf * Vf * tf * (Math.Pow(y / Nf, 2) - 1.0 / 12 * (5 + 3 * tf * tf + hf2 - 9 * hf2 * tf * tf) * Math.Pow(y / Nf, 4) + 1.0 / 360 * (61 + 90 * tf * tf + 45 * tf * tf) * Math.Pow(y / Nf, 6))) * 180 / PI;
            Bdu = (int)BB;
            Bfen = (int)((BB - Bdu) * 60);
            Bmiao = ((BB - Bdu) * 60 - Bfen) * 60;
            BB = Bdu + 0.01 * Bfen + 0.0001 * Bmiao;
            l = 1.0 / Math.Cos(Bf) * (y / Nf - 1.0 / 6.0 * (1 + 2 * tf * tf + hf2) * Math.Pow(y / Nf, 3) + 1.0 / 120.0 * (5 + 28 * tf * tf + 24 * Math.Pow(tf, 4) + 6 * hf2 + 8 * hf2 * tf * tf) * Math.Pow(y / Nf, 5)) * 180.0 / PI;
            LL = L0 + l;
            Ldu = (int)LL;
            Lfen = (int)((LL - Ldu) * 60);
            Lmiao = ((LL - Ldu) * 60 - Lfen) * 60;
            LL = Ldu + 0.01 * Lfen + 0.0001 * Lmiao;
        }
        //弧长反算
        double hcfansuan(double pX)
        {
            double Bf0 = pX / a0;
            double Bf1, Bf2;
            Bf1 = Bf0;
            Bf2 = (pX - F(Bf1)) / a0;
            while ((Bf2 - Bf1) > 1.0E-11)
            {
                Bf1 = Bf2;
                Bf2 = (pX - F(Bf1)) / a0;
            }
            return Bf1;
        }
        double F(double bf1)
        {
            double BF1 = a2 * Math.Sin(2 * bf1) / 2 - a4 * Math.Sin(4 * bf1) / 4 + a6 * Math.Sin(6 * bf1) / 6 - a8 * Math.Sin(8 * bf1) / 8;
            return -BF1;
        }

    }
}
