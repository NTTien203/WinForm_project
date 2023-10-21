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
    public partial class Form4 : Form
    {
        public static DBQuanLySV db;//database
        public static List<Diem> diem;//ds diem
        public static List<HeDT> hedt;//ds he daotao
        public static List<Khoa> khoa;//ds cac khoa
        public static List<KhoaHoc> khoahoc;//ds cac khoa hoc
        public static List<Lop> lop;//ds cac lop
        public static List<MonHoc> monhoc;//ds cac mon hoc
        public static List<SinhVien> sv;//ds cac sinh vien
        public static List<HeDT> hedaotao;//ds cac he dao tao
        public void fillallcombobox()
        {
            //cmbdt.DataSource = hedaotao;
            //cmbdt.ValueMember = "MaHeDT";
            //cmbdt.DisplayMember = "TenHeDT";
            //cmbkh.DataSource = khoahoc;
            //cmbkh.ValueMember = "MaKhoaHoc";
            //cmbkh.DisplayMember = "TenKhoaHoc";
            cmbnganh.DataSource = khoa;
            cmbnganh.ValueMember = "MaKhoa";
            cmbnganh.DisplayMember = "TenKhoa";
            cmbtenkhoa.DataSource = khoa;
            cmbtenkhoa.ValueMember = "MaKhoa";
            cmbtenkhoa.DisplayMember = "TenKhoa";
            //cmblop.DataSource = lop;
            //cmblop.ValueMember = "MaLop";
            //cmblop.DisplayMember = "TenLop";
            //   cmbdt.SelectedIndex = -1;
            cmbkh.SelectedIndex = cmbdt.SelectedIndex = 0;
            cmbnganh.SelectedIndex = cmbtenkhoa.SelectedIndex = 0;
            //  cmbkh.SelectedIndex = 3;
        }
        public void Blinggridview()
        {
            dataGridView1.Rows.Clear();
            foreach (var st in sv)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = st.MaSV;
                dataGridView1.Rows[index].Cells[1].Value = st.TenSV;
                dataGridView1.Rows[index].Cells[2].Value = st.NgaySinh.ToString();
                dataGridView1.Rows[index].Cells[3].Value = st.GioiTinh;
                dataGridView1.Rows[index].Cells[4].Value = st.QueQuan;
                // dataGridView1.Rows[index].Cells[5].Value = st.SDT;
                dataGridView1.Rows[index].Cells[5].Value = st.Lop.TenLop;
                dataGridView1.Rows[index].Cells[6].Value = st.Lop.Khoa.TenKhoa;
                dataGridView1.Rows[index].Cells[7].Value = st.Lop.Khoa.TenKhoa;
                dataGridView1.Rows[index].Cells[8].Value = st.Lop.KhoaHoc.TenKhoaHoc;
                dataGridView1.Rows[index].Cells[9].Value = st.Lop.HeDT.TenHeDT;

            }

        }
        public Form4()
        {
            InitializeComponent();


        }
        public static List<Lop> b;
        public void changeclassbyfalculty()
        {
            b = db.Lops.Where(p => cmbtenkhoa.Text.Equals(p.Khoa.TenKhoa)).ToList();
            cmblop.DataSource = b;
            cmblop.DisplayMember = "TenLop";
            cmblop.ValueMember = "MaLop";
        }
        public void changekhoahocandhedaotaobyALLCLassbyFalculty()
        {
            List<HeDT> c = new List<HeDT>();
            List<KhoaHoc> d = new List<KhoaHoc>();
            foreach (var i in b)
            {
                if (c.Count == 0) c.Add(i.HeDT);
                else
                {
                    var e = c.Where(p => p.MaHeDT == i.MaHeDT).ToList();
                    if (e.Count == 0) c.Add(i.HeDT);
                }
                if (d.Count == 0) d.Add(i.KhoaHoc);
                else
                {
                    var e = d.Where(p => p.MaKhoaHoc == i.MaKhoaHoc).ToList();
                    if (e.Count == 0) d.Add(i.KhoaHoc);
                }
            }
            cmbdt.DataSource = c;
            cmbdt.DisplayMember = "TenHeDT";
            cmbdt.ValueMember = "MaHeDT";
            cmbkh.DataSource = d;
            cmbkh.DisplayMember = "TenKhoaHoc";
            cmbkh.ValueMember = "MaKhoaHoc";
        }
        public void changekhoahocandhedaotaobyclass()
        {
            List<HeDT> h = new List<HeDT>();
            List<KhoaHoc> k = new List<KhoaHoc>();
            foreach (var i in b)
            {
                if (i.MaLop == cmblop.SelectedValue.ToString())
                {
                    h.Add(i.HeDT);
                    k.Add(i.KhoaHoc);
                }

            }
            cmbdt.DataSource = h;
            cmbdt.ValueMember = "MaHeDT";
            cmbdt.DisplayMember = "TenHeDT";
            cmbkh.DataSource = k;
            cmbkh.ValueMember = "MaKhoaHoc";
            cmbkh.DisplayMember = "TenKhoaHoc";
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            db = new DBQuanLySV();
            //add data from database to each list component
            diem = db.Diems.ToList();
            hedt = db.HeDTs.ToList();
            khoa = db.Khoas.ToList();
            khoahoc = db.KhoaHocs.ToList();
            lop = db.Lops.ToList();
            monhoc = db.MonHocs.ToList();
            sv = db.SinhViens.ToList();
            hedaotao = db.HeDTs.ToList();
            Blinggridview();//Show datagird view;
            fillallcombobox();//fill all combobox;
            //changeclassbyfalculty();
            //changekhoahocandhedaotaobyALLCLassbyFalculty();
            radioButton1.Checked = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
           d= txtmaso.Text = selectedRow.Cells[0].Value.ToString();
            txthoten.Text = selectedRow.Cells[1].Value.ToString();
            dateTimePicker1.Text = selectedRow.Cells[2].Value.ToString();
            if (selectedRow.Cells[3].Value.ToString() == "Nam") radioButton1.Checked = true;
            else radioButton2.Checked = true;
            txtquequan.Text = selectedRow.Cells[4].Value.ToString();
         //   txtdienthoai.Text = selectedRow.Cells[5].Value.ToString();
            cmblop.Text = selectedRow.Cells[5].Value.ToString();
            cmbtenkhoa.Text = selectedRow.Cells[6].Value.ToString(); ;
            cmbnganh.Text = selectedRow.Cells[7].Value.ToString(); ;
            cmbkh.Text = selectedRow.Cells[8].Value.ToString(); ;
            cmbdt.Text = selectedRow.Cells[9].Value.ToString(); ;
        }
        public string d;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Year >= 2006)
            {
                MessageBox.Show("Số tuổi không hợp lệ để học đại học", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (/*txtdienthoai.Text == "" || */txthoten.Text == "" || txtmaso.Text == "" || txtquequan.Text == "")
            {
                MessageBox.Show("Nhập đủ thông tin trước khi thêm", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            var a = db.SinhViens.FirstOrDefault(p => p.MaSV == txtmaso.Text);
            if (a == null)
            {
                SinhVien b = new SinhVien()
                {
                    MaSV = txtmaso.Text,
                    TenSV = txthoten.Text,
                    NgaySinh = dateTimePicker1.Value,
                    GioiTinh = (radioButton1.Checked) ? "Nam" : "Nữ",
                    MaLop = cmblop.SelectedValue.ToString(),
                    QueQuan = txtquequan.Text,
                    //SDT = txtdienthoai.Text
                };
                db.SinhViens.Add(b);
                db.SaveChanges();
                sv = db.SinhViens.ToList();
                Blinggridview();
            }
            else
            {
                MessageBox.Show("Trùng mã số sinh viên", "Thông báo", MessageBoxButtons.OK);

            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtmaso.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã số sinh viên muốn xóa hoặc tìm kiếm thông tin sinh viên trước khi xóa", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            var a = db.SinhViens.FirstOrDefault(p => p.MaSV == txtmaso.Text);
            if (a == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên cần xóa", "Thông báo", MessageBoxButtons.OK); return;
            }
            var b = db.Diems.Where(p => p.MaSV == txtmaso.Text).ToList();
            if (b.Count == 0)
            {
                db.SinhViens.Remove(a);
                db.SaveChanges();
                sv = db.SinhViens.ToList();
                Blinggridview();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên có điểm ở các môn học", "Thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (var item in b)
                {
                    db.Diems.Remove(item);
                }
                db.SinhViens.Remove(a);
                db.SaveChanges();
                sv = db.SinhViens.ToList();
                Blinggridview();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);

            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }
   
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
                if (txtmaso.Text == "")
                {
                    MessageBox.Show("Chưa nhập mã số để tiến hành thao tác", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                if(!d.Equals(txtmaso.Text))
                {
                    
                    MessageBox.Show("Không được đổi mã  số sinh viên", "Thông báo", MessageBoxButtons.OK);
                    txtmaso.Text = d;
                    return;

                }
                SinhVien a = db.SinhViens.FirstOrDefault(p => p.MaSV == txtmaso.Text);
                if (a == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK);
                return;
                }
                if (dateTimePicker1.Value.Year >= 2006)
                {
                    MessageBox.Show("Nhập Ngày Sinh Không Hợp Lệ!", "Thông Báo", MessageBoxButtons.OK);
                }

                else
                {
                    
                    if (txthoten.Text != "") a.TenSV = txthoten.Text;
                    //if (txtdienthoai.Text != "") a.SDT = txtdienthoai.Text;
                    a.NgaySinh = dateTimePicker1.Value;
                    a.MaLop = cmblop.SelectedValue.ToString();
                    //MessageBox.Show(cmblop.SelectedValue.ToString(), "Thong bao", MessageBoxButtons.OK);
                    a.GioiTinh = (radioButton1.Checked) ? "Nam" : "Nữ";
                    if (txtquequan.Text != "") a.QueQuan = txtquequan.Text;
                    db.SaveChanges();
                    sv = db.SinhViens.ToList();
                    Blinggridview();
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK);
                }
          
            

        }

        private void cmbtenkhoa_TextChanged(object sender, EventArgs e)
        {
            changeclassbyfalculty();
            changekhoahocandhedaotaobyALLCLassbyFalculty();
        }

        private void txttim_TextChanged(object sender, EventArgs e)
        {
            //string s = (radioButton1.Checked) ? "Nam" : "Nữ";
            var c = db.SinhViens.Where(p => p.MaSV.Contains(txttim.Text) || p.TenSV.Contains(txttim.Text)
             || p.QueQuan.Contains(txttim.Text) || // || p.SDT.Contains(txttim.Text)
            p.Lop.TenLop.Contains(txttim.Text) || p.Lop.Khoa.TenKhoa.Contains(txttim.Text) ||
            p.Lop.KhoaHoc.TenKhoaHoc.Contains(txttim.Text) || p.Lop.HeDT.TenHeDT.Contains(txttim.Text)
            || p.GioiTinh.Contains(txttim.Text)
            ).ToList();
            dataGridView1.Rows.Clear();
            foreach (var st in c)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = st.MaSV;
                dataGridView1.Rows[index].Cells[1].Value = st.TenSV;
                dataGridView1.Rows[index].Cells[2].Value = st.NgaySinh.ToString();
                dataGridView1.Rows[index].Cells[3].Value = st.GioiTinh;
                dataGridView1.Rows[index].Cells[4].Value = st.QueQuan;
                //dataGridView1.Rows[index].Cells[5].Value = st.SDT;
                dataGridView1.Rows[index].Cells[6].Value = st.Lop.TenLop;
                dataGridView1.Rows[index].Cells[7].Value = st.Lop.Khoa.TenKhoa;
                dataGridView1.Rows[index].Cells[8].Value = st.Lop.Khoa.TenKhoa;
                dataGridView1.Rows[index].Cells[9].Value = st.Lop.KhoaHoc.TenKhoaHoc;
                dataGridView1.Rows[index].Cells[10].Value = st.Lop.HeDT.TenHeDT;

            }

        }

        private void cmblop_TextChanged(object sender, EventArgs e)
        {
            changekhoahocandhedaotaobyclass();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

