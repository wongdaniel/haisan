using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    public class Base
    {
        protected int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        protected string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Base()
        {
        }

        public Base(int id)
        {
            this.id = id;
        }

        public Base(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
