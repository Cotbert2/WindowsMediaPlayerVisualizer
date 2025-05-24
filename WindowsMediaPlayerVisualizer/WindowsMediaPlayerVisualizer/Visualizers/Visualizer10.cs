using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsMediaPlayerVisualizer.Utils;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    public class Visualizer10
    {
        private const int PointCount = 100;
        private const float BaseRadiusFactor = 0.25f;
        private const float RandomOffset = 10f;
        private const float WaveScaleFactor = 0.4f;

        private readonly Panel canvas;
        private readonly PointF[] staticPoints;
        private float[] audioSamples;
        private float currentVolume;
        private float rotationAngle;

        public Visualizer10(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.Paint += Canvas_Paint;
            this.staticPoints = InitializePoints();
        }

        private PointF[] InitializePoints()
        {
            PointF center = new PointF(canvas.Width / 2, canvas.Height / 2);
            int radius = (int)(canvas.Height * BaseRadiusFactor);
            return VisualizerHelpers.InitializeCircularPoints(
                PointCount,
                center,
                radius,
                RandomOffset);
        }

        public void Update(float[] frequencyAmplitudes, float volume)
        {
            this.audioSamples = frequencyAmplitudes;
            this.currentVolume = volume;

            rotationAngle = (rotationAngle + 0.2f) % 360; // Rotación por frame

            canvas.Invalidate();
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {
            DrawWaveform(e.Graphics);
            DrawPoints(e.Graphics);
        }

        private void DrawWaveform(Graphics g)
        {
            if (audioSamples == null || audioSamples.Length == 0) return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Black);

            int width = canvas.Width;
            int height = canvas.Height;
            int centerY = height / 2;
            float scale = height * 0.4f * (0.5f + currentVolume * 0.5f);

            PointF[] points = new PointF[audioSamples.Length];
            for (int i = 0; i < audioSamples.Length; i++)
            {
                float x = i * width / (float)audioSamples.Length;
                float y = centerY + audioSamples[i] * scale;
                points[i] = new PointF(x, y);
            }

            PointF[] rotatedPoints = RotatePoints(points, new PointF(width / 2, centerY), rotationAngle);

            //onda
            using (Pen wavePen = new Pen(Color.Cyan, 3))
            {
                g.DrawCurve(wavePen, rotatedPoints, 0.5f);
            }

            //reflejo
            using (Pen reflectionPen = new Pen(Color.FromArgb(100, 0, 255, 255), 1))
            {
                PointF[] reflectionPoints = new PointF[rotatedPoints.Length];
                for (int i = 0; i < rotatedPoints.Length; i++)
                {
                    reflectionPoints[i] = new PointF(
                        rotatedPoints[i].X,
                        centerY + (centerY - rotatedPoints[i].Y)
                    );
                }
                g.DrawCurve(reflectionPen, reflectionPoints, 0.5f);
            }
        }

        private void DrawPoints(Graphics g)
        {
            if (audioSamples == null) return;
            float pointSize = 2 + currentVolume * 3;


            foreach (var point in staticPoints)
            {
                int blueValue = 150 + (int)(currentVolume * 105);
                using (Pen circlePen = new Pen(Color.FromArgb(255, 20, 147), 1))
                {
                    g.DrawEllipse(circlePen, point.X, point.Y, pointSize, pointSize);
                }
            }
        }

        private PointF[] RotatePoints(PointF[] points, PointF center, float angleDegrees)
        {
            PointF[] rotated = new PointF[points.Length];
            double angleRad = angleDegrees * Math.PI / 180.0;
            float cos = (float)Math.Cos(angleRad);
            float sin = (float)Math.Sin(angleRad);

            for (int i = 0; i < points.Length; i++)
            {
                // Trasladar al origen
                float x = points[i].X - center.X;
                float y = points[i].Y - center.Y;

                rotated[i].X = x * cos - y * sin + center.X;
                rotated[i].Y = x * sin + y * cos + center.Y;
            }

            return rotated;
        }
    }
}