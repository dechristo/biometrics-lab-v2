using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FactoryMethod;
using Image.Filters;
using Image.Pool;

namespace BiometricsLab
{
    public partial class frmThreshold : Form
    {
        #region members

        private Bitmap imgGrey;
        private Bitmap imgThreshold;
        private System.Drawing.Image imgOriginal;
        private int iThresholdValue = 65;

        #endregion

        #region Public Methods

        public frmThreshold(System.Drawing.Image imgOriginal)
        {
            InitializeComponent();
            this.imgOriginal = imgOriginal;
        }

        #endregion

        #region Private Methods

        private void frmThreshold_Load(object sender, EventArgs e)
        {
            //apply the binarization with default value 65
            this.ApplyTresholdFilter(iThresholdValue);
            this.UpdateTrackBar();
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            this.iThresholdValue = this.trackBarThreshold.Value;
            this.ApplyCustomTresholdFilter(this.iThresholdValue);
        }

        private void ApplyCustomTresholdFilter(int thresholdValue)
        {
            FactoryFilterBinarization ffb = new FactoryFilterBinarization();
            Filter binaryFilter = ffb.Create();
            ((Binarization)binaryFilter).Threshold = this.iThresholdValue;
            this.imgThreshold = binaryFilter.ApplyFilter(this.imgGrey);
            this.labelThresholdValue.Text = this.trackBarThreshold.Value.ToString();
            this.UpdateTrackBar();
            this.UpdatePictureBox();
            Pool.Instance.Binary = this.imgThreshold;
        }

        private void ApplyTresholdFilter(int thresholdValue)
        {
            FactoryFilterGrayScale ffgs = new FactoryFilterGrayScale();
            Filter greyImage = ffgs.Create();
            this.imgGrey = greyImage.ApplyFilter(this.imgOriginal);
            Bitmap newBmp = new System.Drawing.Bitmap(imgOriginal);
           

            // create filter
            FactoryFilterBinarization ffb = new FactoryFilterBinarization();
            Filter binarizationOtsu = ffb.Create();
                        
            // apply the filter
            this.imgThreshold = binarizationOtsu.ApplyFilter(this.imgGrey);
                      
            this.UpdatePictureBox();
            Pool.Instance.Binary = this.imgThreshold;
        }

        private void UpdateTrackBar()
        {
            this.trackBarThreshold.Value = this.iThresholdValue;
            this.trackBarThreshold.Refresh();
        }

        private void UpdatePictureBox()
        {
            this.pictureBox1.Image = this.imgThreshold;
            this.pictureBox1.Refresh();
        }

        #endregion
    }
}
