using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RGeos.Core
{
    public interface ITool : ICommand
    {
        void OnMouseMove(int x, int y);
        void OnMouseDown(int x, int y, MouseEventArgs e);
        void OnMouseUp(int x, int y);
        void OnKeyUp(KeyEventArgs e);
        void OnKeyDown(KeyEventArgs e);
    }
}
