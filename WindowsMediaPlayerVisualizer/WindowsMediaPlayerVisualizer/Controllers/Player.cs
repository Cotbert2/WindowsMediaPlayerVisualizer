using FontAwesome.Sharp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Guna.UI2.WinForms;
using System.Threading.Tasks;

namespace WindowsMediaPlayerVisualizer.Controllers
{
    public class Player
    {
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private bool isPlaying = false;
        private Label lblArtist;
        private Label lblSong;
        private Guna2TrackBar barPlayer;
        private IconButton btnPlay;
        private Timer timer1;
        private Label lblCounter;
        private Label lblCountdown;

        public Player(Label lblArtist, Label lblSong, Guna2TrackBar barPlayer, IconButton btnPlay,
            Timer timer1, Label lblCounter, Label lblCountdown) 
        {
            this.lblArtist = lblArtist;
            this.lblSong = lblSong;
            this.barPlayer = barPlayer;
            this.btnPlay = btnPlay;
            this.timer1 = timer1;
            this.lblCounter = lblCounter;
            this.lblCountdown = lblCountdown;
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

        public void play()
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
                return;
            }

            waveOut.Pause();
            isPlaying = false;
            btnPlay.IconChar = IconChar.Play;
            timer1.Stop();
        }

        public void stop()
        {
            if (waveOut == null || audioFileReader == null) return;

            waveOut.Stop();
            audioFileReader.Position = 0;
            btnPlay.IconChar = IconChar.Play;
            isPlaying = false;

        }

        public void backward() {
            if (audioFileReader == null) return;

            var newTime = audioFileReader.CurrentTime - TimeSpan.FromSeconds(5);

            if (newTime < TimeSpan.Zero)
                newTime = TimeSpan.Zero;

            audioFileReader.CurrentTime = newTime;
        }


        public void forward()
        {
            if (audioFileReader == null) return;

            var newTime = audioFileReader.CurrentTime + TimeSpan.FromSeconds(5);

            if (newTime < TimeSpan.Zero)
                newTime = TimeSpan.Zero;

            audioFileReader.CurrentTime = newTime;
        }
        public void hanldePosition() {
            if (audioFileReader != null)
            {
                double total = audioFileReader.TotalTime.TotalSeconds;
                double newPosition = (barPlayer.Value / 100.0) * total;
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(newPosition);
            }
        }

        public void tickAction() {
            if (audioFileReader != null && audioFileReader.TotalTime.TotalSeconds > 0)
            {
                double current = audioFileReader.CurrentTime.TotalSeconds;
                double total = audioFileReader.TotalTime.TotalSeconds;
                double remaining = total - current;


                int progress = (int)((current / total) * 100);
                barPlayer.Value = Math.Min(progress, 100);

                TimeSpan currentTime = TimeSpan.FromSeconds(current);
                TimeSpan remainingTime = TimeSpan.FromSeconds(remaining);

                lblCounter.Text = $"{currentTime.Minutes}:{zeroFormat(currentTime.Seconds)}";
                lblCountdown.Text = $"-{remainingTime.Minutes}:{zeroFormat(remainingTime.Seconds)}";

                if (audioFileReader.CurrentTime.TotalSeconds > audioFileReader.TotalTime.TotalSeconds) stop();
            }
        }

        private string zeroFormat(int time)
        {
            return (time <= 9) ? $"0{time}" : $"{time}";
        }

        public void selectFile() {
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
