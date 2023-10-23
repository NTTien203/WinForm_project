using DevExpress.DashboardWin.Native;
using DevExpress.XtraCharts.Native;
using System;
using System.Collections;
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
    public partial class Formtopo : Form
    {
        DBQuanLySV cd;
        List<MonHoc> monhoc, monhoc1;
        public void fillcombobox1()
        {
            comboBox1.DataSource = monhoc;
            //comboBox2.DataSource = monhoc;
            comboBox1.ValueMember = "MaMH";
            comboBox1.DisplayMember = "TenMH";
            //comboBox2.ValueMember = "MaMH";
            //comboBox2.DisplayMember = "TenMH";
        }
        public void fillcombobox2()
        {

            comboBox2.DataSource = monhoc1;
            //comboBox1.ValueMember = "MaMH";
            //comboBox1.DisplayMember = "TenMH";
            comboBox2.ValueMember = "MaMH";
            comboBox2.DisplayMember = "TenMH";
        }
        public Formtopo()
        {
            InitializeComponent();
            cd = new DBQuanLySV();
            monhoc = cd.MonHocs.ToList();
            monhoc1 = cd.MonHocs.ToList();
            fillcombobox1();
            fillcombobox2();
        }
        public void addgridview()
        {
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = comboBox1.Text;
            dataGridView1.Rows[index].Cells[1].Value = comboBox2.Text;
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            addgridview();
        }
        private static string kq = "";
        public class Graph
        {
            private int v;
            private List<int>[] adj;
            public Graph(int v)
            {
                this.v = v;
                adj = new List<int>[v];
                for (int i = 0; i < v; i++)
                {
                    adj[i] = new List<int>();
                }
            }
            public void AddEdge(int u, int v) { adj[u].Add(v); }
            public void topologicalSort()
            {
                int[] in_degree = new int[v];
                for (int u = 0; u < v; u++)
                {
                    foreach (int c in adj[u]) in_degree[c]++;
                }
                Queue<int> q = new Queue<int>();
                for (int i = 0; i < v; i++)
                {
                    if (in_degree[i] == 0) q.Enqueue(i);
                }
                int cnt = 0;
                List<int> top_order = new List<int>();
                while (q.Count > 0)
                {
                    int u = q.Dequeue();
                    top_order.Add(u);
                    foreach (int c in adj[u])
                    {
                        if (--in_degree[c] == 0)
                        {
                            q.Enqueue(c);
                        }
                    }
                    cnt++;
                }
                if (cnt != v)
                {
                    MessageBox.Show("Không có môn nào làm điều kiện tiên quyết để khởi tạo lộ trình", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                for (int i = 0; i < top_order.Count; i++)
                {
                    kq += top_order[i].ToString() + " ";
                }
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
            comboBox1.Text = selectedRow.Cells[0].Value.ToString();
            comboBox2.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void btnkq_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            Dictionary<string, int> ht = new Dictionary<string, int>();
            List<string> e1 = new List<string>();
            int j = 0;
            foreach (var c in monhoc)
            {
                ht.Add(c.TenMH, j++);
                e1.Add(c.TenMH);
            }
            Graph g = new Graph(j);
            for (int i = 0; i < count; i++)
            {
                string a = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string b = dataGridView1.Rows[i].Cells[1].Value.ToString();
                g.AddEdge(ht[b], ht[a]);
            }
            g.topologicalSort();
            string d = "", kq1 = "";
            for (int i = 0; i < kq.Length; i++)
            {
                if (kq[i].Equals(' '))
                {
                    kq1 += e1[int.Parse(d)];
                    kq1 += "\r\n";
                    d = "";
                    continue;
                }
                d += kq[i];
            }
            txtkq.Text = kq1;
            kq = "";
        }
    }
}
