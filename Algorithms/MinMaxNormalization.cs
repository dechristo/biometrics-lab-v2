using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class MinMaxNormalization
    {
        public static List<double> ApplyMinMaxNormalization(List<double> features)
        {
            List<double> normalized = new List<double>();

            foreach (double feature in features)
            {
                double x = MinMax(feature*500, features.Min()-100, features.Max()+200);
                normalized.Add(x);
            }

            return normalized;
        }

        private static double MinMax(double xi, double min, double max)
        {
            double x = (xi - min) / (max - min);
            return x;
        }
    }
}
