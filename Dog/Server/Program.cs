namespace Server
{
    class Program
    {
        static ThreadLocal<string> threadName = new ThreadLocal<string>();

        static void WhoAmI()
        {
            threadName.Value = $"my name is {Thread.CurrentThread.ManagedThreadId}";

            Thread.Sleep(1000);
            Console.WriteLine(threadName.Value);
        }
        static void Main(string[] args)
        {
            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);

        }
    }
}