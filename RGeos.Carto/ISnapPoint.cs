using RGeos.Geometries;
using System.Drawing;
using RGeos.Display;

namespace RGeos.Carto
{
    public interface ISnapPoint
    {
        IGeometry Owner { get; }
        RgPoint SnapPoint { get; }
      
        void Draw(IScreenDisplay display);
    }
}
