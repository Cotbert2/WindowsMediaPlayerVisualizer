using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib.IFD.Tags;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    public class Visualizer1
    {
        private Panel canvas;

        public Visualizer1(Panel canvas) 
        {
            this.canvas = canvas;
            this.canvas.Paint += Canvas_Paint;
        }

        public void Canvas_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Update(float[] frequencyAmplitudes)
        {
            Graphics g = canvas.CreateGraphics();
            g.Clear(Color.Black);

            int numBars = frequencyAmplitudes.Length;
            int barWidth = canvas.Width / numBars;

            for (int i = 0; i < numBars; i++)
            {
                float amplitude = frequencyAmplitudes[i];

                amplitude = Math.Min(amplitude * 5f, 1f);

                int height = (int)(amplitude * canvas.Height);
                Rectangle bar = new Rectangle(i * barWidth, canvas.Height - height, barWidth - 2, height);
                g.FillRectangle(Brushes.Cyan, bar);
            }
        }


    }
}
