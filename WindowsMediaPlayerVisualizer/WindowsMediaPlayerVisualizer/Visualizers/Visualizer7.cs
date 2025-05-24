using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    public class Visualizer7
    {
        private Panel canvas;
        private float[] amplitudes;
        private float angleStep;
        private float rotation = 0;

        public Visualizer7(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.Paint += Canvas_Paint;

            amplitudes = new float[60];
            angleStep = 360f / amplitudes.Length;
        }
        public void Update(float[] freqs)
        {
            int sourceLen = freqs.Length;
            int targetLen = amplitudes.Length;

            for (int i = 0; i < targetLen; i++)
            {
                float freq = freqs[i % sourceLen];
                float db = 20 * (float)Math.Log10(freq + 1e-6f);
                amplitudes[i] = Math.Max((db + 60) * 1.5f, 2f);
            }

            rotation += 0.01f;
            canvas.Invalidate();
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Black);

            float centerX = canvas.Width / 2f;
            float centerY = canvas.Height / 2f;
            float radius = Math.Min(centerX, centerY) * 0.6f;

            using (Pen pen = new Pen(Color.DeepPink, 4))
            {
                for (int i = 0; i < amplitudes.Length; i++)
                {
                    float angle = (i * angleStep + rotation * 180f / (float)Math.PI) * (float)Math.PI / 180f;
                    float x1 = centerX + radius * (float)Math.Cos(angle);
                    float y1 = centerY + radius * (float)Math.Sin(angle);
                    float x2 = centerX + (radius + amplitudes[i]) * (float)Math.Cos(angle);
                    float y2 = centerY + (radius + amplitudes[i]) * (float)Math.Sin(angle);

                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }
    }
}
