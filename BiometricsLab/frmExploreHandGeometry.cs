using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Algorithms;
using Image.Pool;
using Utils;

namespace BiometricsLab
{
    public partial class frmExploreHandGeometry : Form
    {
        private int idxAttribute;
        private HandGeometry _hg = null;
        private HandGeometryUserAttributes handGeometryAttributes = null;
        private Graphics _g = null;
        private Bitmap imgHandGeoExplorer = null;
        private List<Point[]> lstFingerLimits = new List<Point[]>();
        private List<Point[]> lstFingerLimitsA = new List<Point[]>();
        private List<Point[]> lstFingerLimitsB = new List<Point[]>();
        private List<Point[]> lstFingerLimitsC = new List<Point[]>();
        
        public frmExploreHandGeometry(HandGeometry hg)
        {
            InitializeComponent();
            this._hg = hg;
            this.handGeometryAttributes = new HandGeometryUserAttributes();
            this.handGeometryAttributes.Label = Pool.Instance.ImageName;
            this.handGeometryAttributes.HandPerimeter = hg.PixelCount;           
            this.idxAttribute = 1;
            this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [HPer]=" + this.handGeometryAttributes.HandPerimeter.ToString("") + "\n");
            this.rtbAttributes.AppendText("--------------------------------------\n");
            this.idxAttribute++;
        }

        private void frmExploreHandGeometry_Load(object sender, EventArgs e)
        {
            this.labelImageName.Text = Pool.Instance.ImageName;
            imgHandGeoExplorer = Pool.Instance.HandGeometryAttributes;
            this.pbMain.Image = Pool.Instance.HandGeometryAttributes;
            this._g = Graphics.FromImage(imgHandGeoExplorer);
            this.buttonCalcDistanceBetweenValleys_Click(sender, e);
            this.buttonFingerWidthsCalc_Click(sender, e);
            this.buttonCalcFingerTipsDistance_Click(sender, e);
            this.buttonCalcValleysDist_Click(sender, e);
            this.buttonSave_Click(sender, e);
        }

        private void buttonCalcFingerTipsDistance_Click(object sender, EventArgs e)
        {
            this.rtbAttributes.AppendText("--------------------------------------\n");
            foreach (Point edgeP in this._hg.EdgePointsList)
            {
                double dist = Geometry.EuclideanDistance(edgeP, this._hg.HandCentroid);

                if (!handGeometryAttributes.DistanceToFingerTips.ContainsKey(Pool.Instance.ImageName))
                {
                   handGeometryAttributes.DistanceToFingerTips.Add(Pool.Instance.ImageName, new List<double>());
                }
                handGeometryAttributes.DistanceToFingerTips[Pool.Instance.ImageName].Add(dist);
                this._g.DrawLine(new Pen(Color.Orange, 1), edgeP, this._hg.HandCentroid);
                this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [dtFT]=" + dist.ToString("0.00").Replace(",",".") + "\n");
                this.idxAttribute++;
            }
            this.pbMain.Refresh();
        }

        private void buttonCalcValleysDist_Click(object sender, EventArgs e)
        {
            this.rtbAttributes.AppendText("--------------------------------------\n");

            foreach (Point valleyP in this._hg.ValleyPointsList)
            {
                double dist = Geometry.EuclideanDistance(valleyP, this._hg.HandCentroid);

                if (!handGeometryAttributes.DistanceToValleys.ContainsKey(Pool.Instance.ImageName))
                {
                    handGeometryAttributes.DistanceToValleys.Add(Pool.Instance.ImageName, new List<double>());
                }
                handGeometryAttributes.DistanceToValleys[Pool.Instance.ImageName].Add(dist);
                this._g.DrawLine(new Pen(Color.Magenta, 1), valleyP, this._hg.HandCentroid);
                this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [dtV]=" + dist.ToString("0.00").Replace(",", ".") + "\n");
                this.idxAttribute++;
            }
            this.pbMain.Refresh();
        }

