using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Group
    {
        private int id;
        private string name;
        private Dictionary<Module, Permission> permissions = new Dictionary<Module, Permission>();

        internal Dictionary<Module, Permission> Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }

        public Group()
        {
            name = "";
        }

        public Group(int id)
        {
            this.id = id;
            name = "";
        }

        public Group(string name)
        {
            this.name = name;
        }

        public Group(int id, string name)
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

        public override string ToString()
        {
            return name;
        }
    }
}
