using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        PictureBox game_map = new PictureBox();
        PictureBox zmija = new PictureBox();
        PictureBox hrana = new PictureBox();
        PictureBox[] zmija_delovi = new PictureBox[20];
        int cord_x = 1, cord_y = 0, index = 0;
        public int golemina { get; set; } = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game_map.Width = 500;
            game_map.Height = 500;
            game_map.BackColor = Color.Cyan;
            game_map.Location = new Point(40, 30);
            this.Location = new Point(50, 50);
            this.Width = this.Height = 600;
            this.Controls.Add(game_map);

            zmija.Width = zmija.Height = 18;
            zmija.BackColor = Color.Yellow;
            zmija.Location = new Point(100, 100);

            hrana.Width = hrana.Height = 18;
            hrana.BackColor = Color.Red;
            hrana.Location = new Point(300, 300);

            game_map.Controls.Add(zmija);
            game_map.Controls.Add(hrana);
            timer1.Start();
        }

        public void nova_hrana()
        {
            Random r = new Random();
            int x = r.Next(25);
            int y = r.Next(25);
            hrana.Location = new Point(x * 20, y * 20);
            for (int i = 1; i < index; i++)
            {
                if (hrana.Location == zmija_delovi[i].Location)
                {
                    nova_hrana();
                }
            }
        }

        public void zgolemi_zmija()
        {
            if (golemina == 20)
            {
                timer1.Stop();
                MessageBox.Show("ЧЕСТИТКИ! ПОБЕДИВТЕ!");
                this.Close();
            }

            if(index <= 19)
            {
                zmija_delovi[index] = new PictureBox();
                zmija_delovi[index].BackColor = Color.White;
                zmija_delovi[index].Location = zmija.Location;
                zmija_delovi[index].Width = zmija_delovi[index].Height = 18;
                game_map.Controls.Add(zmija_delovi[index]);
            }      
        }

        public void iscrtaj_zmija()
        {
            for(int i = index; i >=2; i--)
            {
                zmija_delovi[i].Location = zmija_delovi[i - 1].Location;
            }
            if (index > 0)
            {
                zmija_delovi[1].Location = zmija.Location;
            }
        }

        public void proveri()
        {
            if (zmija.Location.X < 0 || zmija.Location.X >= 500 || zmija.Location.Y < 0 || zmija.Location.Y >= 500)
            {
                timer1.Stop();
                MessageBox.Show("ИЗГУБИВТЕ!");
                this.Close();
            }
            for(int i = 1; i <index; i++)
            {
                if (zmija.Location == zmija_delovi[i].Location)
                {
                    timer1.Stop();
                    MessageBox.Show("ИЗГУБИВТЕ!");
                    this.Close();
                }
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Checked = !pauseToolStripMenuItem.Checked;
            if (pauseToolStripMenuItem.Checked)
            {
                timer1.Stop();
                pauseToolStripMenuItem.Text = "Продолжи";
            }
            else
            {
                timer1.Start();
                pauseToolStripMenuItem.Text = "Пауза";
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                cord_x = -1;
                cord_y = 0;
            }
            else if (e.KeyCode == Keys.Right)
            {
                cord_x = 1;
                cord_y = 0;
            }
            else if (e.KeyCode == Keys.Up)
            {
                cord_x = 0;
                cord_y = -1;
            }
            else if (e.KeyCode == Keys.Down)
            {
                cord_x = 0;
                cord_y = 1;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            iscrtaj_zmija();
            zmija.Location = new Point(zmija.Location.X + cord_x * 20, zmija.Location.Y + cord_y * 20);
            if (zmija.Location == hrana.Location)
            {
                index = index + 1;
                golemina += 1;
                goleminaStripStatusLabel1.Text = string.Format("Големина: {0}", golemina);
                zgolemi_zmija();
                nova_hrana();
            }
            proveri();
        }
    }
}
