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
        esriROPBlack = 1,//Pixel is always 0.
        esriROPNotMergePen = 2,	//Pixel is the inverse of the esriROPMergePen color.
        esriROPMaskNotPen = 3,	//Pixel is a combination of the colors common to both the screen and the inverse of the pen.
        esriROPNotCopyPen = 4,	//Pixel is the inverse of the pen color.
        esriROPMaskPenNot = 5,	//Pixel is a combination of the colors common to both the pen and the inverse of the screen.
        esriROPNot = 6,//Pixel is the inverse of the screen color.
        esriROPXOrPen = 7,//Pixel is a combination of the colors in the pen and in the screen, but not in both.
        esriROPNotMaskPen = 8,	//Pixel is the inverse of the esriROPMaskPen color.
        esriROPMaskPen = 9,//Pixel is a combination of the colors common to both the pen and the screen.
        esriROPNotXOrPen = 10,//Pixel is the inverse of the esriROPXOrPen color.
        esriROPNOP = 11,//Pixel remains unchanged.
        esriROPMergeNotPen = 12,	//Pixel is a combination of the screen color and the inverse of the pen color.
        esriROPCopyPen = 13,	//Pixel is the pen color.
        esriROPMergePenNot = 14,//Pixel is a combination of the pen color and the inverse of the screen color.
        esriROPMergePen = 15,//	Pixel is a combination of the pen color and the screen color.
        esriROPWhite = 16
    }
}
