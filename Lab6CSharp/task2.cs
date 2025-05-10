using System;
using System.Collections.Generic;

// Інтерфейс Figure успадковує інтерфейси .NET
interface IFigure : IComparable, IFormattable, IDisposable
{
    double GetArea();
    double GetPerimeter();
    void ShowInfo();
}

// Прямокутник
class Rectangle : IFigure
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double w, double h)
    {
        Width = w;
        Height = h;
    }

    public double GetArea() => Width * Height;
    public double GetPerimeter() => 2 * (Width + Height);

    public void ShowInfo()
    {
        Console.WriteLine($"[Rectangle] Width: {Width}, Height: {Height}, Area: {GetArea()}, Perimeter: {GetPerimeter()}");
    }

    public int CompareTo(object obj)
    {
        if (obj is IFigure other)
            return GetArea().CompareTo(other.GetArea());
        throw new ArgumentException("Object is not IFigure");
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return $"Rectangle: {Width} x {Height}";
    }

    public void Dispose()
    {
        Console.WriteLine("Rectangle resources released.");
    }
}

// Коло
class Circle : IFigure
{
    public double Radius { get; set; }

    public Circle(double r)
    {
        Radius = r;
    }

    public double GetArea() => Math.PI * Radius * Radius;
    public double GetPerimeter() => 2 * Math.PI * Radius;

    public void ShowInfo()
    {
        Console.WriteLine($"[Circle] Radius: {Radius}, Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
    }

    public int CompareTo(object obj)
    {
        if (obj is IFigure other)
            return GetArea().CompareTo(other.GetArea());
        throw new ArgumentException("Object is not IFigure");
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return $"Circle with radius {Radius}";
    }

    public void Dispose()
    {
        Console.WriteLine("Circle resources released.");
    }
}

// Трикутник
class Triangle : IFigure
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public Triangle(double a, double b, double c)
    {
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException("Invalid triangle sides.");
        A = a;
        B = b;
        C = c;
    }

    public double GetPerimeter() => A + B + C;

    public double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }

    public void ShowInfo()
    {
        Console.WriteLine($"[Triangle] Sides: {A}, {B}, {C}, Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
    }

    public int CompareTo(object obj)
    {
        if (obj is IFigure other)
            return GetArea().CompareTo(other.GetArea());
        throw new ArgumentException("Object is not IFigure");
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return $"Triangle sides: {A}, {B}, {C}";
    }

    public void Dispose()
    {
        Console.WriteLine("Triangle resources released.");
    }
}

// Тестування
class Program
{
    static void Main()
    {
        IFigure[] figures = new IFigure[]
        {
            new Rectangle(4, 5),
            new Circle(3),
            new Triangle(3, 4, 5),
            new Rectangle(2, 6),
            new Circle(1.5)
        };

        Console.WriteLine("== Фігури ==");
        foreach (var fig in figures)
        {
            fig.ShowInfo();
        }

        Console.WriteLine("\n== Сортування за площею ==");
        Array.Sort(figures); // порівняння через CompareTo
        foreach (var fig in figures)
        {
            Console.WriteLine(fig.ToString("G", null) + $" | Area: {fig.GetArea():F2}");
        }

        Console.WriteLine("\n== Звільнення ресурсів ==");
        foreach (var fig in figures)
        {
            fig.Dispose();
        }

        Console.WriteLine("Програма завершена.");
    }
}
