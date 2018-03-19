using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicEditor.Commands
{
    class CrossCheckCommand : ICommand
    {
        private Document _document { get; set; }

        private Point _basePoint { get; set; }
        private Figure _checkedFigure { get; set; }

        public CrossCheckCommand(Document document, Point basePoint, Figure checkedFigure)
        {
            _document = document;
            _basePoint = basePoint;
            _checkedFigure = checkedFigure;
        }

        public bool Exec()
        {
            return _document.PlacePreliminaryFigure( _checkedFigure, _basePoint);
        }

        public void Undo()
        {

        }
    }
}
