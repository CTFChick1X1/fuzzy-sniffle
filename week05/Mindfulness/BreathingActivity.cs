public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
        : base("Breathing Activity",
               "This activity will help you relax by guiding you through slow breathing. Clear your mind and focus on your breath.") { }

    public void Run()
    {
        ShowStartMessage();

        int cycleTime = _duration >= 6 ? 6 : _duration; // If short time, adjust cycle size
        int cycles = _duration / cycleTime;

        for (int i = 0; i < cycles; i++)
        {
            AnimateBreath("Breathe in...", 3, true, ConsoleColor.Green);  // Set color for "Breathe in..."
            AnimateBreath("Breathe out...", 3, false, ConsoleColor.Red);  // Set color for "Breathe out..."
        }

        ShowEndMessage();
    }

    private void AnimateBreath(string message, int duration, bool expanding, ConsoleColor color)
    {
        Console.WriteLine();
        Console.ForegroundColor = color;  // Set the color for breathing text

        for (int i = 0; i < duration * 2; i++) // 2 "frames" per second
        {
            int pulse = expanding ? i : duration * 2 - i;
            int spaces = Math.Abs(pulse - duration);

            Console.Write("\r" + new string(' ', spaces) + message);
            Thread.Sleep(500);
        }

        Console.ResetColor();  // Reset color to default after breathing message
        Console.WriteLine(); // Move to next line after pulse
    }
}
