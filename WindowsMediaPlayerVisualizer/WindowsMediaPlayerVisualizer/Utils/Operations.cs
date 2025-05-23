using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayerVisualizer.Utils
{
    public class Operations
    {
        public static PointF[] rotation(PointF[] points, float angle)
        {
            PointF[] rotatedPoints = new PointF[points.Length];
            double radians = angle * Math.PI / 180.0;
            for (int i = 0; i < points.Length; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;
                rotatedPoints[i].X = (float)(x * Math.Cos(radians) - y * Math.Sin(radians));
                rotatedPoints[i].Y = (float)(x * Math.Sin(radians) + y * Math.Cos(radians));
            }
            return rotatedPoints;
        }
    }
}
