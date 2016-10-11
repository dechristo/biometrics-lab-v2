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
                double x = MinMax(feature, features.Min(), features.Max());
                normalized.Add(x);
            }

            return normalized;
        }

        private static double MinMax(double xi, double min, double max)
        {
            //adjust hog extracted features scale
            if (xi < 20)            
                xi *= 1000 / (max/20);            

            double x = (xi - min) / (max - min);                        

            return x;
        }
    }
}
