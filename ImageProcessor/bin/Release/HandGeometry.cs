using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Imaging;

using Utils;
using BiometricData;

namespace Algorithms
{
    public class HandGeometry : IHandGeometry
    {
        #region Private Members

        private const int _THUMBS_ = 1;
        private const int _INDEX_FINGER_ = 1;
        private const int _MIDDLE_FINGER_ = 2;
        private const int _RING_FINGER_ = 3;
        private const int _PINKY_FINGER_ = 4;

        private Bitmap imgHandContour = null;
        private int iPixelCount = 0;       
        private Point pHandCentroid = new Point();
        List<Point[]> lstBaseLines = new List<Point[]>();
        List<Point> lstContourPoints = new List<Point>();
        List<Point> lstCurvaturePoints = new List<Point>();
        List<Point> lstRedPixels = new List<Point>();        
        List<Point> EdgePoints = new List<Point>();
        List<Point> ValleyPoints = new List<Point>();
        List<Point> EdgePointsOrdered = new List<Point>();
        List<Point> ValleyPointsOrdered = new List<Point>();
        List<double> lstHandGeometryFeatures = new List<double>();
        HandGeometryUserAttributes hgua = new HandGeometryUserAttributes();
        Dictionary<double, Point> distances = new Dictionary<double, Point>();
        Graphics graphicsControl = null;
        
        #endregion

        # region Constructors
        public HandGeometry(Bitmap imgHandContour, ref Graphics g)
        {            
            this.imgHandContour = imgHandContour;
            this.graphicsControl = g;
            this.lstHandGeometryFeatures.Clear();
        }

        public HandGeometry()
        {
        }

        #endregion

        #region Properties

        public Point HandCentroid
        {
            get
            {
                return this.pHandCentroid;
            }
        }
            
        public int PixelCount
        {
            get
            {
                return this.iPixelCount;
            }
        }

        public List<Point> ContourPoints
        {
            get
            {
                return this.lstContourPoints;
            }
        }

        public List<Point> EdgePointsList
        {
            get
            {
                return this.EdgePoints;
            }
        }

        public List<Point> ValleyPointsList
        {
            get
            {
                return this.ValleyPoints;
            }
        }

        public List<double> HandGeometryFeatures
        {
            get
            {
                return this.lstHandGeometryFeatures;
            }
        }
        
        #endregion
        
        #region Public Methods

        public Bitmap ProccessHandGeometry()
        {            
            this.FindCentroid();
            this.CalculateDistancesFromCentroid();
            this.DetectFingerTips();
            this.DetectHandValleys();
            return this.imgHandContour;
        }                    

        public void CalculateDistancesFromCentroid()
        {
            Point pBase = this.pHandCentroid;

            this.CalculatePixelCount();

            foreach (Point p in this.lstContourPoints)
            {
                if (p.Y >= this.pHandCentroid.Y)
                    continue;

                double distance = Geometry.EuclideanDistance(p, pBase);

                if (!Double.IsNaN(distance) && distance > 0)
                {
                    if (!distances.ContainsKey(distance) && distance > 20)
                    {
                        distances.Add(distance, p);                        
                    }
                }
            }
        }

        public void CalculatePixelCount()
        {
            if (this.PixelCount > 0)
                return;

            for (int x = 2; x < this.imgHandContour.Width - 2; x++)
            {
                for (int y = 2; y < this.imgHandContour.Height - 100; y++)
                {
                    Color cl = this.imgHandContour.GetPixel(x, y);
                    if (cl.Name != "ffffffff")
                    {
                        this.iPixelCount++;
                        lstContourPoints.Add(new Point(x, y));
                    }
                }
            }
            //first feature is the hand perimeter (pixel count) used for normalization.
            lstHandGeometryFeatures.Add((this.PixelCount));
        }

        public Point FindCentroid()
        {
            int xPoints = 0;
            int yPoints = 0;

            this.CalculatePixelCount();

            foreach (Point p in this.lstContourPoints)
            {
                xPoints += p.X;
                yPoints += p.Y;
            }

            this.pHandCentroid.X = xPoints / this.lstContourPoints.Count;
            this.pHandCentroid.Y = (yPoints / this.lstContourPoints.Count) + 180;
            return this.pHandCentroid;
        }        

        public void ExtractUserHandGeometricFeatures()
        {

        }

        public void SortFingerPoints()
        {
           var valleys = from valPoints in this.ValleyPointsList orderby valPoints.X ascending select valPoints;
           var tips = from fingerTips in this.EdgePointsList orderby fingerTips.X ascending select fingerTips;
            
            this.ValleyPointsOrdered = valleys.ToList<Point>();
            this.EdgePointsOrdered = tips.ToList<Point>();
        }

