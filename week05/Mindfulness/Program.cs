class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new BreathingActivity().Run();
                    break;
                case "2":
                    new ReflectionActivity().Run();
                    break;
                case "3":
                    new ListingActivity().Run();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}


// ------------------------
// Enhancements Description
// ------------------------
//
// Enhanced Feature: Animated "pulsing" breathing text
// Location: BreathingActivity.cs
//
// Description:
// Instead of using a basic countdown ("Breathe in... 3, 2, 1"), the program now uses
// a visually dynamic pulsing text animation that expands and contracts to simulate
// inhaling and exhaling. This creates a more immersive and calming experience for the user.
//
// Implementation:
// - Added a private helper method AnimateBreath() to the BreathingActivity class.
// - This method gradually increases and decreases the indentation of the breathing message
//   using spaces and a timed loop to create a “pulse” animation.
// - The Run() method was updated to use AnimateBreath() for both "Breathe in..." and
//   "Breathe out..." cycles.
//
// Result:
// The console output mimics the sensation of breathing by visually “growing” and “shrinking” 
// the breathing text, giving users a smoother, more mindful breathing experience.
//
// Example Output:
//        Breathe in...
//       Breathe in...
//      Breathe in...
//     Breathe in...
//      Breathe in...
//       Breathe in...
//        Breathe in...
//
// This enhancement goes beyond core requirements by adding a creative and calming animation.
// ------------------------
