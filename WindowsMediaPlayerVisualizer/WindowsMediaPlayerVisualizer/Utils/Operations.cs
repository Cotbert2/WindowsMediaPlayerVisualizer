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

        public static PointF RotatePoint(PointF point, PointF center, float angleDegrees)
        {
            double angleRad = angleDegrees * Math.PI / 180.0;
            float cos = (float)Math.Cos(angleRad);
            float sin = (float)Math.Sin(angleRad);

            float x = point.X - center.X;
            float y = point.Y - center.Y;

            return new PointF(
                x * cos - y * sin + center.X,
                x * sin + y * cos + center.Y
            );
        }

        public static PointF[] GenerateEllipsePoints(PointF center, float width, float height, int segments = 36)
        {
            PointF[] points = new PointF[segments];
            for (int i = 0; i < segments; i++)
            {
                double angle = 2 * Math.PI * i / segments;
                points[i] = new PointF(
                    center.X + (width / 2) * (float)Math.Cos(angle),
                    center.Y + (height / 2) * (float)Math.Sin(angle)
                );
            }
            return points;
        }
    }

}
