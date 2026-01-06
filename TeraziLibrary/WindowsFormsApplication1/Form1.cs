using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TeraziLibrary;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 yeni = new Class1();
            string yazı = yeni.Terazi("COM4", "38400", "n", "8", "1", "4B4559303136800D0A");

        }
    }
}
