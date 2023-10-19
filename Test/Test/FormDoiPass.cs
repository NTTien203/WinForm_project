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
    public partial class FormDoiPass : Form
    {
        static int dem = 0;
        static DBQuanLySV context = new DBQuanLySV();
        public FormDoiPass()
        {
            InitializeComponent();
            exitSetting();
        }

        void exitSetting()
        {
            ControlBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (dem == 3)
            {
                MessageBox.Show("Nhap qua 3 lan roi !!!");
                Form3 fr = new Form3();
                this.Hide();
                fr.ShowDialog();
            }
            DangNhap dn = context.DangNhaps.FirstOrDefault(s => s.MatKhau == textBox1.Text && s.TaiKhoang=="admin");
            if (dn != null )
            {
                if(textBox2.Text == textBox3.Text)
                {
                    dn.MatKhau = textBox2.Text;
                    context.SaveChanges();
                    MessageBox.Show("Doi Thanh cong");
                    this.Hide();
                    Form3 fr = new Form3();
                    fr.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nhap lai mat khau sai");
                    
                }

            }
            else
            {
                MessageBox.Show("Khong dung mat khau cu");
                dem++;
            }
        }

        private void FormDoiPass_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 fr = new Form2();
            fr.ShowDialog();
        }
    }
}
