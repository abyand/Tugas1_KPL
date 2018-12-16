using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramToolkit.Shapes
{
    class Polygon : DrawingObject
    {
        private Pen pen;
        List<Point> polygonPoint;
        private int position;
        bool draw;

        const double EPSILON = 3.0;

        public Polygon(Point startPoint, Point endPoint)
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
            polygonPoint = new List<Point>();
            polygonPoint.Add(startPoint);
            polygonPoint.Add(endPoint);
            draw = true;

        }

        

        public void SetLastPoint(Point point)
        {
            polygonPoint[polygonPoint.Count - 1] = point;
        }

        public Polygon(Point startPoint)
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
            polygonPoint = new List<Point>();
            polygonPoint.Add(startPoint);

        }



        public override bool Intersect(int xTest, int yTest)
        {

            draw = false;

            int polygonLength = polygonPoint.Count, i = 0;
            bool inside = false;
            // x, y for tested point.
            float pointX = xTest, pointY = yTest;
            // start / end point for the current polygon segment.
            float startX, startY, endX, endY;
            Point endPoint = polygonPoint.Last();
            endX = xTest;
            endY = yTest;
            while (i < polygonLength)
            {
                startX = endX;
                startY = endY;
                endPoint = polygonPoint[i++];
                endX = endPoint.X;
                endY = endPoint.Y;
                //
                inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                          && /* if so, test if it is under the segment */
                          ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }
            if(inside)
                Debug.WriteLine("Intersect");
            return inside;
        }

       

        public override void RenderOnEditingView()
        {
            pen.Color = Color.Blue;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                
                for(int i=0; i<polygonPoint.Count-1; i++)
                {
                    Point startPoint = polygonPoint[i];
                    Point endPoint = polygonPoint[i + 1];
                    this.GetGraphics().DrawLine(pen, startPoint, endPoint);
                }
                this.GetGraphics().DrawLine(pen, polygonPoint.Last(), polygonPoint.First());

                
            }
        }

        public override void RenderOnPreview()
        {
            if (draw)
            {
                
                pen.Color = Color.Red;
                pen.Width = 1.5f;
                pen.DashStyle = DashStyle.DashDotDot;

                if (this.GetGraphics() != null)
                {
                    this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                    for (int i = 0; i < polygonPoint.Count - 1; i++)
                    {
                        Point startPoint = polygonPoint[i];
                        Point endPoint = polygonPoint[i + 1];
                        this.GetGraphics().DrawLine(pen, startPoint, endPoint);
                    }
                    this.GetGraphics().DrawLine(pen, polygonPoint.Last(), polygonPoint.First());
                }
            }
            else
            {
                pen.Color = Color.Black;
                pen.Width = 1.5f;
                pen.DashStyle = DashStyle.Solid;

                if (this.GetGraphics() != null)
                {
                    this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                    for (int i = 0; i < polygonPoint.Count - 1; i++)
                    {
                        Point startPoint = polygonPoint[i];
                        Point endPoint = polygonPoint[i + 1];
                        this.GetGraphics().DrawLine(pen, startPoint, endPoint);
                    }
                    this.GetGraphics().DrawLine(pen, polygonPoint.Last(), polygonPoint.First());

                    pen.Color = Color.Red;
                    pen.Width = 1.5f;
                    pen.DashStyle = DashStyle.DashDotDot;

                    this.GetGraphics().DrawLine(pen, polygonPoint[position], tempPoint);
                    this.GetGraphics().DrawLine(pen, polygonPoint[position+1], tempPoint);

                }

                
            }
            
        }

        public override void RenderOnStaticView()
        {
            pen.Color = Color.Black;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < polygonPoint.Count - 1; i++)
                {
                    Point startPoint = polygonPoint[i];
                    Point endPoint = polygonPoint[i + 1];
                    this.GetGraphics().DrawLine(pen, startPoint, endPoint);
                }
                this.GetGraphics().DrawLine(pen, polygonPoint.Last(), polygonPoint.First());
            }
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            for (int i = 0; i < polygonPoint.Count; i++)
            {
                polygonPoint[i] = new Point(polygonPoint[i].X + xAmount, polygonPoint[i].Y + yAmount);
            }
        }

        public override void AddPoint(Point point, int position)
        {
            if (position == polygonPoint.Count)
                polygonPoint.Add(point);
            else if(position == -2)
            {
                polygonPoint.Add(point);
            }
            else
                polygonPoint.Insert(position + 1, point);
        }

        public override int IsPointOnLine(Point point)
        {


            Debug.WriteLine("Jumlah line " + polygonPoint.Count);

            for (int i=0; i<polygonPoint.Count-1; i++)
            {
                Point linePointA = polygonPoint[i];
                Point linePointB = polygonPoint[i+1];
                
                if (inLine(linePointA, linePointB, point.X, point.Y))
                {
                    Debug.WriteLine("Ganteng di " + i);
                    position = i;
                    return i;
                }
            }

            Point firstLine = polygonPoint.First();
            Point lastLine = polygonPoint.Last();

            if (inLine(firstLine, lastLine, point.X, point.Y))
            {
                position = polygonPoint.Count;
                return polygonPoint.Count;
            }

            Debug.WriteLine("Jelek ");
            position = 1;
            return -1;
        }

        public bool inLine(Point Startpoint, Point Endpoint, int xTest, int yTest)
        {
            double m = GetSlope(Startpoint, Endpoint);
            double b = Endpoint.Y - m * Endpoint.X;
            double y_point = m * xTest + b;

            if (Math.Abs(yTest - y_point) < EPSILON)
            {

                return true;
            }
            return false;
        }

        public double GetSlope(Point Startpoint, Point Endpoint)
        {
            double m = (double)(Endpoint.Y - Startpoint.Y) / (double)(Endpoint.X - Startpoint.X);
            return m;
        }

        public override void RenderOnPolygonView(Point point)
        {
            pen.Color = Color.Red;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.DashDotDot;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;

                Point startPoint = polygonPoint[position];
                Point endPoint = polygonPoint[position+1];
                this.GetGraphics().DrawLine(pen, startPoint, point);
                this.GetGraphics().DrawLine(pen, point, endPoint);
            }
        }
    }
}
