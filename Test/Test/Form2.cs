using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form2()
        {
            InitializeComponent();
            barButtonItem1.Enabled = false;
            barButtonItem2.Enabled = true;
            barButtonItem3.Enabled = true;
            barButtonItem4.Enabled = true;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem12.Enabled = true;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form4 new_mdi_child = new Form4();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = false;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem12.Enabled = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormRP new_mdi_child = new FormRP();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = true;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem12.Enabled = false;
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form5 new_mdi_child = new Form5();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = true;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = false;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem12.Enabled = true;
        }
    }
}