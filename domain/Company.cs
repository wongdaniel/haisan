using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Company
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
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public Company()
        {
        }

        public Company(int id)
        {
            this.id = id;
        }

        public Company(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
