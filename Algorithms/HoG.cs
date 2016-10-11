using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Algorithms
{
    public  class HoG
    {
        private Size _windowSize;
        private Size _padding;
        private Size _cellSize;
        private Size _blockSize;
        private double min;
        private double max;

        private HOGDescriptor _hogDescriptor;

        public HoG(Size windowSize, Size padding)
        {
            this._windowSize = windowSize;
            this._padding = padding;
            _hogDescriptor = new HOGDescriptor();
        }

        public HoG(Size windowSize, Size cellSize, Size blockSize, Size padding)
        {
            this._windowSize = windowSize;
            this._padding = padding;
            this._cellSize = cellSize;
            this._blockSize = blockSize;
            _hogDescriptor = new HOGDescriptor();
        }

        public HoG(Size windowSize, Size cellSize, Size blockSize)
        {
            this._windowSize = windowSize;            
            this._cellSize = cellSize;
            this._blockSize = blockSize;
            _hogDescriptor = new HOGDescriptor();
        }

        public Size WindowSize
        {
            get
            {
                return this._windowSize;
            }
            set
            {
                this._windowSize = value;
            }
        }

        public Size Padding
        {
            get
            {
                return this._padding;
            }
            set
            {
                this._padding = value;
            }
        }

        public Size CellSize
        {
            get
            {
                return this._cellSize;
            }
            set
            {
                this._cellSize= value;
            }
        }

        public Size BlockSize
        {
            get
            {
                return this._blockSize;
            }
            set
            {
                this._blockSize = value;
            }
        }


        public  float[] Compute(Bitmap roi)
        {
            if(_hogDescriptor == null)
                return null;

            Image<Bgr, Byte> imageOfInterest = new Image<Bgr,byte>(roi);    
            //float[] handPalmVeinDescriptor = hog.Compute(imageOfInterest, new Size(170, 160), new Size(2, 2));
            float[] handPalmVeinDescriptor = _hogDescriptor.Compute(imageOfInterest, new Size(68, 64), new Size(2, 2));

            // min = handPalmVeinDescriptor.Min();
            // max = handPalmVeinDescriptor.Max();

            return handPalmVeinDescriptor;
        }   
    }
}
