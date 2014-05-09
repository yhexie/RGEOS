using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RGeos.PluginEngine
{
    public interface IDisplayFeedback
    {
        IScreenDisplayOld Display { set; }//	The display the feedback object will use.
        void MoveTo(Point pt);//Move to the new point.
        void Refresh(Graphics gc);//	Call this after a refresh to show feedback again.
        double width { get; set; }
        Color color { get; set; }
    }
}
