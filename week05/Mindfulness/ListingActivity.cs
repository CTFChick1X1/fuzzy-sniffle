public class ListingActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string> {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing Activity", 
               "This activity will help you reflect on the good things in your life by listing items.") { }

    public void Run()
    {
        ShowStartMessage();
        Console.WriteLine($"\n{_prompts[new Random().Next(_prompts.Count)]}");
        Console.WriteLine("You may begin in:");
        ShowCountdown(5);

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            items.Add(input);
        }

        Console.WriteLine($"\nYou listed {items.Count} items!");
        ShowEndMessage();
    }
}
// Note: The above code assumes that the MindfulnessActivity class and its methods (ShowStartMessage, ShowEndMessage, ShowSpinner, ShowCountdown) are defined
