using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Geometry;

namespace RGeos.PluginEngine
{
    public class Map
    {
        public REnvelope Extent { get; set; }
        public List<ILayer> Layers { get; set; }
        public Map()
        {
            Layers = new List<ILayer>();
        }
    }
}
