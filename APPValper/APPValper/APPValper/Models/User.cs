using System;
using System.Collections.Generic;
using System.Text;

namespace APPValper.Models
{
    class User
    {

        public User(String name, String email, String password, bool logged, bool admin)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.logged = logged;
            this.admin = admin;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private bool logged;

        public bool Logged
        {
            get { return logged; }
            set { logged = value; }
        }

        private bool admin;

        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        private static List<User> users = new List<User>();

        public static List<User> Users
        {
            get { return users; }
            set { users = value; }
        }


    }
}
