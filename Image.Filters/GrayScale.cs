using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Image.Filters
{
    public class GrayScale : Filter
    {
        private Bitmap imgGrey;

        public override Bitmap ApplyFilter(System.Drawing.Image imgOriginal)
        {
            //converts the original image into a grayscale bitmap.
            this.imgGrey = new System.Drawing.Bitmap(imgOriginal);
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);            
            // apply the filter
            this.imgGrey = filter.Apply(this.imgGrey);            
            return this.imgGrey;
        }
    }
}
