using System;
using System.Collections.Generic;

class Comment
{
    public string Username { get; }
    public string Text { get; }

    public Comment(string username, string text)
    {
        Username = username;
        Text = text;
    }
}

class Video
{
    public string Title { get; }
    public string Author { get; }
    public int LengthInSeconds { get; }
    private List<Comment> comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        comments = new List<Comment>();
    }

    public void AddComment(string username, string text)
    {
        comments.Add(new Comment(username, text));
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.Username}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>
        {
            new Video("Understanding OOP", "TechGuru", 600),
            new Video("C# Basics for Beginners", "CodeMaster", 720),
            new Video("Advanced C# Patterns", "DevExpert", 900)
        };

        videos[0].AddComment("Alice", "Great explanation!");
        videos[0].AddComment("Bob", "Very clear and helpful.");
        videos[0].AddComment("Charlie", "I finally understand OOP!");

        videos[1].AddComment("Dave", "Nice tutorial!");
        videos[1].AddComment("Eve", "This helped a lot, thanks!");
        videos[1].AddComment("Frank", "Could you cover more examples?");

        videos[2].AddComment("Grace", "I love design patterns!");
        videos[2].AddComment("Heidi", "This was super informative.");
        videos[2].AddComment("Ivan", "Do you have a part two?");

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
