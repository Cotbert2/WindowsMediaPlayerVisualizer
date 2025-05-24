using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayerVisualizer.Utils
{
    public class VisualizerHelpers
    {
        public static PointF[] InitializeCircularPoints(int count, PointF center, float radius, float randomOffset)
        {
            var points = new PointF[count];
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                double angle = 2 * Math.PI * i / count;
                points[i] = new PointF(
                    center.X + (float)(radius * Math.Cos(angle)) + (float)(random.NextDouble() * randomOffset * 2 - randomOffset),
                    center.Y + (float)(radius * Math.Sin(angle)) + (float)(random.NextDouble() * randomOffset * 2 - randomOffset)
                );
            }
            return points;
        }

        public static Color GetVolumeBasedColor(float volume, int baseBlue = 150)
        {
            int blueValue = baseBlue + (int)(volume * 105);
            return Color.FromArgb(255, 20, blueValue);
        }
    }
}
