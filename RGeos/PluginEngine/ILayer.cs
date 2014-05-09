using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.PluginEngine
{
    public interface ILayer
    {
        string Name { get; set; }
        string AliasName { get; set; }
        bool Visible { get; set; }
        void Draw(IScreenDisplayOld display);
       
    }
}
