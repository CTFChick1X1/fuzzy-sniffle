using System;

// Write a program that generates a random number between 1 and 100 and asks the user to guess it.
// If the user's guess is higher than the random number, the program should display "Lower".
// If the user's guess is lower than the random number, the program should display "Higher".
class Program
{
    static void Main()
    {
        Random random = new Random();
        bool playAgain = true;

        while (playAgain)
        {
            int magicNumber = random.Next(1, 101); // Generates a random number between 1 and 100
            int guess = 0;
            int guessCount = 0;

            Console.WriteLine("Welcome to the Magic Number Game!");

            while (guess != magicNumber)
            {
                Console.Write("What is the magic number? ");

                // Try to parse the user's input as an integer

               try
                {
                     guess = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }


                // Increment the guess count for each incorrect guess
                guessCount++;

                // Check if the guess is higher or lower than the magic number

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("Congratulations! You found the magic number!");
                    Console.WriteLine($"The magic number was {magicNumber}");

                    Console.WriteLine($"");
                    Console.WriteLine($"You guessed it in {guessCount} tries!");
                }
            }

            // Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");

            string response = Console.ReadLine().ToLower();
            playAgain = response == "yes";
        }

        Console.WriteLine("Thanks for playing! Goodbye!");
    }
}
// dotnet run --project week01/Exercise3/Exercise3.csproj
// dotnet sln CSE210.sln add week01/Exercise3/Exercise3.csproj