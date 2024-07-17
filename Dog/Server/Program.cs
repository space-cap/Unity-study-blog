using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class TcpServer
{
    static void Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddr, port);
            server.Start();
            Console.WriteLine("서버가 시작되었습니다...");

            while (true)
            {
                Console.WriteLine("연결을 기다리는 중...");

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("연결됨!");

                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
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

        Console.WriteLine("\n계속하려면 Enter 키를 누르세요...");
        Console.Read();
    }

    static void HandleClient(TcpClient client)
    {
        byte[] bytes = new byte[256];
        string data = null;

        NetworkStream stream = client.GetStream();

        int i;
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine("받은 데이터: {0}", data);

            byte[] msg = Encoding.ASCII.GetBytes(data.ToUpper());
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("보낸 데이터: {0}", data.ToUpper());
        }

        client.Close();
    }
}