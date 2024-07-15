using System;
using System.Threading;

public class SpinLock
{
    private int _lockFlag = 0;

    public void Enter()
    {
        while (true)
        {
            // 락을 획득하려고 시도
            if (Interlocked.Exchange(ref _lockFlag, 1) == 0)
            {
                return; // 락 획득 성공
            }

            // 잠시 대기하여 CPU를 양보
            Thread.SpinWait(1);
        }
    }

    public void Exit()
    {
        // 락 해제
        Volatile.Write(ref _lockFlag, 0);
    }
}

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
            _spinLock.Enter();
            try
            {
                _counter++;
            }
            finally
            {
                _spinLock.Exit();
            }
        }
    }
}