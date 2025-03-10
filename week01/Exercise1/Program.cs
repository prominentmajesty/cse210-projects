using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Your first Name");
        string firstName = Console.ReadLine();

        Console.Write("Enter Your Last Name");
        string lastName = Console.ReadLine();

        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}");

    }
}