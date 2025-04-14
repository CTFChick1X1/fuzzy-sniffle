// EternalGoal.cs
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) {}

    public override void RecordEvent()
    {
        Console.WriteLine($"You earned {_points} points!");
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}";
    }

    public override bool IsComplete() => false;
}