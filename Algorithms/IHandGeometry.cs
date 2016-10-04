using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Utils;
using BiometricData;

namespace Algorithms
{
    interface IHandGeometry
    {        
        Bitmap ProccessHandGeometry();
        void CalculateDistancesFromCentroid();
        void CalculatePixelCount();
        Point FindCentroid();
        void ExtractUserHandGeometricFeatures();
        void SortFingerPoints();
        Point[] CalculateHandPalmWidth(Bitmap img);
        void CalculareFingerWidths();
        List<Point[]> CalculateBaseLines(Bitmap img);
        Point[] GetFingerHeight(int fingerIndex, Bitmap img);        
        List<Point[]> CalculateIndexFingerWidths(Bitmap img);
        List<Point[]> CalculateMiddleFingerWidths(Bitmap img);
        List<Point[]> CalculateRingFingerWidths(Bitmap img);
        List<Point[]> CalculateDistancesFromCentroidToValleys();
        List<Point[]> CalculateDistancesFromCentroidToFingerTips();
    }
}
