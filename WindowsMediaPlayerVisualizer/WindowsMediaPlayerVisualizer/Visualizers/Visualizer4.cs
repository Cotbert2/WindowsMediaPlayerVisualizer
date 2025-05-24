using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMediaPlayerVisualizer.Visualizers
{
    public class Visualizer4
    {
        private Panel canvas;

        public List<PointF>[] points = new List<PointF>[4];

        public Visualizer4(Panel canvas)
        {
            this.canvas = canvas;
            canvas.Paint += Canvas_Paint;

            float distance = canvas.Width / 50;

            for (int i = 0; i < 4; i ++)
            {
                points[i] = new List<PointF>();
            }


            for (int i = 0; i * distance < canvas.Width; i++)
            {
                points[0].Add(
                    new PointF(0, i * distance)
                    );
                points[1].Add(
                    new PointF(i * distance, canvas.Height)
                    );
                points[2].Add(
                    new PointF(canvas.Width, i * distance)
                    );
                points[3].Add(
                    new PointF(i * distance, 0)
                    );
            }
        }


        public void Update()
        {
            scalePhase += 0.0005f;
            float scale = 1f + 0.05f * (float)Math.Sin(scalePhase);

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = ScalePointsFromCenter(points[i], scale);
            }

            canvas.Invalidate();
        }

        private float scalePhase = 0f;


        public void  Draw(Graphics graphics)
        {
            using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 20, 147), 2))
            {
                graphics.Clear(System.Drawing.Color.Black);
                int count = Math.Min(points[0].Count, Math.Min(points[1].Count, Math.Min(points[2].Count, points[3].Count)));

                for (int i = 0; i < count - 1; i++)
                {
                    graphics.DrawLine(pen, points[0][i], points[1][i]); 
                    graphics.DrawLine(pen, points[1][i], points[2][count - 1 - i]); 
                    graphics.DrawLine(pen, points[2][count - 1 - i], points[3][count - 1 - i]);
                    graphics.DrawLine(pen, points[3][count - 1 - i], points[0][i]);
                }



            }
        }


        private List<PointF> ScalePointsFromCenter(List<PointF> points, float scale)
        {
            float centerX = canvas.Width / 2f;
            float centerY = canvas.Height / 2f;

            List<PointF> scaled = new List<PointF>();

            foreach (PointF p in points)
            {
                float translatedX = p.X - centerX;
                float translatedY = p.Y - centerY;

                float scaledX = translatedX * scale;
                float scaledY = translatedY * scale;

                scaled.Add(new PointF(scaledX + centerX, scaledY + centerY));
            }

            return scaled;
        }


        public void Canvas_Paint(object sender, PaintEventArgs e) 
        {
            Draw(e.Graphics);
        }
    }
}
