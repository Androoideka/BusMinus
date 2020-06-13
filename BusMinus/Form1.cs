using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace BusProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BusSharp.BusSharp GSP;
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            GSP = new BusSharp.BusSharp(File.ReadAllLines("veze.txt"));
            comboBox1.Items.AddRange(GSP.Prikaz());
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.AddRange(GSP.Prikaz());
            comboBox2.SelectedIndex = 1;
            comboBox4.Items.AddRange(GSP.Prikaz());
            comboBox4.SelectedIndex = 0;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text=="Svercovanje") {

                timer1.Enabled = true;
                listBox1.Items.Clear();
                string[] s = GSP.Ispis(comboBox1.Text, comboBox2.Text);
                if (s.Length <= 3) {
                    listBox1.Items.AddRange(s);
                } else {
                    listBox1.Items.Add(s[0]);
                    listBox1.Items.Add(s[1]);
                    listBox1.Items.Add(s[2]);
                }
            } else {
                timer1.Enabled = true;
                listBox1.Items.Clear();
                string[] s = GSP.Ispis(comboBox1.Text, comboBox2.Text);
                if (s.Length <= 3) {
                    listBox1.Items.AddRange(s);
                } else {
                    listBox1.Items.Add(s[0]);
                    listBox1.Items.Add(s[1]);
                    listBox1.Items.Add(s[2]);
                }
                label2.Visible = true;
                label3.Visible = true;
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text!="") {
                if (comboBox3.Text == "Svercovanje") {
                    if (!verovatnoca(40)) {
                        GSP.Kartica = "Jedna voznja";
                        listBox1.Enabled = true;
                        button1.Enabled = true;
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        comboBox4.Enabled = true;
                        button3.Enabled = true;
                    } else {
                        MessageBox.Show("Uhvatila vas je kontrola!");
                        listBox1.Enabled = false;
                        button1.Enabled = false;
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox4.Enabled = false;
                        button3.Enabled = false;
                    }
                } else {
                    GSP.Kartica = comboBox3.Text;
                    listBox1.Enabled = true;
                    button1.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox4.Enabled = true;
                    button3.Enabled = true;
                }
            }
            
        }
        Random r = new Random();
        bool verovatnoca(int x) {
            int broj = r.Next(0, 100);
            if (broj < x) {
                return true;
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = GSP.Kartica;
        }
        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 120;
        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }

        private void button3_Click(object sender, EventArgs e) {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(GSP.Pozovi(comboBox4.Text));
        }

    }
}
