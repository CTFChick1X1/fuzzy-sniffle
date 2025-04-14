// Goal.cs (Base Class)
using System;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public virtual string GetDetailsString() => $"[ ] {_name} ({_description})";
    public abstract string GetStringRepresentation();
    public abstract bool IsComplete();
    public int GetPoints() => _points;
}
