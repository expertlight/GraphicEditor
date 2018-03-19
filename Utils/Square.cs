using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GraphicEditor
{
    class Square : Figure
    {
        public Square(SolidBrush brush)
        {
            InnerBrush = brush;
            GraphicsPath.AddLines(new Point[] {
                new Point(0,0),
                new Point(20,0),
                new Point(20,20),
                new Point(0,20),
                new Point(0,0)
            });
        }
    }
}
