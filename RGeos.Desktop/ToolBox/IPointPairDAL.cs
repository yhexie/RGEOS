using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AppSurveryTools.SevenParams
{
    public interface IPointPairDAL
    {
        PointPair Fetch(DataRow dr);
        void Insert();
        void Update();
        void Detele();
        List<PointPair> GetAllRecord();
        List<PointPair> GetAllRecord(DataTable dt);
    }
}
