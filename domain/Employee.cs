using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class Employee : Base
    {
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private bool sex;

        public bool Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        private DateTime birthday;

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        private string idCard;

        public string IdCard
        {
            get { return idCard; }
            set { idCard = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private DateTime joinDate;

        public DateTime JoinDate
        {
            get { return joinDate; }
            set { joinDate = value; }
        }
        private DateTime contractDate;

        public DateTime ContractDate
        {
            get { return contractDate; }
            set { contractDate = value; }
        }
        private Department department;

        internal Department Department
        {
            get { return department; }
            set { department = value; }
        }
        private string position;

        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private bool isLocked;

        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }


        public Employee()
            : base()
        {
        }

        public Employee(int id)
            : base(id)
        {
        }

        public Employee(int id, string name)
            : base(id, name)
        {
        }


    }
}
