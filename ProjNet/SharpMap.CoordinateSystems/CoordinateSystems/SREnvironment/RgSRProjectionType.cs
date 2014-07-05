using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    /// <summary>
    /// 目前支持的投影类型
    /// </summary>
    enum RgSRProjectionType
    {
        RgSRProjection_Mercator = 43004,//Mercator.
        RgSRProjection_GaussKruger = 43005,//Gauss-Kruger.
        RgSRProjection_TransverseMercator = 43006,//Transverse Mercator.
        RgSRProjection_Albers = 43007,//Albers.
        RgSRProjection_LambertConformalConic = 43020 //Lambert Conformal Conic.
    }
}
