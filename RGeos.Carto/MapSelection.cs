using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Carto
{
    public class MapSelection : ISelection
    {
        private List<Feature> mSelectedFeatures;

        public List<Feature> SelectedFeatures
        {
            get { return mSelectedFeatures; }
            set { mSelectedFeatures = value; }
        }
        public MapSelection()
        {
            mSelectedFeatures = new List<Feature>();
        }
    }
}
