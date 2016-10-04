using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Image.Filters;

namespace FactoryMethod
{
    public abstract class FactoryFilters
    {
        public abstract Filter Create();        
    }
}
