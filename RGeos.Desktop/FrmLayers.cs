using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGeos.Carto;

namespace RGeos.Desktop
{
    public partial class FrmLayers : Form
    {
        Map mMap;
        public FrmLayers(Map map)
        {
            InitializeComponent();
            mMap = map;
        }
        ListViewItem liCurrent = null;
        private void FrmLayers_Load(object sender, EventArgs e)
        {
            foreach (var item in mMap.Layers)
            {
                ILayer lyr = item;
                ListViewItem li = new ListViewItem();
                if (lyr is FetureLayer)
                {
                    FetureLayer featLyr = lyr as FetureLayer;
                    RgEnumShapeType shapeType = featLyr.ShapeType;
                    li.Text = lyr.Name;
                    // li.SubItems[0].Text = i.ToString();
                    li.SubItems.Add(shapeType.ToString());
                    if (mMap.CurrentLayer == lyr)
                    {
                        li.SubItems.Add("是");
                        li.ImageIndex = 1;
                        liCurrent = li;
                    }
                    else
                    {
                        li.SubItems.Add("否");
                        li.ImageIndex = 0;
                    }
                    if (lyr.Visible)
                    {
                        li.Checked = true;
                    }
                    li.Tag = lyr;
                    listView1.Items.Add(li);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[2].Text == "是")
                {
                    ILayer lyr = item.Tag as ILayer;
                    if (lyr is FetureLayer)
                    {
                        mMap.CurrentLayer = lyr;
                        break;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                string strSender_id = listView1.SelectedItems[0].SubItems[2].Text;
                liCurrent.SubItems[2].Text = "否";
                liCurrent.ImageIndex = 0;
                listView1.SelectedItems[0].SubItems[2].Text = "是";
                liCurrent = listView1.SelectedItems[0];
                liCurrent.ImageIndex = 1;
            }
        }
    }
}
