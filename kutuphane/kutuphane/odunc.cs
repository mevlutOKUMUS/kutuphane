using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kutuphane
{
    public partial class odunc : Form
    {

     
        public void Listele()
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                string SorguCumlesi = "select * from odunc_kitaplar";
                SqlDataAdapter adaptor = new SqlDataAdapter(SorguCumlesi, baglanti);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                dataGridView1.DataSource = tablo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void temizlee()
        {
            textBox1.Clear();
            richTextBox1.Clear();
        }
        public odunc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//kaydet
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string SorguCumlesi = "insert into odunc_kitaplar (ogr_no,verilis_tarihi ,kitap_adi,aciklama) values (@okulno,@verilis,@kitap_adi,@acik)";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@okulno", textBox1.Text);
                komut.Parameters.AddWithValue("@kitap_adi", comboBox1.SelectedItem);
                komut.Parameters.AddWithValue("@aciklama", richTextBox1.Text);
                komut.Parameters.AddWithValue("@verilis", DateTime.Now.ToString("yyyy/MM/dd"));
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarıyla Eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizlee();
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        
       
        

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void odunc_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
