using DevExpress.XtraRichEdit.Layout.Engine;
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
                    dgvBangDiem.Rows[index].Cells[6].Value = transNumbertoAlpha((float)(item.DiemL1 + item.DiemL2) / 2);

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
                    if (checkExist() == false)
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

                    Diem diem = context.Diems.FirstOrDefault(p => p.MaSV == txtBangDiemMSSV.Text && p.MaMH == m.MaMH);

                    if (diem != null)
                    {
                        diem.DiemL1 = double.Parse(txtBangDiemDiemLan1.Text);
                        diem.DiemL2 = double.Parse(txtBangDiemDiemLan2.Text);
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
                if (check())
                {
                    DBQuanLySV context = new DBQuanLySV();

                    // Lay ra MonHoc theo txtBangDiemTenMonHoc
                    MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == txtBangDiemTenMonHoc.Text);
                    // Tim Diem co MaSV va MaMH theo txtBangDiemMSSV va m.MaMH
                    Diem dDelete = context.Diems.FirstOrDefault(p => p.MaSV == txtBangDiemMSSV.Text && p.MaMH == m.MaMH);

                    if (dDelete != null)
                    {
                        context.Diems.Remove(dDelete);
                        context.SaveChanges();

                        List<Diem> listDiem = context.Diems.ToList();
                        BindGrid(listDiem);

                        MessageBox.Show("Xoa khoa thanh cong!", "Thong bao!");
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

        private void Form5_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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

        private void dgvBangDiem_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBangDiem.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvBangDiem.SelectedRows[0];
                txtBangDiemMSSV.Text = selectedRow.Cells[0].Value.ToString();
                txtBangDiemTenMonHoc.Text = selectedRow.Cells[2].Value.ToString();
                txtBangDiemDiemLan1.Text = selectedRow.Cells[3].Value.ToString();
                txtBangDiemDiemLan2.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DBQuanLySV context = new DBQuanLySV();

            string searchText = textBox1.Text;

            var query = from diem in context.Diems
                        join sinhvien in context.SinhViens on diem.MaSV equals sinhvien.MaSV
                        join monhoc in context.MonHocs on diem.MaMH equals monhoc.MaMH
                        where sinhvien.TenSV.Contains(searchText) || diem.MaSV.Contains(searchText) || monhoc.TenMH.Contains(searchText)
                        select diem;

            List<Diem> d = query.ToList();
            BindGrid(d);
        }

        private bool checkExist()
        {
            DBQuanLySV context = new DBQuanLySV();
            MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == txtBangDiemTenMonHoc.Text);
            Diem d = context.Diems.FirstOrDefault(a => a.MaSV == txtBangDiemMSSV.Text && a.MaMH == m.MaMH);
            if (d != null)
            {
                MessageBox.Show("Sinh viên đã có điểm môn này rồi!");
                return true;
            }

            return false;
        }


        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string transNumbertoAlpha(float num)
        {
            string result;

            if (num >= 8.5)
            {
                result = "A";
            }
            else if (num >= 7.8)
            {
                result = "B+";
            }
            else if (num >= 7.0)
            {
                result = "B";
            }
            else if (num >= 6.3)
            {
                result = "C+";
            }
            else if (num >= 5.5)
            {
                result = "C";
            }
            else if (num >= 4.8)
            {
                result = "D+";
            }
            else if (num >= 4.0)
            {
                result = "D";
            }
            else if (num >= 3.0)
            {
                result = "F+";
            }
            else
            {
                result = "F";
            }

            return result;
        }


    }
}
