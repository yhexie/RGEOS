using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RGeos.Core;

namespace RGeos.Plugins
{
    class SelectTool : RBaseCommand
    {
        private string mName;
        public override string Name
        {
            get
            {
                return this.mName;
            }
            set
            {
                mName = value;
            }
        }

        public override void OnCreate(HookHelper hook)
        {
            throw new NotImplementedException();
        }

        public override void OnClick()
        {
            throw new NotImplementedException();
        }

        public override void OnMouseMove(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseDown(int x, int y, System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseUp(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
