using System.Collections.Generic;
using System.Linq;

public class ZoneFinder
{
    public string Name => "Зональный поиск";

    public List<Driver> FindNearest(Order order, List<Driver> drivers, int count = 5)
    {
        var found = new List<Driver>();
        int radius = 0;

        while (found.Count < count && radius < 100)
        {
            radius++;
            var inZone = drivers
                .Where(d => Math.Abs(d.X - order.X) <= radius &&
                           Math.Abs(d.Y - order.Y) <= radius)
                .Where(d => !found.Contains(d))
                .Take(count - found.Count);
            found.AddRange(inZone);
        }
        return found;
    }
}