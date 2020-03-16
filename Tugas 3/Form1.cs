using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas_3
{
    public partial class Form1 : Form
    {
        // Deklarasi property
        private int periode = 0,
                    therm = 0,
                    performance = 85,
                    progress = 0,
                    battery = 75,
                    timer = 0,
                    therm_next = 30,
                    time = 0;
        private double poin = 0.0,
                        step = 0.0;
        Random acak = new Random(); // Instansiasi objek random
        public Form1()
        {
            InitializeComponent();

        }

        private void lbAnalogMeter1_Load(object sender, EventArgs e)
        {

        }

        private void aquaGauge1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ( time != 0 && periode != 0)
            {
                timer1.Enabled = !(timer1.Enabled); // toogle timer
                // Menggati warna dan tulisan button ketika di klik
                if (timer1.Enabled == true)
                {
                    button1.Text = "STOP";  // Mengganti tulisan menjadi stop ketika timer berjalan
                    button1.BackColor = Color.Red;  // Mengganti warna menjadi merah ketika timer berjalan
                    lbButton1.ButtonColor = Color.Lime;    // Menyalakan lampu indikasi bahwa mesin berjalan
                }
                else
                {
                    button1.Text = "START"; // Mengganti tulisan menjadi START ketika timer berhenti
                    button1.BackColor = Color.Green;    // Mengganti warna menjadi hijau ketika timer berhenti
                    lbButton1.ButtonColor = Color.Red;    // Menyalakan lampu indikasi bahwa mesin berhenti
                    // Reset Value :: Menubah semua ke keadaan awal
                    trackBar1.Value = 0; trackBar2.Value = 0; progress = 0; timer = 0; time = 0; periode = 0;
                    lB7SegmentDisplay1.Value = 0;
                    lB7SegmentDisplay2.Value = 0;
                    lB7SegmentDisplay3.Value = 0;
                    lB7SegmentDisplay4.Value = 0;
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            time =  trackBar1.Value;    // Menangkap set timer dari trackbar

            // Mengganti tampilan 7 segmen dengan inputan user
            if (time < 10)
            {
                lB7SegmentDisplay2.Value = time;    // Ketika input satu digit, mengganti 7 segmen kedua dengan nilai input
                lB7SegmentDisplay1.Value = 0;   // Memastikan digit puluhan 0
            }
            else
            {
                lB7SegmentDisplay1.Value = 1;   // Ketika input = 10, mengganti 7 segmen paling awal dengan 1
                lB7SegmentDisplay2.Value = 0;   // memasitikan digit satuan 0
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            periode = trackBar2.Value; // Menangkap perubahan nilai trackbar ke dalam variabel periode
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // --->>> BATTERY SIMULATOR <<<---
            if (timer % 100 == 0)
            {
                battery -= 1;   // Simulasi battery berkurang 1% setiap 10 detik
                circularProgressBar1.Text = battery + "%";  // Mengganti teks battery indikator
                circularProgressBar1.Value = battery;  // Mengganti Value battery indikator
            }

            // Setting warna progress bar
            if (battery >= 70)
                circularProgressBar1.ProgressColor = Color.FromArgb(35, 196, 142);  // Mengganti warna battery indicator menjadi hijau jika lebih dari = 70%
            else if (battery > 40)
                circularProgressBar1.ProgressColor = Color.FromArgb(240, 160, 3);   // Mengganti warna battery indicator menjadi kuning jika lebih dari > 40% dan < 70%
            else
                circularProgressBar1.ProgressColor = Color.FromArgb(211, 67, 92);   // Mengganti warna battery indicator menjadi hijau jika lebih dari < 40%

            // --->>> END BATTERY SIMULATOR <<<---


            // --->>> PERFORMANCE SIMULATOR <<<---
            if (acak.Next(1, 100) % 8 == 0) // Membuat bilangan acak untuk menentuka nilai performance
            {
                if (performance > 60)   // Membatasi agar tidak lebih dari 100
                {
                    performance -= 1;   // Mengurangi performance dengan 1 jika acak habis dibagi 7
                    circularProgressBar2.Text = performance + "%"; // Mengganti teks performance indikator
                    circularProgressBar2.Value = performance; // Mengganti Value performance indikator
                }
            }
            if (acak.Next(1, 100) % 9 == 0) // Membuat bilangan acak untuk menentuka nilai performance
            {
                if (performance < 100) // Membatasi agar tidak lebih dari 100
                {
                    performance += 1;   // Menambah performance dengan 1 jika acak habis dibagi 3
                    circularProgressBar2.Text = performance + "%"; // Mengganti teks performance indikator
                    circularProgressBar2.Value = performance; // Mengganti value performance indikator
                }
            }
            // Setting warna performance bar
            if (performance >= 80)
                circularProgressBar2.ProgressColor = Color.FromArgb(35, 196, 142);  // Mengganti warna performance indicator menjadi hijau jika lebih dari = 80%
            else if (performance > 60)
                circularProgressBar2.ProgressColor = Color.FromArgb(240, 160, 3);   // Mengganti warna performance indicator menjadi kuning jika antara 60% - 80%
            else
                circularProgressBar2.ProgressColor = Color.FromArgb(211, 67, 92);   // Mengganti warna performance indicator menjadi hijau jika < 60%

            // --->>> END PERFORMANCE SIMULATOR <<<---



            // TIMER
            if (timer!= 0 && timer % 10 == 0)    // karena timer_tick dijalankan stiap 0.1 detik, butuh 10 kali tick untuk mendapatkan 1 detik
            {
                // --->>> PROGRESS BAR <<<---
                progress = (timer / 10) * 100 / (time * 60);    // Menghitung progress = waktu berjalan / timer yang di set
                progressBar1.Value = (int)progress; // Mengganti value dari progress sesuai dengan perhitungan
                label7.Text = progress + "%";   // Mengganti teks progress sesuai perhitungan

                // --->>> END PROGRESS BAR SIMULATOR <<<---


                // --->>> CHART SIMULATOR <<<--- 
                if ((timer / 10) < periode)
                    chart1.Series[0].Points.AddY(30);   // Menetapkan suhu awal 30 derajat
                else if ((timer / 10) % periode == 0)   // Menangkap data suhu sesuai periode yang ditetapkan
                {
                    therm = therm_next;     // Menangkap nilai suhu nanti yang ditetapkan tadi menjadi suhu sekarang
                    therm_next = acak.Next(20, 40);     // Membuat bilangan acak untuk menentukan suhu nanti
                    chart1.Series[0].Points.AddY(therm);    // Plot suhu sekarang ke chart
                    poin = therm_next;  // Menyimpan nilai suhu nanti
                    step = (therm_next - therm)/periode;    // Membuat nilai buatan seolah suhu berubah secara linear
                }
                else {
                    // Memplot suhu ke chart ketika tidak pada periode agar grafik menjadi bagus
                    poin += step;
                    chart1.Series[0].Points.AddY(poin);
                }

                // --->>> END CHART SIMULATOR <<<---


                // --->>> COUNT-DOWN 7 SEGMEN <<<---

                lB7SegmentDisplay4.Value -= 1;  // Mengurangi digit detik dengan 1
                if (lB7SegmentDisplay4.Value < 0)
                {
                    lB7SegmentDisplay4.Value = 9;   // Menggari digit detik menjadi 9 setelah 0
                    lB7SegmentDisplay3.Value -= 1;  // Mengurangi digit puluhan menit dengan 1
                }
                if (lB7SegmentDisplay3.Value < 0)
                {
                    lB7SegmentDisplay2.Value -= 1;  // Mengurangi digit menit dengan 1
                    lB7SegmentDisplay3.Value = 5;   // Menggari digit puluhan detik menjadi 5 setelah 0
                }
                if (lB7SegmentDisplay2.Value < 0)
                {
                    lB7SegmentDisplay1.Value -= 1;  // Mengurangi digit puluhan menit dengan 1
                    lB7SegmentDisplay2.Value = 9;   // Menganti digit menit menjadi 9 setelah 0
                }
                if (lB7SegmentDisplay1.Value == 0 && lB7SegmentDisplay2.Value == 0 && lB7SegmentDisplay3.Value == 0 && lB7SegmentDisplay4.Value == 0)
                {
                    timer1.Enabled = false; // Mematikan timer
                    button1.Text = "START"; // Mengganti tulisan menjadi START ketika timer berhenti
                    button1.BackColor = Color.Green;    // Mengganti warna menjadi hijau ketika timer berhenti
                    progressBar1.Value = 100;
                    label7.Text = "100%";
                    // Reset Value ke keadaan awal
                    trackBar1.Value = 0; trackBar2.Value = 0; progress = 0; timer = 0; time = 0; periode = 0;
                    lB7SegmentDisplay1.Value = 0;
                    lB7SegmentDisplay2.Value = 0;
                    lB7SegmentDisplay3.Value = 0;
                    lB7SegmentDisplay4.Value = 0;
                }

                // --->>> END COUNT-DOWN 7 SEGMEN <<<---
                
            }


            timer += 1; //timer decrement
        }

        private void lbButton3_Load(object sender, EventArgs e)
        {

        }
    }
}
