using System.Net;
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
                Session session = new Session();
                session.Start(clientSocket);

                // 보낸다.
                byte[] sendBuffer = Encoding.UTF8.GetBytes("welcome to mmorpg server!");
                session.Send(sendBuffer);

                Thread.Sleep(1000);

                session.Disconnect();
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