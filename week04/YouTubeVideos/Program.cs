using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // in seconds
    private List<Comment> comments = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(string commenter, string text)
    {
        comments.Add(new Comment(commenter, text));
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Length: {Length} sec, Comments: {GetCommentCount()}");
        foreach (var comment in comments)
        {
            Console.WriteLine($"{comment.Commenter}: {comment.Text}");
        }
    }
}

class Comment
{
    public string Commenter { get; set; }
    public string Text { get; set; }

    public Comment(string commenter, string text)
    {
        Commenter = commenter;
        Text = text;
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        Video v1 = new Video("Intro to C#", "John Doe", 300);
        v1.AddComment("Alice", "Great tutorial!");
        v1.AddComment("Bob", "Very informative.");
        
        Video v2 = new Video("OOP in C#", "Jane Smith", 450);
        v2.AddComment("Charlie", "Thanks for explaining!");
        v2.AddComment("Dana", "Super helpful.");
        
        videos.Add(v1);
        videos.Add(v2);

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine();
        }
    }
}