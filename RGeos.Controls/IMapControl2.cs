using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.PluginEngine;
using RGeos.Display;
using RGeos.Core.PluginEngine;

namespace RGeos.Controls
{
    public interface IMapControl2 : IMapControl
    {
        IScreenDisplay ScreenDisplay { get; }
    }
}
