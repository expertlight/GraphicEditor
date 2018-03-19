using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GraphicEditor
{
    public class Triangle : Figure
    {
        public Triangle(SolidBrush brush)
        {
            InnerBrush = brush;
            GraphicsPath.AddLines(new Point[] {
                new Point(0,0),
                new Point(0,30),
                new Point(15,0),
                new Point(0,0),
            });
        }
    }
}
