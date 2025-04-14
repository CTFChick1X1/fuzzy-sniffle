

// SimpleGoal.cs
using System;

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false) : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
        Console.WriteLine($"You earned {_points} points!");
    }

    public override string GetDetailsString()
    {
        return ($"[{(_isComplete ? "X" : " ")}] {_name} ({_description})");
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }

    public override bool IsComplete() => _isComplete;
}

