using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Image.Filters
{
    public class Binarization : Filter
    {
        private Bitmap _imgOriginalThreshold;
        private int _iThreshold = 65;

        public Binarization()
        {
            
        }

        public int Threshold
        {
            get
            {
                return this._iThreshold;
            }
            set
            {
                this._iThreshold = value;
            }
        }

        public override Bitmap ApplyFilter(Bitmap imgThreshold)
        {
            //Threshold filterThreshold = new Threshold(this._iThreshold);            
            OtsuThreshold filterThreshold = new OtsuThreshold();
            this._imgOriginalThreshold = filterThreshold.Apply(imgThreshold);
            return this._imgOriginalThreshold;           
        }
    }
}
