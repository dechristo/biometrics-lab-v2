using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Algorithms;
using BiometricData;
using Utils;

namespace Images
{
    public class ImageProcessorMediator : IImageProcessorMediator
    {
        #region Memebers

        private ImageProcessor _imgProc = null;
        private Bitmap _processedImg = null;
        private float[] _fHandVeinsData = null;

        #endregion

        #region Constructor

        public ImageProcessorMediator(System.Drawing.Image img, string fileLabel, string type)
        {
            _imgProc = new ImageProcessor(img, fileLabel, type);
        }

        #endregion

        #region Public methods

        public Bitmap ProcessImage(string type)
        {
            this.Proccess(type);
            return this._processedImg;
        }

        public void SaveUserData(string dataType)
        {
            OutputterTxt ot = new OutputterTxt();
            string userData = "";

            try
            {
                DataFuser df = new DataFuser(UserData.Instance);

                userData = df.FuseData(dataType);
                ot.SaveData(dataType, userData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        #endregion

        #region Private methods

        private string Proccess(string dataType)
        {
            try
            {
                this.ProcessGeometryFeatures();
                this.ProcessTextureFeatures();
                this.UpdateUserData(dataType);
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ProcessGeometryFeatures()
        {
            this._processedImg = _imgProc.ApplyPrimaryFilters();
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
        }

        private void ProcessTextureFeatures()
        {
            _fHandVeinsData = _imgProc.ExtractHandPalmVeinsData();
        }              

        private void UpdateTrainningUserData()
        {
            UserData.Instance.AddTranning(_imgProc.FileLabel, _imgProc.HandGeometryData.HandGeometryFeatures, _fHandVeinsData);

            if (UserData.Instance.UserAttributesTrainning[_imgProc.FileLabel].Count > 1)
                UserData.Instance.NormalizeHandGeometryData(_imgProc.FileLabel, DataType.Trainning);                        
        }

        private void UpdateTestingUserData()
        {
            UserData.Instance.AddTesting(_imgProc.FileLabel, _imgProc.HandGeometryData.HandGeometryFeatures, _fHandVeinsData);
            UserData.Instance.NormalizeHandGeometryData(_imgProc.FileLabel, DataType.Testing);             
        }

        private void UpdateUserData(string dataType)
        {
            if(dataType.Equals(DataType.Trainning))
                this.UpdateTrainningUserData();
            else
                this.UpdateTestingUserData();          
        }

        #endregion
    }

    /*
     * TODO > separate drawing and data acquisition (handGeometry and images)
     * 
     */
}