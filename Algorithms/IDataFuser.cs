using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BiometricData;

namespace Algorithms
{
    interface IDataFuser
    {        
        String FuseData(string dataType, bool bFuseVeins);             
    }
}
