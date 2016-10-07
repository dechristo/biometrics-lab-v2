using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utils
{
    public class OutputterTxt : Outputter
    {
        protected override void SaveTrainningData(string data)
        {
            StreamWriter sw = new StreamWriter("C:\\libsvm-3.21\\windows\\hand_bio", true);
            if(data != string.Empty)
                sw.Write(data);
            sw.Close();
        }

        protected override void SaveTestingData(string data)
        {
            StreamWriter sw = new StreamWriter("C:\\libsvm-3.21\\windows\\hand_bio_testing.t", true);
            sw.WriteLine(data);
            sw.Close();
        }       
    }
}
