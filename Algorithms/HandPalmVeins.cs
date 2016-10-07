using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Algorithms
{
   public class HandPalmVeins : IHandPalmVeins
    {
        Bitmap _imgHand = null;        
        Bitmap _roi = null;
        HoG _hog = null;

        public HandPalmVeins(Image imgHand)
        {
            this._imgHand = new Bitmap(imgHand);
            _hog = new HoG(new Size(68, 64), new Size(2, 2));
        }

        public Bitmap ROI
        {
            get
            {
                return this._roi;
            }
        }

        public Bitmap ExtractHandPalmROI(Point pCentroid)
        {
            try
            {
                int roiWidth = 160;
                int roiHeight = 120;
                
                Bitmap roi = new Bitmap(roiWidth, roiHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                for (int x = 0; x < roiWidth; x++)
                {
                    for (int y = 0; y < roiHeight; y++)
                    {
                        roi.SetPixel(x, y, Color.White);
                    }
                }

                //left part -> centroid left
                for (int x = 0; x < roiWidth / 2; x++)
                {
                    for (int y = 0; y < roiHeight / 2; y++)
                    {
                        roi.SetPixel(roiWidth / 2 - x, roiHeight / 2 - y, this._imgHand.GetPixel(pCentroid.X - x, pCentroid.Y - y)); //top
                        roi.SetPixel(roiWidth / 2 - x, roiHeight / 2 + y, this._imgHand.GetPixel(pCentroid.X - x, pCentroid.Y + y)); //bottom
                    }
                }

                //right part -> centroid right
                for (int x = 0; x < roiWidth / 2; x++)
                {
                    for (int y = 0; y < roiHeight / 2; y++)
                    {
                        roi.SetPixel(roiWidth / 2 + x, roiHeight / 2 - y, this._imgHand.GetPixel(pCentroid.X + x, pCentroid.Y - y)); //top
                        roi.SetPixel(roiWidth / 2 + x, roiHeight / 2 + y, this._imgHand.GetPixel(pCentroid.X + x, pCentroid.Y + y)); //bottom
                    }
                }
                this._roi = roi;
                return this._roi;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return null;
            }            
        }

        public float[] ComputeHoG()
        {
            float[] fHogData = null;
            try
            {
                fHogData = _hog.Compute(this._roi);
                return fHogData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return null;
            }
        }
    }
}
