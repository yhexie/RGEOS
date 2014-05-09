using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Geometry;
using RGeos.Core.PluginEngine;
using System.Drawing;

namespace RGeos.PluginEngine
{
    public class Map : IMap
    {
        public REnvelope Extent { get; set; }
        public List<ILayer> Layers { get; set; }
        public Map()
        {
            Layers = new List<ILayer>();
        }

        public float Zoom { get; set; }

        public REnvelope Size { get; set; }

        public RPoint Center;
        public double PixelAspectRatio { get; set; }
        public double PixelWidth;
        public double PixelHeight;
        IScreenDisplayOld mScreenDisplay = null;
        public IScreenDisplayOld ScreenDisplay
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

       
    }
}
