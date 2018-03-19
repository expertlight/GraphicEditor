using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicEditor.Commands
{
    class SelectFigureCommand : ICommand
    {
        private Document _document { get; set; }

        private Point _selectedPoint { get; set; }

        public SelectFigureCommand(Document document, Point selectedPoint)
        {
            _document = document;
            _selectedPoint = selectedPoint;
        }

        public bool Exec()
        {
            return _document.Select(_selectedPoint);
        }

        public void Undo()
        {
             _document.SelectCancel();
        }
    }
}
