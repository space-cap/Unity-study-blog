using System;
using System.Threading;

class ThreadLocalExample
{
    // ThreadLocal 인스턴스 초기화
    private static ThreadLocal<int> threadLocalData = new ThreadLocal<int>(() =>
    {
        // 각 스레드가 처음 접근할 때 초기 값 설정
        return Thread.CurrentThread.ManagedThreadId;
    });

    static void Main()
    {
        // 여러 스레드 생성 및 시작
        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(Worker);
            thread.Name = $"Thread {i + 1}";
            thread.Start();
        }
    }

    static void Worker()
    {
        // 스레드 로컬 데이터에 접근
        Console.WriteLine($"{Thread.CurrentThread.Name} has ThreadLocal data: {threadLocalData.Value}");
        Thread.Sleep(1000);  // 작업 시뮬레이션
        // 스레드 로컬 데이터 변경
        threadLocalData.Value += 100;
        Console.WriteLine($"{Thread.CurrentThread.Name} updated ThreadLocal data to: {threadLocalData.Value}");
    }
}