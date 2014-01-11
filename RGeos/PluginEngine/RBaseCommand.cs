using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.PluginEngine
{
    public abstract class RBaseCommand : ITool
    {
        public abstract string Name { get; set; }
        public abstract void OnCreate(HookHelper hook);
        public abstract void OnClick();
        public abstract void OnMouseMove(int x, int y);
        public abstract void OnMouseDown(int x, int y);
        public abstract void OnMouseUp(int x, int y);
    }
}
