using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RGeos.Geometries;
using RGeos.Display;

namespace RGeos.Carto
{
    public delegate void CurrentLayerChangedEventHandler(ILayer layer);
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

        public CurrentLayerChangedEventHandler CurrentLayerChanged;
        public ILayer mCurrentLayer;

        public ILayer CurrentLayer
        {
            get
            { return mCurrentLayer; }
            set
            {
                if (mCurrentLayer != value)
                {
                    mCurrentLayer = value;
                    if (CurrentLayerChanged != null)
                    {
                        CurrentLayerChanged(mCurrentLayer);
                    }
                }
            }
        }

        public void AddLayer(ILayer layer)
        {
            Layers.Add(layer);
            CurrentLayer = layer;
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
