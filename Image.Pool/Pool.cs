using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Image.Pool
{
    public class Pool
    {
        private static Pool _instance;

        private string sImgName;
        private Bitmap imgOriginal;
        private Bitmap imgGreyScale;
        private Bitmap imgBinary;
        private Bitmap imgEdgeDetected;
        private Bitmap imgHandContour;
        private Bitmap imgHandGeometryAttributes;
        
        private Pool() 
        {

        }

        public static Pool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Pool();

                return _instance;
            }
            
        }

        public String ImageName
        {
            get
            {
                return this.sImgName;
            }
            set
            {
                this.sImgName = value;
            }
        }

        public Bitmap Original
        {
            get
            {
                return this.imgOriginal;
            }
            set
            {
                this.imgOriginal = value;
            }
        }

        public Bitmap GreyScale
        {
            get
            {
                return this.imgGreyScale;
            }
            set
            {
                this.imgGreyScale = value;
            }
        }

        public Bitmap Binary
        {
            get
            {
                return this.imgBinary;
            }
            set
            {
                this.imgBinary = value;
            }
        }

        public Bitmap EdgeDetected
        {
            get
            {
                return this.imgEdgeDetected;
            }
            set
            {
                this.imgEdgeDetected = value;
            }
        }

        public Bitmap HandContour
        {
            get
            {
                return this.imgHandContour;
            }
            set
            {
                this.imgHandContour = value;
            }
        }

        public Bitmap HandGeometryAttributes
        {
            get
            {
                return this.imgHandGeometryAttributes;
            }
            set
            {
                this.imgHandGeometryAttributes = value;
            }
        }
    }
}
