using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class Geometry
    {
        public static double EuclideanDistance(Point p1, Point p2)
        {
            double distance = 0;
            double d1Diff = Math.Pow((p2.X - p1.X), 2);
            double d2Diff = Math.Pow((p2.Y - p1.Y), 2);
            distance = Math.Sqrt(d1Diff + d2Diff);
            return Math.Round(distance,4);
        }

        public static int AngularCoefficient(int y1, int y2)
        {
            return Math.Abs(y1 - y2);
            //return (y2-y1)/(x2-x1)
        }

        public static Point GetMiddlePoint(Point[] points)
        {
            Point middle = new Point();
            middle.X = points[0].X + (Math.Abs((points[0].X - points[1].X)) / 2);
            middle.Y = points[0].Y + (Math.Abs((points[0].Y - points[1].Y)) / 2);

            return middle;
        }
    }
}
