using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RGeos.Geometries;
using RGeos.Display;
using RGeos.Core;

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

        public RgeosUnits Units
        {
            get;
            set;
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
        //容差
        double tolerance = 0.5;
        /// <summary>
        /// 几何对象集合执行的获取捕捉点的方法
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="runningsnaptypes"></param>
        /// <param name="usersnaptype"></param>
        /// <returns></returns>
        public ISnapPoint SnapPoint(RgPoint point, Type[] runningsnaptypes, Type usersnaptype)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                ILayer lry = Layers[i];
                if (lry is FetureLayer)
                {
                    FetureLayer featlyr = lry as FetureLayer;
                    List<Feature> objects = featlyr.GetHitObjects(point, tolerance);
                    if (objects.Count == 0)
                        return null;

                    foreach (Feature obj in objects)
                    {
                        IGeometry geo = obj.Shape as IGeometry;
                        if (geo is RgPoint)
                        {
                            RgPoint pt = geo as RgPoint;
                            if (RDistanceMeasure.Dist_Point_to_Point(pt, point) < tolerance)
                            {
                                return new SnapPointBase(pt, pt);
                            }
                            // ISnapPoint snap = obj.SnapPoint(point, objects, runningsnaptypes, usersnaptype);
                        }
                        if (geo is LinearRing)
                        {

                        }
                        if (geo is Polygon)
                        {

                        }
                        // ISnapPoint snap = obj.SnapPoint( point, objects, runningsnaptypes, usersnaptype);
                        //if (snap != null)
                        //    return snap;
                    }
                }

            }

            return null;
        }
    }
}
