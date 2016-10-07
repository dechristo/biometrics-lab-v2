using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public abstract class Outputter
    {
        protected  virtual void SaveTrainningData(string data)
        {
        }

        protected virtual void SaveTestingData(string data)
        {
        }

        public void SaveData(string dataType, string data)
        {
            if (dataType.Equals(DataType.Trainning))
                this.SaveTrainningData(data);
            else
                this.SaveTestingData(data);
        }
    }
}
