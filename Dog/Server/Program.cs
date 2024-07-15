using System;
using System.Threading;

class Program
{
    private static object _lock = new object();
    private static int _counter = 0;

    static void Main()
    {
        Thread t1 = new Thread(IncrementCounter);
        Thread t2 = new Thread(IncrementCounter);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine($"Final counter value: {_counter}");
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 1000000; i++)
        {
            Monitor.Enter(_lock);
            try
            {
                _counter++;
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
    }
}