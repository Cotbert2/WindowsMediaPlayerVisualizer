using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
            int centerX = canvas.Width / 2;
            int centerY = canvas.Height / 2;

            float scale = 1.0f + currentVolume * 0.5f;
            int width = (int)(BaseEllipseWidth * scale);
            int height = (int)(BaseEllipseHeight * scale);

            for (int i = 0; i < OrbitCount; i++)
            {
                float orbitRotation = rotationAngle * (i + 1) / 2;

                using (Pen orbitPen = new Pen(GetOrbitColor(i), 2))
                {
                    g.TranslateTransform(centerX, centerY);
                    g.RotateTransform(orbitRotation);
                    g.TranslateTransform(-centerX, -centerY);

                    g.DrawEllipse(orbitPen,
                        centerX - width / 2,
                        centerY - height / 2,
                        width,
                        height);

                    DrawElectron(g, centerX, centerY, width, height, orbitRotation);

                    g.ResetTransform();
                }
            }

            DrawNucleus(g, centerX, centerY);
        }

        private Color GetOrbitColor(int orbitIndex)
        {
            return Color.FromArgb(150,
                0,
                150 + orbitIndex * 50,
                255 - orbitIndex * 30);
        }

        private void DrawElectron(Graphics g, int centerX, int centerY, int width, int height, float orbitRotation)
        {
            double angle = orbitRotation * Math.PI / 180.0;
            float x = centerX + (width / 2) * (float)Math.Cos(angle);
            float y = centerY + (height / 2) * (float)Math.Sin(angle);

            float electronSize = 6f + currentVolume * 10f;

            using (Brush electronBrush = new SolidBrush(Color.LightCyan))
            {
                g.FillEllipse(electronBrush,
                    x - electronSize / 2,
                    y - electronSize / 2,
                    electronSize,
                    electronSize);
            }
        }

        private void DrawNucleus(Graphics g, int centerX, int centerY)
        {
            int nucleusSize = 20 + (int)(currentVolume * 30);
            using (Brush nucleusBrush = new SolidBrush(Color.LightCyan))
            {
                g.FillEllipse(nucleusBrush,
                    centerX - nucleusSize / 2,
                    centerY - nucleusSize / 2,
                    nucleusSize,
                    nucleusSize);
            }
        }
    }
}
