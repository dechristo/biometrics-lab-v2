using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Image.Filters;

namespace FactoryMethod
{
    class FactoryFilterThinning : FactoryFilters
    {
        public override  Filter Create()
        {
            return new Thinning();
        }
    }
}
