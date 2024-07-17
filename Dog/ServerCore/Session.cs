using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    class Session
    {
        private Socket _socket;
        int _disconnected = 0;

        public void Start(Socket socket)
        {
            _socket = socket;
            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);
            recvArgs.SetBuffer(new byte[1024], 0, 1024);

            RegisterRecv(recvArgs);
        }

        public void Send(byte[] sendBuffer)
        {
            _socket.Send(sendBuffer);
        }

        public void Disconnect()
        {
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
                return;

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        void RegisterRecv(SocketAsyncEventArgs args)
        {
            bool panding = _socket.ReceiveAsync(args);
            if (panding == false)
            {
                OnRecvCompleted(null, args);
            }
        }

        void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {

            if (args is {BytesTransferred: > 0, SocketError: SocketError.Success})
            {
                try
                {
                    string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                    Console.WriteLine($"[from client] {recvData}");
                    RegisterRecv(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }

            }
            else
            {
                // todo
                // disconnection
            }
        }
    }
}
