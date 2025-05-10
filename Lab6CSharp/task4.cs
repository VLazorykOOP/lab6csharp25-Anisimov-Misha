using System;
using System.Collections;
using System.Collections.Generic;

class Point : IEnumerable<object>
{
    protected int x, y, c;

    public Point()
    {
        x = 0; y = 0; c = 0;
    }

    public Point(int x, int y, int c)
    {
        this.x = x; this.y = y; this.c = c;
    }

    public void PrintCoordinates()
    {
        Console.WriteLine($"Point coordinates: ({x}, {y}), Color: {c}");
    }

    public double DistanceFromOrigin()
    {
        return Math.Sqrt(x * x + y * y);
    }

    public void Move(int x1, int y1)
    {
        x += x1; y += y1;
    }

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Color { get => c; set => c = value; }

    public object this[int index]
    {
        get => index switch
        {
            0 => x,
            1 => y,
            2 => c,
            _ => throw new IndexOutOfRangeException("Невірний індекс. Дозволені: 0 (x), 1 (y), 2 (колір).")
        };
        set
        {
            switch (index)
            {
                case 0: x = Convert.ToInt32(value); break;
                case 1: y = Convert.ToInt32(value); break;
                case 2: c = Convert.ToInt32(value); break;
                default: throw new IndexOutOfRangeException("Невірний індекс. Дозволені: 0 (x), 1 (y), 2 (колір).");
            }
        }
    }

    public static Point operator ++(Point p) => new Point(p.x + 1, p.y + 1, p.c);
    public static Point operator --(Point p) => new Point(p.x - 1, p.y - 1, p.c);
    public static bool operator true(Point p) => p.x == p.y;
    public static bool operator false(Point p) => p.x != p.y;
    public static Point operator +(Point p, int scalar) => new Point(p.x + scalar, p.y + scalar, p.c);
    public static implicit operator string(Point p) => $"{p.x},{p.y},{p.c}";
    public static implicit operator Point(string s)
    {
        var parts = s.Split(',');
        if (parts.Length != 3)
            throw new FormatException("Строка має містити 3 значення, розділені комами.");
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    // Реалізація foreach
    public IEnumerator<object> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return c;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class Program
{
    static void Main(string[] args)
    {
        Point p = new Point(3, 3, 1);
        Console.WriteLine(p ? "x == y" : "x != y");

        p++;
        Console.WriteLine((string)p);

        Point q = "5,6,2";
        q.PrintCoordinates();

        Console.WriteLine(q[0]); // 5
        q[1] = 10;
        q.PrintCoordinates();

        Console.WriteLine("== Перебір foreach ==");
        foreach (var value in q)
        {
            Console.WriteLine(value);
        }
    }
}
