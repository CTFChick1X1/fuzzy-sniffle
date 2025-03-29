
using System;

// Declare and initialize variables for first name and last name

// dotnet sln CSE210.sln add week01/Exercise2/Exercise2.csproj

class Program

{
    static void Main(string[] args)
    {
        // Declare variables for first name and last name
        string firstname;
        string lastname;

        // Ask for user input for first name and last name
        Console.Write("What is your first name: ");
        firstname = Console.ReadLine() ?? "Unknown";

        Console.Write("What is your last name: ");
        lastname = Console.ReadLine() ?? "Unknown";

        // Write both last name and first name in the same line
        Console.WriteLine($"Your name is {lastname},  {firstname} {lastname}!");
    }
}

