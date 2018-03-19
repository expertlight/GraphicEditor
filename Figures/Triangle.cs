using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GraphicEditor.Figures
{
    class Triangle : Figure
    {
        public Triangle()
        {
            graphicsPath = new GraphicsPath();
            graphicsPath.AddLines(new Point[] {
                new Point(0,0),
                new Point(0,10),
                new Point(10,0),
            });
        }
    }
}
