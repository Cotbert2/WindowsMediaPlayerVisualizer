using FontAwesome.Sharp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsMediaPlayerVisualizer.Controllers;
using WindowsMediaPlayerVisualizer.Visualizers;

namespace WindowsMediaPlayerVisualizer.Views
{
    public partial class MainFrm : Form
    {

        private Player mediaPlayer;

        public MainFrm()
        {
            InitializeComponent();
            mediaPlayer = new Player(lblArtist, lblSong, barPlayer, btnPlay, timer1, lblCounter, lblCountdown, canvas);
        }


        private void onLoad(object sender, EventArgs e)
        {

        }
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            mediaPlayer.play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mediaPlayer.stop();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            mediaPlayer.forward();
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            mediaPlayer.backward();
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            mediaPlayer.hanldePosition();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mediaPlayer.tickAction();
        }


        private void btnMusic_click(object sender, EventArgs e)
        {
            mediaPlayer.selectFile();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

    }

        private void Draw(object sender, PaintEventArgs e)
        {

        }
    }
}
