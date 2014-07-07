using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    public enum RgSRGeoCSType
    {
        RgSRGeoCS_Clarke1880 = 4034,// Clarke 1880. 
        RgSRGeoCS_Beijing1954 = 4214,//Beijing 1954. 
        RgSRGeoCS_WGS1984 = 4326,// WGS 1984. 
    }
    public enum RgSRGeoCS2Type
    {
        RgSRGeoCS_MajorAuxSphere_WGS1984 = 104199,// Major auxiliary sphere based upon WGS 1984. 
    }
    public enum RgSRGeoCS3Type
    {
        RgSRGeoCS_Xian1980 = 4610 //Xian 1980. 
    }
}
