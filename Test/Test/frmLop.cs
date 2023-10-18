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
    public partial class frmLop : Form
    {
        static DBQuanLySV context = new DBQuanLySV();
        List<Lop> listL = context.Lops.ToList();
        List<Khoa> listk = context.Khoas.ToList();
        List<KhoaHoc>listKH = context.KhoaHocs.ToList();
        List<HeDT>listHDH = context.HeDTs.ToList();
        public void fillMaKhoatoCombobox()
        {
            cbxMaKhoa.DataSource = listk;
            cbxMaKhoa.ValueMember = "MaKhoa";
            cbxMaKhoa.DisplayMember = "TenKhoa";
        }
        public void fillMaKhoaHoctoCombobox()
        {
            cbxMaKhoaHoc.DataSource = listKH;
            cbxMaKhoaHoc.ValueMember = "MaKhoaHoc";
            cbxMaKhoaHoc.DisplayMember = "TenKhoaHoc";
        }
        public void fillMaHeDTCombobox()
        {
            cbxHeDaoTao.DataSource = listL;
            cbxHeDaoTao.ValueMember = "MaHeDT";
            cbxHeDaoTao.DisplayMember = "TenHeDT";
        }
        public bool checkNull()
        {
            if (txtMalop.Text == "" || txtTenlop.Text == "" || cbxMaKhoa.Text == "" || cbxMaKhoaHoc.Text == "")
            {
                return true;
            }
            return false;
        }
        public void BindDataGrid(List<Lop> l)
        {
            dgvDanhSach.Rows.Clear();
            foreach (var item in l)
            {
                int index = dgvDanhSach.Rows.Add();
                dgvDanhSach.Rows[index].Cells[0].Value = item.MaLop;
                dgvDanhSach.Rows[index].Cells[1].Value = item.TenLop;
                dgvDanhSach.Rows[index].Cells[2].Value = item.Khoa.TenKhoa.ToString();
                dgvDanhSach.Rows[index].Cells[3].Value = item.HeDT.MaHeDT.ToString();
                dgvDanhSach.Rows[index].Cells[4].Value = item.KhoaHoc.TenKhoaHoc.ToString();

            }
            
        }
        public frmLop()
        {
            InitializeComponent();
            fillMaKhoatoCombobox();
            fillMaKhoaHoctoCombobox();
            fillMaHeDTCombobox();
            BindDataGrid(listL);
        }

        public Boolean KTTrung(string maKhoa, string tenKhoa)
        {


            bool trungmalop = context.Lops.Any(k => k.MaLop == txtMalop.Text);
            if (trungmalop)
            {
                MessageBox.Show("Mã Lớp đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK);
                return true;
            }
            bool trungtenlop = context.Lops.Any(k => k.TenLop == txtTenlop.Text);
            if (trungtenlop)
            {
                MessageBox.Show("Tên khoa đã tồn tại, vui lòng nhập tên khác", "Thông báo", MessageBoxButtons.OK);
                return true;
            }
            return false;

        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = dgvDanhSach.CurrentRow;
            txtMalop.Text = a.Cells[0].Value.ToString();
            txtTenlop.Text = a.Cells[1].Value.ToString();
            cbxMaKhoa.Text = a.Cells[2].Value.ToString();
            cbxHeDaoTao.Text = a.Cells[3].Value.ToString();
            cbxMaKhoaHoc.Text = a.Cells[4].Value.ToString();
            
        }

        private void spbtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(cbxMaKhoaHoc.SelectedValue.ToString(), "Thông báo", MessageBoxButtons.OK);
                if (checkNull())
                {
                    MessageBox.Show("Nhập thiếu thông tin", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    if (!KTTrung(txtMalop.Text, txtTenlop.Text))
                    {
                        Lop s = new Lop()
                        {
                            MaLop = txtMalop.Text,
                            TenLop = txtTenlop.Text,
                            MaKhoa = cbxMaKhoa.SelectedValue.ToString(),
                            MaHeDT = cbxHeDaoTao.SelectedValue.ToString(),
                            MaKhoaHoc = cbxMaKhoaHoc.SelectedValue.ToString(),

                        };

                        context.Lops.Add(s);
                        context.SaveChanges();
                        List<Lop> listDG = context.Lops.ToList();
                        BindDataGrid(listDG);

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

                if (!KTTrung(txtMalop.Text, txtTenlop.Text))
                {
                    Lop deleteL = context.Lops.FirstOrDefault(s => s.MaLop == txtMalop.Text);
                    if (deleteL != null)
                    {
                        context.Lops.Remove(deleteL);
                        context.SaveChanges();
                        List<Lop> listL = context.Lops.ToList();
                        BindDataGrid(listL);
                        MessageBox.Show("Xóa Khoa thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Khoa chưa tồn tại trong danh sách", "Thông báo", MessageBoxButtons.OK);
                    }
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

                    Lop updateL = context.Lops.FirstOrDefault(s => s.MaLop == txtMalop.Text);
                    if (updateL != null)
                    {
                        updateL.TenLop = txtTenlop.Text;
                        updateL.MaKhoa = cbxMaKhoa.SelectedValue.ToString();
                        updateL.MaHeDT = cbxHeDaoTao.SelectedValue.ToString();
                        updateL.MaKhoaHoc = cbxMaKhoaHoc.SelectedValue.ToString();
                        context.SaveChanges();
                        List<Lop> listK = context.Lops.ToList();
                        BindDataGrid(listK);
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                    }

                }

            }
            catch (Exception ex)// neu xay ra loi
            {
                List<Khoa> listK = context.Khoas.ToList();
                MessageBox.Show(ex.ToString());
            }
        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            var dg = context.Lops.Where(s => s.TenLop.ToString().Contains(txttimkiem.Text)).ToList();
            BindDataGrid(dg);
        }
    }
}
