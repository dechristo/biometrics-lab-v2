using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    interface IHandPalmVeins
    {
        Bitmap ExtractHandPalmROI(Point pCentroid);
        float[] ComputeHoG();
    }
}
