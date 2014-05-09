
using RGeos.PluginEngine;
namespace RGeos.Core.PluginEngine
{
    public interface IMapControl
    {
        ITool CurrentTool { get; set; }
    }
    public interface IMapControlOld : IMapControl
    {
        IScreenDisplayOld ScreenDisplay { get; }
    }
}
