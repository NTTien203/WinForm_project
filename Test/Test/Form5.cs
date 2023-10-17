using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Model1;

namespace Test
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }


        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }


        private bool check()
        {
            if (string.IsNullOrEmpty(txtBangDiemMSSV.Text) || string.IsNullOrEmpty(txtBangDiemTenMonHoc.Text) || string.IsNullOrEmpty(txtBangDiemDiemLan1.Text) || string.IsNullOrEmpty(txtBangDiemDiemLan2.Text))
            {
                MessageBox.Show("Vui long nhap day du thong tin", "Thong bao");
                return false;
            }

            DBQuanLySV context = new DBQuanLySV();

            SinhVien s = context.SinhViens.FirstOrDefault(a => a.MaSV == txtBangDiemMSSV.Text);

            if (s == null)
            {
                MessageBox.Show("Sinh vien khong ton tai", "Thong bao");
                return false;
            }

            MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == txtBangDiemTenMonHoc.Text);

            if (m == null)
            {
                MessageBox.Show("Mon hoc khong ton tai", "Thong bao");
                return false;
            }

            if (float.TryParse(txtBangDiemDiemLan1.Text, out float diem1) && float.TryParse(txtBangDiemDiemLan2.Text, out float diem2))
            {
                if (diem1 < 0 && diem2 < 0)
                {
                    MessageBox.Show("Diem khong la gia tri am", "Thong bao");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Diem khong dung dinh dang", "Thong bao");
                return false;
            }

            return true;
        }


        private void BindGrid(List<Diem> listDiem)
        {
            try
            {

                dgvBangDiem.Rows.Clear();

                foreach (var item in listDiem)
                {
                    int index = dgvBangDiem.Rows.Add();
                    dgvBangDiem.Rows[index].Cells[0].Value = item.MaSV;
                    DBQuanLySV context = new DBQuanLySV();
                    SinhVien s = context.SinhViens.FirstOrDefault(a => a.MaSV == item.MaSV);
                    dgvBangDiem.Rows[index].Cells[1].Value = s.TenSV;
                    MonHoc d = context.MonHocs.FirstOrDefault(a => a.MaMH == item.MaMH);
                    dgvBangDiem.Rows[index].Cells[2].Value = d.TenMH;
                    dgvBangDiem.Rows[index].Cells[3].Value = item.DiemL1;
                    dgvBangDiem.Rows[index].Cells[4].Value = item.DiemL2;
                    dgvBangDiem.Rows[index].Cells[5].Value = (item.DiemL1 + item.DiemL2) / 2;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    DBQuanLySV context = new DBQuanLySV();

                    MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == txtBangDiemTenMonHoc.Text);

                    Diem d = new Diem()
                    {
                        MaSV = txtBangDiemMSSV.Text,
                        MaMH = m.MaMH,
                        DiemL1 = float.Parse(txtBangDiemDiemLan1.Text),
                        DiemL2 = float.Parse(txtBangDiemDiemLan2.Text)
                    };

                    context.Diems.Add(d);

                    context.SaveChanges();

                    List<Diem> listDiem = context.Diems.ToList();

                    BindGrid(listDiem);

                    MessageBox.Show("Them thanh cong!", "Thong bao!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    DBQuanLySV context = new DBQuanLySV();

                    MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == txtBangDiemTenMonHoc.Text);

                    Diem dUpdate = context.Diems.FirstOrDefault(p => p.MaSV == txtBangDiemMSSV.Text && p.MaMH == m.MaMH);

                    if (dUpdate != null)
                    {
                        dUpdate.DiemL1 = float.Parse(txtBangDiemDiemLan1.Text);
                        dUpdate.DiemL1 = float.Parse(txtBangDiemDiemLan1.Text);
                        context.SaveChanges();
                        List<Diem> listDiem = context.Diems.ToList();
                        BindGrid(listDiem);
                        MessageBox.Show("Cap nhat du lieu thanh cong!", "Thong bao!");
                    }
                    else
                        MessageBox.Show("Khong tim thay!", "Thong bao!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                DBQuanLySV context = new DBQuanLySV();
                List<Diem> listDiem = context.Diems.ToList();
                BindGrid(listDiem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
