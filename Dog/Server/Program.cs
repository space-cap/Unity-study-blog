using System;
using System.Threading;

class ManualResetEventExample
{
    private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    static void Main()
    {
        Console.WriteLine("메인 스레드가 작업자 스레드를 시작합니다...");

        for (int i = 1; i <= 3; i++)
        {
            Thread workerThread = new Thread(Worker);
            workerThread.Name = $"작업자 {i}";
            workerThread.Start();
        }

        Console.WriteLine("메인 스레드가 작업을 수행한 후 이벤트를 신호로 설정합니다...");
        Thread.Sleep(2000);  // 작업을 시뮬레이션
        manualResetEvent.Set();

        Console.WriteLine("메인 스레드가 이벤트를 설정했습니다. 작업자 스레드가 진행합니다...");

        // 미래 사용을 위해 이벤트를 비신호 상태로 재설정
        manualResetEvent.Reset();

        Console.WriteLine("메인 스레드가 다른 작업을 수행한 후 종료합니다...");
        Thread.Sleep(2000);  // 더 많은 작업을 시뮬레이션
    }

    static void Worker()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}이(가) 이벤트가 신호로 설정되기를 기다리고 있습니다...");
        manualResetEvent.WaitOne();  // 이벤트가 신호로 설정되기를 기다림
        Console.WriteLine($"{Thread.CurrentThread.Name}이(가) 신호를 받고 작업을 계속합니다...");
    }
}