﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml.Linq;

namespace adm
{
    class Client
    {

        public const string Error = "ERROR: ";
        public const string SecurityIncorrectLogin = Error + "Incorrect login";
        public const string SecurityNotSufficientPrivileges = Error + "Not sufficient privileges";
        public const string SecurityProfileAlreadyExists = Error + "Security profile already exists";
        public const string SecurityUserAlreadyExists = Error + "Security user already exists";
        public const string SecurityProfileDoesNotExist = Error + "Security profile does not exist";
        public const string SecurityUserDoesNotExist = Error + "Security user does not exist";
        public static string userDB = "";


        //send a string to Server and return a confirm
        static String Connect(String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                //Int32 port = 13000;
                TcpClient client = new TcpClient("127.0.0.1", 8001);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();

                return responseData;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();

            return "Error";

        }

        static string GetPassword(string passMsg)
        {
            StringBuilder password = new StringBuilder("");
            bool passwordConfirmed = false;

            ConsoleKeyInfo curKey;

            // Hidding charcaters of password in console. Writting "*"  
            while (!passwordConfirmed)
            {
                Console.Write(passMsg);
                for (int i = 0; i < password.Length; i++) Console.Write("*");

                curKey = Console.ReadKey(true);

                if (curKey.Key.ToString() == "Enter")
                {
                    if (password.Length < 4 || password.Length > 30)
                    {
                        Console.WriteLine("\nPassword must have between 4 and 30 characters long");
                        continue;
                    }
                    passwordConfirmed = true;
                    continue;
                }
                else if (curKey.Key.ToString() == "Backspace" && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                }
                else if (curKey.Key.ToString() != "Backspace")
                {
                    password.Append(curKey.KeyChar);
                }

                Console.Clear();
            }
            return password.ToString();
        }

        // Login 
        static bool Login(bool newUser)
        {
            string username = "";
            string password = "";
            string passwordConfirm = "";

            bool passwordValidated = false;

            // Introduce username
            Console.Write("Enter username: ");
            username = Console.ReadLine();

            // Introduce password two times
            while (!passwordValidated)
            {
                password = GetPassword("Enter password: ");
                Console.WriteLine();
                passwordConfirm = GetPassword("Enter password again: ");

                if (password == passwordConfirm)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nPassword doesn't match...");
                    continue;
                }
            }
            // Once user and password introduced, send them to server to try the login
            Console.WriteLine("\nLogin inputs successful... Sending credentials to server...");


            return SendLoginInfo(username, password, newUser);
        }

        // DataBase 
        static bool database(string task)
        {
            string databaseName = "";


            // Introduce dataBaseName
            Console.Write("The database example will be created. Please press any key to continue...");
            databaseName = Console.ReadLine();
            databaseName = "agenda";

            // Once the database name is created, try the creation in server
            Console.WriteLine("\nnew Database request has been created... Sending request to server...");
            return SendNewDataBaseInfo(databaseName, task);
        }

        
        
        // SQL processing 
        static bool sqlProcessing(string task)
        {
            string databaseName = "";


            //process txt file
            Console.Write("The processation of the txt FILE with sqls will start in the server");
            databaseName = Console.ReadLine();
            databaseName = "noDBName";

            // Once the database name is created, try the creation in server
            Console.WriteLine("\nSending request to server...");
            return SendNewDataBaseInfo(databaseName, task);
        }


        // Send DB name to server
        static bool SendNewDataBaseInfo(string databasename, string task)
        {
            TcpClient client;

            // Try connection with server
            try
            {
                client = new TcpClient("127.0.0.1", 1111); // CHange IP and PORT here if necessary
                Console.WriteLine("databasename " + databasename + " sent to server...");
            }
            catch
            {
                return false;
            }

            // Opening network stream with server
            NetworkStream netStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(databasename + "#.*;#" + userDB + "#.*;#" + task);

            // Sending credentials and waiting for response.
            netStream.Write(bytesToSend, 0, bytesToSend.Length);

            // Reading response from server
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = netStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            client.Close();

            // If Server sends "createdDataBase", database is created
            if (Encoding.ASCII.GetString(bytesToRead, 0, bytesRead).Substring(0, 15) == "createdDataBase")
            {
                return true;
            }

            // If Server sends "processSQLOK", processate is done
            if (Encoding.ASCII.GetString(bytesToRead, 0, bytesRead).Substring(0, 12) == "processSQLOK")
            {
                return true;
            }
            return false;
        }


