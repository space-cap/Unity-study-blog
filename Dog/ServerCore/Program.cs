using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // 작업 시작
        Task task = Task.Run(() => DoWork());

        // 작업이 완료될 때까지 대기
        await task;

        Console.WriteLine("Task completed.");
    }

    static void DoWork()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Task is working: " + i);
            Task.Delay(100).Wait(); // 작업을 시뮬레이트
        }
    }
}