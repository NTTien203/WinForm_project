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
    public partial class Form3 : Form
    {
        static DBQuanLySV context = new DBQuanLySV();
        
        public Form3()
        {
            InitializeComponent();
           
       
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            button1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DangNhap dn = context.DangNhaps.FirstOrDefault(s => s.TaiKhoang == textBox1.Text && s.MatKhau == textBox2.Text);
            if(dn!=null)
            {
                MessageBox.Show("dang nhap thanh cong");
                Form2 fr = new Form2();
                this.Hide();
                fr.ShowDialog();
                
              
            }
            else
            {
                MessageBox.Show("Tai khoang hay mat khau sai!!");
            }
        }
    }
}
