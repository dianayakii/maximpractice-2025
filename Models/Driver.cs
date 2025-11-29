public class Driver
{
    public string Identifier { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }

    public double DistanceTo(int targetX, int targetY)
    {
        int dx = X - targetX;
        int dy = Y - targetY;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public int ManhattanTo(int targetX, int targetY)
    {
        return Math.Abs(X - targetX) + Math.Abs(Y - targetY);
    }
}