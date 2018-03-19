using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using GraphicEditor.Commands;

namespace GraphicEditor.Invokers
{
    class Invoker
    {
        private Document _document { get; set; }
        private Point _selectedFigurePoint { get; set; }

        private List<ICommand> _commandHistory = new List<ICommand>();
        private int _commandHistoryPosition = 0;

        public Invoker(Graphics formGraphics)
        {
            _document = new Document(formGraphics);
        }

        public bool Run(ICommand cmd)
        {
            if ((cmd is DrawFigureCommand) 
                || (cmd is DeleteSelectedFigureCommand)
                )
                PutCommandToHistory(cmd);
            return cmd.Exec();
        }

        public void Undo()
        {
            if (_commandHistoryPosition > 0)
            {
                _commandHistory[_commandHistoryPosition - 1].Undo();
                _commandHistoryPosition--;
            }
        }

        public void Redo()
        {
             if (_commandHistoryPosition < _commandHistory.Count)
            {
                _commandHistory[_commandHistoryPosition].Exec();
                _commandHistoryPosition++;
            }
        }

        public void PutCommandToHistory(ICommand cmd)
        {
            if (_commandHistory.Count == _commandHistoryPosition)
            {
                _commandHistory.Add(cmd);
                _commandHistoryPosition++;
            }
            else
            {
                _commandHistory[_commandHistoryPosition] = cmd;
                _commandHistoryPosition++;
            }
        }

        public void RepeatLastCommand()
        {
            if (_commandHistory.Count != 0)
            {
                 _commandHistory.Last<ICommand>().Exec();
                PutCommandToHistory(_commandHistory.Last<ICommand>());
            }
        }

        public void ClearHistory()
        {
            _commandHistory.Clear();
            _commandHistoryPosition = 0;
        }

    }
}
