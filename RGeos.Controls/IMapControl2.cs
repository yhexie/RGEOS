using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Display;
using RGeos.Core;

namespace RGeos.Controls
{
    public interface IMapControl2 : IMapControl
    {
        RGeos.Carto.IMap Map { get; }
        IScreenDisplay ScreenDisplay { get; }
        void Refresh();
    }
}
