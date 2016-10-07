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
            Point[] pOrdered = points.OrderBy(p => p.X).ToArray();
            middle.X = pOrdered[0].X + (Math.Abs((pOrdered[0].X - pOrdered[1].X)) / 2);
            middle.Y = pOrdered[0].Y + (Math.Abs((pOrdered[0].Y - pOrdered[1].Y)) / 2);

            return middle;
        }
    }
}
