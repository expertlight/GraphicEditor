using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicEditor
{
    public class Document
    {
        private List<DocumentObject> _documentObjects = new List<DocumentObject>();
        private DocumentObject _objectSelector = new DocumentObject();
        private DocumentObject _objectSelectorPrevious = new DocumentObject();
        private DocumentObject _preliminaryFigureContur = new DocumentObject();
        private Stack<DocumentObject> _objectTrashBin = new Stack<DocumentObject>();
        private Graphics _graphics;

        public Document(Graphics graphics)
        {
            _graphics = graphics;
            _preliminaryFigureContur.Figure = new Figure();
        }


        public bool Draw(Figure figure, Point basePoint )
         {
            if (!CrossCheck( _preliminaryFigureContur.Figure))
            {
                Figure transformedFigure = TransformCoordinatesToAbsolute(figure, basePoint);
                _documentObjects.Add(new DocumentObject { Figure = transformedFigure/*, BasePoint = basePoint */});
                _preliminaryFigureContur.Figure = null;
                Refresh();
                return true;
            }
            else
            {
                _preliminaryFigureContur.Figure = null;
                Refresh();
                return false;
            }          
        }


        public void DeleteSelectedFigure(Point basePoint, bool moveToTrashBin)
        {
            for (int i = 0; i < _documentObjects.Count; i++)
            {
                if (CrossCheck(basePoint, _documentObjects[i].Figure))
                {
                    if (moveToTrashBin)
                        _objectTrashBin.Push(_documentObjects[i]);
                    _documentObjects.RemoveAt(i);
                    _objectSelector.Figure = null;
                    Refresh();
                    break;
                }
            }
        }

        public void RestoreLastFigureFromThashBin()
        {
            if (_objectTrashBin.Count!=0)
                _documentObjects.Add(_objectTrashBin.Pop());
            Refresh();
        }

        public bool Select(Point point)
        {
            for (int i = 0; i < _documentObjects.Count; i++)
            {
                if (_documentObjects[i].Figure.GraphicsPath.IsVisible(point))
                {_objectSelector.Figure = new Figure();
                    _objectSelector.Figure.GraphicsPath = _documentObjects[i].Figure.GraphicsPath;
                    Refresh();
                    return true;
                }
            }
            return false;
        } 

        public void SelectCancel()
        {
            _objectSelector.Figure =  null;
            Refresh();
        }

        public bool PlacePreliminaryFigure( Figure figure, Point basePoint)
        {
            Figure transformedFigure = TransformCoordinatesToAbsolute(figure, basePoint);
            _preliminaryFigureContur = new DocumentObject { Figure = transformedFigure };
            Refresh();
            return CrossCheck(_preliminaryFigureContur.Figure);
        }

        /// <summary>
        /// Check if figure crosses any from existing document objects
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        private bool CrossCheck(Figure figure)
        {
            if (figure != null)
            {
                Region crossedRegion = new Region(figure.GraphicsPath);
                foreach (DocumentObject a in _documentObjects)
                {
                    crossedRegion = new Region(figure.GraphicsPath);
                    crossedRegion.Intersect(a.Figure.GraphicsPath);
                    if (!crossedRegion.IsEmpty(_graphics))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if two figures cross
        /// </summary>
        /// <param name="figure1"></param>
        /// <param name="figure2"></param>
        /// <returns></returns>
        private bool CrossCheck(Figure figure1, Figure figure2)
        {
            if (figure1 != null && figure2 != null)
            {
                Region crossedRegion = new Region(figure1.GraphicsPath);
                crossedRegion.Intersect(figure2.GraphicsPath);
                if (!crossedRegion.IsEmpty(_graphics))
                {
                    return true;
                }
                
            }
            return false;
        }

        /// <summary>
        /// Check if point is in figure inside
        /// </summary>
        /// <param name="point"></param>
        /// <param name="figure"></param>
        /// <returns></returns>
        private bool CrossCheck(Point point, Figure figure)
        {
            if (figure.GraphicsPath.IsVisible(point))
            {
                return true;
            }
            return false;
        }

        private void Refresh()
        {
            _graphics.Clear(Color.White);
            foreach (DocumentObject a in _documentObjects)
                _graphics.FillPath(a.Figure.InnerBrush, a.Figure.GraphicsPath);
            if (_objectSelector.Figure != null)
                _graphics.DrawPath(Pens.Black, _objectSelector.Figure.GraphicsPath);
            if (_preliminaryFigureContur.Figure != null)
            {
                if (CrossCheck( _preliminaryFigureContur.Figure))
                {
                    _graphics.DrawPath(Pens.Red, _preliminaryFigureContur.Figure.GraphicsPath);
                }
                else
                {
                    _graphics.DrawPath(Pens.Green, _preliminaryFigureContur.Figure.GraphicsPath);
                }
            }

        }

        public void ClearDocument()
        {
            _documentObjects.Clear();
            _objectSelector.Figure = null;
            _graphics.Clear(Color.White);
        }

        private Figure TransformCoordinatesToAbsolute(Figure figure, Point basePoint)
        {
            Figure newFigure = new Figure();
            newFigure.InnerBrush = figure.InnerBrush;

            PointF[] updatedPoints = figure.GraphicsPath.PathData.Points;
            for (int i = 0; i < figure.GraphicsPath.PointCount; i++)
            {
                updatedPoints[i].X += basePoint.X;
                updatedPoints[i].Y += basePoint.Y;
            }
            newFigure.GraphicsPath.AddLines(updatedPoints);

            return newFigure;
        }
    }


}
