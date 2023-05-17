using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Models
{
    public class Employee : Entity
    {
        private String username;
        private String password;


        public Employee(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public String getUsername()
        {
            return this.username;
        }

        public String getPassword()
        {
            return this.password;
        }

        public void setUsername(String givenUsername)
        {
            this.username = givenUsername;
        }

        public void setPassword(String givenPassword)
        {
            this.password = givenPassword;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
