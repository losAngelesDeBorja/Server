using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adm
{
     public class Security
    {

        public Security()
        {

        }

        //security_profilem

        public void createSecurityProfile()
        {
            //CREATE SECURITY PROFILE security_profile;

        }
        public void dropSecurityProfile()
        {
            //DROP SECURITY PROFILE security_profile;


        }
        public Boolean grant(PrivilegeType privilege_type, Table table, String security_profile)
        {
            //GRANT privilege_type ON table TO security_profile;

            return false;
        }
        public Boolean revoke(PrivilegeType privilege_type, Table table, String security_profile)
        {
            //REVOKE privilege_type ON table TO security_profile;


            return false;
        }
        public void addUser(String user, String password, String security_profile)
        {
            //ADD USER(user, password, security_profile);


        }
        public void deleteUser(String user)
        {
            //DELETE USERuser;


        }


    }
}
