using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using GraphicEditor.Commands;
using GraphicEditor.Invokers;

namespace GraphicEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Graphics formGraphics;
        private Invoker invoker;
        private Document document;
        private bool crossChecking;

        private Figure selectedUIFigure;
        private Figure lastSelectedUIFigure;
        private Point selectedFigurePoint;


        private void Form1_Load(object sender, EventArgs e)
        {
            formGraphics = this.CreateGraphics();
            invoker = new Invoker(formGraphics);
            document = new Document(formGraphics);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = Cursor.Position.X.ToString() + " " + Cursor.Position.Y.ToString();
            if (selectedUIFigure != null)
            {
                CrossCheckCommand cmd = new CrossCheckCommand(document, e.Location, selectedUIFigure);
                crossChecking = invoker.Run(cmd);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedUIFigure == null)
            {
                SelectFigureCommand cmd = new SelectFigureCommand(document, e.Location);
                if (invoker.Run(cmd))
                {
                    selectedFigurePoint = e.Location;
                }
            }

            if (selectedUIFigure != null && crossChecking == false)
            {
                DrawFigureCommand cmd = new DrawFigureCommand(document, e.Location, selectedUIFigure);
                invoker.Run(cmd);
                selectedUIFigure = null;
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            selectedUIFigure = lastSelectedUIFigure = new Triangle(new SolidBrush(Color.YellowGreen));

            this.Cursor = Cursors.Cross;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearDocumentCommand cmd = new ClearDocumentCommand(document);
            invoker.Run(cmd);
            invoker.ClearHistory();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            selectedUIFigure = lastSelectedUIFigure = new Square(new SolidBrush(Color.Thistle));

            this.Cursor = Cursors.Cross;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            DeleteSelectedFigureCommand cmd = new DeleteSelectedFigureCommand(document, selectedFigurePoint);
            invoker.Run(cmd);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            invoker.Undo();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            invoker.Redo();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            selectedUIFigure = lastSelectedUIFigure;
        }
    }
}
