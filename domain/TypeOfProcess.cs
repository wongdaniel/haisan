﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    public class TypeOfProcess
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
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public TypeOfProcess()
        {
            name = "";
            unit = "";
        }

        public TypeOfProcess(int id, string name, string unit)
        {
            this.id = id;
            this.name = name;
            this.unit = unit;
        }

        public override bool Equals(Object typeOfProcess)
        {
            TypeOfProcess pro = typeOfProcess as TypeOfProcess;
            if (null == pro)
                return false;

            return this.id == pro.id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
