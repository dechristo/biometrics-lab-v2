using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiometricData
{
    public class Normalization
    {
        private double _dPerimeter;
        private double _dPalmWidht;
        private double _dBaseLineFinger1;
        private double _dWidth1Finger1;
        private double _dWidth2Finger1;
        private double _dWidth3Finger1;
        private double _dBaseLineFinger2;
        private double _dWidth1Finger2;
        private double _dWidth2Finger2;
        private double _dWidth3Finger2;
        private double _dHeightFinger1;
        private double _dHeightFinger2;

        public double HandPerimeter
        {
            get
            {
                return _dPerimeter;
            }
            set
            {
                _dPerimeter = value;
            }
        }

        public double PalmWidth
        {
            get
            {
                return _dPalmWidht;
            }
            set
            {
                _dPalmWidht = value;
            }
        }

        public double BaseLineFinger1
        {
            get
            {
                return _dBaseLineFinger1;
            }
            set
            {
                _dBaseLineFinger1 = value;
            }
        }

        public double BaseLineFinger2
        {
            get
            {
                return _dBaseLineFinger2;
            }
            set
            {
                _dBaseLineFinger2 = value;
            }
        }

        public double HeightFinger1
        {
            get
            {
                return _dHeightFinger1;
            }
            set
            {
                _dHeightFinger1 = value;
            }
        }

        public double HeightFinger2
        {
            get
            {
                return _dHeightFinger2;
            }
            set
            {
                _dHeightFinger2 = value;
            }
        }

        public double Finger1Width1
        {
            get
            {
                return this._dWidth1Finger1;
            }
            set
            {
                this._dWidth1Finger1 = value;
            }
        }

        public double Finger1Width2
        {
            get
            {
                return this._dWidth2Finger1;
            }
            set
            {
                this._dWidth2Finger1 = value;
            }
        }

        public double Finger1Width3
        {
            get
            {
                return this._dWidth3Finger1;
            }
            set
            {
                this._dWidth3Finger1 = value;
            }
        }

        public double Finger2Width1
        {
            get
            {
                return this._dWidth1Finger2;
            }
            set
            {
                this._dWidth1Finger2 = value;
            }
        }

        public double Finger2Width2
        {
            get
            {
                return this._dWidth2Finger2;
            }
            set
            {
                this._dWidth2Finger2 = value;
            }
        }

        public double Finger2Width3
        {
            get
            {
                return this._dWidth3Finger2;
            }
            set
            {
                this._dWidth3Finger2 = value;
            }
        }

        public double Finger1Average
        {
            get
            {
                return this.FingerWidthAverage(1);
            }
        }

        public double Finger2Average
        {
            get
            {
                return this.FingerWidthAverage(2);
            }
        }

        public Normalization(double[] data)
        {
            this._dPerimeter = data[0];
            this._dPalmWidht = data[1];
            this._dBaseLineFinger1 = data[2];
            this._dWidth1Finger1 = data[3];
            this._dWidth2Finger1 = data[4];
            this._dWidth3Finger1 = data[5];
            this._dHeightFinger1 = data[6];
            this._dBaseLineFinger2 = data[7];
            this._dWidth1Finger2 = data[8];
            this._dWidth2Finger2 = data[9];
            this._dWidth3Finger2 = data[10];
            this._dHeightFinger2 = data[11];
        }

        public double FingerWidthAverage(int finger)
        {
            if(finger == 1)
                return (this._dWidth1Finger1 + this._dWidth2Finger1 + this._dWidth3Finger1) / 3;

            if (finger == 2)
                return (this._dWidth1Finger2 + this._dWidth2Finger2 + this._dWidth3Finger2) / 3;

            return 0;
        }
    }
}
