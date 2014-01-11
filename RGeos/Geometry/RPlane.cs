using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometry
{
    /// <summary>
    /// 平面类
    /// </summary>
    public class RPlane : RGeometry
    {
        /// <summary>
        /// 平面上任意一点
        /// </summary>
        public RPoint P0;
        /// <summary>
        /// 法向量
        /// </summary>
        public Vector3d V;
    }
}
