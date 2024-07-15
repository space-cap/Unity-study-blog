using System;
using System.Threading;

class Program
{
    static void Main()
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork));

        // 메인 쓰레드에서 다른 작업 수행
        Console.WriteLine("Main thread is doing some work.");
        Thread.Sleep(1000); // ThreadPool 쓰레드가 완료될 시간을 줌
    }

    static void DoWork(object state)
    {
        Console.WriteLine("ThreadPool thread is doing some work.");
    }
}