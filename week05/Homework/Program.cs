class Program
{
    static void Main(string[] args)
    {

        // Create Assignment instance and display its information
        Console.WriteLine("\n");
        Assignment a1 = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(a1.GetSummary());
        Console.WriteLine("\n");

        // Create a MathAssignment instance and display its summary and homework list
        MathAssignment math = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(math.GetSummary());
        Console.WriteLine(math.GetHomeworkList());
        Console.WriteLine("\n");

        // Create a WritingAssignment instance and display its summary and writing information
        WritingAssignment writing = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(writing.GetSummary());
        Console.WriteLine(writing.GetWritingInformation());
    }
}
// Output: