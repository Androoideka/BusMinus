using System;
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
        
        BusMinus GSP;
        private void Form1_Load(object sender, EventArgs e)
        {
            GSP = new BusMinus(File.ReadAllLines("veze.txt"));
            comboBox1.Items.AddRange(GSP.Prikaz());
            comboBox2.Items.AddRange(GSP.Prikaz());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(GSP.Ispis(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()));
        }
       
    }
}
