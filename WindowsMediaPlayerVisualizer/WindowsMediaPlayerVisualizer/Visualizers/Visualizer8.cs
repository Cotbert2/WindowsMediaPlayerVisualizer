using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    public class Visualizer8
    {
        private Panel canvas;
        private float[] amplitudes = new float[0];

        public Visualizer8(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.Paint += Canvas_Paint;
        }

        public void Update(float[] frequencyAmplitudes)
        {
            // Guardar los datos para repintar
            this.amplitudes = frequencyAmplitudes;
            canvas.Invalidate(); // Forzar repintado
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(10, 10, 30)); // Fondo oscuro

            if (amplitudes == null || amplitudes.Length == 0)
                return;

            int numBars = amplitudes.Length;
            int barWidth = 2; // Fijo y delgado

            // Calculamos spacing para que llene el ancho total
            int totalBarWidth = barWidth * numBars;
            int availableSpace = canvas.Width - totalBarWidth;
            float spacing = (numBars > 1) ? (float)availableSpace / (numBars - 1) : 0;

            int centerY = canvas.Height / 2;

            using (Brush barBrush = new SolidBrush(Color.FromArgb(150, 100, 255))) // Violeta neón
            {
                for (int i = 0; i < numBars; i++)
                {
                    float amplitude = Math.Min(amplitudes[i] * 5f, 1f);
                    int barHeight = (int)(amplitude * (canvas.Height / 2));

                    int x = (int)(i * (barWidth + spacing));
                    Rectangle topBar = new Rectangle(x, centerY - barHeight, barWidth, barHeight);
                    Rectangle bottomBar = new Rectangle(x, centerY, barWidth, barHeight);

                    g.FillRectangle(barBrush, topBar);
                    g.FillRectangle(barBrush, bottomBar);
                }
            }
        }
    }
}