        public Point[] CalculateHandPalmWidth(Bitmap img)
        {
            int iRightBorder = this.pHandCentroid.X + 20;

            while (img.GetPixel(iRightBorder, this.pHandCentroid.Y + Math.Abs(ValleyPointsOrdered[0].Y - this.pHandCentroid.Y)).Name == "ffffffff")
            {
                iRightBorder++;
            }

            Point pRightBorder = new Point(iRightBorder, this.pHandCentroid.Y + Math.Abs(ValleyPointsOrdered[0].Y - this.pHandCentroid.Y));
            Point[] points = new Point[2] { ValleyPointsOrdered[0], pRightBorder };

            //second feature: hand palm width:
            lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(ValleyPointsOrdered[0], pRightBorder));

            return points;
        }

        public void CalculareFingerWidths()
        {

        }

        public List<Point[]> CalculateBaseLines(Bitmap img)
        {
            try
            {
                //thumbs -> index
                Point pStart = this.AdjusFirstValleyPoint(img);
                //brings thumbs valley near to index finger left base
                this.ValleyPointsOrdered[0] = pStart;
                lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(pStart, ValleyPointsOrdered[_INDEX_FINGER_]));
                this.lstBaseLines.Add(new Point[2] { pStart, ValleyPointsOrdered[_INDEX_FINGER_] });

                //index-> middle
                lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(this.ValleyPointsOrdered[_MIDDLE_FINGER_], this.ValleyPointsOrdered[_INDEX_FINGER_]));
                this.lstBaseLines.Add(new Point[2] { this.ValleyPointsOrdered[_MIDDLE_FINGER_], this.ValleyPointsOrdered[_INDEX_FINGER_] });

