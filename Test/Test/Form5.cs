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

        // Handle form load
        private void Form5_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            try
            {
                DBQuanLySV context = new DBQuanLySV();
                List<Diem> listDiem = context.Diems.ToList();
                List<MonHoc> listMonhoc = context.MonHocs.ToList();
                fillComboboxTenMon(listMonhoc);
                BindGrid(listDiem);
                cbbTenMon.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Fill combox Ten Mon hoc
        private void fillComboboxTenMon(List<MonHoc> listMonhoc)
        {
            this.cbbTenMon.DataSource = listMonhoc;
            this.cbbTenMon.DisplayMember = "TenMH";
            this.cbbTenMon.ValueMember = "MaMH";
        }

        // Handle sinh viên có 2 điểm 1 môn học 
        private bool checkExist()
        {
            DBQuanLySV context = new DBQuanLySV();
            MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == cbbTenMon.Text);
            Diem d = context.Diems.FirstOrDefault(a => a.MaSV == txtBangDiemMSSV.Text && a.MaMH == m.MaMH);
            if (d != null)
            {
                MessageBox.Show("Sinh viên đã có điểm môn này rồi!");
                return true;
            }

            return false;
        }

        // Handle các trường hợp cho các chức năng: thêm, xoá, sửa
        private bool check()
        {
            if (string.IsNullOrEmpty(txtBangDiemMSSV.Text) || string.IsNullOrEmpty(cbbTenMon.Text) || string.IsNullOrEmpty(txtBangDiemDiemLan1.Text) || string.IsNullOrEmpty(txtBangDiemDiemLan2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!");
                return false;
            }

            DBQuanLySV context = new DBQuanLySV();

            SinhVien s = context.SinhViens.FirstOrDefault(a => a.MaSV == txtBangDiemMSSV.Text);

            if (s == null)
            {
                MessageBox.Show("Sinh viên không tồn tại!", "Thông báo!");
                return false;
            }

            MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == cbbTenMon.Text);

            if (m == null)
            {
                MessageBox.Show("Môn học không tồn tại!", "Thông báo!");
                return false;
            }

            if (float.TryParse(txtBangDiemDiemLan1.Text, out float diem1) && float.TryParse(txtBangDiemDiemLan2.Text, out float diem2))
            {
                if (diem1 < 0 && diem2 < 0)
                {
                    MessageBox.Show("Điểm phải lớn hơn hoặc bằng 0", "Thông báo!");
                    return false;
                }
                else if (diem1 > 10 || diem2 > 10)
                {
                    MessageBox.Show("Điểm phải bé hơn hoặc bằng 10", "Thông báo!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập điểm là số!", "Thông báo!");
                return false;
            }

            return true;
        }

        // Đẩy dữ liệu "listDiem" vào GridView
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

        // Handle chức năng Thêm điểm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    if (checkExist() == false)
                    {
                        DBQuanLySV context = new DBQuanLySV();

                        MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == cbbTenMon.Text);

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

                        MessageBox.Show("Thêm điểm thành công!", "Thông báo!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Handle chức năng Sửa điểm
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    DBQuanLySV context = new DBQuanLySV();

                    MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == cbbTenMon.Text);

                    Diem diem = context.Diems.FirstOrDefault(p => p.MaSV == txtBangDiemMSSV.Text && p.MaMH == m.MaMH);

                    if (diem != null)
                    {
                        diem.DiemL1 = double.Parse(txtBangDiemDiemLan1.Text);
                        diem.DiemL2 = double.Parse(txtBangDiemDiemLan2.Text);
                        context.SaveChanges();

                        List<Diem> listDiem = context.Diems.ToList();
                        BindGrid(listDiem);

                        MessageBox.Show("Cập nhật điểm thành công!", "Thông báo!");
                    }
                    else
                        MessageBox.Show("Không tìm thấy điểm cần sửa!", "Thông báo!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Handle chức năng Xoá điểm
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    DBQuanLySV context = new DBQuanLySV();

                    // Lay ra MonHoc theo txtBangDiemTenMonHoc
                    MonHoc m = context.MonHocs.FirstOrDefault(a => a.TenMH == cbbTenMon.Text);
                    // Tim Diem co MaSV va MaMH theo txtBangDiemMSSV va m.MaMH
                    Diem dDelete = context.Diems.FirstOrDefault(p => p.MaSV == txtBangDiemMSSV.Text && p.MaMH == m.MaMH);

                    if (dDelete != null)
                    {
                        context.Diems.Remove(dDelete);
                        context.SaveChanges();

                        List<Diem> listDiem = context.Diems.ToList();
                        BindGrid(listDiem);

                        MessageBox.Show("Xoá điểm thành công!", "Thông báo!");
                    }
                    else
                        MessageBox.Show("Không tìm thấy điểm cần xoá!", "Thông báo!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        // Handle Đẩy dữ liệu từ gridview và textbox
        private void dgvBangDiem_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBangDiem.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvBangDiem.SelectedRows[0];
                txtBangDiemMSSV.Text = selectedRow.Cells[0].Value.ToString();
                int index = cbbTenMon.FindString(selectedRow.Cells[2].Value.ToString());
                cbbTenMon.SelectedIndex = index;
                txtBangDiemDiemLan1.Text = selectedRow.Cells[3].Value.ToString();
                txtBangDiemDiemLan2.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        // Hanlde lọc dữ liệu hiển thị trên gridview
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

        

        // Handle close
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Handle chuyển điểm hệ số sang hệ chữ
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
