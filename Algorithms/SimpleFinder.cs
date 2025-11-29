using System.Collections.Generic;
using System.Linq;

public class SimpleFinder
{
    public string Name => "Простой поиск";

    public List<Driver> FindNearest(Order order, List<Driver> drivers, int count = 5)
    {
        return drivers
            .OrderBy(d => d.DistanceTo(order.X, order.Y))
            .Take(count)
            .ToList();
    }
}