                //middle->ring
                lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(this.ValleyPointsOrdered[_RING_FINGER_], this.ValleyPointsOrdered[_MIDDLE_FINGER_]));
                this.lstBaseLines.Add(new Point[2] { this.ValleyPointsOrdered[_RING_FINGER_], this.ValleyPointsOrdered[_MIDDLE_FINGER_] });

                //ring->pinky
                int iPinkyWidth = 10;

                while (img.GetPixel(this.ValleyPointsOrdered[_RING_FINGER_].X + iPinkyWidth, this.ValleyPointsOrdered[_RING_FINGER_].Y + 50).Name == "ffffffff")
                {
                    iPinkyWidth++;
                }

                lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(new Point(this.ValleyPointsOrdered[_RING_FINGER_].X, this.ValleyPointsOrdered[_RING_FINGER_].Y), new Point(this.ValleyPointsOrdered[_RING_FINGER_].X + iPinkyWidth, this.ValleyPointsOrdered[_RING_FINGER_].Y + 50)));
                this.lstBaseLines.Add(new Point[2] { new Point(this.ValleyPointsOrdered[_RING_FINGER_].X, this.ValleyPointsOrdered[_RING_FINGER_].Y), new Point(this.ValleyPointsOrdered[_RING_FINGER_].X + iPinkyWidth, this.ValleyPointsOrdered[_RING_FINGER_].Y + 50) });
            }
            catch (Exception ex)
            {
                lstHandGeometryFeatures.Add(0);
                this.lstBaseLines.Add(new Point[] {new Point(0,0), new Point(0,0)});
            }
            return this.lstBaseLines;
        }

        public Point[] GetFingerHeight(int fingerIndex, Bitmap img)
        {
            Point[] pFingerHeight = new Point[]{};

            if (this.lstBaseLines.Count == 0)
                this.CalculateBaseLines(img);

            switch (fingerIndex)
            {
                case _INDEX_FINGER_:
                    pFingerHeight = this.CalculateFingerHeight(lstBaseLines[0], this.EdgePointsOrdered[_INDEX_FINGER_]);
                    pFingerHeight[0].Y -= 10; //correction index finger;
                    break;

                case _MIDDLE_FINGER_:
                    pFingerHeight = this.CalculateFingerHeight(lstBaseLines[1], this.EdgePointsOrdered[_MIDDLE_FINGER_]);
                    break;

                case _RING_FINGER_:
                    pFingerHeight = this.CalculateFingerHeight(lstBaseLines[2], this.EdgePointsOrdered[_RING_FINGER_]);
                    break;

                case _PINKY_FINGER_:
                    pFingerHeight = this.CalculateFingerHeight(lstBaseLines[3], this.EdgePointsOrdered[_PINKY_FINGER_]);              
                    break;
            }

            return pFingerHeight;
        }

        private Point[] CalculateFingerHeight(Point[] baseP, Point edge)
        {
            try
            {
                Point middle = Geometry.GetMiddlePoint(baseP);
                double distance = Geometry.EuclideanDistance(middle, edge);
                lstHandGeometryFeatures.Add(distance);
                return new Point[2] { middle, edge };
            }
            catch (Exception ex)
            {
                lstHandGeometryFeatures.Add(0);
                return new Point[2] { new Point(0, 0), new Point(0, 0) };
            }
        }

        public List<Point[]> CalculateIndexFingerWidths(Bitmap img)
        {
            List<Point[]> lstIndexFingerWidths = new List<Point[]>();
            FingerBaseLineData fbld = new FingerBaseLineData();

            fbld = this.CalculateFingerBaseLength(_INDEX_FINGER_,img);
            Gap gap = new Gap(30,10,60, new Point(fbld.MiddleX, fbld.MiddleY));
            lstIndexFingerWidths = this.CalculateFingerWidths(gap, fbld.Inclination, img, _INDEX_FINGER_);
            
            return lstIndexFingerWidths;
        }

        public List<Point[]> CalculateMiddleFingerWidths(Bitmap img)
        {
            List<Point[]> lstMiddleFingerWidths = new List<Point[]>();            
            FingerBaseLineData fbld = new FingerBaseLineData();

            fbld = this.CalculateFingerBaseLength(_MIDDLE_FINGER_, img);
            Gap gap = new Gap(50, 10, 80, new Point(fbld.MiddleX, fbld.MiddleY));
            lstMiddleFingerWidths = this.CalculateFingerWidths(gap, fbld.Inclination, img, _MIDDLE_FINGER_);
               
            return lstMiddleFingerWidths;
        }
        
        public List<Point[]> CalculateRingFingerWidths(Bitmap img)
        {
            List<Point[]> lstRingFingerWidths = new List<Point[]>();
            FingerBaseLineData fbld = new FingerBaseLineData();

            fbld = this.CalculateFingerBaseLength(_RING_FINGER_, img);
            Gap gap = new Gap(50, 10, 80, new Point(fbld.MiddleX, fbld.MiddleY));
            lstRingFingerWidths = this.CalculateFingerWidths(gap, fbld.Inclination, img, _RING_FINGER_);
    
            return lstRingFingerWidths;
        }    

        public List<Point[]> CalculateDistancesFromCentroidToValleys()
        {
            List<Point[]> points = new List<Point[]>();
            foreach (Point p in this.ValleyPointsOrdered)
            {                
                lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(this.HandCentroid, p));             
                points.Add(new Point[2]{this.HandCentroid, p});
            }

            return points;
        }

        public List<Point[]> CalculateDistancesFromCentroidToFingerTips()
        {
            List<Point[]> points = new List<Point[]>();
            foreach (Point p in this.EdgePointsOrdered)
            {                
                lstHandGeometryFeatures.Add(Geometry.EuclideanDistance(this.HandCentroid, p));             
                points.Add(new Point[2]{this.HandCentroid, p});
            }

            return points;
        }     
        
        #endregion

        #region Private Methods

        private FingerBaseLineData CalculateFingerBaseLength(int fingerIndex, Bitmap img)
        {
            Point pCurrent = new Point();
            pCurrent = this.ValleyPointsOrdered[fingerIndex-1];
            int iInclination = Geometry.AngularCoefficient(this.ValleyPointsOrdered[fingerIndex].Y, pCurrent.Y);

            if (fingerIndex == _RING_FINGER_)
            {
                if (iInclination > 20 && iInclination < 32)
                    iInclination -= 10;

                if (iInclination > 32)
                    iInclination -= 20;
            }
            else
            {
                if (iInclination > 8)
                    iInclination -= 5;
            }

            int iMiddleX = pCurrent.X + Math.Abs(this.ValleyPointsOrdered[fingerIndex].X - pCurrent.X) / 2;
            int iMiddleY = pCurrent.Y + Math.Abs(this.ValleyPointsOrdered[fingerIndex].Y - pCurrent.Y) / 2;

            FingerBaseLineData fbld = new FingerBaseLineData(iMiddleX, iMiddleY, iInclination);

            return fbld;
        }

        private List<Point[]> CalculateFingerWidths(Gap gap, int inclination, Bitmap img, int fingerIndex)
        {
            List<Point[]> widths = new List<Point[]>();
            int iMiddleX = gap.MiddleCoordinates.X;
            int iMiddleY = gap.MiddleCoordinates.Y;
            int iYdiff = 0;
                    
            for (int iGap = gap.Start; iGap < gap.Limit; iGap += gap.Step)
            {
                try
                {
                    //get contour pixels
                    Point startPoint = new Point();
                    Point endPoint = new Point();

                    int iMiddleXStart = iMiddleX;

                    //ring finger correction
                    if (fingerIndex == _RING_FINGER_)
                        iMiddleXStart += 15;

                    while (img.GetPixel(iMiddleXStart, iMiddleY - iGap).Name == "ffffffff")
                    {
                        iMiddleXStart--;
                    }

                    startPoint.X = iMiddleXStart;
                    startPoint.Y = iMiddleY - iGap;

                    //ring finger correction
                    if (fingerIndex == _RING_FINGER_)
                        startPoint.Y += 5;

                    //if index finger, adds inclination at start
                    if (fingerIndex == _INDEX_FINGER_)
                        startPoint.Y += inclination;

                    int iMiddleXEnd = iMiddleX;

                    //add incklination factor for middle and ring finger while searching for a black pixel.
                    if (fingerIndex > _INDEX_FINGER_)
                        iYdiff = inclination;

                    while (img.GetPixel(iMiddleXEnd, iMiddleY - iGap + iYdiff).Name == "ffffffff")
                    {
                        iMiddleXEnd++;
                    }
                    endPoint.X = iMiddleXEnd;
                    endPoint.Y = iMiddleY - iGap;

                    //if middle or ring finger, adds inclination at start
                    if (fingerIndex > _INDEX_FINGER_)
                        endPoint.Y += inclination;

                    //finger widths
                    double width = Geometry.EuclideanDistance(startPoint, endPoint);
                    lstHandGeometryFeatures.Add(width);
                    widths.Add(new Point[2] { startPoint, endPoint });
                    inclination = Math.Abs(endPoint.Y - startPoint.Y);
                    iMiddleX = startPoint.X + (endPoint.X - startPoint.X) / 2;
                    iMiddleY = startPoint.Y + (endPoint.Y - startPoint.Y) / 2;
                }
                catch (Exception ex)
                {
                    lstHandGeometryFeatures.Add(0);
                    widths.Add(new Point[2]{new Point(0,0), new Point(0,0)});
                }
            }
            return widths;
        }  

        private void DetectFingerTips()
        {
            //var items = from pair in distances orderby pair.Key descending select pair.Value;
            //var farthestDistances = from pair in distances orderby pair.Key descending select pair;
            var ordered = distances.OrderByDescending(c => c.Key);
            bool bFirstPoint = true;
            int previousXRight = 0;
            int previousXLeft = 0;
            int previousYLeft = 0;            

            foreach (KeyValuePair<double, Point> kvp in ordered)
            {

                if (kvp.Value.X < 150) //skip thumbs
                    //continue;

                if (kvp.Value.Y + 50 >= this.pHandCentroid.Y) //thumb case normalization
                    continue;

                if (bFirstPoint)
                {
                    this.imgHandContour.SetPixel(kvp.Value.X, kvp.Value.Y, Color.Red); //this line and the bellow one should be on a separated methos to avoid duplicatiom with the other if bellow
                    //g.DrawEllipse(new Pen(Color.Red, 2), kvp.Value.X, kvp.Value.Y, 10, 10);
                    this.graphicsControl.FillEllipse(new SolidBrush(Color.Red), kvp.Value.X, kvp.Value.Y, 10, 10);
                    bFirstPoint = false;
                    previousXRight = kvp.Value.X;
                    previousXLeft = kvp.Value.X;
                    previousYLeft = kvp.Value.Y;
                    this.EdgePoints.Add(kvp.Value);
                    continue;
                }
                
                
                //right scan
                if (previousXRight + 40 < kvp.Value.X)
                {
                    this.imgHandContour.SetPixel(kvp.Value.X, kvp.Value.Y, Color.Red); //this line and the bellow one should be on a separated methos to avoid duplicatiom with the other if bellow
                    //g.DrawEllipse(new Pen(Color.Red, 2), kvp.Value.X, kvp.Value.Y, 10, 10);
                    this.graphicsControl.FillEllipse(new SolidBrush(Color.Red), kvp.Value.X, kvp.Value.Y, 10, 10);
                    previousXRight = kvp.Value.X;
                    EdgePoints.Add(kvp.Value);
                    continue;
                }

              //left scan
                if   (previousXLeft - 60 > kvp.Value.X)
                {
                    this.imgHandContour.SetPixel(kvp.Value.X, kvp.Value.Y, Color.Red); //this line and the bellow one should be on a separated methos to avoid duplicatiom with the other if bellow
                    //g.DrawEllipse(new Pen(Color.Red, 2), kvp.Value.X, kvp.Value.Y, 10, 10);
                    this.graphicsControl.FillEllipse(new SolidBrush(Color.Red), kvp.Value.X, kvp.Value.Y, 10, 10);
                    previousXLeft = kvp.Value.X;
                    previousYLeft = kvp.Value.Y;
                    EdgePoints.Add(kvp.Value);
                }
            }
        }

        private void DetectHandValleys()
        {
            //for a more genereic algorithm order the list by X axis values as in (from r in EdgePoints orderby r.X descending select r.X)) 
            int iValleyIndex = 0;
            int yEdge = this.pHandCentroid.Y;
            var orderedAsc = distances.OrderBy(c => c.Key);
            //var orderedAsc = from pair in distances orderby pair.Key descending, pair.Value.X ascending select pair;
                        
            List<double> shortest = new List<double>();
            Point previousPoint = new Point(0, 0);

            this.ValleyPoints.Clear();
            bool bContinue = false;
            int iGetShortestFive = 0;
            foreach (KeyValuePair<double, Point> shortestKvp in orderedAsc)
            {
                if (iGetShortestFive == 4)
                    break;

                if (shortestKvp.Value.X < 150 || shortestKvp.Value.Y > this.pHandCentroid.Y-50)
                    continue;
         
                if (this.ValleyPoints.Count >= 4)
                    break;
                              
                if (shortestKvp.Value.Y > this.pHandCentroid.Y-50) //plus 100 is the offset used for the centroid
                {
                    previousPoint.X = shortestKvp.Value.X;
                    previousPoint.Y = shortestKvp.Value.Y;
                    continue;
                }

                bContinue = false;
                foreach (Point pEval in this.ValleyPoints)
                {
                    if(Math.Abs(shortestKvp.Value.X - pEval.X) <= 45)
                    {
                        bContinue = true;
                        previousPoint.X = shortestKvp.Value.X;
                        previousPoint.Y = shortestKvp.Value.Y;
                        break;
                    }
                }

                if (bContinue)
                    continue;                                           
               
               this.imgHandContour.SetPixel(shortestKvp.Value.X, shortestKvp.Value.Y, Color.Red); //this line and the bellow one should be on a separated methos to avoid duplicatiom with the other if bellow
               this.graphicsControl.FillEllipse(new SolidBrush(Color.Red), shortestKvp.Value.X, shortestKvp.Value.Y, 5, 5);
               previousPoint.X = shortestKvp.Value.X;
               previousPoint.Y = shortestKvp.Value.Y;
               this.ValleyPoints.Add(shortestKvp.Value);
               iValleyIndex++;
               shortest.Add(shortestKvp.Key);
               iGetShortestFive++;
            }
        }

        private void CalculateNeighborhoodWhitePixelCount()
        {
            foreach (Point point in lstRedPixels)
            {
                int iWhitePixelCount = 0;
                Color cl = this.imgHandContour.GetPixel(point.X, point.Y);

                //white neighbor counting
                if (this.imgHandContour.GetPixel(point.X - 1, point.Y).Name == "ffffffff")
                    iWhitePixelCount++;

                if (this.imgHandContour.GetPixel(point.X + 1, point.Y).Name == "ffffffff")
                    iWhitePixelCount++;

                if (this.imgHandContour.GetPixel(point.X, point.Y - 1).Name == "ffffffff")
                    iWhitePixelCount++;

                if (this.imgHandContour.GetPixel(point.X, point.Y + 1).Name == "ffffffff")
                    iWhitePixelCount++;

                //90% of the width is being processed. the other 10% is not necessary and if included add noise to the hand shape.
                if (iWhitePixelCount > 2 && point.X < 1440)
                    this.imgHandContour.SetPixel(point.X, point.Y, Color.White);

                else
                    lstCurvaturePoints.Add(new Point(point.X, point.Y));
            }            
        }   
    
        private Point AdjusFirstValleyPoint(Bitmap img)
        {
            //get first valley location and brings to top
            int iStartX = ValleyPointsOrdered[1].X;
            int iStartY = ValleyPointsOrdered[1].Y + 3; //+3 correction
            int iWidth = 0;

            while (img.GetPixel(iStartX, iStartY).Name == "ffffffff")
            {
                iWidth++;
                iStartX--;

                if (iStartX % 15 == 0)
                    iStartY++;
            }

            return new Point(iStartX, iStartY);
        }

        #endregion
    }
}