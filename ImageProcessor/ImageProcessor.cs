using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Algorithms;
using FactoryMethod;
using Image.Filters;

using AForge.Imaging.Filters;

namespace Images
{

    /*
     * TODO: create a separate methor to GET the data (features) and to DRAW the data. use a deisng pattern: mediator?      
     */

    public class ImageProcessor : IImageProcessor
    {
        System.Drawing.Image _imgOriginal;
        string _fileLabel;
        string _type;
        Bitmap _handCountour;
        Graphics _graphicsControl;
        HandGeometry _handGeometry = null;
        HandPalmVeins _handPalmVeins = null;

        public ImageProcessor(System.Drawing.Image img, string fileLabel, string type)
        {
            this._imgOriginal = img;
            this._fileLabel = fileLabel;
            this._type = type;            
        }

        public string FileLabel
        {
            get
            {
                return this._fileLabel;
            }
            set
            {
                this._fileLabel = value;
            }
        }

        public HandGeometry HandGeometryData
        {
            get
            {
                return this._handGeometry;
            }
        }

        /**
         * 1 - Gray Scale
         * 2 - Binarization
         * 3 - Edge Detection
         * 4 - Blob extractor (hand contour)         
         **/
        public Bitmap ApplyPrimaryFilters()
        {
            Bitmap imgProcessed = null;
            HandContour hc = new HandContour();

            //  Binarization
            FactoryFilterGrayScale ffgs = new FactoryFilterGrayScale();
            Filter greyImage = ffgs.Create();
            Bitmap imgGrey = greyImage.ApplyFilter(this._imgOriginal);
            OtsuThreshold filter = new OtsuThreshold();
            imgProcessed = filter.Apply(imgGrey);

            //  Edge Detection start 
            FactoryFilterEdgeDetection ffed = new FactoryFilterEdgeDetection();
            Filter edgeDetection = ffed.Create();
            imgProcessed = edgeDetection.ApplyFilter(imgProcessed);

            //  Contour Extraction 
            _handCountour = hc.ExtractHandContour(imgProcessed);            
            return _handCountour;
        }

        public void GetUserHandGeometricFeatures()
        {

        }

        public void CalculateHandPerimeter()
        {
            _graphicsControl = Graphics.FromImage(this._handCountour);
            _handGeometry = new HandGeometry(this._handCountour, ref _graphicsControl);
            _handGeometry.CalculatePixelCount();
        }

        public void FindHandCentroid()
        {
            Point pCentroid = _handGeometry.FindCentroid();
            _graphicsControl.FillEllipse(new SolidBrush(Color.SpringGreen), pCentroid.X, pCentroid.Y, 10, 10);
        }

        public void ProccessHandGeometry()
        {
            _handCountour = _handGeometry.ProccessHandGeometry();
        }

        public void CalculatePalmWidth()
        {
            _handGeometry.SortFingerPoints(); //this il be the first call on GetUserGeoFeatures

            Point[] points = _handGeometry.CalculateHandPalmWidth(_handCountour);
            _graphicsControl.DrawLine(new Pen(Color.Cyan, 1), points[0], points[1]);
        }

        public void GetBaseLines()
        {
            List<Point[]> points = _handGeometry.CalculateBaseLines(this._handCountour);

            foreach (Point[] coords in points)
            {
                _graphicsControl.DrawLine(new Pen(Color.Brown, 1), coords[0], coords[1]);
            }
        }

        public void GetIndexFingerWidths()
        {
            List<Point[]> pointsList = _handGeometry.CalculateIndexFingerWidths(this._handCountour);

            foreach(Point[] pointsTuple in pointsList)
            {
                _graphicsControl.DrawLine(new Pen(Color.Red, 1), pointsTuple[0], pointsTuple[1]);
            }            
        }

        public void GetMiddleFingerWidths()
        {
            List<Point[]> pointsList = _handGeometry.CalculateMiddleFingerWidths(this._handCountour);

            foreach (Point[] pointsTuple in pointsList)
            {
                _graphicsControl.DrawLine(new Pen(Color.Red, 1), pointsTuple[0], pointsTuple[1]);
            }
        }

        public void GetRingFingerWidths()
        {
            List<Point[]> pointsList = _handGeometry.CalculateRingFingerWidths(this._handCountour);

            foreach (Point[] pointsTuple in pointsList)
            {
                _graphicsControl.DrawLine(new Pen(Color.Red, 1), pointsTuple[0], pointsTuple[1]);
            }
        }

        public void GetDistancesFromCentroidToFingerTips()
        {
            List<Point[]> pointsList = _handGeometry.CalculateDistancesFromCentroidToFingerTips();

            foreach (Point[] pointsTuple in pointsList)
            {
                _graphicsControl.DrawLine(new Pen(Color.Blue, 1), pointsTuple[0], pointsTuple[1]);
            }
        }


        public void GetDistancesFromCentroidToValleys()
        {
            List<Point[]> pointsList = _handGeometry.CalculateDistancesFromCentroidToValleys();

            foreach (Point[] pointsTuple in pointsList)
            {
                _graphicsControl.DrawLine(new Pen(Color.Magenta, 1), pointsTuple[0], pointsTuple[1]);
            }
        }

        public void GetFingerHeights()
        {
            for (int i = 1; i <= 4; i++)
            {
                Point[] points = _handGeometry.GetFingerHeight(i, this._handCountour);
                _graphicsControl.DrawLine(new Pen(Color.Orange, 1), points[0], points[1]);
            }
        }

        public float[] ExtractHandPalmVeinsData()
        {
            float[] fHoGData = null;
            this._handPalmVeins = new HandPalmVeins(this._imgOriginal);
            _handPalmVeins.ExtractHandPalmROI(_handGeometry.HandCentroid);
            fHoGData = _handPalmVeins.ComputeHoG();

            return fHoGData;
        }
    }
}
