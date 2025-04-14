class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _completedCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _completedCount = 0;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _completedCount++;
        int totalPoints = _points;
        if (_completedCount == _targetCount)
        {
            totalPoints += _bonus;
            Console.WriteLine($"Bonus! You earned an extra {_bonus} points!");
        }
        Console.WriteLine($"You earned {totalPoints} points!");
    }

    public override string GetDetailsString()
    {
        return ($"[{(_completedCount >= _targetCount ? "X" : " ")}] {_name} ({_description}) -- Completed {_completedCount}/{_targetCount} times");
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_bonus}|{_targetCount}|{_completedCount}";
    }

    public override bool IsComplete() => _completedCount >= _targetCount;
}