        private void buttonFingerWidthsCalc_Click(object sender, EventArgs e)
        {
            Point pFingerWidthRightLimit = new Point();
            Point pFingerWidthLeftLimit = new Point();

            if (!handGeometryAttributes.FingerWidths.ContainsKey(Pool.Instance.ImageName))
            {
                handGeometryAttributes.FingerWidths.Add(Pool.Instance.ImageName, new List<double>());
            }

            foreach (Point edgeP in this._hg.EdgePointsList)
            {
                int iFingerAxisY = edgeP.Y + 30;
                                
                int offset = 1;
                try
                {
                    while (imgHandGeoExplorer.GetPixel(edgeP.X + offset, iFingerAxisY).Name == "ffffffff")
                    {
                        offset++;
                    }

                    pFingerWidthRightLimit.X = edgeP.X + offset;
                    pFingerWidthRightLimit.Y = iFingerAxisY;

                    offset = 1;
               
                    while (imgHandGeoExplorer.GetPixel(edgeP.X - offset, iFingerAxisY).Name == "ffffffff")
                    {
                        offset++;
                    }
                    pFingerWidthLeftLimit.X = edgeP.X - offset;
                    pFingerWidthLeftLimit.Y = iFingerAxisY;

                    this._g.DrawLine(new Pen(Color.Blue, 1), pFingerWidthLeftLimit, pFingerWidthRightLimit);
                    lstFingerLimits.Add(new Point[2] { pFingerWidthLeftLimit, pFingerWidthRightLimit });
                    double dist = Geometry.EuclideanDistance(pFingerWidthLeftLimit, pFingerWidthRightLimit);
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w1]=" + dist.ToString() + "\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(dist);
                    this.idxAttribute++;
                }
                catch (Exception ex)
                {
                    lstFingerLimits.Add(new Point[2] { new Point(0, 0), new Point(0, 0) });
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w1]=0\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(0);
                    this.idxAttribute++;
                }
            }

            //pink finger correction: if this thinger wasn't detect, added zeroes in the width distance values list for this thinger.
            //if (handGeometryAttributes.FingerWidths.Count < (this._hg.EdgePointsList.Count * 6))

            if (this._hg.EdgePointsList.Count < 5)
            {
                handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(0);
                this.idxAttribute++;
            }
            

            this.rtbAttributes.AppendText("--------------------------------------\n");
           
            foreach (Point[] pLimits in this.lstFingerLimits)
            {
                Point pFingerWidthRightLimitA = new Point();
                Point pFingerWidthLeftLimitA = new Point();

                pFingerWidthLeftLimitA.Y = pLimits[0].Y + 20;
                pFingerWidthRightLimitA.Y = pLimits[1].Y + 20;

                int midPoint = (pLimits[0].X  + pLimits[1].X ) / 2;
                int offset = 0;
                try
                {
                    while (imgHandGeoExplorer.GetPixel(midPoint + offset, pLimits[1].Y + 20).Name == "ffffffff")
                    {
                        offset++;
                    }

                    pFingerWidthRightLimitA.X = midPoint + offset;

                    offset = 1;
                    while (imgHandGeoExplorer.GetPixel(midPoint - offset, pLimits[1].Y + 20).Name == "ffffffff")
                    {
                        offset++;
                    }
                    pFingerWidthLeftLimitA.X = midPoint - offset;

                    this._g.DrawLine(new Pen(Color.Blue, 1), pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                    this.lstFingerLimitsA.Add(new Point[2] { pFingerWidthLeftLimitA, pFingerWidthRightLimitA });
                    double dist = Geometry.EuclideanDistance(pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w2]=" + dist.ToString() + "\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(dist);
                    this.idxAttribute++;
                }                
                catch (Exception ex)
                {
                    this.lstFingerLimitsA.Add(new Point[2] { new Point(0, 0), new Point(0,0) });
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w2]=0\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(0);
                    this.idxAttribute++;
                }
            }

            this.rtbAttributes.AppendText("--------------------------------------\n");

            foreach (Point[] pLimits in this.lstFingerLimitsA)
            {
                Point pFingerWidthRightLimitA = new Point();
                Point pFingerWidthLeftLimitA = new Point();

                pFingerWidthLeftLimitA.Y = pLimits[0].Y + 20;
                pFingerWidthRightLimitA.Y = pLimits[1].Y + 20;

                int midPoint = (pLimits[0].X + pLimits[1].X) / 2;
                int offset = 0;

                try
                {
                    while (imgHandGeoExplorer.GetPixel(midPoint + offset, pLimits[1].Y + 20).Name == "ffffffff")
                    {
                        offset++;
                    }

                    pFingerWidthRightLimitA.X = midPoint + offset;

                    offset = 1;
                    while (imgHandGeoExplorer.GetPixel(midPoint - offset, pLimits[1].Y + 20).Name == "ffffffff")
                    {
                        offset++;
                    }
                    pFingerWidthLeftLimitA.X = midPoint - offset;

                    this._g.DrawLine(new Pen(Color.Blue, 1), pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                    this.lstFingerLimitsB.Add(new Point[2] { pFingerWidthLeftLimitA, pFingerWidthRightLimitA });
                    double dist = Geometry.EuclideanDistance(pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w3]=" + dist.ToString() + "\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(dist);
                    this.idxAttribute++;
                }
                catch (Exception ex)
                {
                    this.lstFingerLimitsB.Add(new Point[2] { new Point(0, 0), new Point(0, 0) });
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w3]=0\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(0);
                    this.idxAttribute++;
                }
            }

            this.rtbAttributes.AppendText("--------------------------------------\n");

