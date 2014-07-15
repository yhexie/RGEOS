using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AppSurveryTools.SevenParams
{
    public class ExcelPointPair : IPointPairDAL
    {
        public PointPair Fetch(DataRow dr)
        {
            PointPair comm = new PointPair();
            object obj = dr[0];
            comm.ID = Convert.IsDBNull(obj) ? string.Empty : Convert.ToString(obj);
            obj = dr[1];
            string x1 = Convert.IsDBNull(obj) ? string.Empty : Convert.ToString(obj);
            comm.X1 = new Angle(x1).Degree;
            obj = dr[2];
            string y1 = Convert.IsDBNull(obj) ? string.Empty : Convert.ToString(obj);
            comm.Y1 = new Angle(y1).Degree;
            obj = dr[3];
            comm.Z1 = Convert.IsDBNull(obj) ? 0 : Convert.ToDouble(obj);
            obj = dr[4];

            obj = dr[5];
            comm.X2 = Convert.IsDBNull(obj) ? 0 : Convert.ToDouble(obj);
            obj = dr[6];
            comm.Y2 = Convert.IsDBNull(obj) ? 0 : Convert.ToDouble(obj);
            obj = dr[7];
            comm.Z2 = Convert.IsDBNull(obj) ? 0 : Convert.ToDouble(obj);
            return comm;
        }

        public void Insert()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Detele()
        {
            throw new NotImplementedException();
        }
        public List<PointPair> GetAllRecord()
        {
            return null;
        }
        public List<PointPair> GetAllRecord(DataTable dt)
        {
            List<PointPair> pts = new List<PointPair>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PointPair pt = Fetch(dt.Rows[i]);
                pts.Add(pt);
            }
            return pts;
        }
    }
}
