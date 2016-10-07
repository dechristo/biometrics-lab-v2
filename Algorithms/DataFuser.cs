using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BiometricData;
using Utils;

namespace Algorithms
{
    public class DataFuser : IDataFuser
    {
        private HandGeometryUserAttributes handGeometryAttributes;
        private UserData userData;
        private StringBuilder _sbAttributes = new StringBuilder();        
        private string _sLabel = "";

        public DataFuser(HandGeometryUserAttributes hgua)
        {
            this.handGeometryAttributes = hgua;
            this.setImageLabel(this.handGeometryAttributes.Label);            
        }

        public DataFuser(UserData ud)
        {
            this.userData = ud;
        }

        public String FuseData(string dataType)
        {
            if(dataType.Equals(DataType.Trainning))
                return this.FuseTranningData();
            
            return this.FuseTestingData();
        }

        private String FuseTranningData()
        {
            if (userData == null)
                return "ERROR: features dictioary is null.";

            _sbAttributes.Clear();
          
            foreach(KeyValuePair<string, List<List<double>>> data in userData.UserAttributesTrainning)
            {              
                foreach (List<double> lstFeatures in data.Value)
                {
                    _sbAttributes.Append(data.Key).Append(" ");
                    int iAttIndex = 1;

                    //List<double> zstandard = new List<double>();
                    //zstandard = this.ApplyZScoreStandardization(lstFeatures);
                    List<double> minMaxNormalized = new List<double>();
                    minMaxNormalized = MinMaxNormalization.ApplyMinMaxNormalization(lstFeatures);

                    foreach (double feature in minMaxNormalized)
                    {
                        string frmtFeature = string.Format("{0:0.0000}", feature);
                        frmtFeature = frmtFeature.Replace(',', '.');

                        _sbAttributes.Append(iAttIndex).Append(":").Append(frmtFeature).Append(" ");
                        iAttIndex++;                      
                    }                    
                    _sbAttributes.Append("\n");
                }             
            }
            return _sbAttributes.ToString();
        }

        private String FuseTestingData()
        {
            if (userData == null)
                return "ERROR: features dictioary is null.";

            _sbAttributes.Clear();

            foreach (KeyValuePair<string, List<List<double>>> data in userData.UserAttributesTesting)
            {
                foreach (List<double> lstFeatures in data.Value)
                {
                    _sbAttributes.Append(data.Key).Append(" ");
                    int iAttIndex = 1;

                    //List<double> zstandard = new List<double>();
                    //zstandard = this.ApplyZScoreStandardization(lstFeatures);
                    List<double> minMaxNormalized = new List<double>();
                    minMaxNormalized = MinMaxNormalization.ApplyMinMaxNormalization(lstFeatures);

                    foreach (double feature in minMaxNormalized)
                    {
                        string frmtFeature = string.Format("{0:0.0000}", feature);
                        frmtFeature = frmtFeature.Replace(',', '.');

                        _sbAttributes.Append(iAttIndex).Append(":").Append(frmtFeature).Append(" ");
                        iAttIndex++;
                    }
                    _sbAttributes.Append("\n");
                }
            }
            return _sbAttributes.ToString();
        }       

        //used on v3. deprecated
     
        private void setImageLabel(string label)
        {
            string[] parts = label.Split('_');
            this._sLabel = parts[0];
        }

        private List<double> ApplyZScoreStandardization(List<double> features)
        {
            List<double> lstZStandard = new List<double>();

            ZScore zScore = new ZScore();
            lstZStandard = zScore.GetStandardScore(features);

            return lstZStandard;
        }

    }
}
