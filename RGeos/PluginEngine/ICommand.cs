using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.PluginEngine
{
    public interface ICommand
    {
        string Name { get; set; }
        void OnCreate(HookHelper hook);
        void OnClick();
    }
}
