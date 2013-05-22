using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace haisan.domain
{
    public class User
    {
        public User()
        {
            username = "";
            password = "";
            isOnline = false;
            isLock = false;
        }

        public User(int id)
        {
            username = "";
            password = "";
            isOnline = false;
            isLock = false;
            this.id = id;
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            isOnline = false;
            isLock = false;
        }

        private int id;
        private string username;
        private string password;
        private string name;
        private string email;
        private string phone;
        private Group group;

        internal Group Group
        {
            get { return group; }
            set { group = value; }
        }
        private string description;
        private bool isLock;
        private bool isOnline;

        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool IsLock
        {
            get { return isLock; }
            set { isLock = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            set
            {
                username = value;
            }
            get
            {
                return username;
            }
        }

        public string Password
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }

        public string Email
        {
            set
            {
                email = value;
            }
            get
            {
                return email;
            }
        }

        public string Phone
        {
            set
            {
                phone = value;
            }
            get
            {
                return phone;
            }
        }

    }
}
