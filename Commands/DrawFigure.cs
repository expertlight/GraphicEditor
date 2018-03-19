using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor
{
    public class DrawFigure : ICommand
    {
        private Document _document;

        public DrawFigure(Document document)
        {
            _document = document;
        }

        public void Exec(Figure figure)
        {
            _document.Draw(figure);
        }

        public void Undo()
        {
            _document.Delete();
        }
    }
}
