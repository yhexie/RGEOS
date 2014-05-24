using System.Drawing;

namespace RGeos.Display
{
    public interface IDisplayFeedback
    {
        IScreenDisplay Display { set; }//	The display the feedback object will use.
        void MoveTo(Point pt);//Move to the new point.
        void Refresh(Graphics gc);//	Call this after a refresh to show feedback again.
        double width { get; set; }
        Color color { get; set; }
    }
}
