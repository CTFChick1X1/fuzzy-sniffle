using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store all activities
        List<Activity> activities = new List<Activity>();

        // Add activities of different types
        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0)); // 3 miles
        activities.Add(new Cycling(new DateTime(2022, 11, 3), 30, 18.0)); // 18 mph
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 40)); // 40 laps

        // Iterate through the list and display the summary for each activity
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
