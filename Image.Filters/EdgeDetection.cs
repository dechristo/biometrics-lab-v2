using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using AForge.Imaging.Filters;

namespace Image.Filters
{
    public class EdgeDetection : Filter
    {
        private Bitmap imgEdgeDetected;

        public override Bitmap ApplyFilter(Bitmap img)
        {
            CannyEdgeDetector ced = new CannyEdgeDetector(255, 0);
            this.imgEdgeDetected = ced.Apply(img);
            return this.imgEdgeDetected;
        }

        
    }
}
