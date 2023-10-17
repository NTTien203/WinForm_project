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
            BindGrid(listMonHoc);
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
                MonHoc monhoc = new MonHoc()
                {

                    MaMH = textBox1.Text,
                    TenMH = textBox2.Text,
                    SoTC = int.Parse(textBox3.Text),
                };
                dataGridView1.Rows.Clear();
                context.MonHocs.Add(monhoc);
                context.SaveChanges();
                listMonHoc = context.MonHocs.ToList();
                BindGrid(listMonHoc);
                MessageBox.Show("Them Thong Tin Thanh cong");
            }
            catch
            {
                MessageBox.Show("Trung MMH roi!!");
                BindGrid(listMonHoc);
            }
          
        }
        //Button Xoa
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            MonHoc db = context.MonHocs.FirstOrDefault(s => s.MaMH == id);
            if (db == null)
            {
                MessageBox.Show("Không tìm thấy mon hoc co MMH do!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                context.MonHocs.Remove(db);
                context.SaveChanges();
                dataGridView1.Rows.Clear();
                listMonHoc = context.MonHocs.ToList();
                BindGrid(listMonHoc);
                MessageBox.Show("Xoa Thong Tin thanh cong");
            }
        }
        //Sua Thong Tin
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            MonHoc db = context.MonHocs.FirstOrDefault(s => s.MaMH == id);
            if (db == null)
            {
                MessageBox.Show("Không tìm thấy mon hoc co MMH do!!!", "Thông báo", MessageBoxButtons.OK);
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
    }
}
