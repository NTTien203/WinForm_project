using Microsoft.Reporting.WinForms;
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
    public partial class FormRP : Form
    {
        public FormRP()
        {
            InitializeComponent();
        }

        private void FormRP_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            /* DBQuanLySV  context = new DBQuanLySV();
             List<SinhVien> listSinhVien = context.SinhViens.ToList();
             List<SinhVienRp> listSinhVienRP = new List<SinhVienRp>();
             foreach(SinhVien sv in listSinhVien)
             {
                 SinhVienRp temp = new SinhVienRp();
                 temp.MaSV = sv.MaSV;
                 temp.TenSV = sv.TenSV;
                 temp.GioiTinh = sv.GioiTinh;
                 temp.NgaySinh = sv.NgaySinh;
                 temp.MaLop = sv.MaLop;
                 temp.QueQuan = sv.QueQuan;

                 listSinhVienRP.Add(temp);
             }
             reportViewer1.LocalReport.ReportPath = "RPSinhVien.rdlc";
             var source = new ReportDataSource("DataSet1", listSinhVienRP);
             reportViewer1.LocalReport.DataSources.Clear();
             reportViewer1.LocalReport.DataSources.Add(source);
             this.reportViewer1.RefreshReport();*/
            DBQuanLySV context = new DBQuanLySV();
            List<SinhVien> listSinhVien = context.SinhViens.ToList();
            List<SinhVienRp> listSinhVienRP = new List<SinhVienRp>();
            foreach (SinhVien sv in listSinhVien)
            {
                SinhVienRp temp = new SinhVienRp();
                temp.MaSV = sv.MaSV;
                temp.TenSV = sv.TenSV;
                temp.GioiTinh = sv.GioiTinh;
                temp.NgaySinh = sv.NgaySinh;
                temp.MaLop = sv.MaLop;
                temp.QueQuan = sv.QueQuan;

                listSinhVienRP.Add(temp);
            }
            reportViewer1.LocalReport.ReportPath = "RPSinhVien.rdlc";
            var source = new ReportDataSource("DataSet1", listSinhVienRP);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
