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
    public partial class Form6 : Form
    {
        static DBQuanLySV context = new DBQuanLySV();
        List<MonHoc> listMonHoc = context.MonHocs.ToList();

        public Form6()
        {
            InitializeComponent();
        }
        public bool checkTrung(string tenmoi)
        {
            DBQuanLySV context = new DBQuanLySV();
            MonHoc d = context.MonHocs.FirstOrDefault(a => a.TenMH == tenmoi);
            if (d != null)
                return true;
            return false;
        }

        public bool checkNull()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                return true;
            return false;
        }

        private bool KTTenMon(string tenmoi, string tencu)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i != dataGridView1.CurrentRow.Index)
                {
                    string tenMon = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (tenMon == tenmoi)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean KTTrung(string maMon, string tenMon)
        {

            DBQuanLySV context = new DBQuanLySV();

            bool trungMaMon = context.MonHocs.Any(k => k.MaMH == maMon);
            if (trungMaMon)
            {
                MessageBox.Show("Mã Môn đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK);
                return true;
            }

            bool trungTenMon = context.MonHocs.Any(k => k.TenMH == tenMon);
            if (trungTenMon)
            {
                MessageBox.Show("Tên Môn đã tồn tại, vui lòng nhập tên khác", "Thông báo", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        //Show Thong tin Bang
        void BindGrid(List<MonHoc> listMonHoc)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listMonHoc)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.MaMH;
                dataGridView1.Rows[index].Cells[1].Value = item.TenMH;
                dataGridView1.Rows[index].Cells[2].Value = item.SoTC;
            }
        }

        //Button Them
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkNull() == true)
                {
                    MessageBox.Show("Thiếu thông tin", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    if (!KTTrung(textBox1.Text, textBox2.Text))
                    {
                        MonHoc monhoc = new MonHoc()
                        {

                            MaMH = textBox1.Text,
                            TenMH = textBox2.Text,
                            SoTC = int.Parse(textBox3.Text),
                        };
                        DBQuanLySV context = new DBQuanLySV();
                        context.MonHocs.Add(monhoc);
                        context.SaveChanges();
                        List<MonHoc> listMonHoc = context.MonHocs.ToList();
                        listMonHoc = context.MonHocs.ToList();
                        BindGrid(listMonHoc);
                        MessageBox.Show("Them Thong Tin Thanh cong");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                BindGrid(listMonHoc);
            }

        }
       
        private bool checkMonhocinDiem(string maMH)
        {
            DBQuanLySV context = new DBQuanLySV();
            Diem d = context.Diems.FirstOrDefault(a => a.MaMH == maMH);
            if (d != null)
                return true;
            return false;
        }

        //Button Xoa
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DBQuanLySV context = new DBQuanLySV();
                MonHoc m = context.MonHocs.FirstOrDefault(s => s.MaMH == textBox1.Text);
                if (m != null)
                {
                    if (checkMonhocinDiem(m.MaMH) == false)
                    {
                        context.MonHocs.Remove(m);
                        context.SaveChanges();
                        List<MonHoc> listMonHoc = context.MonHocs.ToList();
                        BindGrid(listMonHoc);
                        MessageBox.Show("Xoa Thong Tin thanh cong");
                    }
                    else
                        MessageBox.Show("Sinh viên có điểm trong môn học này, không thể xóa!", "Thông báo");
                }
                else

                    MessageBox.Show("Không tìm thấy mon hoc co MMH do!!!", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Sua Thong Tin
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DBQuanLySV context = new DBQuanLySV();
                if (checkNull() == true)
                {
                    MessageBox.Show("Thieu thong tin", "Thong bao", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    MonHoc db = context.MonHocs.FirstOrDefault(s => s.MaMH == textBox1.Text);
                    string tenmoi = textBox2.Text;
                    if (db != null)
                    {
                        string tencu = db.TenMH;
                        if (KTTenMon(tenmoi, tencu) == true)
                        {
                            MessageBox.Show("Trùng tên Môn ", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (checkTrung(tenmoi)  )
                            {
                                if (db.MaMH == textBox1.Text)
                                {
                                    db.SoTC = int.Parse(textBox3.Text);
                                    context.SaveChanges();
                                    dataGridView1.Rows.Clear();
                                    listMonHoc = context.MonHocs.ToList();
                                    BindGrid(listMonHoc);
                                    MessageBox.Show("Sua So TC thanh cong");
                                }
                                else
                                {
                                    MessageBox.Show("Trùng tên Môn ", "Thông báo", MessageBoxButtons.OK);
                                }
                               
                            }
                            else
                            {
                                db.TenMH = textBox2.Text;
                                db.SoTC = int.Parse(textBox3.Text);
                                context.SaveChanges();
                                dataGridView1.Rows.Clear();
                                listMonHoc = context.MonHocs.ToList();
                                BindGrid(listMonHoc);
                                MessageBox.Show("Sua Thong Tin thanh cong");
                            }
                                
                            
                            
                        }
                    }
                    if (db == null)
                    {
                        MessageBox.Show("Mon không tồn tại", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)// neu xay ra loi
            {
                List<Khoa> listK = context.Khoas.ToList();
                MessageBox.Show(ex.ToString());
            }
        }

        //Tran` Thong Tin vo TextBox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                int rowIndex = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0]?.Value?.ToString();
                textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1]?.Value?.ToString();
                textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2]?.Value?.ToString();
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            BindGrid(listMonHoc);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            var dg = context.MonHocs.Where(s => s.TenMH.Contains(txtTimKiem.Text)).ToList();
            BindGrid(dg);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