        // Send login credential to server
        static bool SendLoginInfo(string username, string password, bool newUser)
        {

            /*
            TcpClient client;
            // Try connection with server
            try
            {
                client = new TcpClient("127.0.0.1", 1111); // CHange IP and PORT here if necessary
                Console.WriteLine("Credentials sent to server...");
            }
            catch
            {
                return false;
            }

            // Opening network stream with server
            NetworkStream netStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(username + "#.*;#" + password + "#.*;#" + newUser);

            // Sending credentials and waiting for response.
            netStream.Write(bytesToSend, 0, bytesToSend.Length);

            // Reading response from server
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = netStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            client.Close();

            // If Server sends "true", client is logged in
            if (Encoding.ASCII.GetString(bytesToRead, 0, bytesRead).Substring(0, 4) == "True")
            {
                userDB = username;
                return true;
            }
            */


            string message = string.Format("<Open Database={0} User={1} Password={2}/>",username, password, null);

            String res = Connect(message);


            return false;


        }


        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome! This is the Client application. You are going to communicate with the Server");
            Console.WriteLine("");
            bool notValid = true;
            bool notValidDB = true;
            bool notEnded = true;

            char answer;
            char answerDB;


            while (notEnded)
            {
                notValidDB = true;
                while (notValid)
                {
                    try
                    {
                        Console.Write("1) Login\n2) Create Account\n0) exit\n> ");
                        answer = Convert.ToChar(Console.ReadLine());
                        Console.WriteLine();

                        // Run Login function to loggin
                        if (answer == '1')
                        {
                            if (Login(false))
                            {
                                notValid = false;
                                Console.WriteLine("You are in!");
                            }
                            else
                            {
                                Console.WriteLine("\n Server down or credentials invalid...");
                            }
                        }
                        else if (answer == '2')
                        { // Registration, Login function with true variable
                            if (Login(true))
                            {
                                notValid = false;
                            }
                            else
                            {
                                Console.WriteLine("\n Server down or credentials invalid...");
                            }
                        }
                        else if (answer == '0')
                        { // Registration, Login function with true variable

                            Console.WriteLine("\n Ending connection...");
                            notValid = false;
                            notValidDB = false;
                            notEnded = false;
                            break;

                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nInput error when login...");
                    }

                }


                // Put main program from heren inside on this while loop
                while (notValidDB)
                {
                    try
                    {
                        Console.Write("1) show all Databases (Not functional) \n2) create Database Example\n3) processate of input text file with SQLs sentences\n0) <-Back\n> ");
                        answerDB = Convert.ToChar(Console.ReadLine());
                        Console.WriteLine();

                        // Run Database function to createDatabase
                        if (answerDB == '1')
                        {
                            if (database("showDatabases"))
                            {
                                notValidDB = false;
                            }
                            else
                            {
                                Console.WriteLine("\n Server down or error creating DB...");
                            }
                        }
                        else if (answerDB == '2')
                        { // Registration, Database function with true variable
                            if (database("createDataBaseExample"))
                            {
                                Console.WriteLine("\n Success creating DB...");
                                notValidDB = true;
                            }
                            else
                            {
                                Console.WriteLine("\n Server down or error creating DB...");
                            }
                        }
                        else if (answerDB == '3')
                        { // Process sql txt file
                            if (sqlProcessing("processSQL"))
                            {
                                Console.WriteLine("\n Success processing SQL...");
                                notValidDB = true;
                            }
                            else
                            {
                                Console.WriteLine("\n Server down or error...");
                            }
                        }
                        else if (answerDB == '0')
                        { // Registration, Login function with true variable

                            Console.WriteLine("\n Returning to previous screen...Please, press ENTER key");
                            notValid = true;
                            notValidDB = false;
                            notEnded = true;
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nInput error when creating DB...");
                    }
                }


                // Get options
                string option;
                // Validation
                while (true)
                {
                    try//
                    {
                        option = Console.ReadLine();
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Input error...");
                    }
                }
            }


        }


    }
}
