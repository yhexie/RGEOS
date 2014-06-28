using RGeos.Display;
using RGeos.Core;

namespace RGeos.Carto
{
    public interface IMap
    {
        ILayer CurrentLayer { get; set; }
        void AddLayer(ILayer layer);
        void RemoveLayer(ILayer layer);
        void Draw(IScreenDisplay display);
        RgeosUnits Units { get; set; }
    }
}
