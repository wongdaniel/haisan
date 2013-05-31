using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class ProductName : Base
    {
        public ProductName()
            : base()
        {
        }

        public ProductName(int id, string name)
            : base(id, name)
        {
        }
    }
}
