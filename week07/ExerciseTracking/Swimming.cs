using System;

public class Swimming : Activity
{
    private int _laps; // Laps in the pool

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return (_laps * 50) / 1000.0; // Distance = laps * 50 meters (converted to kilometers)
    }

    public override double GetSpeed()
    {
        double distance = GetDistance();
        return (distance / _minutes) * 60; // Speed = distance / time (converted to hours)
    }

    public override double GetPace()
    {
        double distance = GetDistance();
        return (double)_minutes / distance; // Pace = time / distance
    }
}
