using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Images
{
    interface IImageProcessor
    {
        Bitmap ApplyPrimaryFilters();
        void GetUserHandGeometricFeatures();
        void CalculateHandPerimeter();
        void FindHandCentroid();
        void ProccessHandGeometry();
        void CalculatePalmWidth();
        void GetBaseLines();
        void GetIndexFingerWidths();
        void GetMiddleFingerWidths();
        void GetRingFingerWidths();
        void GetDistancesFromCentroidToFingerTips();
        void GetDistancesFromCentroidToValleys();
        void GetFingerHeights();
        float[] ExtractHandPalmVeinsData();
    }
}
