using FontAwesome.Sharp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Guna.UI2.WinForms;
using System.Threading.Tasks;
using WindowsMediaPlayerVisualizer.Visualizers;
using NAudio.Wave.SampleProviders;
using WindowsMediaPlayerVisualizer.Utils;

namespace WindowsMediaPlayerVisualizer.Controllers
{
    public class Player
    {
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private ISampleProvider sampleProvider;

        private bool isPlaying = false;
        private Label lblArtist;
        private Label lblSong;
        private Guna2TrackBar barPlayer;
        private IconButton btnPlay;
        private Timer timer1;
        private Label lblCounter;
        private Label lblCountdown;
        private Panel canvas;

        //Visualizers
        private Visualizer1 visualizer1;
        private Visualizer2 visualizer2;
        private Visualizer3 visualizer3;
        private Visualizer4 visualizer4;


        private int currentVisualizerIndex = 0;
        private double visualizerTimerSeconds = 0;
        private const double visualizerDuration = 30;

        public Player(Label lblArtist, Label lblSong, Guna2TrackBar barPlayer, IconButton btnPlay,
            Timer timer1, Label lblCounter, Label lblCountdown, Panel canvas) 
        {
            this.lblArtist = lblArtist;
            this.lblSong = lblSong;
            this.barPlayer = barPlayer;
            this.btnPlay = btnPlay;
            this.timer1 = timer1;
            this.lblCounter = lblCounter;
            this.lblCountdown = lblCountdown;
            this.canvas = canvas;

            //setup visualizers

            visualizer1 = new Visualizer1(canvas);
            visualizer2 = new Visualizer2(canvas);
            visualizer3 = new Visualizer3(canvas);
            visualizer4 = new Visualizer4(canvas);

            currentVisualizer = visualizer1;
            canvas.Paint += visualizer1.Canvas_Paint;
        }

        private void setSong(string path)
        {
            var file = TagLib.File.Create(path);

            lblArtist.Text = file.Tag.FirstPerformer ?? "Unknown Artist";
            lblSong.Text = file.Tag.Title ?? "Unknown Song";

            audioFileReader = new AudioFileReader(path);
            var sampleChannel = new SampleChannel(audioFileReader, true);
            var meteringProvider = new MeteringSampleProvider(sampleChannel);

            meteringProvider.StreamVolume += (s, a) =>
            {
                currentVolume = a.MaxSampleValues.Average(); 
            };

            fftSampleProvider = new FFTSampleProvider(meteringProvider, 64);
            sampleProvider = fftSampleProvider;

            waveOut = new WaveOutEvent();
            waveOut.Init(sampleProvider);
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

        float currentVolume = 0f;
        float volumeSmoothing = 0.2f;
        private FFTSampleProvider fftSampleProvider;



        public void tickAction() {
            tickCounter++;
            if (tickCounter % 120 == 0)
            {
                SwitchVisualizer();
            }

            if (currentVisualizer is Visualizer1 v1 && fftSampleProvider != null)
                v1.Update(fftSampleProvider.GetFrequencies());
            else if (currentVisualizer is Visualizer2 v2)
                v2.Update(currentVolume, volumeSmoothing);
            else if (currentVisualizer is Visualizer3 v3)
                v3.Update();
            else if (currentVisualizer is Visualizer4 v4)
                v4.Update();
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

        private int tickCounter = 0;
        private object currentVisualizer;

        private void DetachCurrentVisualizer()
        {
            if (currentVisualizer is Visualizer1 v1) canvas.Paint -= v1.Canvas_Paint;
            else if (currentVisualizer is Visualizer2 v2) canvas.Paint -= v2.Canvas_Paint;
            else if (currentVisualizer is Visualizer3 v3) canvas.Paint -= v3.Canvas_Paint;
            else if (currentVisualizer is Visualizer4 v4) canvas.Paint -= v4.Canvas_Paint;
        }

        private void SwitchVisualizer()
        {
            DetachCurrentVisualizer();

            currentVisualizerIndex = (currentVisualizerIndex + 1) % 4;

            switch (currentVisualizerIndex)
            {
                case 0:
                    currentVisualizer = visualizer1;
                    break;
                case 1:
                    currentVisualizer = visualizer2;
                    break;
                case 2:
                    currentVisualizer = visualizer3;
                    break;
                case 3:
                    currentVisualizer = visualizer4;
                    currentVisualizer = visualizer4;
                    break;
            }

            if (currentVisualizer is Visualizer1 v1) canvas.Paint += v1.Canvas_Paint;
            else if (currentVisualizer is Visualizer2 v2) canvas.Paint += v2.Canvas_Paint;
            else if (currentVisualizer is Visualizer3 v3) canvas.Paint += v3.Canvas_Paint;
            else if (currentVisualizer is Visualizer4 v4) canvas.Paint += v4.Canvas_Paint;

            canvas.Invalidate();
        }



    }

}
