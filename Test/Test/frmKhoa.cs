using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Model1;

namespace Test
{

    public partial class frmKhoa : Form
    {
        static DBQuanLySV context = new DBQuanLySV();
        List<Khoa> listk = context.Khoas.ToList();


        public bool checkNull()
        {
            if (txtMaKhoa.Text == "" || txtTenKhoa.Text == "" || txtDiaChi.Text == "" || txtDT.Text == "")
            {
                return true;
            }
            return false;
        }
        public void BindDaTaGrid(List<Khoa> k)
        {
            dgvDanhSach.Rows.Clear();
            foreach (var item in k)
            {
                int index = dgvDanhSach.Rows.Add();
                dgvDanhSach.Rows[index].Cells[0].Value = item.MaKhoa;
                dgvDanhSach.Rows[index].Cells[1].Value = item.TenKhoa;
                dgvDanhSach.Rows[index].Cells[2].Value = item.DiaChi;
                dgvDanhSach.Rows[index].Cells[3].Value = item.DienThoai;

            }
        }
        public Boolean KTTrung(string maKhoa, string tenKhoa)
        {
            maKhoa = maKhoa.Trim().ToLower();
            tenKhoa = tenKhoa.Trim().ToLower();
            bool trungmakhoa = context.Khoas.Any(k => k.MaKhoa == txtMaKhoa.Text);
            if (trungmakhoa)
            {
                MessageBox.Show("Mã khoa đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK);
                return true;
            }
            bool trungtenkhoa = context.Khoas.Any(k => k.TenKhoa == txtTenKhoa.Text);
            if (trungtenkhoa)
            {
                MessageBox.Show("Tên khoa đã tồn tại, vui lòng nhập tên khác", "Thông báo", MessageBoxButtons.OK);
                return true;
            }
            return false;

        }
        public frmKhoa()
        {
            InitializeComponent();
            BindDaTaGrid(listk);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void spbtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spbtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkNull() == true)
                {
                    MessageBox.Show("Nhập thiếu thông tin", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    if (!KTTrung(txtMaKhoa.Text, txtTenKhoa.Text))
                    {
                        Khoa s = new Khoa()
                        {

                            MaKhoa = txtMaKhoa.Text,
                            TenKhoa = txtTenKhoa.Text,
                            DiaChi = txtDiaChi.Text,
                            DienThoai = txtDT.Text,

                        };

                        context.Khoas.Add(s);
                        context.SaveChanges();
                        List<Khoa> listDG = context.Khoas.ToList();
                        BindDaTaGrid(listDG);

                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);

                    }
                }

            }
            catch (Exception ex)// neu xay ra loi
            {
                List<Khoa> listDG = context.Khoas.ToList();
                MessageBox.Show(ex.ToString());
            }
        }

        private void spbtnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                var a = context.SinhViens.Where(p => p.Lop.MaKhoa == txtMaKhoa.Text).ToList();
                var b = context.Lops.Where(p => p.MaKhoa == txtMaKhoa.Text).ToList();

                if (a != null)
                {

                    DialogResult result = MessageBox.Show("Còn sinh viên trong khoa, bạn có muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        foreach (var item in a)
                        {
                            context.SinhViens.Remove(item);
                            var c = context.Diems.Where(s => s.MaSV == item.MaSV).ToList();
                            foreach (var c2 in c)
                            {
                                context.Diems.Remove(c2);
                            }
                        }
                        context.SaveChanges();
                        if (b != null)
                        {
                            foreach (var item in b)
                            {
                                context.Lops.Remove(item);
                            }
                        }
                        context.SaveChanges();
                        MessageBox.Show("Đã xóa sinh viên thành công", "Thông báo", MessageBoxButtons.OK);

                    }

                    else
                    {
                        MessageBox.Show("Đã chọn Không xóa");
                    }

                }
                Khoa deleteDG = context.Khoas.FirstOrDefault(s => s.MaKhoa == txtMaKhoa.Text);
                if (deleteDG != null)
                {
                    context.Khoas.Remove(deleteDG);
                    context.SaveChanges();
                    List<Khoa> listK = context.Khoas.ToList();
                    BindDaTaGrid(listK);
                    MessageBox.Show("Xóa Khoa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Khoa chưa tồn tại trong danh sách", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void spbtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkNull() == true)
                {
                    MessageBox.Show("Nhập thiếu thông tin", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    if (!KTTrung(txtMaKhoa.Text, txtTenKhoa.Text))
                    {

                        Khoa updateK = context.Khoas.FirstOrDefault(s => s.MaKhoa == txtMaKhoa.Text);
                        if (updateK != null)
                        {
                            updateK.TenKhoa = txtTenKhoa.Text;
                            updateK.DiaChi = txtDiaChi.Text;
                            updateK.DienThoai = txtDT.Text;



                            context.SaveChanges();
                            List<Khoa> listK = context.Khoas.ToList();
                            BindDaTaGrid(listK);
                            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                }

            }
            catch (Exception ex)// neu xay ra loi
            {
                List<Khoa> listK = context.Khoas.ToList();
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = dgvDanhSach.CurrentRow;
            txtMaKhoa.Text = a.Cells[0].Value.ToString();
            txtTenKhoa.Text = a.Cells[1].Value.ToString();
            txtDiaChi.Text = a.Cells[2].Value.ToString();
            txtDT.Text = a.Cells[3].Value.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var dg = context.Khoas.Where(s => s.TenKhoa.ToString().Contains(txtTimKiem.Text)).ToList();
            BindDaTaGrid(dg);
        }

        private void frmKhoa_Load(object sender, EventArgs e)
        {

        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
                MessageBox.Show("Chỉ nhập số", "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }


        private void txtTenKhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ nhập chữ", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
