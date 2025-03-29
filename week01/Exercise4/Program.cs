using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        int number;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        do
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine());

            if (number != 0)
            {
                numbers.Add(number);
            }

        } while (number != 0);

        // Compute sum
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        Console.WriteLine("");
        Console.WriteLine($"The sum is: {sum}");

        // Compute average
        double average = (double)sum / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // Find max number
        int maxNumber = numbers[0];
        foreach (int num in numbers)
        {
            if (num > maxNumber)
            {
                maxNumber = num;
            }
        }
        Console.WriteLine($"The largest number is: {maxNumber}");
    }
}
// This program allows the user to enter a list of numbers, computes the sum, average, and finds the largest number.