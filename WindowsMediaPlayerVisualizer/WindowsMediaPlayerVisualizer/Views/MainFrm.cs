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

namespace WindowsMediaPlayerVisualizer.Views
{
    public partial class MainFrm : Form
    {

        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private bool isPlaying = false;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            try
            {
                string path = @"E:\Downloads\ROADS UNTRAVELED - Linkin Park (LIVING THINGS).mp3";

                audioFileReader = new AudioFileReader(path);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reproducir audio: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            waveOut?.Stop();
            audioFileReader?.Dispose();
            waveOut?.Dispose();
            base.OnFormClosing(e);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (audioFileReader == null || waveOut == null)
            {
                MessageBox.Show("No audio file loaded.");
                return;
            }

            if (!isPlaying)
            {
                waveOut.Play();
                isPlaying = true;
                btnPlay.IconChar = IconChar.Pause;
                timer1.Start();

            }
            else
            {
                waveOut.Pause();
                isPlaying = false;
                btnPlay.IconChar = IconChar.Play;
                timer1.Stop();
                barPlayer.Value = 0;

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (waveOut == null || audioFileReader == null)
                return;

            waveOut.Stop();
            audioFileReader.Position = 0; 
            isPlaying = false;
            btnPlay.Text = "Play";
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (audioFileReader == null) return;

            var newTime = audioFileReader.CurrentTime + TimeSpan.FromSeconds(5);

            if (newTime > audioFileReader.TotalTime)
                newTime = audioFileReader.TotalTime;

            audioFileReader.CurrentTime = newTime;
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (audioFileReader == null) return;

            var newTime = audioFileReader.CurrentTime - TimeSpan.FromSeconds(5);

            if (newTime < TimeSpan.Zero)
                newTime = TimeSpan.Zero;

            audioFileReader.CurrentTime = newTime;
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (audioFileReader != null)
            {
                double total = audioFileReader.TotalTime.TotalSeconds;
                double newPosition = (barPlayer.Value / 100.0) * total;
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(newPosition);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (audioFileReader != null && audioFileReader.TotalTime.TotalSeconds > 0)
            {
                double current = audioFileReader.CurrentTime.TotalSeconds;
                double total = audioFileReader.TotalTime.TotalSeconds;

                int progress = (int)((current / total) * 100);
                barPlayer.Value = Math.Min(progress, 100);
            }
        }
    }
}
