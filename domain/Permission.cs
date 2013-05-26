using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Permission
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private short query;

        public short Query
        {
            get { return query; }
            set { query = value; }
        }
        private short add;

        public short Add
        {
            get { return add; }
            set { add = value; }
        }
        private short modify;

        public short Modify
        {
            get { return modify; }
            set { modify = value; }
        }
        private short delete;

        public short Delete
        {
            get { return delete; }
            set { delete = value; }
        }
        private short print;

        public short Print
        {
            get { return print; }
            set { print = value; }
        }
        private short run;

        public short Run
        {
            get { return run; }
            set { run = value; }
        }
        private short saveTable;

        public short SaveTable
        {
            get { return saveTable; }
            set { saveTable = value; }
        }
        private short check;

        public short Check
        {
            get { return check; }
            set { check = value; }
        }
        private short anticheck;

        public short Anticheck
        {
            get { return anticheck; }
            set { anticheck = value; }
        }

        public override string ToString()
        {
            return "id:["+id+"] query:["+query+"] add:["+add+"] modify:["+modify+"]";
        }
    }
}
