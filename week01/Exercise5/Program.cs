using System;

class Program
{
    static void Main()
    {
        DisplayWelcome();
        // Prompt the user for their name and favorite number

        string userName = PromptUserName();
        int favoriteNumber = PromptUserNumber();
        // Calculate and display the square of the user's favorite number
        int squaredNumber = SquareNumber(favoriteNumber);

        DisplayResult(userName, squaredNumber);
    }

    // Function to display welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // Function to get user's name
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Function to get user's favorite number
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    //Function to square the number
    static int SquareNumber(int number)
    {
        return number * number;
    }

    //Function to display the result
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }
}
