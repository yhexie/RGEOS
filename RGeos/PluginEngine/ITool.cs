using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.PluginEngine
{
    public interface ITool : ICommand
    {
        void OnMouseMove(int x, int y);
        void OnMouseDown(int x, int y);
        void OnMouseUp(int x, int y);
    }
}