            /*foreach (Point[] pLimits in this.lstFingerLimitsB)
            {
                Point pFingerWidthRightLimitA = new Point();
                Point pFingerWidthLeftLimitA = new Point();

                pFingerWidthLeftLimitA.Y = pLimits[0].Y + 30;
                pFingerWidthRightLimitA.Y = pLimits[1].Y + 30;

                int midPoint = (pLimits[0].X + pLimits[1].X) / 2;
                int offset = 0;
                try
                {
                while (imgHandGeoExplorer.GetPixel(midPoint + offset, pLimits[1].Y + 30).Name == "ffffffff")
                {
                    offset++;
                }

                pFingerWidthRightLimitA.X = midPoint + offset;

               
                offset = 1;
                while (imgHandGeoExplorer.GetPixel(midPoint - offset, pLimits[1].Y + 30).Name == "ffffffff")
                {
                    offset++;
                }
                pFingerWidthLeftLimitA.X = midPoint - offset;

                this._g.DrawLine(new Pen(Color.Blue, 1), pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                this.lstFingerLimitsC.Add(new Point[2] { pFingerWidthLeftLimitA, pFingerWidthRightLimitA });
                double dist = Geometry.EuclideanDistance(pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w4]=" + dist.ToString() + "\n");
                handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(dist);
                this.idxAttribute++;
                }
                catch (Exception ex)
                {
                    this.lstFingerLimitsC.Add(new Point[2] { new Point(0, 0), new Point(0, 0) });
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w4]=0\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(0);
                    this.idxAttribute++;
                }
            }*/

           // this.rtbAttributes.AppendText("--------------------------------------\n");

            /*foreach (Point[] pLimits in this.lstFingerLimitsC)
            {
                Point pFingerWidthRightLimitA = new Point();
                Point pFingerWidthLeftLimitA = new Point();

                pFingerWidthLeftLimitA.Y = pLimits[0].Y + 15;
                pFingerWidthRightLimitA.Y = pLimits[1].Y + 15;

                int midPoint = (pLimits[0].X + pLimits[1].X) / 2;
                try
                {
                int offset = 0;
                while (imgHandGeoExplorer.GetPixel(midPoint + offset, pLimits[1].Y + 15).Name == "ffffffff")
                {
                    offset++;
                }

                pFingerWidthRightLimitA.X = midPoint + offset;

               
                offset = 1;
                while (imgHandGeoExplorer.GetPixel(midPoint - offset, pLimits[1].Y + 15).Name == "ffffffff")
                {
                    offset++;
                }
                pFingerWidthLeftLimitA.X = midPoint - offset;

                this._g.DrawLine(new Pen(Color.Blue, 1), pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                //this.lstFingerLimitsC.Add(new Point[2] { pFingerWidthLeftLimitA, pFingerWidthRightLimitA });
                double dist = Geometry.EuclideanDistance(pFingerWidthLeftLimitA, pFingerWidthRightLimitA);
                this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w5]=" + dist.ToString() + "\n");
                handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(dist);
                this.idxAttribute++;
                }
                catch (Exception ex)
                {
                    this.rtbAttributes.AppendText("Attribute " + this.idxAttribute + ": [w5]=0\n");
                    handGeometryAttributes.FingerWidths[Pool.Instance.ImageName].Add(0);
                    this.idxAttribute++;
                }
            }*/

            this.pbMain.Refresh();
            this.buttonCalcFingerTipsDistance.Enabled = true;
            this.buttonCalcValleysDist.Enabled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataFuser df = new DataFuser(this.handGeometryAttributes);
            string data = df.FuseHandGeometryData();
            OutputterTxt ot = new OutputterTxt();
            //ot.SaveTrainningData(data);
            ot.SaveTestingData(data);
        }

        private void buttonCalcDistanceBetweenValleys_Click(object sender, EventArgs e)
        {
            //order the points by x value coord.: from left to right (ascending).
            var valleyPointsOrdered = from dist in this._hg.ValleyPointsList orderby dist.X ascending select dist;
            List<Point> orderedVallyes = valleyPointsOrdered.ToList();

            for (int i = 0; i < orderedVallyes.Count - 1; i++)
            {
                double dist = Geometry.EuclideanDistance(orderedVallyes[i], orderedVallyes[i + 1]);
                this._g.DrawLine(new Pen(Color.DarkGreen, 1), orderedVallyes[i], orderedVallyes[i + 1]);


                if (!handGeometryAttributes.DistanceBetweenValleys.ContainsKey(Pool.Instance.ImageName))
                {
                    handGeometryAttributes.DistanceBetweenValleys.Add(Pool.Instance.ImageName, new List<double>());
                }
                                
                this.handGeometryAttributes.DistanceBetweenValleys[Pool.Instance.ImageName].Add(dist);
            }
            this.pbMain.Refresh();         
        }            
    }
}
