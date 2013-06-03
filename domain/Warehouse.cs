using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    public class Warehouse
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private bool isLocked;

        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        public Warehouse()
        {

        }
        
        public Warehouse(int id)
        {
            this.id = id;
        }
    }
}
