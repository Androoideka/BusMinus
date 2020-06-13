using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Bus_Minus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StreamReader sr = new StreamReader("veze.txt");

        BusMinus GSP;
        private void Form1_Load(object sender, EventArgs e)
        {
            GSP = new BusMinus(sr);
            comboBox1.Items.AddRange(GSP.Prikaz());
            comboBox2.Items.AddRange(GSP.Prikaz());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GSP.Ispis(listBox1, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());
        }
       
    }
}
