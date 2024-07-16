using System;
using System.Threading;

class MutexExample
{
    private static Mutex mutex = new Mutex();

    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            Thread thread = new Thread(Worker);
            thread.Name = $"스레드 {i}";
            thread.Start();
        }
    }

    static void Worker()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}가 Mutex를 기다립니다...");
        mutex.WaitOne();  // Mutex 소유를 시도합니다
        try
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}가 Mutex를 소유합니다.");
            // 공유 자원 접근 (여기서는 콘솔 출력)
            Thread.Sleep(2000);  // 작업을 시뮬레이션
            Console.WriteLine($"{Thread.CurrentThread.Name}가 작업을 완료했습니다.");
        }
        finally
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}가 Mutex를 해제합니다.");
            mutex.ReleaseMutex();  // Mutex 해제
        }
    }
}