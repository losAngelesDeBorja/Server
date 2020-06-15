using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using adm;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Text.RegularExpressions;


namespace adm
{
    class Server
    {
        public const string FileOutputName = @"output-file.txt";
        public Database accessDB;
        public Database dbExample;
        public List<Database> dbList;
        public Table tableAcesss;
        public Table newTable;
        public Server() {
            dbList = new List<Database>();
        }

        
        

        // Create a new user
        static bool MakeNewUser(string userInfo)
        {
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

        // Create a new db example
        static bool MakeNewDataBase(string dbInfo, string dbUser, List<Database> l)
        {
            bool uniqueDataBasename = true;
            Console.WriteLine("dbInfo: "+ dbInfo);
            Console.WriteLine("dbUser: "+ dbUser);

            foreach (Database d in l)
            {
                if (d.dbName == dbInfo)
                {
                    uniqueDataBasename = false;
                }
            }

            // Check if the DB does not exists
           
            // When db name exists, returns false
            if (!uniqueDataBasename)
            {
                return false;
            }
            // If it's true and does not exists, add db
            
            return true;
        }

        // Create a new user
        static bool showAllTables(string dbInfo, string dbUser)
        {
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
        //Show all databases
        string showAllDatabases()
        {
            Console.WriteLine("showAllDatabases method ");
            String databaseListNames = "#.*;#";
            if (dbList.Count>0) {
                foreach (Database db in dbList)
                {
                    databaseListNames = databaseListNames + db.dbName + "#.*;#";
                }
            }
            return databaseListNames;
        }


        // Validation of user info
        static bool ValidateUser(string userInfo, Database dbAccess)
        {
            bool validUsername = false;
            bool validPassword = false;
            bool accessGranted = false;
            String[] splitedUserInfo = new String[3];
            string[] separator = new string[] { "#.*;#" };
            splitedUserInfo = userInfo.Split(separator, StringSplitOptions.None);

            Console.WriteLine("\nAn user tries to logging from Client with user name: " + splitedUserInfo[0]);
            string nameUser = splitedUserInfo[0];
            string passPass = splitedUserInfo[1];

            // Reads line by line checking user info
            for (int i = 0; i < dbAccess.listTable[0].dataTableStorage.Rows.Count; i++)
            {
                if ((dbAccess.listTable[0].dataTableStorage.Rows[i]["USER"].ToString() == nameUser) && (dbAccess.listTable[0].dataTableStorage.Rows[i]["PASS"].ToString() == passPass))
                {
                    accessGranted = true;
                    Console.WriteLine("Access granted");
                    break;
                }
            }
            if (accessGranted == false)
            {
                Console.WriteLine("Access denied. The user"+ nameUser + " or password "+ passPass + " are incorrect. Please try again");
            }
            // When returns false, user/password were incorrect
            if (!accessGranted)
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

        public Database initializeDbEngine() {

            Console.WriteLine("Starting...");

            //Initilize the first database
            string dbName, dbNameUser, dbPassUser;
            string message;
            dbName = "access";
            dbNameUser = "admin";
            dbPassUser = "admin";
            accessDB = new Database();
            //Create database
            message = accessDB.createDatabase(dbName, dbNameUser, dbPassUser);

            //Initilize the first Table
            List<string> listType = new List<string>() { DataType.INT.ToString(), DataType.STRING.ToString(), DataType.INT.ToString(), DataType.STRING.ToString() };
            List<string> listNames = new List<string>() { "ID", "USER", "PASS", "PRIVILEDGE" };
            tableAcesss = new Table("ACCESS", listNames.Count, listNames, listType);

            tableAcesss.addField("ID", DataType.INT);
            tableAcesss.addField("USER", DataType.STRING);
            tableAcesss.addField("PASS", DataType.INT);
            tableAcesss.addField("PRIVILEDGE", DataType.STRING);
            
            //Insert tuples
            List<string> insertTupleList = new List<string>();
            insertTupleList.Add("1");
            insertTupleList.Add("admin");
            insertTupleList.Add("admin");
            insertTupleList.Add("1");
            tableAcesss.addTupleToTable(insertTupleList);
            
            //Add first Table to first DataBase
            accessDB.addTable(tableAcesss,"ACCESS");

            //Security
            Security security = new Security();

            //Create security profile
            security.createSecurityProfile("admin");
            
            //Add user to security profile
            security.addUser(dbNameUser, dbPassUser, "admin");

            //Request user and password
            string userLogin = "";
            string passwordLogin = "";
            Boolean accessGranted = false;
            Console.WriteLine("#######################");
            Console.WriteLine("# Credentials request #");
            Console.WriteLine("#######################");
            while (accessGranted == false)
            {
                //Request user
                Console.WriteLine("User:");
                userLogin = Console.ReadLine();
                //Request password
                Console.WriteLine("Password:");
                passwordLogin = Security.GetHiddenConsoleInput();
                

                //Getting the Table of Users. In order to protect from SQL Injection the process of search USER NAME is here instead using WHERE clause
                Table result=accessDB.executeSQLByCommandReturnResult("SELECT USER,PASS FROM ACCESS", tableAcesss);

                for(int i=0; i< result.dataTableStorage.Rows.Count;i++)
                {
                    if ((result.dataTableStorage.Rows[i]["USER"].ToString() == userLogin)&&(result.dataTableStorage.Rows[i]["PASS"].ToString()== passwordLogin))
                    {
                        accessGranted = true;
                        Console.WriteLine("Access granted");
                        break;
                    }
                }
                if (accessGranted == false)
                {
                    Console.WriteLine("Access denied. The user or password are incorrect. Please try again");
                }
            }

            return accessDB;
        }
        
        public void initializeExampleDb()
        {
            //SOME FAKE DATA INIT
            string dbName, dbNameUser, dbPassUser;
            string message;
            dbName = "agenda";
            dbNameUser = "user";
            dbPassUser = "user";
            dbExample = new Database(dbName, dbNameUser, dbPassUser);

            Security security = new Security();
            security.createSecurityProfile("ControlOfUsers");
            security.addUser(dbNameUser, dbPassUser, PrivilegeType.INSERT.ToString());

            //Create database
            message = dbExample.createDatabase(dbName, dbNameUser, dbPassUser);

            //Create new Table
            List<string> listType = new List<string>() { DataType.INT.ToString(), DataType.STRING.ToString(), DataType.INT.ToString(), DataType.STRING.ToString() };
            List<string> listNames = new List<string>() { "ID", "NAME", "AGE", "ADDRESS" };
            newTable = new Table("agenda", listNames.Count, listNames, listType);


            newTable.addField("ID", DataType.INT);
            newTable.addField("NAME", DataType.STRING);
            newTable.addField("AGE", DataType.INT);
            newTable.addField("ADDRESS", DataType.STRING);

            //Add Table to the Database
            dbExample.addTable(newTable, "db1");
            dbList.Add(dbExample);

            //Insert tuples
            List<string> insertTupleList = new List<string>();
            insertTupleList.Add("1");
            insertTupleList.Add("JOHN");
            insertTupleList.Add("19");
            insertTupleList.Add("Street Name Street Example n 1");
            newTable.addTupleToTable(insertTupleList);

            insertTupleList = new List<string>();
            insertTupleList.Add("2");
            insertTupleList.Add("Kathy");
            insertTupleList.Add("91");
            insertTupleList.Add("Street Name Street Example n 1");
            newTable.addTupleToTable(insertTupleList);
            //SOME FAKE DATA END


            
        }

        public void processTxtFile()
        {
            //INPUT-OUTPUT FILE INIT
            try
            {

                //Creation of file 
                string fileName = FileOutputName;
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Created output file. " + DateTime.Now);
                    System.Console.WriteLine("::::::::CREATED TEXT FILE output::::::::");
                }


                int counter = 0;
                string line;

                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(@"input-file.txt");
                System.Console.WriteLine();
                System.Console.WriteLine("::::::::READING FROM FILE::::::::");
                System.Console.WriteLine();

                //Limitators of read lines
                int counterReadLine = 0;
                int counterLimitReadLine = 6;
                //Limitator of time
                var watch = Stopwatch.StartNew();
                while (((line = file.ReadLine()) != null) && (counterReadLine < counterLimitReadLine))
                {
                    System.Console.WriteLine("Reading line " + counter + " from file: ");
                    //Executing sqls as Tasks 
                    using (var task = Task.Delay(1000))
                    {
                        dbExample.executeSQLByCommand(line, newTable);
                        task.Wait();
                    }
                    watch.Stop();

                    System.Console.WriteLine();
                    counterReadLine++;
                    counter++;

                    watch.Start();
                }
                file.Close();

            }
            catch
            {


            }
            //INPUT-OUTPUT FILE END



        }
        
        

        private static void Main()
        {
            TcpListener server = null;
            try
            {

                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                // Set the TcpListener on port 8001.
                server = new TcpListener(localAddr, 8001);


                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = "";
                

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();


                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();



            /*
                        
                    
            
            Console.Clear();
            Server server = new Server();
            Database dbInit=server.initializeDbEngine();

            // IP and PORT declarations.
            IPAddress localIP = IPAddress.Parse("127.0.0.1"); // Server IP here 127.0.0.1
            TcpListener listener = new TcpListener(IPAddress.Any, 1111); // Number of PORT 1111 here
            Console.WriteLine("Server engine inizialized...");
            Console.WriteLine("Waiting for client communication...");

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
                    if (ValidateUser(dataRecieved.Substring(0, dataRecieved.Length - 10), dbInit))
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

                // Code to create new database
                if (dataRecieved.Substring(dataRecieved.Length - 21, 21) == "createDataBaseExample")
                {
                    String[] splitedDataRecieved = new String[3];
                    string[] separator = new string[] { "#.*;#" };
                    splitedDataRecieved = dataRecieved.Split(separator, StringSplitOptions.None);
                    Console.WriteLine("\nAn user tries to make a new database named: "+ splitedDataRecieved[0]);
                    string nameDb = splitedDataRecieved[0];
                    string userDb = splitedDataRecieved[1];
                  
                    if (MakeNewDataBase(nameDb, userDb,server.dbList))
                    { 
                        // New database process OK
                        server.initializeExampleDb();
                        Console.WriteLine("Success creating new data base...");
                        buffer = ASCIIEncoding.ASCII.GetBytes("createdDataBase");   
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    { 
                        // New user process KO
                        Console.WriteLine("Failed creating new database...");
                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                 }

                

                // Code to process the TXT with SQL all databases
                if (dataRecieved.Substring(dataRecieved.Length - 10, 10) == "processSQL")
                {
                    Console.WriteLine("\nAn user tried to process a TXT file with SQLs: " + dataRecieved.Substring(0, dataRecieved.IndexOf("#.*;#")));
                    if (true)
                    {
                        server.processTxtFile();
                        // show all databases process OK
                        Console.WriteLine("Success processing TXT file...");
                        buffer = ASCIIEncoding.ASCII.GetBytes("processSQLOK");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        // New user process KO
                        Console.WriteLine("Failed getting all database...");
                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                }

                // Code to show all database
                if (dataRecieved.Substring(dataRecieved.Length - 13, 13) == "showDatabases")
                {
                    Console.WriteLine("\nAn user tried to get list of all databases: " + dataRecieved.Substring(0, dataRecieved.IndexOf("#.*;#")));
                    if (true)
                    {
                        // show all databases process OK
                        Console.WriteLine("Success getting all database...");
                        buffer = ASCIIEncoding.ASCII.GetBytes("createdDataBase");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        // New user process KO
                        Console.WriteLine("Failed getting all database...");
                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        netStream.Write(buffer, 0, buffer.Length);
                    }
                }
                dataRecieved = "";
                // End of transmission
                client.Close();
                listener.Stop();
            }
        
            */

        }






    }
}

