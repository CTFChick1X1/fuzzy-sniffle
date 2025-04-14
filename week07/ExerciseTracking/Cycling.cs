using System;

public class Cycling : Activity
{
    private double _speed; // in miles per hour

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return _speed * _minutes / 60; // Distance = speed * time (converted to hours)
    }

    public override double GetSpeed()
    {
        return _speed; // Speed is already given in mph
    }

    public override double GetPace()
    {
        return 60 / _speed; // Pace = 60 / speed
    }
}
