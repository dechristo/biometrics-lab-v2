using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class DataType
    {
        private static readonly string _DATA_TYPE_TRAINNING = "trainning";
        private static readonly string _DATA_TYPE_TESTING = "testing";

        public static string Trainning
        {
            get
            {
                return _DATA_TYPE_TRAINNING;
            }
        }

        public static string Testing
        {
            get
            {
                return _DATA_TYPE_TESTING;
            }
        }
    }
}
