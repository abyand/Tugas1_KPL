using DiagramToolkit.Shapes;
using System;
using System.Windows.Forms;

namespace DiagramToolkit.Tools
{
    public class LineTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Polygon lineSegment;

        public Cursor Cursor
        {
            get
            {
                return Cursors.Arrow;
            }
        }

        public ICanvas TargetCanvas
        {
            get
            {
                return this.canvas;
            }

            set
            {
                this.canvas = value;
            }
        }

        public LineTool()
        {
            this.Name = "Stateful Line tool";
            this.ToolTipText = "Stateful Line tool";
            this.Image = IconSet.diagonal_line;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lineSegment = new Polygon(new System.Drawing.Point(e.X, e.Y), new System.Drawing.Point(e.X, e.Y));
                canvas.AddDrawingObject(lineSegment);
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.lineSegment != null)
                {
                    lineSegment.SetLastPoint(new System.Drawing.Point(e.X, e.Y));
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (this.lineSegment != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    lineSegment.SetLastPoint(new System.Drawing.Point(e.X, e.Y));
                    lineSegment.Select();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    canvas.RemoveDrawingObject(this.lineSegment);
                }
            }
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
             
        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {
            
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void ToolHotKeysDown(object sender, Keys e)
        {
           
        }
    }
}
