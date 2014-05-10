using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Display;

namespace RGeos.Carto
{
    public interface ILayer
    {
        string Name { get; set; }
        string AliasName { get; set; }
        bool Visible { get; set; }
        void Draw(IScreenDisplay display);

    }
}
