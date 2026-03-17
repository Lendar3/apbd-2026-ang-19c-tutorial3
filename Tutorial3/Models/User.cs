namespace Tutorial3.Models;
public abstract class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public abstract int MaxRentals { get; }

    protected User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class Student : User
{
    public Student(string firstName, string lastName) : base(firstName, lastName) { }
    public override int MaxRentals => 2; // Правило: студент максимум 2 вещи
}

public class Employee : User
{
    public Employee(string firstName, string lastName) : base(firstName, lastName) { }
    public override int MaxRentals => 5; // Правило: сотрудник максимум 5 вещей
}