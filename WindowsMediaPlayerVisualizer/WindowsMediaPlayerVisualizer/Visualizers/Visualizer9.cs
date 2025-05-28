using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowsMediaPlayerVisualizer.Utils;

namespace WindowsMediaPlayerVisualizer.Visualizers
{

    public class Visualizer9
    {
        private Panel canvas;
        private float[] audioSamples;
        private float currentVolume;
        private float rotationAngle = 0;
        private const int OrbitCount = 3;

        private const int BaseEllipseWidth = 400;
        private const int BaseEllipseHeight = 100;

        public Visualizer9(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.Paint += Canvas_Paint;
        }

        public void Update(float[] frequencyAmplitudes, float volume)
        {
            this.audioSamples = frequencyAmplitudes;
            this.currentVolume = volume;
            this.rotationAngle = (rotationAngle + 0.5f) % 360;
            canvas.Invalidate();
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.Black);
            if (audioSamples == null || audioSamples.Length == 0) return;
            DrawUniformOrbits(e.Graphics);
        }

        private void DrawUniformOrbits(Graphics g)
        {
            PointF center = new PointF(canvas.Width / 2, canvas.Height / 2);
            float scale = 1.0f + currentVolume * 0.5f;
            int width = (int)(BaseEllipseWidth * scale);
            int height = (int)(BaseEllipseHeight * scale);

            for (int i = 0; i < OrbitCount; i++)
            {
                float orbitRotation = rotationAngle * (i + 1) / 2;
                Color orbitColor = GetOrbitColor(i);

                using (Pen orbitPen = new Pen(orbitColor, 2))
                {
                    PointF[] ellipsePoints = Operations.GenerateEllipsePoints(center, width, height);

                    PointF[] rotatedPoints = new PointF[ellipsePoints.Length];
                    for (int j = 0; j < ellipsePoints.Length; j++)
                    {
                        rotatedPoints[j] = Operations.RotatePoint(ellipsePoints[j], center, orbitRotation);
                    }

                    g.DrawPolygon(orbitPen, rotatedPoints);

                    DrawElectron(g, center, width, height, orbitRotation);
                }
            }

            DrawNucleus(g, center);
        }

        private void DrawElectron(Graphics g, PointF center, float width, float height, float orbitRotation)
        {
            PointF electronPos = new PointF(
                center.X + (width / 2),
                center.Y
            );
            electronPos = Operations.RotatePoint(electronPos, center, orbitRotation);

            float electronSize = 6f + currentVolume * 10f;

            using (Brush electronBrush = new SolidBrush(Color.LightCyan))
            {
                g.FillEllipse(electronBrush,
                    electronPos.X - electronSize / 2,
                    electronPos.Y - electronSize / 2,
                    electronSize,
                    electronSize);
            }
        }

        private Color GetOrbitColor(int orbitIndex)
        {
            return Color.FromArgb(150,
                0,
                150 + orbitIndex * 50,
                255 - orbitIndex * 30);
        }

        private void DrawNucleus(Graphics g, PointF center)
        {
            int nucleusSize = 20 + (int)(currentVolume * 30);
            using (Brush nucleusBrush = new SolidBrush(Color.LightCyan))
            {
                g.FillEllipse(nucleusBrush,
                    center.X - nucleusSize / 2,
                    center.Y - nucleusSize / 2,
                    nucleusSize,
                    nucleusSize);
            }
        }
    }
}
