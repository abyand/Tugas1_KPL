using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace DrawingToolkit
{
    public partial class Form1 : Form
    {
        private readonly int RECTANGLE = 1;
        private readonly int CIRCLE = 2;
        private readonly int LINE = 3;
        private readonly int OPACITY = 4;

        private int typeShape;
        private ArrayList listShape = new ArrayList();

        
        private Point startPoint;
        private Point endPoint;

        private Pen pen;
        private Graphics graphics;
        private bool isPaint = false;
        private bool canPaint = false;

        private double vector = 0;
        private double angle = 0;


        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine("Send to debug output.");
            pen = new Pen(Color.Black)
            {
                Width = OPACITY
            };
            graphics = panel1.CreateGraphics();
        }
        private void RectangleClick(object sender, EventArgs e)
        {
            ResetType();
            rectangleToolStripMenuItem.BackColor = Color.Violet;
            typeShape = RECTANGLE;
            isPaint = true;
        }
        private void LineClick(object sender, EventArgs e)
        {

            ResetType();
            lineToolStripMenuItem.BackColor = Color.Violet;
            typeShape = LINE;
            isPaint = true;
        }
        private void CircleClick(object sender, EventArgs e)
        {

            ResetType();
            circleToolStripMenuItem.BackColor = Color.Violet;
            typeShape = CIRCLE;
            isPaint = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Send to debug output2.");
            if (e.Button == MouseButtons.Left && isPaint)
            {
                startPoint = e.Location;
                Debug.WriteLine("Send to debug output.");
                canPaint = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(typeShape == LINE)
            {

            }
            else if(typeShape == RECTANGLE)
            {

            }
            else if(typeShape == CIRCLE)
            {

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (canPaint)
            {
                endPoint = new Point(e.X, e.Y);
                listShape.Add(new Shape(typeShape, startPoint, endPoint, vector, angle));
                Debug.WriteLine(listShape.Count);
                canPaint = false;
                DrawAllShape();
            }
            

        }

        private void DrawAllShape()
        {

            
            foreach (Shape shape in listShape)
            {
                Debug.WriteLine("ini tanda baris");
                Debug.WriteLine(shape.GetStartPoint().X);
                Debug.WriteLine(shape.GetStartPoint().Y);
                Debug.WriteLine(shape.GetEndPoint().X);
                Debug.WriteLine(shape.GetEndPoint().Y);
                Debug.WriteLine(shape.getShapeType());
                if (shape.getShapeType() == LINE)
                {
                    DrawLine(shape);
                }
                else if(shape.getShapeType() == RECTANGLE)
                {
                    DrawRectangle(shape);
                }
                else if(shape.getShapeType() == CIRCLE)
                {
                    DrawCircle(shape);
                }
            }
        }

        private void DrawCircle(Shape shape)
        {
            Rectangle rectangle = new Rectangle(Math.Min(shape.GetStartPoint().X, shape.GetEndPoint().X),
                       Math.Min(shape.GetStartPoint().Y, shape.GetEndPoint().Y),
                       Math.Abs(shape.GetStartPoint().X - shape.GetEndPoint().X),
                       Math.Abs(shape.GetStartPoint().Y - shape.GetEndPoint().Y));
            graphics.DrawEllipse(pen, rectangle);
        }

        private void DrawRectangle(Shape shape)
        {
            Rectangle rectangle = new Rectangle(Math.Min(shape.GetStartPoint().X, shape.GetEndPoint().X),
                        Math.Min(shape.GetStartPoint().Y, shape.GetEndPoint().Y),
                        Math.Abs(shape.GetStartPoint().X - shape.GetEndPoint().X),
                        Math.Abs(shape.GetStartPoint().Y - shape.GetEndPoint().Y));
            graphics.DrawRectangle(pen, rectangle);
        }

        private void DrawLine(Shape shape)
        {
            graphics.DrawLine(pen, shape.GetStartPoint(), shape.GetEndPoint());
        }

        private void ResetType()
        {
            typeShape = 0;
            lineToolStripMenuItem.BackColor = Color.White;
            rectangleToolStripMenuItem.BackColor = Color.White;
            circleToolStripMenuItem.BackColor = Color.White;
        }
    }
}
