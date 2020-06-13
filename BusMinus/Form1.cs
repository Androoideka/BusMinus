using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace BusMinus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BusMinus GSP;
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            GSP = new BusMinus(File.ReadAllLines("veze.txt"));
            comboBox1.Items.AddRange(GSP.Prikaz());
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.AddRange(GSP.Prikaz());
            comboBox2.SelectedIndex = 0;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            listBox1.Items.Clear();
            listBox1.Items.AddRange(GSP.Ispis(comboBox1.Text, comboBox2.Text));
            label2.Visible = true;
            label3.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GSP.Karta = comboBox3.Text;
            listBox1.Enabled = true;
            button1.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = GSP.Karta;
        }
        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 50;
        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }

    }
}
