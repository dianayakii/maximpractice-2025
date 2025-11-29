using System.Collections.Generic;
using System.Linq;

public class GridFinder
{
    public string Name => "Сеточный поиск";

    public List<Driver> FindNearest(Order order, List<Driver> drivers, int count = 5)
    {
        return drivers
            .OrderBy(d => d.ManhattanTo(order.X, order.Y))
            .Take(count)
            .ToList();
    }
}