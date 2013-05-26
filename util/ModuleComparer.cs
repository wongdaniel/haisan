using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;

namespace haisan.util
{
    class ModuleComparer : IEqualityComparer<Module>
    {
        public bool Equals(Module x, Module y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(Module module)
        {
            return 0;
        }
    }
}
