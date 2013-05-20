using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Provider
    {
        private int id;
        private string name;

        public Provider()
        {
            id = 0;
            name = "";
        }

        public Provider(int id)
        {
            this.id = id;
        }

        public Provider(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


    }
}
