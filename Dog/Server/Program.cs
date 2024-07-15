using System;
using System.Threading;

class Program
{
    private static SpinLock _spinLock = new SpinLock();
    private static int _counter = 0;

    static void Main()
    {
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("최종 카운터 값: " + _counter);
    }

    private static void IncrementCounter()
    {
        for (int i = 0; i < 1000; i++)
        {
            bool lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);
                _counter++;
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }
    }
}