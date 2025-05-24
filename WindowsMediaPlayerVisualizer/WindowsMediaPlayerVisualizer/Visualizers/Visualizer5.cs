using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace WindowsMediaPlayerVisualizer.Visualizers
{
    internal class Visualizer5
    {
        private Panel canvas;
        private float[] frequencies = new float[64];
        private float angle = 0;

        private class Particle
        {
            public float x, y;
            public float radius;
            public float angle;
            public float speed;
            public float baseSize;
            public Color color;
        }

        private List<Particle> particles = new List<Particle>();
        private Random rand = new Random();


        public Visualizer5(Panel canvas)
        {
            this.canvas = canvas;
            canvas.Paint += Canvas_Paint;

            for (int i = 0; i < 80; i++)
            {
                float radius = rand.Next(30, 150);
                float angle = (float)(rand.NextDouble() * Math.PI * 2);

                particles.Add(new Particle
                {
                    radius = radius,
                    angle = angle,
                    speed = (float)(rand.NextDouble() * 0.01 + 0.005),
                    baseSize = rand.Next(2, 6),
                    color = Color.FromArgb(100, Color.Cyan)
                });
            }

        }

        public void Update(float[] freqs)
        {
            frequencies = freqs;
            angle += 0.05f;
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

            float cx = canvas.Width / 2f;
            float cy = canvas.Height / 2f;
            int numWaves = 6;
            float maxRadius = Math.Min(cx, cy) - 0.2f; 

            for (int i = 0; i < numWaves; i++)
            {
                double rotation = (2 * Math.PI / numWaves) * i + angle;
                DrawWave(g, cx, cy, rotation, maxRadius);
                DrawBounceLine(g);
                DrawParticles(g);

            }


        }

        private void DrawWave(Graphics g, float cx, float cy, double rotation, float maxRadius)
        {
            
            PointF[] points = new PointF[frequencies.Length];
            for (int i = 0; i < frequencies.Length; i++)
            {
                float amplitude = Math.Min(frequencies[i] * 4f, 3f);
                float radius = maxRadius * amplitude;

                double angle = (i / (double)frequencies.Length) * Math.PI * 2;

                float x = cx + (float)(Math.Cos(angle + rotation) * radius);
                float y = cy + (float)(Math.Sin(angle + rotation) * radius);
                points[i] = new PointF(x, y);
            }
            for (int j = 1; j <= 3; j++)
            {
                PointF[] blurred = points.Select(p =>
                    new PointF(
                        cx + (p.X - cx) * (1f + j * 0.05f),
                        cy + (p.Y - cy) * (1f + j * 0.05f))
                    ).ToArray();

                using (Pen auraPen = new Pen(Color.FromArgb(255, Color.White), 2f))
                {
                    g.DrawPolygon(auraPen, blurred);
                }
            }
        }

        private void DrawBounceLine(Graphics g)
        {
            float avg = frequencies.Average(); 
            float height = Math.Min(avg * 2000f, 2000f); 

            float baseY = canvas.Height - 40;
            float lineY = baseY - height;

            using (Pen pen = new Pen(Color.AntiqueWhite, 4f))
            {
                g.DrawLine(pen, 40, lineY, canvas.Width - 40, lineY);
            }
        }

        private void DrawParticles(Graphics g)
        {
            float cx = canvas.Width / 2f;
            float cy = canvas.Height / 2f;

            float volume = frequencies.Average(); 

            foreach (var p in particles)
            {
                p.angle += p.speed;
                float x = cx + (float)(Math.Cos(p.angle) * p.radius);
                float y = cy + (float)(Math.Sin(p.angle) * p.radius);
                float size = p.baseSize + (volume * 20f); 

                using (Brush brush = new SolidBrush(p.color))
                {
                    g.FillEllipse(brush, x - size / 2f, y - size / 2f, size, size);
                }
            }
        }



    }
}
