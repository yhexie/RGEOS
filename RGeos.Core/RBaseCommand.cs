using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RGeos.Core
{
    public abstract class RBaseCommand : ITool
    {
        public abstract string Name { get; set; }

        public abstract void OnCreate(HookHelper hook);

        public abstract void OnClick();

        public abstract void OnMouseMove(int x, int y);

        // public abstract void OnMouseDown(int x, int y);
        public abstract void OnMouseDown(int x, int y, MouseEventArgs e);

        public abstract void OnMouseUp(int x, int y);

        public virtual void OnKeyUp(KeyEventArgs e)
        {
        }

        public virtual void OnKeyDown(KeyEventArgs e)
        {
        }
    }
}
