using System;
using System.Collections.Generic;
using System.Text;

namespace ProjNet.CoordinateSystems
{
    enum RgSRSpheroidType
    {
        RgSRSpheroid_Airy1830 = 7001,//	Airy 1830.
        RgSRSpheroid_ModifiedAiry = 7002,//	Airy modified.
        RgSRSpheroid_ATS1977 = 7041,//	Average Terrestrial System 1977.
        RgSRSpheroid_Australian = 7003,//	Australian National.
        RgSRSpheroid_Bessel1841 = 7004,//	Bessel 1841.
        RgSRSpheroid_ModifiedBessel = 7005,//	Bessel modified.
        RgSRSpheroid_BesselNamibia = 7006,//	Bessel Namibia.
        RgSRSpheroid_Clarke1858 = 7007,//	Clarke 1858.
        RgSRSpheroid_Clarke1866 = 7008,//	Clarke 1866.
        RgSRSpheroid_Clarke1866Michigan = 7009,//	Clarke 1866 Michigan.
        RgSRSpheroid_Clarke1880 = 7034,//	Clarke 1880.
        RgSRSpheroid_Clarke1880Arc = 7013,//	Clarke 1880 (Arc).
        RgSRSpheroid_Clarke1880Benoit = 7010,//	Clarke 1880 (Benoit).
        RgSRSpheroid_Clarke1880IGN = 7011,//	Clarke 1880 (IGN).
        RgSRSpheroid_Clarke1880RGS = 7012,//	Clarke 1880 (RGS).
        RgSRSpheroid_Clarke1880SGA = 7014,//	Clarke 1880 (SGA).
        RgSRSpheroid_Everest1830 = 7042,//	Everest 1830.
        RgSRSpheroid_Everest1937 = 7015,//	Everest (adjustment 1937).
        RgSRSpheroid_Everest1962 = 7044,//	Everest (definition 1962).
        RgSRSpheroid_Everest1967 = 7016,//	Everest (definition 1967).
        RgSRSpheroid_Everest1975 = 7045,//	Everest (definition 1975).
        RgSRSpheroid_ModifiedEverest = 7018,//	Everest modified.
        RgSRSpheroid_GEM10C = 7031,//GEM gravity potential model.
        RgSRSpheroid_GRS1967 = 7036,//	GRS 1967 = International 1967.
        RgSRSpheroid_GRS1980 = 7019,//GRS 1980.
        RgSRSpheroid_Helmert1906 = 7020,//Helmert 1906.
        RgSRSpheroid_Indonesian = 7021,//	Indonesian National.
        RgSRSpheroid_International1924 = 7022,//	International 1924.
        RgSRSpheroid_International1967 = 7023,//	International 1967.
        RgSRSpheroid_Krasovsky1940 = 7024,//Krasovsky 1940.克拉索夫椭球
        RgSRSpheroid_NWL9D = 7025,//	Transit precise ephemeris.
        RgSRSpheroid_OSU1986F = 7032,//	OSU 1986 geoidal model.
        RgSRSpheroid_OSU1991A = 7033,//	OSU 1991 geoidal model.
        RgSRSpheroid_Plessis1817 = 7027,//	Plessis 1817.
        RgSRSpheroid_AuthalicSphere = 7035,//	Authalic sphere.
        RgSRSpheroid_Struve1860 = 7028,//Struve 1860.
        RgSRSpheroid_WarOffice = 7029,//	War Office.
        RgSRSpheroid_NWL10D = 7026,//	NWL-10D.
        RgSRSpheroid_WGS1972 = 7043,//	WGS 1972.
        RgSRSpheroid_WGS1984 = 7030,//	WGS 1984.椭球
        RgSRSpheroid_WGS1966 = 107001,//	WGS 1966.
        RgSRSpheroid_Fischer1960 = 107002,//	Fischer 1960.
        RgSRSpheroid_Fischer1968 = 107003,//	Fischer 1968.
        RgSRSpheroid_ModifiedFischer = 107004,//	Fischer modified.
        RgSRSpheroid_Hough1960 = 7053,//	Hough 1960.
        RgSRSpheroid_ModifiedEverest1969 = 7056,//	Everest modified 1969.
        RgSRSpheroid_Walbeck = 107007,//Walbeck.
        RgSRSpheroid_AuthalicSphereArcInfo = 107008,//	Authalic sphere (ARC/INFO).
        RgSRSpheroid_GRS1967Truncated = 7050,//	GRS 1967 Truncated.
        RgSRSpheroid_Clarke1866AuthalicSphere = 7052,//	Clarke 1866 Authalic Sphere.
        RgSRSpheroid_Danish1876 = 7051,//Danish 1876.
        RgSRSpheroid_PZ1990 = 7054,//	PZ 1990.
        RgSRSpheroid_Clarke1880_IntlFt = 7055,//Clarke 1880 (Intl Foot)
        RgSRSpheroid_AuthalicSphere_Intl1924 = 7057,//	Authalic sphere based upon Intl 1924.
        RgSRSpheroid_Hughes1980 = 7058,	//Hughes 1980.
        RgSRSpheroid_MajorAuxiliarySphere_WGS1984 = 107037	//Major auxiliary sphere based upon WGS 1984.
    }
}
