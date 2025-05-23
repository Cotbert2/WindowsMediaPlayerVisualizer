using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsMediaPlayerVisualizer.Utils;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    public class Visualizer2
    {
        private Panel canvas;
        private float currentVolume;
        private float volumeSmoothing;
        private int flashAlpha = 0;
        private const float volumeThreshold = 0.9f;
        private Timer fadeTimer;
        private float currentAngle = 0f;


        public Visualizer2(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.Paint += Canvas_Paint;

            fadeTimer = new Timer();
            fadeTimer.Interval = 50;
            fadeTimer.Tick += (s, e) =>
            {
                if (flashAlpha > 0)
                {
                    flashAlpha -= 15;
                    if (flashAlpha < 0) flashAlpha = 0;
                    canvas.Invalidate();
                }
            };
            fadeTimer.Start();


            points[0] = new PointF(0, 0);
            points[1] = new PointF(canvas.Width, canvas.Height);
            points[2] = new PointF(canvas.Width, 0);
            points[3] = new PointF(0, canvas.Height);
        }

        public void Update(float currentVolume, float volumeSmoothing)
        {
            this.currentAngle += 5f;
            this.currentVolume = currentVolume;
            this.volumeSmoothing = volumeSmoothing;

            if (currentVolume > volumeThreshold)
            {
                flashAlpha = 255;
            }

            canvas.Invalidate();
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        PointF[] points = new PointF[4];

        private void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.Clear(Color.Black);

            float centerX = canvas.Width / 2f;
            float centerY = canvas.Height / 2f;

            int minRadius = 30;
            int maxRadius = 160;
            int radius = minRadius + (int)((maxRadius - minRadius) * currentVolume);

            Rectangle circle = new Rectangle(
                (int)(centerX - radius),
                (int)(centerY - radius),
                radius * 2,
                radius * 2
            );

            if (flashAlpha > 0)
            {
                float len = Math.Max(canvas.Width, canvas.Height);
                Color flashColor = Color.FromArgb(flashAlpha, Color.White);
                using (Pen pen = new Pen(flashColor, 2))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        float angle = currentAngle + i * 45; // 8 rayos cada 45°
                        DrawZigZagRay(graphics, pen, centerX, centerY, len, angle, 10);
                    }
                }
            }

            graphics.FillEllipse(Brushes.Cyan, circle);
        }



        private void DrawZigZagRay(Graphics g, Pen pen, float cx, float cy, float length, float angleDegrees, int segments)
        {
            float angleRadians = angleDegrees * (float)(Math.PI / 180);
            float dx = (float)Math.Cos(angleRadians);
            float dy = (float)Math.Sin(angleRadians);

            float segmentLength = length / segments;
            float zigzagOffset = 10f;

            PointF[] points = new PointF[segments + 1];
            for (int i = 0; i <= segments; i++)
            {
                float x = cx + i * segmentLength * dx;
                float y = cy + i * segmentLength * dy;

                if (i % 2 == 1)
                {
                    float px = -dy * zigzagOffset;
                    float py = dx * zigzagOffset;
                    x += px;
                    y += py;
                }

                points[i] = new PointF(x, y);
            }

            g.DrawLines(pen, points);
        }





    }


}
