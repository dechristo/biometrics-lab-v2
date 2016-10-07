using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class Statistics
    { 
        public static double StandardDeviation(double average, List<double> values)
        {
            double variance = Variance(average, values);
            return Math.Sqrt(variance);
        }

        public static double Variance(double average, List<double> values)
        {
            double variance = 0;          

            foreach (double value in values)
            {
                variance += Math.Pow((value - average),2); 
            }

            variance /= values.Count;

            return variance;
        }
    }
}
