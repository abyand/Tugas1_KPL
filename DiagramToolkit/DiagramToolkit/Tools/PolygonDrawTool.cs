using DiagramToolkit.Shapes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace DiagramToolkit.Tools
{
    public class PolygonDrawTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private DrawingObject selectedObject;
        private int xInitial;
        private int yInitial;
        private int position;

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

        public PolygonDrawTool()
        {
            this.Name = "Polygon Draw tool";
            this.ToolTipText = "Polygon Draw tool";
            this.Image = IconSet.polygon;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            this.xInitial = e.X;
            this.yInitial = e.Y;

            if (e.Button == MouseButtons.Left && canvas != null)
            {
                canvas.DeselectAllObjects();
                selectedObject = canvas.SelectObjectAt(e.X, e.Y);
                
                if (selectedObject != null)
                {
                    selectedObject.ChangeState(PolygonDrawState.GetInstance());
                    position = selectedObject.IsPointOnLine(new System.Drawing.Point(e.X, e.Y));
                    Debug.WriteLine("position " + position);
                }
                
            }

        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                if (selectedObject != null && position != -1)
                {

                    Debug.WriteLine("temp point di " + e.X);
                    selectedObject.tempPoint = new Point(e.X, e.Y);
                    
                    //selectedObject.Draw();
                    //selectedObject.RenderOnPolygonView(new Point(e.X, e.Y));
                    //selectedObject.Translate(e.X, e.Y, xAmount, yAmount);
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                if (selectedObject != null)
                {
                    if(position!= -1)
                        selectedObject.AddPoint(new System.Drawing.Point(e.X, e.Y), position);
                    selectedObject.Deselect();
                }
            }
            
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("selection tool double click");
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
