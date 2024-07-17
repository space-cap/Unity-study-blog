using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public class Session
    {
        private Socket _socket;
        int _disconnected = 0;

        

        object _lock = new object();
        Queue<byte[]> _sendQueue = new Queue<byte[]>();
        bool _panding = false;
        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();

        public void Start(Socket socket)
        {
            _socket = socket;
            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);
            recvArgs.SetBuffer(new byte[1024], 0, 1024);

            _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);


            RegisterRecv(recvArgs);
        }

        public void Send(byte[] sendBuffer)
        {
            lock (_lock)
            {
                _sendQueue.Enqueue(sendBuffer);
                if (_panding == false)
                {
                    RegisterSend();
                }
            }
        }

        

        public void Disconnect()
        {
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
                return;

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        

        #region 네트워크 통신

        void RegisterSend()
        {
            _panding = true;
            byte[] buffer = _sendQueue.Dequeue();
            _sendArgs.SetBuffer(buffer, 0, buffer.Length);

            bool panding = _socket.SendAsync(_sendArgs);
            if (panding == false)
            {
                OnSendCompleted(null, _sendArgs);
            }
        }

        void OnSendCompleted(object sender, SocketAsyncEventArgs args)
        {
            lock (_lock)
            {
                if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    try
                    {
                        if (_sendQueue.Count > 0)
                        {
                            RegisterSend();
                        }
                        else
                        {
                            _panding = false;
                        }
                    }
                    catch (Exception e)
                    {
                        Disconnect();
                        Console.WriteLine($"OnSendCompleted Failed {e}");
                    }
                }
                else
                {
                    Disconnect();
                }
            }
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
                Disconnect();
            }
        }

        #endregion
    }
}
