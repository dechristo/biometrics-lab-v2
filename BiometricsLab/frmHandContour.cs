using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Algorithms;
using FactoryMethod;
using Image.Filters;
using Image.Pool;


namespace BiometricsLab
{
    public partial class frmHandContour : Form
    {
        #region Private Members

        private Bitmap contourExtracted = null;        
        private Bitmap imgThinning = null;        
        private HandGeometry handGeometry = null;       
        private HandContour hc = new HandContour();
        private Graphics graphicsControl = null;
        private Point pCentroid = Point.Empty;

        #endregion

        #region Constructor

        public frmHandContour(Bitmap imgEdgeDetected)
        {
            InitializeComponent();            
            this.ExtractHandContour(imgEdgeDetected);            
            this.graphicsControl = Graphics.FromImage(this.contourExtracted);
            this.handGeometry = new HandGeometry(this.contourExtracted, ref this.graphicsControl);
            this.handGeometry.CalculatePixelCount();
            this.lblPixelCount.Text = this.handGeometry.PixelCount + " (" + Math.Round((double)(this.handGeometry.PixelCount * 100) / (this.contourExtracted.Width * this.contourExtracted.Height), 2) + "%)";
        }

        #endregion

        # region Private Methods

        private void ExtractHandContour(Bitmap imgEdgeDetected)
        {
            Bitmap imgHandContour = this.hc.ExtractHandContour(imgEdgeDetected);
            imgHandContour = this.hc.Invert();
            this.contourExtracted = new Bitmap(imgHandContour);
            this.pbMain.Image = this.contourExtracted;
            this.pbMain.Refresh();
            Pool.Instance.HandContour = this.contourExtracted;
        }

        #endregion

        #region Private Events

        private void btnThinning_Click(object sender, EventArgs e)
        {
            Thinning th = new Thinning();
            this.imgThinning = th.ApplyFilter(this.contourExtracted);
            this.pbMain.Image = this.imgThinning;
            this.pbMain.Refresh();
        }

        //TODO: USE threads!!!!
        private void pbMain_Click(object sender, EventArgs e)
        {
            if (this.pCentroid != Point.Empty)
                return;

            this.pCentroid = this.handGeometry.FindCentroid();
            //this.graphicsControl.DrawEllipse(new Pen(Color.SpringGreen, 2), centroid.X, centroid.Y + 100, 10, 10); //+100 for nomalization
            this.graphicsControl.FillEllipse(new SolidBrush(Color.SpringGreen), this.pCentroid.X, this.pCentroid.Y, 10, 10); //+100 for nomalization
                       
            //put this on a thread!!
           //this.pbMain.Image = this.contourExtracted;
            //this.pbMain.Refresh();

            //put this on a thread!!
            this.contourExtracted = this.handGeometry.ProccessHandGeometry();
            this.pbMain.Image = this.contourExtracted;
            this.pbMain.Refresh();
            Pool.Instance.HandGeometryAttributes = this.contourExtracted;
            this.butttonExplore.Enabled = true;
            this.buttonExplore_Click(sender, e); //later comment this line and uncomment at on paint
                  
        }          

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblCoords.Text = "x=" + e.X * 2 + "," + "y=" + e.Y * 2;
        }       

        private void frmHandContour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
                pbMain_Click(sender, e);
        }

        private void frmHandContour_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.pbMain_Click(sender, e);    
        }

        #endregion

        private void buttonExplore_Click(object sender, EventArgs e)
        {
            frmExploreHandGeometry frmEHG = new frmExploreHandGeometry(this.handGeometry);            
            frmEHG.Show();
            frmEHG.BringToFront();
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
           //this.buttonExplore_Click(sender, e); 
        }       
    }
}
