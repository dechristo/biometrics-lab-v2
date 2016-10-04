using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BiometricData;

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

        public String FuseTranningData()
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
                    
                    foreach (double feature in lstFeatures)
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

        public String FuseTestingData()
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

                    foreach (double feature in lstFeatures)
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

    }
}
