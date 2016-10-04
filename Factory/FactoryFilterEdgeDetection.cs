﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Image.Filters;

namespace FactoryMethod
{
    public class FactoryFilterEdgeDetection : FactoryFilters
    {
        public override Filter Create()
        {
            return new EdgeDetection();
        }
    }
}
