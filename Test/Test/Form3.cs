﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form3 : Form
    {
    
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
            if(textBox1.Text=="Tien" && textBox2.Text == "Tien")
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
