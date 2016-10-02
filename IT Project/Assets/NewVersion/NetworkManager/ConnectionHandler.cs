using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Net.Sockets;
using System.IO;
using Command;
using Manager;

namespace NetworkManager
{
    public static class ConnectionHandler
    {

        public static TcpClient client;
        public static Thread receive;

        private static StreamReader sreader;
        private static StreamWriter swriter;

        public static void Send(string s)
        {
            swriter.WriteLine(s);
            swriter.Flush();
        }

        public static void StartConnection()
        {
            Connect();
            receive = new Thread(new ThreadStart(Receive));
            // Start the thread
            receive.Start();

        }

        public static void Connect()
        {
            String server = "localhost";
            // Create a TcpClient.
            Int32 port = 9878;
            client = new TcpClient(server, port);
            // Receive the TcpServer.response.
            NetworkStream networkStream = client.GetStream();
            sreader = new StreamReader(networkStream);
            swriter = new StreamWriter(networkStream);

        }


        public static void Receive()
        {
            while (true)
            {
                String s = sreader.ReadLine();

                Debug.Log(s);

                if (s.Equals("Ready"))
                {
                    GameManager.Ready();
                    continue;
                }

                if (s[0] == 'J')
                {
                    GameManager.SetMainChar(s);
                    continue;
                }

                if(s[0] == 'S')
                {
                    GameManager.StartGame(s);
                    continue;
                }


                CommandReceiver.Receive(s);
            }
        }

    }
}