using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Images
{
    public class ImageProcessorMediator
    {
        private ImageProcessor _imgProc;

        public ImageProcessorMediator(System.Drawing.Image img, string fileLabel, string type)
        {
            _imgProc = new ImageProcessor(img, fileLabel, type);
        }

        public Bitmap GetProcessedImage()
        {
            if(_imgProc == null)
                return null;

            Bitmap img = _imgProc.ApplyPrimaryFilters();
            _imgProc.CalculateHandPerimeter();
            _imgProc.FindHandCentroid();
            _imgProc.ProccessHandGeometry();
            _imgProc.CalculatePalmWidth();
            _imgProc.GetBaseLines();
            _imgProc.GetIndexFingerWidths();
            _imgProc.GetMiddleFingerWidths();
            _imgProc.GetRingFingerWidths();
            _imgProc.GetDistancesFromCentroidToFingerTips();
            _imgProc.GetDistancesFromCentroidToValleys();
            _imgProc.GetFingerHeights();

            return img;
        }
    }

    /*
     * TODO > separate drawing and data acquisition (handGeometry and images)
     * 
     */
}