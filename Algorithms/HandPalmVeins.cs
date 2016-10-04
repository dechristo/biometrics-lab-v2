using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class HandPalmVeins
    {
        Bitmap _imgHand = null;
        Bitmap _imgPalm = null;
        HoG _hog = null;

        public void FingerVeins(Bitmap imgHand)
        {
            this._imgHand = imgHand;
            _hog = new HoG(new Size(68, 64), new Size(2, 2));
        }

        public Bitmap FindHandPalmROI()
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
                    roi.SetPixel(roiWidth / 2 - x, roiHeight / 2 - y, imgGrey.GetPixel(pCentroid.X - x, pCentroid.Y - y)); //top
                    roi.SetPixel(roiWidth / 2 - x, roiHeight / 2 + y, imgGrey.GetPixel(pCentroid.X - x, pCentroid.Y + y)); //bottom
                }
            }

            //right part -> centroid right
            for (int x = 0; x < roiWidth / 2; x++)
            {
                for (int y = 0; y < roiHeight / 2; y++)
                {
                    roi.SetPixel(roiWidth / 2 + x, roiHeight / 2 - y, imgGrey.GetPixel(pCentroid.X + x, pCentroid.Y - y)); //top
                    roi.SetPixel(roiWidth / 2 + x, roiHeight / 2 + y, imgGrey.GetPixel(pCentroid.X + x, pCentroid.Y + y)); //bottom
                }
            }
        }

        public float[] ComputeHoG()
        {
            return _hog.Compute(_imgPalm);
        }
    }
}
