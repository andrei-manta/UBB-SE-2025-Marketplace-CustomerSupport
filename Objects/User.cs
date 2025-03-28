using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace_SE.Objects
{
    public class User
    {
        public int id;
        public string username;
        public string password;
        public string token;

        public void SetId(int id)
        {
            this.id = id;
        }

        public User(string username, string password)
        {
            id = -1;
            this.username = username;
            this.password = password;
        }

        public void SetToken(string token)
        {
            this.token = token;
        }
    }
}
