using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Geometries;
using RGeos.Core;

namespace RGeos.Display
{
    public interface ISymbol
    {
        void Draw(IGeometry Geometry);
        void QueryBoundary(int hDC, ITransformation displayTransform, IGeometry Geometry, Polygon boundary);
        void ResetDC();
        RgRasterOpCode ROP2 { get; set; }
        void SetupDC(int hDC, ITransformation Transformation);
    }
    /// <summary>
    /// Binary Raster op-codes for symbol drawing.
    /// </summary>
    public enum RgRasterOpCode
    {
        RgROPBlack = 1,//Pixel is always 0.
        RgROPNotMergePen = 2,	//Pixel is the inverse of the RgROPMergePen color.
        RgROPMaskNotPen = 3,	//Pixel is a combination of the colors common to both the screen and the inverse of the pen.
        RgROPNotCopyPen = 4,	//Pixel is the inverse of the pen color.
        RgROPMaskPenNot = 5,	//Pixel is a combination of the colors common to both the pen and the inverse of the screen.
        RgROPNot = 6,//Pixel is the inverse of the screen color.
        RgROPXOrPen = 7,//Pixel is a combination of the colors in the pen and in the screen, but not in both.
        RgROPNotMaskPen = 8,	//Pixel is the inverse of the RgROPMaskPen color.
        RgROPMaskPen = 9,//Pixel is a combination of the colors common to both the pen and the screen.
        RgROPNotXOrPen = 10,//Pixel is the inverse of the RgROPXOrPen color.
        RgROPNOP = 11,//Pixel remains unchanged.
        RgROPMergeNotPen = 12,	//Pixel is a combination of the screen color and the inverse of the pen color.
        RgROPCopyPen = 13,	//Pixel is the pen color.
        RgROPMergePenNot = 14,//Pixel is a combination of the pen color and the inverse of the screen color.
        RgROPMergePen = 15,//	Pixel is a combination of the pen color and the screen color.
        RgROPWhite = 16
    }
}
