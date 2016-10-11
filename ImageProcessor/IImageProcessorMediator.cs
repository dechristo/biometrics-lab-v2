using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Images
{
    interface IImageProcessorMediator
    {
        Bitmap ProcessImage(string type);
        void SaveUserData(string dataType, bool bFuseVeins);
    }
}
