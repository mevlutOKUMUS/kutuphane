using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ogrenciform ogrenciform = new ogrenciform();
            ogrenciform.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formkitap formkitap = new formkitap();
            formkitap.ShowDialog();
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kitaptur kitaptur = new kitaptur(); 
            kitaptur.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            odunc odunc = new odunc();
            odunc.ShowDialog();
        }
    }
}
