using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    public class Department : Base
    {
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Department()
        {
        }

        public Department(int id) : base(id)
        {
            
        }

        public Department(int id, string name): base(id, name)
        {

        }

        public Department(int id, string name, string description)
            : base(id, name)
        {
            this.description = description;
        }

    }
}
