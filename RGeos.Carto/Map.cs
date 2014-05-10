using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RGeos.Geometries;
using RGeos.Display;

namespace RGeos.Carto
{
    public class Map : IMap
    {
        public BoundingBox Extent { get; set; }
        public List<ILayer> Layers { get; set; }
        public Map()
        {
            Layers = new List<ILayer>();
        }
        IScreenDisplay mScreenDisplay = null;
        public IScreenDisplay ScreenDisplay
        {
            get
            {
                return mScreenDisplay;
            }
        }
        public ISelection Selection { get; set; }

        public void AddLayer(ILayer layer)
        {
            Layers.Add(layer);
        }
        public void RemoveLayer(ILayer layer)
        {
            Layers.Remove(layer);
        }
        public void Draw(IScreenDisplay display)
        {
            mScreenDisplay = display;
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Draw(mScreenDisplay);
            }
        }
    }
}
