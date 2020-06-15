using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Collections.Generic;

namespace DataBaseDPD
{
    class Program
    {
        public static Connection con = new Connection();

        public static String ParseQuery(String message)
        {

            Match match;
            String response = "A problem has occurred";


            const string query = @"<Query>([^<]+)</Query>";
            const string quit = @"quit";

            match = Regex.Match(message, query);
            if (match.Success)
            {

                string req = (string)match.Groups[1].Value;
                Console.WriteLine(req);
                string res = con.RunQuery(req);

                if (res.StartsWith("ERROR"))
                {
                    response = string.Format("<Answer><Error>{0}</Error></Answer>", res);
                }
                else
                {
                    response = string.Format("<Answer>{0}</Answer>", res);
                }
                return response;
            }
            match = Regex.Match(message, connection);
            if (match.Success)
            {
                string database = (string)match.Groups[1].Value;
                string user = (string)match.Groups[2].Value;
                string password = (string)match.Groups[3].Value;

                //Console.WriteLine(database +" "+ user+ " " +password );
                string res = con.Connect(database, user, password);


                if (con.isConnected())
                {
                    response = "<Success/>";
                }
                else
                {
                    response = string.Format("<Error>{0}</Error>", res);
                }
                return response;
            }

            return response;

        }



        public static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {

                Int32 port = 13000;
                string ip = "127.0.0.1";
                IPAddress localAddr = IPAddress.Parse(ip);


                server = new TcpListener(localAddr, port);


                server.Start();


                Byte[] bytes = new Byte[256];
                String data = null;


                while (true)
                {
                    Console.Write("Waiting for a connection... ");


                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;


                    NetworkStream stream = client.GetStream();

                    int i;


                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {

                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);


                        data = data.ToUpper();


                        byte[] msg = Encoding.ASCII.GetBytes(data);


                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }


                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {

                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();



        }
    }
}