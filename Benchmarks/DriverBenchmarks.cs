using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

[MemoryDiagnoser]
public class DriverBenchmarks
{
    private Order order;
    private List<Driver> drivers;
    private SimpleFinder simpleFinder;
    private ZoneFinder zoneFinder;
    private GridFinder gridFinder;

    [GlobalSetup]
    public void Setup()
    {
        order = new Order { X = 10, Y = 10 }; 

        drivers = new List<Driver>();
        var random = new System.Random(42);

        // Создаем 15 водителей для теста
        for (int i = 0; i < 15; i++)
        {
            drivers.Add(new Driver
            {
                Identifier = $"В{i}",
                X = random.Next(0, 20), 
                Y = random.Next(0, 20)
            });
        }

        simpleFinder = new SimpleFinder();
        zoneFinder = new ZoneFinder();
        gridFinder = new GridFinder();
    }

    [Benchmark]
    public List<Driver> SimpleFinder_Benchmark()
    {
        return simpleFinder.FindNearest(order, drivers, 5);
    }

    [Benchmark]
    public List<Driver> ZoneFinder_Benchmark()
    {
        return zoneFinder.FindNearest(order, drivers, 5);
    }

    [Benchmark]
    public List<Driver> GridFinder_Benchmark()
    {
        return gridFinder.FindNearest(order, drivers, 5);
    }
}