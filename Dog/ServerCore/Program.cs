using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Create a new thread
        Thread myThread = new Thread(new ThreadStart(MyThreadMethod));

        // Start the thread
        myThread.Start();

        // Do something on the main thread
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Main thread: " + i);
            Thread.Sleep(100);
        }
    }

    static void MyThreadMethod()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("MyThreadMethod: " + i);
            Thread.Sleep(100);
        }
    }
}