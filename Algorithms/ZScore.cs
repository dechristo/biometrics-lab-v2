using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class ZScore
    {
        private double _z;
        private double _x;
        private double _average;
        private double _stddv;
        private List<double> _values = null;
        private List<double> _standardScoreList = null;
        
        public ZScore()
        {           
        }            

        public double Average
        {
            get
            {
                return this._average;
            }

            set
            {
                this._average = value;
            }
        }

        public double StdDev
        {
            get
            {
                return this._stddv;
            }

            set
            {
                this._stddv = value;
            }
        }

        public List<double> GetStandardScore(List<double> values)
        {
            this._standardScoreList = new List<double>();

            this._values = values;
            this._average = values.Average();            
            this._stddv = Statistics.StandardDeviation(this._average, values);

            foreach (double value in values)
            {
                double z = (value - this._average) / this._stddv;
                this._standardScoreList.Add(z);
            }

            return this._standardScoreList;
        }
    }
}
