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
    // Bu formun adına yanılmayın. Bu form öprenci formu değil ,kitap  işlemleri için kullanılan bir formdur.

    public partial class ogrenciform : Form
    {

        public void Listele()
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                string SorguCumlesi = "select * from kitaplar";
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


        public ogrenciform()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ogrenciform_Load(object sender, EventArgs e)
        {
            Listele();
            TurleriYukle();
            TurleriYukle();
        }
        public void temizlee()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void TurleriYukle()
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    string sorgu = "SELECT * FROM kitap_turleri";
                    SqlDataAdapter adaptor = new SqlDataAdapter(sorgu, baglanti);
                    DataTable tablo = new DataTable();
                    adaptor.Fill(tablo);

                    comboBox1.DataSource = tablo;
                    comboBox1.DisplayMember = "tur_adi";  // kullanıcıya görünen
                    comboBox1.ValueMember = "tur_id";     // veritabanına gidecek
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) //kaydet
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string SorguCumlesi = "insert into kitaplar (tur_id,kitap_adi,yazar,yayınevi,sayfa_sayisi) values (@tur,@adi,@yazar,@yayınevi,@sayi)";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@tur", comboBox1.SelectedValue);
                komut.Parameters.AddWithValue("@adi", textBox1.Text);
                komut.Parameters.AddWithValue("@yazar", textBox2.Text);
                komut.Parameters.AddWithValue("@yayınevi", textBox3.Text);
                komut.Parameters.AddWithValue("@sayi", int.Parse(textBox4.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizlee();
                Listele();


                MessageBox.Show("Kayıt işlemi başarılı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//sil
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string SorguCumlesi = "delete from kitaplar where kitap_id=@kitap_id";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@kitap_id", int.Parse(dataGridView1.CurrentRow.Cells["kitap_id"].Value.ToString()));
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme işlemi başarılı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();
                Listele();
            }

            catch (SqlException ex)
            {
                if (ex.Message.Contains("FK_odunc_kitaplar_kitaplar")) // yabancı anahtar hatasıysa
                {
                    MessageBox.Show("Bu kitap ödünç verildiği için silinemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                string SorguCumlesi = "update kitaplar set tur_id=@tur,kitap_adi=@adi,yazar=@yazar,yayınevi=@yayınevi,sayfa_sayisi=@sayi where kitap_id=@kitap_id";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@tur", comboBox1.SelectedValue);
                komut.Parameters.AddWithValue("@adi", textBox1.Text);
                komut.Parameters.AddWithValue("@yazar", textBox2.Text);
                komut.Parameters.AddWithValue("@yayınevi", textBox3.Text);
                komut.Parameters.AddWithValue("@sayi", int.Parse(textBox4.Text));
                komut.Parameters.AddWithValue("@kitap_id", int.Parse(dataGridView1.CurrentRow.Cells["kitap_id"].Value.ToString()));
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme işlemi başarılı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ogrenci_arama(string aranacakkelime)
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    string sorgu = "SELECT * FROM kitaplar WHERE kitap_adi LIKE @aranan";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@aranan", aranacakkelime + "%");

                        SqlDataAdapter da = new SqlDataAdapter(komut);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama hatası: " + ex.Message);
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ogrenci_arama(textBox5.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.SelectedItem = dataGridView1.CurrentRow.Cells["tur_id"].Value.ToString(); // kitap türü
            textBox1.Text = dataGridView1.CurrentRow.Cells["kitap_adi"].Value.ToString(); //kitap adı
            textBox2.Text = dataGridView1.CurrentRow.Cells["yazar"].Value.ToString(); // yazar
            textBox3.Text = dataGridView1.CurrentRow.Cells["yayınevi"].Value.ToString(); // yayınevi
            textBox4.Text = dataGridView1.CurrentRow.Cells["sayfa_sayisi"].Value.ToString(); // sayfa sayısı

        }
    
    public void ogrenciArama(string aranacakKelime)
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    string sorgu = "SELECT * FROM ogrenciler WHERE adi LIKE @aranan";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@aranan", aranacakKelime + "%");

                        SqlDataAdapter da = new SqlDataAdapter(komut);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama hatası: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
    
    
    
   

