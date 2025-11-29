using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("СИСТЕМА ПОИСКА ВОДИТЕЛЕЙ");
        Console.WriteLine("=========================\n");

        // Создаем карту
        Map map = new Map(20, 20);
        Random random = new Random();

        Console.WriteLine("Добавляем водителей на карту...\n");

        // Добавляем водителей
        for (int i = 0; i < 15; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int x = random.Next(0, 20);
                int y = random.Next(0, 20);

                try
                {
                    Driver driver = new Driver
                    {
                        Identifier = $"В{i + 1}",
                        X = x,
                        Y = y
                    };

                    map.SetMapValue(x, y, driver);
                    Console.WriteLine($"Водитель {driver.Identifier} ({x}, {y})");
                    placed = true;
                }
                catch
                {
                    // Клетка занята, пробуем другую
                }
            }
        }

        // Добавляем заказ
        Order order = new Order { X = 5, Y = 5 };
        try
        {
            map.SetMapValue(order.X, order.Y, order);
            Console.WriteLine($"\nЗаказ ({order.X}, {order.Y})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        // Показываем карту
        Console.WriteLine("\nКАРТА:");
        Console.WriteLine("======");
        Map.MapVisualisation(map);

        // Создаем алгоритмы
        var algorithms = new List<object>
        {
            new SimpleFinder(),
            new ZoneFinder(),
            new GridFinder()
        };

        // Получаем всех водителей
        var allDrivers = map.GetAllDrivers();

        Console.WriteLine($"\nВсего водителей на карте: {allDrivers.Count}");

        Console.WriteLine("\nПОИСК 5 БЛИЖАЙШИХ ВОДИТЕЛЕЙ:");
        Console.WriteLine("=============================");

        // Тестируем алгоритмы - ищем 5 водителей
        foreach (dynamic algorithm in algorithms)
        {
            Console.WriteLine($"\n{algorithm.Name}:");
            Console.WriteLine(new string('-', algorithm.Name.Length));

            var nearestDrivers = algorithm.FindNearest(order, allDrivers, 5);

            if (nearestDrivers.Count == 0)
            {
                Console.WriteLine("   Водители не найдены");
                continue;
            }

            foreach (var driver in nearestDrivers)
            {
                double distance = driver.DistanceTo(order.X, order.Y);
                Console.WriteLine($"   {driver.Identifier} в ({driver.X}, {driver.Y}) - расстояние: {distance:F1}");
            }
        }

        // Запуск BenchmarkDotNet
        Console.WriteLine("\nЗАПУСК BENCHMARKDOTNET...");
        Console.WriteLine("Для выхода нажмите любую клавишу");

        try
        {
            // Запускаем бенчмарк
            var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<DriverBenchmarks>();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Ошибка при запуске бенчмарков: {ex.Message}");
        }

        Console.WriteLine("\nBenchmark завершен");
        Console.ReadKey();
    }
}