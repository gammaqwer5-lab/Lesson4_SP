
using System;
using System.Threading;

public class Program
{
    private static int completedOrders = 0;

    public static void Main()
    {
        const int threadCount = 8;
        const int incrementsPerThread = 100_000;

        Thread[] threads = new Thread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < incrementsPerThread; j++)
                {
                    // TODO 1. Попробуйте воспроизвести
                    // гонку данных с обычным инкрементом:
                    // completedOrders++;
                    Interlocked.Increment(ref completedOrders);
                }
            });
        }

        foreach (Thread thread in threads)
        {
            thread.Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        int expected = threadCount * incrementsPerThread;

        Console.WriteLine($"Ожидалось: {expected}");
        Console.WriteLine($"Получено: {completedOrders}");

        // TODO 2. Исправьте инкремент так, чтобы итог
        // гарантированно совпадал с ожидаемым значением.

        // TODO 3. Повторите запуск не менее пяти раз
        // и запишите наблюдения.
    }
}