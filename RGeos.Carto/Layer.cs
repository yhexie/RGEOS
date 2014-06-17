using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Display;
using RGeos.Geometries;
using System.Drawing;

namespace RGeos.Carto
{
    public abstract class Layer : ILayer
    {
        public string Name { get; set; }
        public string AliasName { get; set; }
        private bool mVisible = true;

        public bool Visible
        {
            get { return mVisible; }
            set { mVisible = value; }
        }
        private BoundingBox mEnvelope;
        public virtual BoundingBox Envelope
        {
            get { return mEnvelope; }
        }
        public virtual void Draw(IScreenDisplay display)
        {
        }
    }
  
}
