using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SurveyingCal;
using AppSurveryTools.SevenParams;
using AppSurveryTools;


namespace RGeos.Desktop
{
    public partial class FrmSevenParams : Form
    {
        public FrmSevenParams()
        {
            InitializeComponent();
        }
        private void frmSevenParams_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            Column1.DataPropertyName = "ID";
            Column2.DataPropertyName = "X1";
            Column3.DataPropertyName = "Y1";
            Column4.DataPropertyName = "Z1";
            Column5.DataPropertyName = "X2";
            Column6.DataPropertyName = "Y2";
            Column7.DataPropertyName = "Z2";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "";
            dlg.Filter = "Excel文件(*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string flie = dlg.FileName;
                DataTable dt=null;// = ExcelManager.GetDataFromExcelEx(flie, OfficeVersion.Office2007, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    IPointPairDAL cptDDL = new ExcelPointPair();
                    List<PointPair> pts = cptDDL.GetAllRecord(dt);
                    dataGridView1.DataSource = pts;
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            double L0 = 0;//(double)numericUpDown1.Value;//中央经线
            List<PointPair> pts = dataGridView1.DataSource as List<PointPair>;

            List<PointPair> DealData = new List<PointPair>();

            for (int i = 0; i < pts.Count; i++)
            {
                PointPair pt = pts[i];
                double b1 = pt.X1;
                double l1 = pt.Y1;
                double h1 = pt.Z1;

                double bb1 = ZMath.RADtoDMS(new Angle(b1).ToRadian().Rad);
                double ll1 = ZMath.RADtoDMS(new Angle(l1).ToRadian().Rad);

                //转换成空间直角坐标
                double X, Y, Z;
                ZMath.BLHtoXYZ(ZMath.a84, ZMath.e284, bb1, ll1, h1, out X, out Y, out Z);

                double x2 = pt.X2;
                double y2 = pt.Y2;
                double z2 = pt.Z2;
                double a0, a2, a4, a6, a8;
                ZMath.Cal_a(ZMath.a54, ZMath.e254, out a0, out a2, out a4, out a6, out a8);
                double bf = ZMath.Bf(x2, a0, a2, a4, a6, a8);//底点坐标
                Angle angle = new Angle(L0);
                double bfRadian=angle.ToRadian().Rad;
                double B, L;
                y2 = y2 - 500000;
                ZMath.Cal_BL(bf, y2, bfRadian, ZMath.a54, ZMath.e254, ZMath.e2254, out B, out L);

                //Radian rad = new Radian(B);
                double b2 = ZMath.RADtoDMS(B);
                //Radian rad2 = new Radian(L);
                double l2 = ZMath.RADtoDMS(L);

                double XX, YY, ZZ;
                //转换成空间直角坐标
                ZMath.BLHtoXYZ(ZMath.a54, ZMath.e254, b2, l2, z2, out XX, out YY, out ZZ);
                PointPair pt22 = new PointPair();
                pt22.ID = pt.ID;
                pt22.X1 = X;
                pt22.Y1 = Y;
                pt22.Z1 = Z;
                pt22.X2 = XX;
                pt22.Y2 = YY;
                pt22.Z2 = ZZ;
                DealData.Add(pt22);
            }

            double dx;
            double dy;
            double dz;
            double k;
            double ex;
            double ey;
            double ez;
            ZMath.Cal7Parameter(DealData, out  dx, out  dy, out  dz, out  k, out  ex, out  ey, out  ez);
            string res = string.Format("七参数结果如下：\nDx:{0}\nDy:{1}\nDz:{2}\nex:{3}\ney:{4}\nez:{5}\nk:{6}", dx, dy, dz, ex, ey, ez, k);
            richTextBox1.Text = res;
        }
    }
}
