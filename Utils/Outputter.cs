using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public abstract class Outputter
    {
        public virtual void SaveTrainningData(string data)
        {
        }

        public virtual void SaveTestingData(string data)
        {
        }  
    }
}
