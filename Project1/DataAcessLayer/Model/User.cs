using Project1.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class User
    {
        protected string account;
        protected string password;
        protected int role;

        public User()
        {

        }

        public User(string account, string password, int role)
        {
            this.account = account;
            this.password = password;
            this.role = role;
        }

        public string Account
        {
            get { return this.account; }
            set { this.account = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value;  }
        }

        public int Role
        {
            get { return this.role; }
            set { this.role = value; }
        }
    }
}
