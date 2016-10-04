using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class Gap
    {
        private int _start;
        private int _step;
        private int _limit;
        private Point _middleCoordinates;

        public Gap(int start, int step, int limit, Point middleCoords)
        {
            this._start = start;
            this._step = step;
            this._limit = limit;
            this._middleCoordinates = middleCoords;
        }

        public int Start
        {
            get
            {
                return this._start;
            }
            set
            {
                this._start = value;
            }
        }

        public int Step
        {
            get
            {
                return this._step;
            }
            set
            {
                this._step = value;
            }
        }

        public int Limit
        {
            get
            {
                return this._limit;
            }
            set
            {
                this._limit = value;
            }
        }

        public Point MiddleCoordinates
        {
            get
            {
                return this._middleCoordinates;
            }
            set
            {
                this._middleCoordinates = value;
            }
        }


    }
}
