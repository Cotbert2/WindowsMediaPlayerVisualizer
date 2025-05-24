using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    internal class Visualizer6
    {
        private Panel canvas;
        private float[] frequencies = new float[64];
        private float angle = 0;
        private List<FloatingBubble> bubbles = new List<FloatingBubble>();
        private Random random = new Random();

        public Visualizer6(Panel canvas)
        {
            this.canvas = canvas;
            canvas.Paint += Canvas_Paint;
        }

        public void Update(float[] freqs)
        {
            frequencies = freqs;
            angle += 0.01f;


            if (random.NextDouble() < 0.2)
            {
                bubbles.Add(new FloatingBubble(
                    x: random.Next(canvas.Width),
                    y: canvas.Height + 10,
                    radius: random.Next(4, 10),
                    speed: (float)(random.NextDouble() * 1.5 + 0.5),
                    color: Color.FromArgb(50, Color.White)
                ));
            }


            for (int i = bubbles.Count - 1; i >= 0; i--)
            {
                bubbles[i].Y -= bubbles[i].Speed;
                if (bubbles[i].Y + bubbles[i].Radius < 0)
                    bubbles.RemoveAt(i);
            }

            canvas.Invalidate();
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Black);


            foreach (var b in bubbles)
            {
                using (Brush brush = new SolidBrush(b.Color))
                {
                    g.FillEllipse(brush, b.X - b.Radius, b.Y - b.Radius, b.Radius * 2, b.Radius * 2);
                }
            }

            float cx = canvas.Width / 2f;
            float cy = canvas.Height / 2f;
            float baseRadius = Math.Min(cx, cy) * 0.5f;

            int detail = 256;
            PointF[] points = new PointF[detail];

            for (int i = 0; i < detail; i++)
            {
                float interpIndex = (i / (float)detail) * frequencies.Length;
                int idx = (int)Math.Floor(interpIndex);
                int next = (idx + 1) % frequencies.Length;
                float t = interpIndex - idx;

                float amp = 0;
                if (idx < frequencies.Length && next < frequencies.Length)
                {
                    float interp = frequencies[idx] * (1 - t) + frequencies[next] * t;
                    amp = Math.Min(interp * 12f, 1.2f);
                }

                float radius = baseRadius + amp * 50f;
                double theta = (i / (double)detail) * Math.PI * 2;
                float x = cx + (float)(Math.Cos(theta + angle) * radius);
                float y = cy + (float)(Math.Sin(theta + angle) * radius);
                points[i] = new PointF(x, y);
            }

            float hue = (float)(angle % 1.0);
            Color dynamicColor = ColorFromHSV(hue * 360, 1, 1);

            using (Pen pen = new Pen(dynamicColor, 2f))
            {
                g.DrawClosedCurve(pen, points, 1.0f, FillMode.Alternate);
            }


            for (int i = 0; i < 12; i++)
            {
                int band = random.Next(frequencies.Length);
                float amplitude = frequencies[band] * 80f;

                float angleOffset = angle * 2 + i;
                float r = baseRadius + amplitude;

                float x = cx + (float)Math.Cos(angleOffset) * r;
                float y = cy + (float)Math.Sin(angleOffset) * r;

                Color bubbleColor = ColorFromHSV((hue * 360 + i * 30) % 360, 0.8, 1);
                using (Brush b = new SolidBrush(Color.FromArgb(100, bubbleColor)))
                {
                    g.FillEllipse(b, x - 6, y - 6, 12, 12);
                }
            }
        }

        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0: return Color.FromArgb(v, t, p);
                case 1: return Color.FromArgb(q, v, p);
                case 2: return Color.FromArgb(p, v, t);
                case 3: return Color.FromArgb(p, q, v);
                case 4: return Color.FromArgb(t, p, v);
                default: return Color.FromArgb(v, p, q);
            }
        }


        private class FloatingBubble
        {
            public float X, Y, Radius, Speed;
            public Color Color;

            public FloatingBubble(float x, float y, float radius, float speed, Color color)
            {
                X = x;
                Y = y;
                Radius = radius;
                Speed = speed;
                Color = color;
            }
        }
    }
}