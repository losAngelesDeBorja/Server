using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace adm
{
    class Server
    {
        // Create a new user
        static bool MakeNewUser(string userInfo)
        {//
            bool uniqueUsername = true;

            // Check if the bew user does not exists
            foreach (string curLine in File.ReadAllLines("user_database.txt"))
            {
                if (userInfo.Substring(0, userInfo.IndexOf("#.*;#")) == curLine.Substring(0, curLine.IndexOf("#.*;#")))
                {
                    uniqueUsername = false;
                }
            }
            // When user name exists, returns false
            if (!uniqueUsername)
            {
                return false;
            }

            // If it's true and does not exists, add user to list in txt file
            File.AppendAllText("user_database.txt", userInfo + "\n");
            return true;
        }

        // Create a new user
        static bool MakeNewDataBase(string dbInfo, string dbUser)
        {//
            bool uniqueDataBasename = true;

            // Check if the bew user does not exists
            foreach (string curLine in File.ReadAllLines("name_database.txt"))
            {
                if (dbInfo.Substring(0, dbInfo.IndexOf("#.*;#")) == curLine.Substring(0, curLine.IndexOf("#.*;#")))
                {
                    uniqueDataBasename = false;
                }
            }
            // When user name exists, returns false
            if (!uniqueDataBasename)
            {
                return false;
            }

            // If it's true and does not exists, add user to list in txt file
            File.AppendAllText("name_database.txt", dbInfo + "\n");
            return true;
        }

        // Validation of user info
        static bool ValidateUser(string userInfo)
        {
            bool validUsername = false;
            bool validPassword = false;

            // Reads line by line the file with user info
            foreach (string curLine in File.ReadAllLines("user_database.txt"))
            {
                validPassword = false;
                validUsername = false;

                // When username is valid on current line validUsername = true
                if (userInfo.Substring(0, userInfo.IndexOf("#.*;#")) == curLine.Substring(0, curLine.IndexOf("#.*;#")))
                {
                    validUsername = true;
                }

                // When password is valid on current line validPassword = true
                if (userInfo.Substring(userInfo.IndexOf("#.*;#"), userInfo.Length - userInfo.IndexOf("#.*;#")) == curLine.Substring(curLine.IndexOf("#.*;#"), curLine.Length - curLine.IndexOf("#.*;#")))
                {
                    validPassword = true;
                }

                // When user and pass match with user introduced, stop checking
                if (validPassword && validUsername)
                {
                    break;
                }
            }

            // When returns false, user/password were incorrect
            if (!validPassword || !validUsername)
            {
                return false;
            }
            // When successful login retrieves true
            return true;

           

        }


        // Validation of user info
        static bool ValidateDBName(string dbNameInfo)
        {
            bool validDBname = false;
 

            // Reads line by line the file with user info
            foreach (string curLine in File.ReadAllLines("name_database.txt"))
            {
                validDBname = false;

                // When name database is valid on current line validDBname = true
                if (dbNameInfo.Substring(0, dbNameInfo.IndexOf("#.*;#")) == curLine.Substring(0, curLine.IndexOf("#.*;#")))
                {
                    validDBname = true;
                }
            }

            // When returns false, user/password were incorrect
            if (!validDBname)
            {
                return false;
            }
            // When successful login retrieves true
            return true;



        }



        static bool CreateNewDatabase(string dbInfo) { return false; }
        static bool DeleteNewDatabase(string dbInfo) { return false; }

        static void Main()
        {
            //creation of table into database test
            Database newDB = new Database("miDB","user","user");
            //The SQL senetence will arrive from client when the programming finnished.
            // Until the SQL request is written into a text file (XML format?), it is introduced as string here: 
            string sql = "CREATE TABLE Persons (PersonID int,LastName varchar(255),FirstName varchar(255),Address varchar(255),City varchar(255) );";
            newDB.createDatabaseByText(sql); 

            




            // When file doesn't exist, create it
            if (!File.Exists("user_database.txt"))
            {
                Console.WriteLine("File not found... Creating it...");
                File.Create("user_database.txt");
            }

            // When file doesn't exist, create it
            if (!File.Exists("name_database.txt"))
            {
                Console.WriteLine("File not found... Creating it...");
                File.Create("name_database.txt");
            }

            // IP and PORT declarations.
            IPAddress localIP = IPAddress.Parse("127.0.0.1"); // Server IP here 127.0.0.1
            TcpListener listener = new TcpListener(IPAddress.Any, 1111); // Number of PORT 1111 here

            Console.WriteLine("Server engine started...");

            // Main
            while (true)
            {
                // Starts listener
                listener.Start();
                TcpClient client = listener.AcceptTcpClient();

                // Opens network stream
                NetworkStream netStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                // Reads buffer and converts to string
                int bytesRead = netStream.Read(buffer, 0, client.ReceiveBufferSize);
                string dataRecieved = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // If the user wants to create a new account, do the next
                if (dataRecieved.Substring(dataRecieved.Length - 4, 4) == "True")
                {
                    Console.WriteLine("\nUser: {0}, tried to make a new account...", dataRecieved.Substring(0, dataRecieved.IndexOf("#.*;#")));
                    if (MakeNewUser(dataRecieved.Substring(0, dataRecieved.Length - 9)))
                    { // New user process OK
                        Console.WriteLine("Success...");

                        buffer = ASCIIEncoding.ASCII.GetBytes("True");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    { // New user process OK
                        Console.WriteLine("Failed...");

                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                }

                // Code for user log in
                if (dataRecieved.Substring(dataRecieved.Length - 5, 5) == "False")
                {
                    Console.WriteLine("\nUser: {0}, tried to login...", dataRecieved.Substring(0, dataRecieved.IndexOf("#.*;#")));
                    if (ValidateUser(dataRecieved.Substring(0, dataRecieved.Length - 10)))
                    { // Login ok
                        Console.WriteLine("Success...");

                        buffer = ASCIIEncoding.ASCII.GetBytes("True");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    { // Login KO
                        Console.WriteLine("Failed...");

                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                }

                // Code for create new database
                if (dataRecieved.Substring(dataRecieved.Length - 14, 14) == "createDataBase")
                {
         
                    Console.WriteLine("\nAn user tried to make a new database named: "+ dataRecieved.Substring(0, dataRecieved.IndexOf("#.*;#")));
                    if (MakeNewDataBase(dataRecieved.Substring(0, dataRecieved.Length - 19), dataRecieved.Substring(1, dataRecieved.IndexOf("#.*;#"))))
                    { // New database process OK
                        Console.WriteLine("Success creatting new data base...");

                        buffer = ASCIIEncoding.ASCII.GetBytes("createdDataBase");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    { // New user process KO
                        Console.WriteLine("Failed creating new database...");

                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                 }



                dataRecieved = "";


                // End of transmission
                client.Close();
                listener.Stop();


  
            }


        }
    
    }
}

