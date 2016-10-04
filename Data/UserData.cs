using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiometricData
{
    public class UserData
    {
        private Dictionary<string, List<List<double>>> dctUserAttributesTrainning = new Dictionary<string,List<List<double>>>();
        private Dictionary<string, List<List<double>>> dctUserAttributesTesting= new Dictionary<string, List<List<double>>>();
        private static UserData _instance = null;
        private BiometricData.Normalization _normalize = null;

        public Dictionary<string, List<List<double>>> UserAttributesTrainning
        {
            get
            {
                return dctUserAttributesTrainning;
            }
            set
            {
                dctUserAttributesTrainning = value;
            }
        }

        public Dictionary<string, List<List<double>>> UserAttributesTesting
        {
            get
            {
                return dctUserAttributesTesting;
            }
            set
            {
                dctUserAttributesTesting= value;
            }
        }

        private UserData()
        {

        }
        
        public static UserData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserData();
                }
                return _instance;
            }
        }

        public void AddTranning(string key, List<double> dists)
        {
            if (this.dctUserAttributesTrainning.ContainsKey(key))
            {
                dctUserAttributesTrainning[key].Add(dists);
            }
            else
            {
                dctUserAttributesTrainning.Add(key, new List<List<double>>());
                dctUserAttributesTrainning[key].Add(dists);
            }
        }

        public void AddTesting(string key, List<double> dists)
        {
            if (this.dctUserAttributesTesting.ContainsKey(key))
            {
                dctUserAttributesTesting[key].Add(dists);
            }
            else
            {
                dctUserAttributesTesting.Add(key, new List<List<double>>());
                dctUserAttributesTesting[key].Add(dists);
            }
        }

        public void NormalizeData(string key, string type)
        {
            _normalize = new Normalization(_instance.dctUserAttributesTrainning[key][0].ToArray());

            if (type.Equals("train"))
            {
                for (int iListIndex = 1; iListIndex < _instance.dctUserAttributesTrainning[key].Count; iListIndex++)
                {
                    for (int iFeatureIndex = 0; iFeatureIndex < _instance.dctUserAttributesTrainning[key][iListIndex].Count; iFeatureIndex++)
                    {
                        double dFactor = 0;
                        switch (iFeatureIndex)
                        {
                            case 0:
                                dFactor = _normalize.HandPerimeter / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 1:
                                dFactor = _normalize.PalmWidth / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 2:
                                dFactor = _normalize.BaseLineFinger1 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 3:
                                dFactor = _normalize.Finger1Width1 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 4:
                                dFactor = _normalize.Finger1Width2 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 5:
                                dFactor = _normalize.Finger1Width3 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 6:
                                dFactor = _normalize.HeightFinger1 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 7:
                                dFactor = _normalize.BaseLineFinger2 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 8:
                                dFactor = _normalize.Finger2Width1 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 9:
                                dFactor = _normalize.Finger2Width2 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 10:
                                dFactor = _normalize.Finger2Width3 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            case 11:
                                dFactor = _normalize.HeightFinger2 / _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex];
                                break;
                            default:
                                dFactor = 1;
                                break;
                        }
                        dFactor = Math.Round(dFactor, 4);
                        _instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex] = Math.Round(_instance.dctUserAttributesTrainning[key][iListIndex][iFeatureIndex] * dFactor, 4);
                    }
                }
            }

            else
            {
                for (int iFeatureIndex = 0; iFeatureIndex < _instance.dctUserAttributesTesting[key][0].Count; iFeatureIndex++)
                {
                    double dFactor = 0;
                    switch (iFeatureIndex)
                    {
                        case 0:
                            dFactor = _normalize.HandPerimeter / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 1:
                            dFactor = _normalize.PalmWidth / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 2:
                            dFactor = _normalize.BaseLineFinger1 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 3:
                            dFactor = _normalize.Finger1Width1 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 4:
                            dFactor = _normalize.Finger1Width2 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 5:
                            dFactor = _normalize.Finger1Width3 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 6:
                            dFactor = _normalize.HeightFinger1 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 7:
                            dFactor = _normalize.BaseLineFinger2 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 8:
                            dFactor = _normalize.Finger2Width1 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 9:
                            dFactor = _normalize.Finger2Width2 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 10:
                            dFactor = _normalize.Finger2Width3 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        case 11:
                            dFactor = _normalize.HeightFinger2 / _instance.dctUserAttributesTesting[key][0][iFeatureIndex];
                            break;
                        default:
                            dFactor = 1;
                            break;
                    }
                    dFactor = Math.Round(dFactor, 4);
                    _instance.dctUserAttributesTesting[key][0][iFeatureIndex] = Math.Round(_instance.dctUserAttributesTesting[key][0][iFeatureIndex] * dFactor, 4);
                }
                return;
            }
        }
    }
}
