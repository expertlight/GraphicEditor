using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicEditor
{
    class Invoker
    {
        private Document _document;
        private Point _selectedFigurePoint;

        public List<ICommand> commandHistory = new List<ICommand>();
        public int commandHistoryPosition = 0;

        public Invoker(Graphics formGraphics)
        {
            _document = new Document(formGraphics);
        }

        public bool DrawTriangle(Point basePoint)
        {
            DrawFigureCommand cmd = new DrawFigureCommand(_document);
            cmd.figure = new Triangle(new SolidBrush(Color.Brown));
            cmd.basePoint = basePoint;
            if (cmd.Exec())
                {
                    PutCommandToHistory(cmd);
                    return true;
                }

            return false; 
        }

        public bool DrawSquare(Point basePoint)
        {
            DrawFigureCommand cmd = new DrawFigureCommand(_document);
            cmd.figure = new Square(new SolidBrush(Color.Aqua));
            cmd.basePoint = basePoint;
            if (cmd.Exec())
            {
                PutCommandToHistory(cmd);
                return true;
            }

            return false;
        }

        public void ClearDocument()
        {
            ClearDocumentCommand cmd = new ClearDocumentCommand(_document);
            commandHistory.Clear();
            commandHistoryPosition = 0;
            cmd.Exec();
        }

        public void SelectFigure(Point point)
        {
            SelectFigureCommand cmd = new SelectFigureCommand(_document);
            cmd.selectedPoint = point;
            if (cmd.Exec())
            {
                _selectedFigurePoint = point;
            }
        }

        public void CancelSelection()
        {
            SelectFigureCancelCommand cmd = new SelectFigureCancelCommand(_document);
            cmd.Exec();
        }

        public bool CrossCheck(Point point, Figure figure)
        {
            CrossCheckCommand cmd = new CrossCheckCommand(_document);
            cmd.basePoint = point;
            cmd.checkedFigure = figure;
            cmd.Exec();

            return false;
        }

        public void DeleteSelectedFigure()
        {
            DeleteSelectedFigureCommand cmd = new DeleteSelectedFigureCommand(_document);
            PutCommandToHistory(cmd);
            cmd.basePoint = _selectedFigurePoint;
            cmd.Exec();
        }

        public void Undo()
        {
            CancelSelection();
            if (commandHistoryPosition > 0)
            {
                commandHistory[commandHistoryPosition - 1].Undo();
                commandHistoryPosition--;
            }
        }

        public void Redo()
        {
            CancelSelection();
             if (commandHistoryPosition < commandHistory.Count)
            {
                commandHistory[commandHistoryPosition].Exec();
                commandHistoryPosition++;
            }
        }

        public void PutCommandToHistory(ICommand cmd)
        {
            if (commandHistory.Count == commandHistoryPosition)
            {
                commandHistory.Add(cmd);
                commandHistoryPosition++;
            }
            else
            {
                commandHistory[commandHistoryPosition] = cmd;
                commandHistoryPosition++;
            }
        }

        public void RepeatLastCommand()
        {
            if (commandHistory.Count != 0)
            {
                 commandHistory.Last<ICommand>().Exec();
                PutCommandToHistory(commandHistory.Last<ICommand>());
            }
        }

    }
}
