using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicEditor.Commands
{
    class DrawFigureCommand : ICommand
    {
        private Document _document { get; set; }

        private Point _basePoint { get; set; }
        private Figure _figure { get; set; }

        public DrawFigureCommand(Document document, Point basePoint, Figure figure)
        {
            _document = document;
            _basePoint = basePoint;
            _figure = figure;

        }

        public bool Exec()
        {
            return _document.Draw(_figure, _basePoint);
        }

        public void Undo()
        {
            _document.Select(_basePoint);
            _document.DeleteSelectedFigure(_basePoint, false);
        }

    }
}
