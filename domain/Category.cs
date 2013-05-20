using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Category
    {
        private int id;
        private string name;
        private Category parent;

        public static int NAME_SIZE = 20;

        public Category()
        {
            id = 0;
            name = "";
            parent = null;
        }

        public Category(int id)
        {
            this.id = id;
        }

        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Category(int id, string name, Category parent)
        {
            this.id = id;
            this.name = name;
            this.parent = parent;
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

        public Category Parent
        {
            get { return parent; }
            set { parent = value; }
        }


    }
}
