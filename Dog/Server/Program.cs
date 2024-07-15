using System;
using System.Threading;

class Program
{
    private static int _x = 0;
    private static int _y = 0;

    static void Main()
    {
        Thread t1 = new Thread(Thread1);
        Thread t2 = new Thread(Thread2);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine($"x: {_x}, y: {_y}");
    }

    static void Thread1()
    {
        _x = 1; // Write to _x
        Thread.MemoryBarrier(); // Insert a memory barrier
        _y = 1; // Write to _y
    }

    static void Thread2()
    {
        while (_y != 1) ; // Wait until _y is set to 1
        Thread.MemoryBarrier(); // Insert a memory barrier
        Console.WriteLine(_x); // Read _x
    }
}