using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class DriverTests
{
    private Order order;
    private List<Driver> drivers;

    [SetUp]
    public void Setup()
    {
        order = new Order { X = 5, Y = 5 };

        drivers = new List<Driver>
        {
            new Driver { Identifier = "В1", X = 3, Y = 3 },
            new Driver { Identifier = "В2", X = 7, Y = 7 },
            new Driver { Identifier = "В3", X = 1, Y = 1 },
            new Driver { Identifier = "В4", X = 9, Y = 9 },
            new Driver { Identifier = "В5", X = 5, Y = 6 }
        };
    }

    [Test]
    public void SimpleFinder_FindNearest_ReturnsCorrectDrivers()
    {
        var finder = new SimpleFinder();

        var result = finder.FindNearest(order, drivers, 3);

        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0].Identifier, Is.EqualTo("В5")); // Самый близкий
    }

    [Test]
    public void ZoneFinder_FindNearest_ReturnsCorrectCount()
    {
        var finder = new ZoneFinder();

        var result = finder.FindNearest(order, drivers, 2);

        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void GridFinder_FindNearest_ReturnsCorrectDrivers()
    {
        var finder = new GridFinder();

        var result = finder.FindNearest(order, drivers, 3);

        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void Driver_DistanceTo_CalculatesCorrectly()
    {
        var driver = new Driver { X = 3, Y = 4 };

        var distance = driver.DistanceTo(0, 0);

        Assert.That(distance, Is.EqualTo(5).Within(0.001));
    }

    [Test]
    public void Driver_ManhattanTo_CalculatesCorrectly()
    {
        var driver = new Driver { X = 3, Y = 4 };

        var distance = driver.ManhattanTo(1, 1);

        Assert.That(distance, Is.EqualTo(5));
    }
}