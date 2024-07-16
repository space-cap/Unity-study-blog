using System;
using System.Threading;

class ReaderWriterLockSlimExample
{
    private static ReaderWriterLockSlim rwLockSlim = new ReaderWriterLockSlim();
    private static int sharedResource = 0;

    static void Main()
    {
        Thread writerThread = new Thread(Writer);
        writerThread.Name = "Writer";

        Thread[] readerThreads = new Thread[3];
        for (int i = 0; i < readerThreads.Length; i++)
        {
            readerThreads[i] = new Thread(Reader);
            readerThreads[i].Name = $"Reader {i + 1}";
        }

        writerThread.Start();
        foreach (var reader in readerThreads)
        {
            reader.Start();
        }
    }

    static void Writer()
    {
        for (int i = 0; i < 5; i++)
        {
            rwLockSlim.EnterWriteLock();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is writing...");
                sharedResource++;
                Thread.Sleep(1000);  // Simulate write operation
                Console.WriteLine($"{Thread.CurrentThread.Name} updated sharedResource to {sharedResource}");
            }
            finally
            {
                rwLockSlim.ExitWriteLock();
            }
            Thread.Sleep(500);  // Simulate time between writes
        }
    }

    static void Reader()
    {
        for (int i = 0; i < 5; i++)
        {
            rwLockSlim.EnterReadLock();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is reading sharedResource: {sharedResource}");
                Thread.Sleep(500);  // Simulate read operation
            }
            finally
            {
                rwLockSlim.ExitReadLock();
            }
            Thread.Sleep(500);  // Simulate time between reads
        }
    }
}