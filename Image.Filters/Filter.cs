using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Image.Filters
{
    public abstract class Filter
    {
        public virtual Bitmap ApplyFilter(Bitmap img) { return null; }
        public virtual Bitmap ApplyFilter(System.Drawing.Image img) { return null; }
        public virtual void ApplyFilterInline(Bitmap img){}

        public Bitmap Invert(Bitmap imgEdgeDetected)
        {
            Invert inv = new Invert();
            inv.ApplyInPlace(imgEdgeDetected);
            return imgEdgeDetected;
        }
    }
}
