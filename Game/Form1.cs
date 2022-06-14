using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        int ProbelX = 0, ProbelY = 0, Shirota = 0, StoronaKvadrata = 0, Diametr = 0, ShirotaStenki = 0;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void GrafKvadrat()
        {
            Shirota = pictureBox1.Width;
            StoronaKvadrata = Shirota / 14;

            pictureBox1.Image = new Bitmap(Shirota, Shirota);
            Graphics gr = Graphics.FromImage(pictureBox1.Image);

            ProbelX = StoronaKvadrata / 2;
            ProbelY = StoronaKvadrata / 2;

            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    gr.FillRectangle(Brushes.White, ProbelX, ProbelY, StoronaKvadrata, StoronaKvadrata);
                    ProbelX = ProbelX + StoronaKvadrata / 2 + StoronaKvadrata;
                }
                ProbelX = StoronaKvadrata / 2;
                ProbelY = ProbelY + StoronaKvadrata / 2 + StoronaKvadrata;
            }
            pictureBox1.Refresh();
        }
        void PlaerGreen()
        {
            Shirota = pictureBox1.Width;
            Diametr = Shirota / 14;

            Graphics gr = Graphics.FromImage(pictureBox1.Image);

            ProbelX = Diametr / 2;
            gr.FillEllipse(new SolidBrush(Color.Green), Shirota / 2 - ProbelX, ProbelX, Diametr, Diametr);
        }
        void PlaerRed()
        {
            Shirota = pictureBox1.Width;
            Diametr = Shirota / 14;

            Graphics gr = Graphics.FromImage(pictureBox1.Image);

            ProbelX = Diametr / 2;
            gr.FillEllipse(new SolidBrush(Color.Red), Shirota / 2 - ProbelX, Shirota - ProbelX - Diametr, Diametr, Diametr);
        }
        void StenkaVertik()
        {
            Shirota = pictureBox1.Width;
            StoronaKvadrata = Shirota / 14;
            ShirotaStenki = Shirota / 14 / 2;

            Graphics gr = Graphics.FromImage(pictureBox1.Image);

            ProbelX = StoronaKvadrata / 2;
            gr.FillRectangle(Brushes.Gray, StoronaKvadrata + ProbelX, ProbelX, ShirotaStenki, StoronaKvadrata * 2 + ProbelX);
        }

        void StenkaGorizont()
        {
            Shirota = pictureBox1.Width;
            StoronaKvadrata = Shirota / 14;
            ShirotaStenki = Shirota / 14 / 2;

            Graphics gr = Graphics.FromImage(pictureBox1.Image);

            ProbelX = StoronaKvadrata / 2;
            gr.FillRectangle(Brushes.Gray, ProbelX, StoronaKvadrata + ProbelX, StoronaKvadrata * 2 + ProbelX,ShirotaStenki );
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GrafKvadrat();
            PlaerGreen();
            PlaerRed();
            StenkaVertik();
            StenkaGorizont();
        }
    }
}
