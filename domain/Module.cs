using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Module
    {
        private int id;
        private string name;
        private string displayName;
        private Module parent;

        public Module()
        {
            name = "";
            parent = null;
        }

        public Module(int id)
        {
            this.id = id;
            name = "";
            parent = null;
        }

        public Module(string name)
        {
            this.name = name;
            parent = null;
        }

        public Module(int id, string name)
        {
            this.id = id;
            this.name = name;
            parent = null;
        }

        public Module(int id, string name, string displayName, Module parent)
        {
            this.id = id;
            this.name = name;
            this.displayName = displayName;
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

        public override string ToString()
        {
            return name;
        }
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        bool Comparer(Module module)
        {
            Console.Write("in comparer\n");
            return name.Equals(module.name);
        }

    }
}
