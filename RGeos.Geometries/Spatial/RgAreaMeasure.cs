using System;
using RGeos.Geometries;

namespace RGeos.Geometries
{
    // a Point (or vector) is defined by its coordinates
    // typedef struct {int x, y, z;} Point;    // exclude z for 2D
    // a Triangle is given by three points: Point V0, V1, V2 
    // a Polygon is given by:
    //        int n = number of vertex points
    //        Point* V[] = an array of points with V[n]=V[0], V[n+1]=V[1]
    public class RgAreaMeasure
    {
        //===================================================================
        //测试三角形坐标点排列的方向
        // orientation2D_Triangle(): test the orientation of a triangle
        //    Input:  three vertex points V0, V1, V2
        //    Return: >0 for counterclockwise 
        //            =0 for none (degenerate)
        //            <0 for clockwise
        public static int orientation2D_Triangle(RgPoint V0, RgPoint V1, RgPoint V2)
        {
            return RgMath.isLeft(V0, V1, V2);
        }
        //===================================================================

        // area2D_Triangle(): compute the area of a triangle
        //    Input:  three vertex points V0, V1, V2
        //    Return: the (float) area of T
        public static float area2D_Triangle(RgPoint V0, RgPoint V1, RgPoint V2)
        {
            return (float)(RgMath.isLeft(V0, V1, V2) / 2.0);
        }
        //===================================================================

        // orientation2D_Polygon(): tests the orientation of a simple polygon
        //    Input:  int n = the number of vertices in the polygon
        //            Point* V = an array of n+1 vertices with V[n]=V[0]
        //    Return: >0 for counterclockwise 
        //            =0 for none (degenerate)
        //            <0 for clockwise
        //    Note: this algorithm is faster than computing the signed area.
        public static int orientation2D_Polygon(int n, RgPoint[] V)
        {
            // first find rightmost lowest vertex of the polygon
            int rmin = 0;
            double xmin = V[0].X;
            double ymin = V[0].X;

            for (int i = 1; i < n; i++)
            {
                if (V[i].Y > ymin)
                    continue;
                if (V[i].Y == ymin)
                {    // just as low
                    if (V[i].X < xmin)   // and to left
                        continue;
                }
                rmin = i;          // a new rightmost lowest vertex
                xmin = V[i].X;
                ymin = V[i].Y;
            }

            // test orientation at this rmin vertex
            // ccw <=> the edge leaving is left of the entering edge
            if (rmin == 0)
                return RgMath.isLeft(V[n - 1], V[0], V[1]);
            else
                return RgMath.isLeft(V[rmin - 1], V[rmin], V[rmin + 1]);
        }
        //===================================================================
        //2D空间多边形面积
        // area2D_Polygon(): computes the area of a 2D polygon
        //    Input:  int n = the number of vertices in the polygon
        //            Point* V = an array of n+2 vertices 
        //                       with V[n]=V[0] and V[n+1]=V[1]
        //    Return: the (float) area of the polygon
        public static double area2D_Polygon(int n, RgPoint[] V)
        {
            double area = 0;
            int i, j, k;     // indices

            for (i = 1, j = 2, k = 0; i <= n; i++, j++, k++)
            {
                area += V[i].X * (V[j].Y - V[k].Y);
            }
            return area / 2.0;
        }
        //===================================================================
        //3D空间任意多边形面积
        // area3D_Polygon(): computes the area of a 3D planar polygon
        //    Input:  int n = the number of vertices in the polygon
        //            Point* V = an array of n+2 vertices in a plane
        //                       with V[n]=V[0] and V[n+1]=V[1]
        //            Point N = unit normal vector of the polygon's plane
        //    Return: the (float) area of the polygon
        public static double area3D_Polygon(int n, Point3D[] V, Point3D N)
        {
            double area = 0;
            double an, ax, ay, az;  // abs value of normal and its coords
            int coord;           // coord to ignore: 1=x, 2=y, 3=z
            int i, j, k;         // loop indices

            // select largest abs coordinate to ignore for projection
            ax = (N.X > 0 ? N.X : -N.X);     // abs x-coord
            ay = (N.Y > 0 ? N.Y : -N.Y);     // abs y-coord
            az = (N.Z > 0 ? N.Z : -N.Z);     // abs z-coord

            coord = 3;                     // ignore z-coord
            if (ax > ay)
            {
                if (ax > az) coord = 1;    // ignore x-coord
            }
            else if (ay > az) coord = 2;   // ignore y-coord

            // compute area of the 2D projection
            for (i = 1, j = 2, k = 0; i <= n; i++, j++, k++)
                switch (coord)
                {
                    case 1:
                        area += (V[i].Y * (V[j].Z - V[k].Z));
                        continue;
                    case 2:
                        area += (V[i].X * (V[j].Z - V[k].Z));
                        continue;
                    case 3:
                        area += (V[i].X * (V[j].Y - V[k].Y));
                        continue;
                }

            // scale to get area before projection
            an = Math.Sqrt(ax * ax + ay * ay + az * az);  // length of normal vector
            switch (coord)
            {
                case 1:
                    area *= (an / (2 * ax));
                    break;
                case 2:
                    area *= (an / (2 * ay));
                    break;
                case 3:
                    area *= (an / (2 * az));
                    break;
            }
            return area;
        }
        //===================================================================
    }
}
