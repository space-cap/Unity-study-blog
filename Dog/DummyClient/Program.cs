using System;
using System.Net.Sockets;
using System.Text;

class TcpClientExample
{
    static void Main(string[] args)
    {
        try
        {
            int port = 13000;
            TcpClient client = new TcpClient("127.0.0.1", port);

            NetworkStream stream = client.GetStream();

            string message = "Hello, Server!";
            byte[] data = Encoding.ASCII.GetBytes(message);

            stream.Write(data, 0, data.Length);
            Console.WriteLine("보낸 데이터: {0}", message);

            data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("받은 데이터: {0}", responseData);

            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\n계속하려면 Enter 키를 누르세요...");
        Console.Read();
    }
}