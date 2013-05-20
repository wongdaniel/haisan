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
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        private int id;
        private string username;
        private string password;
        private string email;
        private string phone;

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
