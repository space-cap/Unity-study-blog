using System;
using System.Threading;

class Program
{
    private static int counter = 0;

    static void Main()
    {
        Thread t1 = new Thread(IncrementCounter);
        Thread t2 = new Thread(DecrementCounter);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine($"Final counter value: {counter}");
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 1000; i++)
        {
            Interlocked.Increment(ref counter);
        }
    }

    static void DecrementCounter()
    {
        for (int i = 0; i < 1000; i++)
        {
            Interlocked.Decrement(ref counter);
        }
    }
}