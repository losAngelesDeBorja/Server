using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;

namespace adm
{
     public class Security
    {
        private static List<string> securityProfiles = new List<string>();
        //private static List<string> tablaPrivilegios = new List<string>();


        public Security()
        {

        }

        //


        //security_profilem

        public void createSecurityProfile(String nombre)
        {
            
            //CREATE SECURITY PROFILE security_profile;
            securityProfiles.Add(nombre);  //Admin    User  

        }

        public void dropSecurityProfile(String nombre)
        {

            //DROP SECURITY PROFILE security_profil
            securityProfiles.Remove(nombre);

        }
        
        public Boolean grant(PrivilegeType privilege_type, Table table, String security_profile)
        {
            //GRANT privilege_type ON table TO security_profile;
            /* 
            
             Table, Security_profile, privilege_type

             Clientes, DELETE, Admin
             Empresa, INSERT, User
             Empresa, DELETE, Admin
             Empleado, SELECT, User 
             Departamentos, UPDATE, Admin 
  
             */
            //tablaPrivilegios.Add(privilege_type.ToString + table.ToString + security_profile);


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

        public static string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }
    }
}
