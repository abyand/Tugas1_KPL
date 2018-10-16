using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    class Shape
    {
        private int type;
        private Point startPoint;
        private Point endPoint;
        private double vector;
        private double angle;

        public Shape(int type, Point startPoint, Point endPoint, double vector, double angle)
        {
            this.type = type;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.vector = vector;
            this.angle = angle;
        }

        public int getShapeType()
        {
            return this.type;
        }
        public Point GetStartPoint()
        {
            return this.startPoint;
        }
        public Point GetEndPoint()
        {
            return this.endPoint;
        }
        public double GetVector()
        {
            return this.vector;
        }
        public double GetAngle()
        {
            return this.angle;
        }

    }
}
