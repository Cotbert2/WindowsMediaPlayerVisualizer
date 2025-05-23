using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayerVisualizer.Utils
{
    public class FFTSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        private readonly int fftLength;
        private readonly Complex[] fftBuffer;
        private readonly float[] result;
        private int bufferPos = 0;
        private readonly int channels;
        private readonly object lockObj = new object();

        public float[] Frequencies => result;

        public FFTSampleProvider(ISampleProvider source, int fftLength = 64)
        {
            this.source = source;
            this.fftLength = fftLength;
            fftBuffer = new Complex[fftLength];
            result = new float[fftLength / 2];
            channels = source.WaveFormat.Channels;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = source.Read(buffer, offset, count);
            lock (lockObj)
            {
                for (int i = 0; i < samplesRead; i += channels)
                {
                    float sample = buffer[offset + i];

                    fftBuffer[bufferPos].X = (float)(sample * FastFourierTransform.HammingWindow(bufferPos, fftLength));
                    fftBuffer[bufferPos].Y = 0;
                    bufferPos++;

                    if (bufferPos >= fftLength)
                    {
                        FastFourierTransform.FFT(true, (int)Math.Log(fftLength, 2.0), fftBuffer);

                        for (int j = 0; j < result.Length; j++)
                        {
                            result[j] = (float)Math.Sqrt(fftBuffer[j].X * fftBuffer[j].X + fftBuffer[j].Y * fftBuffer[j].Y);
                        }

                        bufferPos = 0;
                    }
                }
            }

            return samplesRead;
        }

        public WaveFormat WaveFormat => source.WaveFormat;

        public float[] GetFrequencies()
        {
            lock (lockObj)
            {
                return result.ToArray();
            }
        }
    }

}
