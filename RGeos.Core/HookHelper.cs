using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Core
{
    public class HookHelper
    {
        public static HookHelper Hook;
        public static HookHelper Instance()
        {
            if (Hook==null)
            {
                Hook = new HookHelper();
            }
            return Hook;
        }
        private HookHelper()
        {
        }
        public IMapControl MapControl;
    }
}
