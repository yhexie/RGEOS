using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.PluginEngine
{
    public abstract class Layer : ILayer
    {
        public string Name { get; set; }
        public string AliasName { get; set; }
        public bool Visible { get; set; }
        public void Draw(IScreenDisplay display)
        {
        }


    }
}
