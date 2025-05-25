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
    
    
           
    public partial class kitaptur : Form
    {
        public void Listele()
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    string SorguCumlesi = "select * from kitap_turleri";
                    SqlDataAdapter adaptor = new SqlDataAdapter(SorguCumlesi, baglanti);
                    DataTable tablo = new DataTable();
                    adaptor.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public kitaptur()
        {
            InitializeComponent();
        }
        
        private void kitaptur_Load(object sender, EventArgs e)
        {
            Listele();

        }

        private void button1_Click(object sender, EventArgs e)// ekleme
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string SorguCumlesi = "insert into kitap_turleri (kitap_adi) values (@kitap_adi)";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@kitap_adi", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kitap türü eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();
                textBox1.Clear();
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)// silme
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string SorguCumlesi = "delete from kitap_turleri where kitap_adi=@kitap_adi";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@kitap_adi", dataGridView1.CurrentRow.Cells["kitap_adi"].Value.ToString());
                komut.ExecuteNonQuery();
                MessageBox.Show("Kitap türü silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();
                textBox1.Clear();
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)//güncelle
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                string eskiKitapAdi = dataGridView1.CurrentRow.Cells["kitap_adi"].Value.ToString();
                string yeniKitapAdi = textBox1.Text;

                string SorguCumlesi = "update kitap_turleri set kitap_adi = @yeni where kitap_adi = @eski";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@yeni", yeniKitapAdi);
                komut.Parameters.AddWithValue("@eski", eskiKitapAdi);
                komut.ExecuteNonQuery();

                MessageBox.Show("Kitap türü güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();
                textBox1.Clear();
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

