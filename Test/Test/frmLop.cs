using DevExpress.XtraRichEdit;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test
{
    public partial class frmLop : Form
    {
        static DBQuanLySV context = new DBQuanLySV();
        List<Lop> listL = context.Lops.ToList();
        List<Khoa> listk = context.Khoas.ToList();
        List<KhoaHoc> listKH = context.KhoaHocs.ToList();
        List<HeDT> listHDT = context.HeDTs.ToList();
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
            cbxHeDaoTao.DataSource = listHDT;
            cbxHeDaoTao.ValueMember = "MaHeDT";
            cbxHeDaoTao.DisplayMember = "TenHeDT";
        }
        public bool checkTrung(string tenmoi)
        {
            DBQuanLySV context = new DBQuanLySV();
            Lop d = context.Lops.FirstOrDefault(a => a.TenLop == tenmoi);
            if (d != null)
                return true;
            return false;
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
                dgvDanhSach.Rows[index].Cells[2].Value = item.Khoa.TenKhoa;
                dgvDanhSach.Rows[index].Cells[3].Value = item.HeDT.TenHeDT;
                dgvDanhSach.Rows[index].Cells[4].Value = item.KhoaHoc.TenKhoaHoc;

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
                MessageBox.Show("Tên lớp đã tồn tại, vui lòng nhập tên khác", "Thông báo", MessageBoxButtons.OK);
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
        private bool KTTenKhoa(string tenmoi, string tencu)
        {
            for (int i = 0; i < dgvDanhSach.Rows.Count; i++)
            {
                if (i != dgvDanhSach.CurrentRow.Index)
                {
                    string tenlop = dgvDanhSach.Rows[i].Cells[1].Value.ToString();
                    if (tenlop == tenmoi)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void spbtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                Lop updateL = context.Lops.FirstOrDefault(s => s.MaLop == txtMalop.Text);
                if (checkNull() == true)
                {
                    MessageBox.Show("Nhập thiếu thông tin", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    if (updateL != null)
                    {
                        string tencu = updateL.TenLop;
                        string tenmoi = txtTenlop.Text;
                        if (KTTenKhoa(tenmoi, tencu) == true)
                        {
                            MessageBox.Show("Trùng tên Lớp", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (checkTrung(tenmoi))
                            {
                                if (updateL.MaLop == txtMalop.Text)
                                {
                                    updateL.MaKhoa = cbxMaKhoa.SelectedValue.ToString();
                                    updateL.MaHeDT = cbxHeDaoTao.SelectedValue.ToString();
                                    updateL.MaKhoaHoc = cbxMaKhoaHoc.SelectedValue.ToString();
                                    context.SaveChanges();
                                    dgvDanhSach.Rows.Clear();
                                    listk = context.Khoas.ToList();
                                    BindDataGrid(listL);
                                    MessageBox.Show("Sua ten khoa, khóa học,hệ đào tạo");
                                }
                                else
                                {
                                    MessageBox.Show("Trùng tên khoa ", "Thông báo", MessageBoxButtons.OK);
                                }
                            }
                            else
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
                    else
                    {
                        MessageBox.Show("Lớp không tồn tại trong danh sách", "Thông báo", MessageBoxButtons.OK);
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
