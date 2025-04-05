public class ReflectionActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string> {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string> {
        "Why was this experience meaningful to you?",
        "What did you learn about yourself?",
        "How did you get started?",
        "What is your favorite thing about this experience?"
    };

    public ReflectionActivity() 
        : base("Reflection Activity", 
               "This activity will help you reflect on times in your life when you have shown strength and resilience.") { }

    public void Run()
    {
        ShowStartMessage();
        Console.WriteLine($"\n{_prompts[new Random().Next(_prompts.Count)]}");
        ShowSpinner(5);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        Random rand = new Random();

        while (DateTime.Now < endTime)
        {
            Console.WriteLine($"\n> {_questions[rand.Next(_questions.Count)]}");
            ShowSpinner(5);
        }

        ShowEndMessage();
    }
}
