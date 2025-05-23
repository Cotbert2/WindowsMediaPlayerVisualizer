using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMediaPlayerVisualizer.Utils
{
    public class PolygonBuilder
    {

        public static List<PointF> buildPolygon(float numberOfPoints, float sideLarge)
        {
            float currentAngle = 0f;
            List<PointF> points = new List<PointF>();
            float angleSteps = (float)(360f / numberOfPoints);
            for (float i = 0; i < 360; i += angleSteps)
            {
                float x = (float)(Math.Cos(currentAngle * Math.PI / 180f) * sideLarge);
                float y = (float)(Math.Sin(currentAngle * Math.PI / 180f) * sideLarge);
                points.Add(new PointF(x, y));
                currentAngle += angleSteps;
            }

            return points;

        }

        public static List<PointF> rotate(List<PointF> points, float degrees)
        {
            List<PointF> rotatedPoints = new List<PointF>();
            double radians = degrees * Math.PI / 180.0;

            float centerX = points.Average(p => p.X);
            float centerY = points.Average(p => p.Y);

            foreach (PointF point in points)
            {
                float translatedX = point.X - centerX;
                float translatedY = point.Y - centerY;

                float rotatedX = (float)(translatedX * Math.Cos(radians) - translatedY * Math.Sin(radians));
                float rotatedY = (float)(translatedX * Math.Sin(radians) + translatedY * Math.Cos(radians));

                rotatedPoints.Add(new PointF(rotatedX + centerX, rotatedY + centerY));
            }

            return rotatedPoints;
        }


        public static List<PointF> centerPolygon(List<PointF> points, Panel canvas)
        {
            List<PointF> centeredPoints = new List<PointF>();
            float centerX = (float)(canvas.Width / 2);
            float centerY = (float)(canvas.Height / 2);
            foreach (PointF point in points)
            {
                centeredPoints.Add(new PointF(point.X + centerX, point.Y + centerY));
            }
            return centeredPoints;
        }

        public static List<PointF> scaleFromCenter(List<PointF> points, float scale)
        {
            List<PointF> scaledPoints = new List<PointF>();
            float centerX = points.Average(p => p.X);
            float centerY = points.Average(p => p.Y);

            foreach (PointF point in points)
            {
                float scaledX = centerX + (point.X - centerX) * scale;
                float scaledY = centerY + (point.Y - centerY) * scale;
                scaledPoints.Add(new PointF(scaledX, scaledY));
            }

            return scaledPoints;
        }

    }
}
