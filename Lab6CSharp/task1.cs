using System;
using System.Collections.Generic;

// Власний інтерфейс
interface IShowable
{
    void Show();
}

// Абстрактний базовий клас Person
abstract class Person : IShowable, IComparable, IDisposable
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Індексатор (за умовою)
    private List<string> notes = new List<string>();
    public string this[int index]
    {
        get => (index >= 0 && index < notes.Count) ? notes[index] : "Немає нотатки";
        set
        {
            if (index >= 0 && index < notes.Count)
                notes[index] = value;
            else
                notes.Add(value);
        }
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public abstract void Show(); // абстрактний метод

    public virtual void Greet() // віртуальний метод
    {
        Console.WriteLine($"Hello, I am {Name}.");
    }

    public int CompareTo(object obj) // інтерфейс IComparable
    {
        if (obj is Person p)
            return this.Age.CompareTo(p.Age);
        throw new ArgumentException("Object is not a Person");
    }

    public void Dispose() // IDisposable
    {
        Console.WriteLine($"{Name} resources released.");
    }

    ~Person()
    {
        Console.WriteLine($"Destructor for {Name} called.");
    }
}

// Student — похідний клас
class Student : Person
{
    public string University { get; set; }

    public Student(string name, int age, string university)
        : base(name, age)
    {
        University = university;
    }

    public override void Show()
    {
        Console.WriteLine($"[Student] Name: {Name}, Age: {Age}, University: {University}");
    }

    public override void Greet()
    {
        Console.WriteLine($"Hi, I'm student {Name} from {University}.");
    }
}

// Teacher — похідний клас
class Teacher : Person
{
    public string Subject { get; set; }

    public Teacher(string name, int age, string subject)
        : base(name, age)
    {
        Subject = subject;
    }

    public override void Show()
    {
        Console.WriteLine($"[Teacher] Name: {Name}, Age: {Age}, Subject: {Subject}");
    }
}

// DepartmentHead — похідний від Teacher
class DepartmentHead : Teacher
{
    public string Department { get; set; }

    public DepartmentHead(string name, int age, string subject, string department)
        : base(name, age, subject)
    {
        Department = department;
    }

    public override void Show()
    {
        Console.WriteLine($"[DeptHead] Name: {Name}, Age: {Age}, Subject: {Subject}, Department: {Department}");
    }

    public void HoldMeeting()
    {
        Console.WriteLine($"{Name} is holding a department meeting.");
    }
}

// Програма тестування
class Program
{
    static void Main()
    {
        Person[] people = new Person[]
        {
            new Student("Іван", 20, "КПІ"),
            new Teacher("Олена", 45, "Математика"),
            new DepartmentHead("Сергій", 50, "Фізика", "Кафедра прикладної фізики")
        };

        foreach (var person in people)
        {
            Console.WriteLine("==============");
            person.Show();
            person.Greet();

            // Тест індексатора
            person[0] = "Перша нотатка";
            person[1] = "Друга нотатка";
            Console.WriteLine($"Нотатка[0]: {person[0]}");

            // Використання is, as, typeof
            if (person is Student s)
                Console.WriteLine("Це студент (is)");

            if (person as Teacher != null && !(person is DepartmentHead))
                Console.WriteLine("Це викладач (as)");

            if (person.GetType() == typeof(DepartmentHead))
            {
                Console.WriteLine("Це завідувач кафедри (typeof)");
                DepartmentHead head = (DepartmentHead)person;
                head.HoldMeeting();
            }

            Console.WriteLine();
        }

        // Порівняння
        Console.WriteLine($"Порівняння студента і викладача за віком: {people[0].CompareTo(people[1])}");

        // Виклик Dispose()
        foreach (var p in people)
            p.Dispose();

        Console.WriteLine("Програма завершила роботу.");
    }
}
