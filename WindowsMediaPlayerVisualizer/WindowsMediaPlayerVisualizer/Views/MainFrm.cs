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

        private void setSong(string path)
        {
            var file = TagLib.File.Create(path);

            lblArtist.Text = file.Tag.Title ?? "Unknown Song";
            lblSong.Text = file.Tag.FirstPerformer ?? "Unknown Artist";
            audioFileReader = new AudioFileReader(path);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
            barPlayer.Value = 0;
            isPlaying = false;
            btnPlay.IconChar = IconChar.Play;
        }

        private void onLoad(object sender, EventArgs e)
        {

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

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (waveOut == null || audioFileReader == null) return;

            waveOut.Stop();
            audioFileReader.Position = 0;
            btnPlay.IconChar = IconChar.Play;
            isPlaying = false;
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
                double remaining = total - current;




                int progress = (int)((current / total) * 100);
                barPlayer.Value = Math.Min(progress, 100);

                TimeSpan currentTime = TimeSpan.FromSeconds(current);
                TimeSpan remainingTime = TimeSpan.FromSeconds(remaining);

                lblCounter.Text = $"{currentTime.Minutes}:{zeroFormat( currentTime.Seconds)}";
                lblCountdown.Text = $"-{remainingTime.Minutes}:{zeroFormat(remainingTime.Seconds)}";
            }
        }

        private string zeroFormat(int time)
        {
            return (time <= 9)? $"0{time}" : $"{time}";
        }

        private void btnMusic_click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Audio File|*.mp3;*.wav;*.wma";
                openFileDialog.Title = "Chose an audio file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;

                    try
                    {
                        waveOut?.Stop();
                        audioFileReader?.Dispose();
                        waveOut?.Dispose();
                        setSong(path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar el archivo: " + ex.Message);
                    }
                }
            }
        }
    }
}
