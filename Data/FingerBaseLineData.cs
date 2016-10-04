using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiometricData
{
    public class FingerBaseLineData
    {
        private int _middleX;
        private int _middleY;
        private int _inclination;

        public FingerBaseLineData(int mX, int mY, int inclination)
        {
            this._middleX = mX;
            this._middleY = mY;
            this._inclination = inclination;
        }

        public FingerBaseLineData()
        {

        }

        public int MiddleX
        {
            get
            {
                return this._middleX;
            }
            set
            {
                this._middleX = value;
            }
        }

        public int MiddleY
        {
            get
            {
                return this._middleY;
            }
            set
            {
                this._middleY = value;
            }
        }

        public int Inclination
        {
            get
            {
                return this._inclination;
            }
            set
            {
                this._inclination = value;
            }
        }
    }
}
