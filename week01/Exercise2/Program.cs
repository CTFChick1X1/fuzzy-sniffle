using System;

// grades based on percentage

public class Program
{
    public static void Main()
    {
        // Request user to enter their marks
        Console.WriteLine("Enter your percentage marks: ");
        string marks = Console.ReadLine() ?? "Unknown";

        // Convert the string to a percentage
        int percentage = int.Parse(marks);

        // Determine the grade based on the percentage
        string grade = "";

        // Chech grades based on percentage
        if (percentage >= 90)
        {
            grade = "A";
            Console.WriteLine($"Your grade is {grade}");
        }
        else if (percentage >= 80)
        {
            grade = "B";
            Console.WriteLine($"Your grade is {grade}");
        }
        else if (percentage >= 70)
        {
            grade = "C";
            Console.WriteLine($"Your grade is {grade}");
        }
        else if (percentage >= 60)
        {
            grade = "D";
            Console.WriteLine($"Your grade is {grade}");
        }
        else
        {
            grade = "F";
            Console.WriteLine($"Your grade is {grade}");
        }


        // Printing a message to congratulate the student if grade >= 70
        // else, print a message to inform the student of their failure
        if (grade == "A" || grade == "B" || grade == "C")
        {
            Console.WriteLine("Congratulations! You have passed the exam.");
        }
        else
        {
            Console.WriteLine("Sorry! You have failed the exam.");
        }
        

        // If the percentage is not within the specified range, print an error message
        if (percentage < 0 || percentage > 100)
        {
            Console.WriteLine("Invalid percentage. Please enter a number between 0 and 100.");
        }
    }
}