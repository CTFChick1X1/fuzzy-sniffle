using System;

public class Running : Activity
{
    private double _distance; // in miles

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / _minutes) * 60; // Speed = distance / time (converted to hours)
    }

    public override double GetPace()
    {
        return (double)_minutes / _distance; // Pace = time / distance
    }
}
