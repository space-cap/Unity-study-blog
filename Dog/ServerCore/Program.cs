﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    class Program
    {
        private static Listener _listener = new Listener();

        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {
                // 받는다.
                byte[] recvBuffer = new byte[1024];
                int recvBytes = clientSocket.Receive(recvBuffer);
                string recvData = Encoding.UTF8.GetString(recvBuffer, 0, recvBytes);
                Console.WriteLine($"[from client] {recvData}");

                // 보낸다.
                byte[] sendBuffer = Encoding.UTF8.GetBytes("welcome to mmorpg server!");
                clientSocket.Send(sendBuffer);

                // 쫓아낸다.
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        static void Main(string[] args)
        {
            // DNS(domain name system)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            // 문지기
            Socket listenSocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _listener.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("listening...");

            while (true)
            {
                ;
            }
        }
    }
}