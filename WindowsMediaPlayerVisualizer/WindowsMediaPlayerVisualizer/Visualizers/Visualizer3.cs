using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsMediaPlayerVisualizer.Utils;

public class Visualizer3
{
    private Panel canvas;
    private List<PointF[]> polygons;
    private float alphaChannel = 255;
    private float scaleFactor = 1.05f;
    private int frameCount = 0;

    public Visualizer3(Panel canvas)
    {
        this.canvas = canvas;
        this.canvas.Paint += Canvas_Paint;
        this.polygons = new List<PointF[]>();

        // Primer decágono en el centro
        polygons.Add(
            PolygonBuilder.centerPolygon(
                PolygonBuilder.buildPolygon(10, 30),
                canvas
            ).ToArray()
        );
    }

    public void Update()
    {
        frameCount++;

        if (frameCount % 10 == 0)
        {
            polygons.Add(
                PolygonBuilder.centerPolygon(
                    PolygonBuilder.buildPolygon(10, 30),
                    canvas
                ).ToArray()
            );
        }

        canvas.Invalidate();
    }

    public void Canvas_Paint(object sender, PaintEventArgs e)
    {
        Draw(e.Graphics);
    }


    public void Draw(Graphics graphics)
    {
        graphics.Clear(Color.Black);

        using (Pen pen = new Pen(Color.FromArgb((int)alphaChannel, 255, 20, 147), 2))
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i][0].X - polygons[i][1].X > canvas.Width)
                {
                    polygons.RemoveAt(i);
                    i--;
                    continue;
                }
                graphics.DrawPolygon(pen, polygons[i]);

                polygons[i] = PolygonBuilder.scaleFromCenter(polygons[i].ToList(), scaleFactor).ToArray();

                float angle = (i % 2 == 0) ? 1.5f : -1.5f;
                polygons[i] = PolygonBuilder.rotate(polygons[i].ToList(), angle).ToArray();
            }

            alphaChannel -= 0.25f;
            if (alphaChannel < 50) alphaChannel = 255;
        }
    }
}
