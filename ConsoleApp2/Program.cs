using System;
using System.Diagnostics;
using System.Threading;

public record Order(int Id, decimal Amount);
 
public class Program
{
    private static void ProcessOrder(Order order)
    {
        Console.WriteLine(
            $"Заказ {order.Id}: поток " +
            $"{Environment.CurrentManagedThreadId}"
        );

        // Имитация затратной работы.
        Thread.Sleep(100);

        decimal total = order.Amount * 1.05m;

        Console.WriteLine(
            $"Заказ {order.Id} завершён. Сумма: {total:F2}"
        );
    }

    public static void Main()
    {
        Order[] orders =
        {
            new(1, 100),
            new(2, 200),
            new(3, 300),
            new(4, 400),
            new(5, 500),
            new(6, 600),
            new(7, 700),
            new(8, 800)
        };

        // TODO 1. Измерьте время последовательной обработки.
        // Обработайте все заказы циклом через ProcessOrder.
        // Сохраните результат Stopwatch.Elapsed.
        Console.WriteLine("Последовательная обработка");
        Stopwatch swOrder = Stopwatch.StartNew();

        foreach (var order in orders)
        {
            ProcessOrder(order);
        }

        swOrder.Stop();
        Console.WriteLine($"Времи: {swOrder.Elapsed}\n");

        // TODO 2. Создайте массив Thread размером orders.Length.
        Thread[] threads = new Thread[orders.Length];

        // TODO 3. Для каждого заказа создайте поток,
        // который вызывает ProcessOrder для этого заказа.
        for (int i = 0; i < orders.Length; i++)
        {
            Order current = orders[i];
            threads[i] = new Thread(() => ProcessOrder(current));
        }

        // TODO 4. Запустите все созданные потоки.
        Console.WriteLine("\nМногопоточная обработка");
        Stopwatch swParallel = Stopwatch.StartNew();

        foreach (Thread thread in threads)
        {
            thread.Start();
        }

        // TODO 5. Дождитесь завершения каждого потока
        // с помощью Join().
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        swParallel.Stop();

        // TODO 6. Измерьте длительность многопоточной
        // обработки и выведите оба результата.
        Console.WriteLine($"Время: {swParallel.Elapsed}");
    }
}