using System;
using System.Collections.Generic;

public class Map
{
    private readonly int width;
    private readonly int height;
    private readonly object[,] grid;

    public Map(int x, int y)
    {
        this.width = x;
        this.height = y;
        this.grid = new object[y, x];
    }

    public static void MapVisualisation(Map map)
    {
        for (int i = 0; i < map.height; i++)
        {
            Console.Write($"{i}| ");
            for (int j = 0; j < map.width; j++)
            {
                if (map.grid[i, j] == null)
                {
                    Console.Write("X ");
                }
                else if (map.grid[i, j] is Driver driver)
                {
                    Console.Write(driver.Identifier + " ");
                }
                else if (map.grid[i, j] is Order)
                {
                    Console.Write("OO ");
                }
            }
            Console.Write("|");
            Console.WriteLine();
        }
    }

    public void SetMapValue(int x, int y, object value)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            throw new Exception($"Координаты ({x}, {y}) вне карты {width}x{height}!");
        }

        if (grid[y, x] != null)
        {
            throw new Exception($"Клетка ({x}, {y}) уже занята!");
        }

        grid[y, x] = value;
    }

    public List<Driver> GetAllDrivers()
    {
        var drivers = new List<Driver>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[y, x] is Driver driver)
                {
                    drivers.Add(driver);
                }
            }
        }

        return drivers;
    }
}