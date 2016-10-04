using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class HandGeometryUserAttributes
    {
        private string sLabel; 
        private Dictionary<string, List<double>> lstFingerWidths = new Dictionary<string, List<double>>();
        private Dictionary<string, List<double>> lstDistanceToFingerTips = new Dictionary<string, List<double>>(); //distance from centroid
        private Dictionary<string, List<double>> lstDistanceToValleys = new Dictionary<string, List<double>>();    //distance from centroid
        private Dictionary<string, List<double>> lstBetweenValleys = new Dictionary<string, List<double>>();
        private int iHandPerimeter = 0; 

         public HandGeometryUserAttributes()
         {
             this.sLabel = "";
         }

         public Dictionary<string, List<double>> FingerWidths
         {
             get
             {
                 return this.lstFingerWidths;
             }
             set
             {
                 this.lstFingerWidths = value;
             }
         }

         public Dictionary<string, List<double>> DistanceToFingerTips
         {
             get
             {
                 return this.lstDistanceToFingerTips;
             }
             set
             {
                 this.lstDistanceToFingerTips = value;
             }
         }

         public Dictionary<string, List<double>> DistanceToValleys
         {
             get
             {
                 return this.lstDistanceToValleys;
             }
             set
             {
                 this.lstDistanceToValleys = value;
             }
         }

         public Dictionary<string, List<double>> DistanceBetweenValleys
         {
             get
             {
                 return this.lstBetweenValleys;
             }
             set
             {
                 this.lstBetweenValleys = value;
             }
         }

         public string Label
         {
             get
             {
                 return this.sLabel;
             }
             set
             {
                 this.sLabel = value;
             }
         }

         public int HandPerimeter
         {
             get
             {
                 return this.iHandPerimeter;
             }
             set
             {
                 this.iHandPerimeter = value;
             }
         }

         public void Save()
         {
             //Data.UserAttributes.Instance.Add();
         }
    }
}
