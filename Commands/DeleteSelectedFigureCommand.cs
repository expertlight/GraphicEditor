  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicEditor.Commands
{
    public class DeleteSelectedFigureCommand :ICommand
    {
        private Document _document { get; set; }
        private Point _basePoint { get; set; }

        public DeleteSelectedFigureCommand(Document document, Point basePoint)
        {
            _document = document;
            _basePoint = basePoint;
        }

        public bool Exec()
        {
            _document.DeleteSelectedFigure(_basePoint, true);
            return false;
        }

        public void Undo()
        {
            _document.RestoreLastFigureFromThashBin();
        }


    }
}
