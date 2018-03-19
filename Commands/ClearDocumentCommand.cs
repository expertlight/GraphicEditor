using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.Commands
{
    class ClearDocumentCommand : ICommand
    {
        private Document _document { get; set; }

        public ClearDocumentCommand(Document document)
        {
            _document = document;
        }

        public bool Exec()
        {
            _document.ClearDocument();
            return true;
        }
        public void Undo()
        {

        }
    }
}
