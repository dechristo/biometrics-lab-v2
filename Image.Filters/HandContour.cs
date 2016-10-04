using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Imaging.Filters;

namespace Image.Filters
{
    public class HandContour : Filter
    {
        private Bitmap imgHandContour;
        private Bitmap imgHandContourCut; 

        public Bitmap ExtractHandContour(Bitmap img)
        {
            // create filter to remove noise (small blobs)
            BlobsFiltering filterB = new BlobsFiltering();
            // configure filter
            //filterB.CoupledSizeFiltering = true;
            filterB.MinWidth =200;           
            filterB.MinHeight = 400;          
            // apply the filter            


            this.imgHandContour = filterB.Apply(img);
            this.imgHandContour = this.Invert();            
            int iVerticalOffset = 150; //cut bottom noise

            Bitmap newImg = new Bitmap(this.imgHandContour.Width, this.imgHandContour.Height - iVerticalOffset, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int x = 0; x < this.imgHandContour.Width; x++)
            {
                for (int y = 0; y < this.imgHandContour.Height - iVerticalOffset; y++)
                {
                    //if(this.imgHandContour.GetPixel(x,y).Name !=  "ffffffff")                    
                    newImg.SetPixel(x, y, this.imgHandContour.GetPixel(x, y));
                }               
            }
            return newImg;

            //return imgHandContour;
        }

        public Bitmap Invert()
        {
            Invert inv = new Invert();
            this.imgHandContour = inv.Apply(this.imgHandContour);
            //this.imgHandContour = inv.Apply(this.imgHandContourCut);
            return this.imgHandContour;
        }
    }
}
