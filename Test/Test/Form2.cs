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
using Test.Model1;

namespace Test
{
    public partial class Form2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {

     
        //tat tab khi mo 1 tab khac
        public void DisposeAllButThis(Form form)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm!=null
                    && frm != form)
                {
                    frm.Dispose();
                    frm.Close();
                }
            }
        }
        //Khoi tao Form
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
            barButtonItem13.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem12.Enabled = true;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }
        //Mo Form Sinh Vien
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
            barButtonItem13.Enabled = true;
            barButtonItem12.Enabled = true;
            DisposeAllButThis(new_mdi_child);
        }

       
        //Mo Form Report
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
            barButtonItem13.Enabled = true;
            barButtonItem12.Enabled = false;
            DisposeAllButThis(new_mdi_child);
        }
        //Mo Form Bang Diem
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
            barButtonItem13.Enabled = true;
            barButtonItem12.Enabled = true;
            DisposeAllButThis(new_mdi_child);
        }
        //Mo Form Khoa
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmKhoa new_mdi_child = new frmKhoa();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = true;
            barButtonItem5.Enabled = false;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem13.Enabled = true;
            barButtonItem12.Enabled = true;
            DisposeAllButThis(new_mdi_child);
        }
        //Mo Form Mon hoc
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form6 new_mdi_child = new Form6();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = true;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = false;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem13.Enabled = true;
            barButtonItem12.Enabled = true;
            DisposeAllButThis(new_mdi_child);
        }
        //Mo Form Lop
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmLop new_mdi_child = new frmLop();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = true;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = false;
            barButtonItem11.Enabled = true;
            barButtonItem13.Enabled = true;
            barButtonItem12.Enabled = true;
            DisposeAllButThis(new_mdi_child);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormDoiPass fr = new FormDoiPass();
            this.Hide();
            fr.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("Dang Xuat Thanh Cong");
            Form3 fr = new Form3();
            this.Hide();
            fr.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            Formtopo new_mdi_child = new Formtopo();
            new_mdi_child.Text = "Cửa sổ con MDI";
            new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
            barButtonItem2.Enabled = true;
            barButtonItem5.Enabled = true;
            barButtonItem8.Enabled = true;
            barButtonItem9.Enabled = true;
            barButtonItem10.Enabled = true;
            barButtonItem11.Enabled = true;
            barButtonItem12.Enabled = true;
            barButtonItem13.Enabled = false;
            DisposeAllButThis(new_mdi_child);
        }
    }
}