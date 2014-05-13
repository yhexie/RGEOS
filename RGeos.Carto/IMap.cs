using RGeos.Display;

namespace RGeos.Carto
{
    public interface IMap
    {
        void AddLayer(ILayer layer);
        void RemoveLayer(ILayer layer);
        void Draw(IScreenDisplay display);
    }
}
