using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace yazlab2_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8, pic9, pic10, pic11, pic12, pic13, pic14, pic15, pic16;
        private Bitmap[] picArr, picMixArr;
        private double skor = -1;
        private PictureBox ilkKutu = null;
        private int globalGenislik, globalYukseklik, flag = -1;

        private void fotoSecBtn(object sender, EventArgs e)
        {
            Bitmap sourceImage = null;
            button2.Enabled = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceImage = new Bitmap(openFileDialog1.FileName); // Kaynak fotonun alinmasi
            }

            if (sourceImage != null)
            {
                skor = 0;
                button1.Visible = false;

                int pGenislik = sourceImage.Width / 4;
                int pYukseklik = sourceImage.Height / 4;
                globalGenislik = pGenislik;
                globalYukseklik = pYukseklik;

                // 1. satir
                pic1 = parcaOlustur(0, 0, sourceImage);
                pic2 = parcaOlustur(pGenislik, 0, sourceImage);
                pic3 = parcaOlustur(pGenislik * 2, 0, sourceImage);
                pic4 = parcaOlustur(pGenislik * 3, 0, sourceImage);

                // 2. satir
                pic5 = parcaOlustur(0, pYukseklik, sourceImage);
                pic6 = parcaOlustur(pGenislik, pYukseklik, sourceImage);
                pic7 = parcaOlustur(pGenislik * 2, pYukseklik, sourceImage);
                pic8 = parcaOlustur(pGenislik * 3, pYukseklik, sourceImage);

                // 3. satir
                pic9 = parcaOlustur(0, pYukseklik * 2, sourceImage);
                pic10 = parcaOlustur(pGenislik, pYukseklik * 2, sourceImage);
                pic11 = parcaOlustur(pGenislik * 2, pYukseklik * 2, sourceImage);
                pic12 = parcaOlustur(pGenislik * 3, pYukseklik * 2, sourceImage);

                // 4. satir
                pic13 = parcaOlustur(0, pYukseklik * 3, sourceImage);
                pic14 = parcaOlustur(pGenislik, pYukseklik * 3, sourceImage);
                pic15 = parcaOlustur(pGenislik * 2, pYukseklik * 3, sourceImage);
                pic16 = parcaOlustur(pGenislik * 3, pYukseklik * 3, sourceImage);

                Bitmap[] tempArr = { pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8, pic9, pic10, pic11, pic12, pic13, pic14, pic15, pic16 };
                picArr = tempArr;
                PictureBox[] boxArray = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10, box11, box12, box13, box14, box15, box16 };
                Random rnd = new Random();
                picMixArr = picArr.OrderBy(x => rnd.Next()).ToArray();

                for (int i = 0; i < 16; i++)
                {
                    boxArray[i].Image = picMixArr[i];
                }
                int sayac = 0;
                for (int i = 0; i < 16; i++)
                {
                    if (compare(picArr[i], picMixArr[i]))
                    {
                        boxArray[i].Enabled = false;
                        sayac++;
                    }
                    else boxArray[i].Enabled = true;
                }

                if (sayac != 0)
                {
                    if (sayac == 16)
                    {
                        label1.Text = "Skor: 100";
                        oyunuBitir();
                    }
                    else
                    {
                        skor = sayac * 6.25;
                        label1.Text = "Skor: " + skor.ToString();
                        button2.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Doğru parça yok. Karıştıra basın.");
                }

            }
            else
            {
                MessageBox.Show("Fotoğraf seçiniz.");
            }
        }

        private void karistirBtn(object sender, EventArgs e)
        {
            int sayac = 0;
            PictureBox[] boxArray = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10, box11, box12, box13, box14, box15, box16 };
            Random rnd = new Random();
            picMixArr = picArr.OrderBy(x => rnd.Next()).ToArray();
            for (int i = 0; i < 16; i++)
            {
                boxArray[i].Image = picMixArr[i];
            }
            for (int i = 0; i < 16; i++)
            {
                if (compare(picArr[i], picMixArr[i]))
                {
                    boxArray[i].Enabled = false;
                    sayac++;
                }
                else boxArray[i].Enabled = true;
            }
            if (sayac != 0)
            {
                skor = sayac * 6.25;
                label1.Text = "Skor: " + skor.ToString();
                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Doğru parça yok. Karıştıra basın.");
            }
        }

        private static bool compare(Bitmap pic1, Bitmap pic2)
        {
            int flag = -1;
            for (int i = 0; i < pic1.Width; i++)
            {
                for (int j = 0; j < pic1.Height; j++)
                {
                    Color p1 = pic1.GetPixel(i, j);
                    Color p2 = pic2.GetPixel(i, j);

                    if (p1 != p2)
                    {
                        flag = 100;
                        break;
                    }
                }
            }

            if (flag == 100) return false;
            else return true;

        }

        private static Bitmap parcaOlustur(int p, int q, Bitmap image)
        {
            Bitmap parca = new Bitmap(image.Width / 4, image.Height / 4);

            for (int i = 0 + p; i < image.Width / 4 + p; i++)
            {
                for (int j = 0 + q; j < image.Height / 4 + q; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    parca.SetPixel(i - p, j - q, pixel);
                }
            }
            return parca;
        }


        // https://www.kodlamamerkezi.com/c-net/c-ile-dosya-okuma-ve-yazma-islemleri/
        public static double yuksekSkorGetir()
        {
            string path = @"enyüksekskor.txt";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string skor = sr.ReadLine();
            double enBuyuk = double.Parse(skor);
            while (skor != null)
            {
                if (enBuyuk < double.Parse(skor)) enBuyuk = double.Parse(skor);
                skor = sr.ReadLine();

            }
            sr.Close();
            fs.Close();
            return enBuyuk;
        }

        private static void skorYaz(double skor)
        {
            string path = @"enyüksekskor.txt";
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(skor.ToString());
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private void boxClick(object sender, EventArgs e)
        {
            PictureBox kutu = (PictureBox)sender;
            flag = -1;
            if (ilkKutu == null)
            {
                ilkKutu = kutu;
            }
            else
            {
                if (kutu != ilkKutu)
                {
                    int ikinciKutu = int.Parse(ilkKutu.Name.Substring(3));
                    int ilkKutuNo = int.Parse(kutu.Name.Substring(3));

                    if (!compare(picMixArr[ikinciKutu - 1], picArr[ilkKutuNo - 1])) // ilk tıklanan kutu - ikinci tıklanan kutu
                    {
                        flag = 100;
                    }

                    if (flag != 100)
                    {

                        ilkKutu.Image = picMixArr[ilkKutuNo - 1];
                        kutu.Image = picMixArr[ikinciKutu - 1];

                        Bitmap temp = picMixArr[ilkKutuNo - 1];
                        picMixArr[ilkKutuNo - 1] = picMixArr[ikinciKutu - 1];
                        picMixArr[ikinciKutu - 1] = temp;
                        skor += 6.25;
                        kutu.Enabled = false;
                        if (compare(picMixArr[ikinciKutu - 1], picArr[ikinciKutu - 1]))
                        {
                            ilkKutu.Enabled = false;
                            skor += 6.25;
                        }

                        label1.Text = "Skor: " + skor.ToString();

                        if (oyunBittiMi())
                        {
                            oyunuBitir();
                        }

                        ilkKutu = null;
                        kutu = null;
                    }
                    else
                    {
                        skor -= 6.25;
                        label1.Text = "Skor: " + skor.ToString();
                        ilkKutu = null;
                        kutu = null;
                    }
                }
                else
                {
                    MessageBox.Show("iki kere tikladin");
                    ilkKutu = null;
                    kutu = null;
                }
            }
        }

        private bool oyunBittiMi()
        {
            int sayac = 0;
            for (int i = 0; i < 16; i++)
            {
                if (compare(picArr[i], picMixArr[i])) sayac++;
            }
            if (sayac == 16)
            {
                return true;
            }
            else return false;
        }

        private void oyunuBitir()
        {
            PictureBox[] boxArray = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10, box11, box12, box13, box14, box15, box16 };
            skorYaz(skor);
            for (int i = 0; i < 16; i++) boxArray[i].Enabled = false;
            button1.Visible = true;
            MessageBox.Show("oyun bitti yeni fotoğraf seçebilrsiniz");
            label2.Text = "Yüksek Skor: " + yuksekSkorGetir().ToString();
            skor = 0;
            label1.Text = "Skor: " + skor.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Yüksek Skor: " + yuksekSkorGetir().ToString();
            PictureBox[] boxArray = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10, box11, box12, box13, box14, box15, box16 };
            for (int i = 0; i < 16; i++)
            {
                boxArray[i].Enabled = false;
            }

            Bitmap sourceImage = new Bitmap(@"images\giris.jpg");

            int pGenislik = sourceImage.Width / 4;
            int pYukseklik = sourceImage.Height / 4;

            // 1. satir
            pic1 = parcaOlustur(0, 0, sourceImage); box1.Image = pic1;
            pic2 = parcaOlustur(pGenislik, 0, sourceImage); box2.Image = pic2;
            pic3 = parcaOlustur(pGenislik * 2, 0, sourceImage); box3.Image = pic3;
            pic4 = parcaOlustur(pGenislik * 3, 0, sourceImage); box4.Image = pic4;

            // 2. satir
            pic5 = parcaOlustur(0, pYukseklik, sourceImage); box5.Image = pic5;
            pic6 = parcaOlustur(pGenislik, pYukseklik, sourceImage); box6.Image = pic6;
            pic7 = parcaOlustur(pGenislik * 2, pYukseklik, sourceImage); box7.Image = pic7;
            pic8 = parcaOlustur(pGenislik * 3, pYukseklik, sourceImage); box8.Image = pic8;

            // 3. satir
            pic9 = parcaOlustur(0, pYukseklik * 2, sourceImage); box9.Image = pic9;
            pic10 = parcaOlustur(pGenislik, pYukseklik * 2, sourceImage); box10.Image = pic10;
            pic11 = parcaOlustur(pGenislik * 2, pYukseklik * 2, sourceImage); box11.Image = pic11;
            pic12 = parcaOlustur(pGenislik * 3, pYukseklik * 2, sourceImage); box12.Image = pic12;

            // 4. satir
            pic13 = parcaOlustur(0, pYukseklik * 3, sourceImage); box13.Image = pic13;
            pic14 = parcaOlustur(pGenislik, pYukseklik * 3, sourceImage); box14.Image = pic14;
            pic15 = parcaOlustur(pGenislik * 2, pYukseklik * 3, sourceImage); box15.Image = pic15;
            pic16 = parcaOlustur(pGenislik * 3, pYukseklik * 3, sourceImage); box16.Image = pic16;
        }
    }
}