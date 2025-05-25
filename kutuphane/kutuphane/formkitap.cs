using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace kutuphane
{




    // Bu formun adına yanılmayın. Bu form kitap formu değil, öğrenci formudur. 

    public partial class formkitap : Form
    {
        public formkitap()
        {
            InitializeComponent();
        }

        public void temizlee()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();


        }
        public void Listele()
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                string SorguCumlesi = "select * from ogrenciler";
                SqlDataAdapter adaptor = new SqlDataAdapter(SorguCumlesi, baglanti);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                dataGridView1.DataSource = tablo;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ;



        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string SorguCumlesi = "insert into ogrenciler (ogrenci_no ,adi , soyadi ,cinsiyet ,telefon ,sinif) values (@ogrenci_no,@adi,@soyadi,@cinsiyet,@telefon,@sinif)";
                SqlCommand komut = new SqlCommand(SorguCumlesi, baglanti);
                komut.Parameters.AddWithValue("@ogrenci_no", int.Parse(textBox1.Text));
                komut.Parameters.AddWithValue("@adi", textBox2.Text);
                komut.Parameters.AddWithValue("@soyadi", textBox3.Text);
                komut.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                komut.Parameters.AddWithValue("@telefon", int.Parse(textBox4.Text));
                komut.Parameters.AddWithValue("@sinif", (comboBox2.SelectedItem.ToString()));
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizlee();
                MessageBox.Show("işlem başarılı", "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Listele();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formkitap_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["ogrenci_no"].Value.ToString(); // Öğrenci no
                textBox2.Text = dataGridView1.CurrentRow.Cells["adi"].Value.ToString();        // Ad
                textBox3.Text = dataGridView1.CurrentRow.Cells["soyadi"].Value.ToString();     // Soyad
                comboBox2.SelectedItem = dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString(); // Cinsiyet
                textBox4.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();    // Telefon
                comboBox1.SelectedItem = dataGridView1.CurrentRow.Cells["sinif"].Value.ToString();    // Sınıf
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                string sorgu_cumlesi = "delete from ogrenciler where ogrenci_no=@ogrenci_no";
                SqlCommand komut = new SqlCommand(sorgu_cumlesi, baglanti);
                komut.Parameters.AddWithValue("@ogrenci_no", dataGridView1.CurrentRow.Cells["ogrenci_no"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizlee();
                MessageBox.Show("silme işlemi başarılı", "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("lütfen boş alan bırakmayınız", "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
    {
                try
                {
                    string baglantiCumlesi = "Data Source=CEZA\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True;";
                    using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                    {
                        baglanti.Open();

                        string komutSatiri = "UPDATE ogrenciler SET adi = @adi, soyadi = @soyadi, cinsiyet = @cinsiyet, telefon = @telefon, sinif = @sinif WHERE ogrenci_no = @ogrenci_no";
                        using (SqlCommand komut = new SqlCommand(komutSatiri, baglanti))
                        {
                            komut.Parameters.AddWithValue("@adi", textBox2.Text); // Ad
                            komut.Parameters.AddWithValue("@soyadi", textBox3.Text); // Soyad
                            komut.Parameters.AddWithValue("@cinsiyet", comboBox2.SelectedItem?.ToString()); // Cinsiyet
                            komut.Parameters.AddWithValue("@telefon", textBox4.Text); // Telefon
                            komut.Parameters.AddWithValue("@sinif", comboBox1.SelectedItem?.ToString()); // Sınıf
                            komut.Parameters.AddWithValue("@ogrenci_no", Convert.ToInt32(textBox1.Text)); // Öğrenci No

                            komut.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Güncelleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                    temizlee();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
    catch (Exception ex)
    {
        MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }


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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ogrenciArama(textBox5.Text);


        }
    }    
    }

