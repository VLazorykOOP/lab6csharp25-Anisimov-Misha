using System;
using System.Collections;

interface IFigure
{
    double GetArea();
    double GetPerimeter();
    void ShowInfo();
}

// Власний виняток
public class InvalidFigureDataException : Exception
{
    public InvalidFigureDataException(string message) : base(message) { }
}

// Прямокутник
class Rectangle : IFigure
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        if (width <= 0 || height <= 0)
            throw new InvalidFigureDataException("Сторони прямокутника мають бути додатними.");
        Width = width;
        Height = height;
    }

    public double GetArea() => Width * Height;
    public double GetPerimeter() => 2 * (Width + Height);
    public void ShowInfo()
    {
        Console.WriteLine($"[Rectangle] Width: {Width}, Height: {Height}, Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
    }
}

// Коло
class Circle : IFigure
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        if (radius <= 0)
            throw new InvalidFigureDataException("Радіус має бути додатнім.");
        Radius = radius;
    }

    public double GetArea() => Math.PI * Radius * Radius;
    public double GetPerimeter() => 2 * Math.PI * Radius;
    public void ShowInfo()
    {
        Console.WriteLine($"[Circle] Radius: {Radius}, Area: {GetArea():F2}, Perimeter: {GetPerimeter():F2}");
    }
}

class Program
{
    static void Main()
    {
        try
        {
            // 1. Перевірка користувацького винятку
            Console.WriteLine("== Перевірка введення фігур ==");
            IFigure goodCircle = new Circle(3);
            IFigure badRectangle = new Rectangle(-1, 4);  // викличе виняток
        }
        catch (InvalidFigureDataException ex)
        {
            Console.WriteLine($"[Власний виняток] {ex.Message}");
        }

        try
        {
            // 2. Обробка ArrayTypeMismatchException
            Console.WriteLine("\n== Перевірка помилкового присвоєння в масиві ==");

            string[] stringArray = new string[2];
            object[] objectArray = stringArray;

            objectArray[0] = "text"; // OK
            objectArray[1] = 42;     // викликає ArrayTypeMismatchException
        }
        catch (ArrayTypeMismatchException ex)
        {
            Console.WriteLine($"[Стандартний виняток] {ex.GetType().Name}: {ex.Message}");
        }

        Console.WriteLine("\n== Програма завершена ==");
    }
}
