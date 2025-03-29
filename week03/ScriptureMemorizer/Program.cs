using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading;


/// Represents a Bible verse with a reference and text.
class Verse
{
    public string Reference { get; private set; }
    public string Text { get; private set; }

    public Verse(string reference, string text)
    {
        Reference = reference;
        Text = text;
    }
}
/// Manages a collection of verses, including adding, displaying, searching, saving, and loading from a file.
class VerseCollection
{
    private readonly List<Verse> verses = new List<Verse>();
    private const string FilePath = "verses.json";


    /// Adds a new verse to the collection and saves it to a file.
    public void AddVerse(string reference, string text)
    {
        verses.Add(new Verse(reference, text));
        SaveToFile();
    }

    
    /// Displays all verses in the collection with pagination and color formatting.
    public void DisplayVerses()
    {
        if (verses.Count == 0)
        {
            Console.WriteLine("No verses available.");
            return;
        }

        int pageSize = 5;
        int totalPages = (int)Math.Ceiling(verses.Count / (double)pageSize);
        int currentPage = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Page {currentPage + 1} of {totalPages}\n");
            
            var pageVerses = verses.Skip(currentPage * pageSize).Take(pageSize);
            
            foreach (var verse in pageVerses)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(verse.Reference + ": ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(verse.Text + "\n");
                Console.ResetColor();
            }
            
            Console.WriteLine("Use arrow keys to navigate, or press Enter to exit.");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.RightArrow && currentPage < totalPages - 1)
                currentPage++;
            else if (key == ConsoleKey.LeftArrow && currentPage > 0)
                currentPage--;
            else if (key == ConsoleKey.Enter)
                break;
        }
    }

    
    /// Searches for verses by reference or keyword.
    public void SearchVerse(string query)
    {
        var results = verses.Where(v => v.Reference.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                         v.Text.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        
        if (results.Count == 0)
        {
            Console.WriteLine("No matching verses found.");
        }
        else
        {
            foreach (var verse in results)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(verse.Reference + ": ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(verse.Text + "\n");
                Console.ResetColor();
            }
        }
    }

  
    /// Saves the verse collection to a JSON file.
    private void SaveToFile()
    {
        string json = JsonSerializer.Serialize(verses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

   
    /// Loads verses from a JSON file if it exists.
    
    public void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            var loadedVerses = JsonSerializer.Deserialize<List<Verse>>(json);
            if (loadedVerses != null)
            {
                verses.Clear();
                verses.AddRange(loadedVerses);
            }
        }
    }


    /// Returns a copy of the list of verses to maintain encapsulation.
    public List<Verse> GetVerses() => new List<Verse>(verses);
}


/// Handles the memorization process.
class MemorizationSession
{
    public void Start(List<Verse> verses)
    {
        foreach (var verse in verses)
        {
            Console.Clear();
            Console.WriteLine($"Memorizing: {verse.Reference}\n");
            string hiddenText = HideWords(verse.Text);
            Console.WriteLine(hiddenText);
            
            Console.WriteLine("Press Enter to reveal the full verse...");
            Console.ReadLine();
            Console.WriteLine(verse.Text + "\n");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    private string HideWords(string text)
    {
        string[] words = text.Split(' ');
        Random random = new Random();
        for (int i = 0; i < words.Length / 2; i++)
        {
            int index = random.Next(words.Length);
            words[index] = new string('_', words[index].Length);
        }
        return string.Join(" ", words);
    }
}


/// The main program entry point, providing a menu-driven interface for the user.
class Program
{
    static void Main()
    {
        VerseCollection collection = new VerseCollection();
        collection.LoadFromFile();
        MemorizationSession memorizationSession = new MemorizationSession();
        
        while (true)
        {
            Console.WriteLine("1. Add Verse\n2. Display Verses\n3. Search Verse\n4. Start Memorization\n5. Exit");
            Console.Write("Enter Your Choice: ");
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Console.Write("Enter Reference: ");
                    string reference = Console.ReadLine();
                    Console.Write("Enter Verse: ");
                    string text = Console.ReadLine();
                    collection.AddVerse(reference, text);
                    break;
                case "2":
                    collection.DisplayVerses();
                    break;
                case "3":
                    Console.Write("Enter search query: ");
                    string query = Console.ReadLine();
                    collection.SearchVerse(query);
                    break;
                case "4":
                    memorizationSession.Start(collection.GetVerses());
                    break;
                case "5":
                    return;
            }
        }
    }
}
