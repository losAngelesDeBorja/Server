using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace adm
{
    class User
    {
        public string name;
        public string password;
        List privileges;

        public User(string pName, string pPassword)
        {
            pName = name;
            pPassword = password;
        }

        public void setPrivilege(string name, string privilege)
        {

        }

        public Boolean getPrivilege(string name)
        {
            return false;
        }
    }
}
