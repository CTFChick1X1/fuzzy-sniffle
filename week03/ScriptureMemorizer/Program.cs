using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

/// Represents a Bible reference (e.g., "John 3:16" or "Proverbs 3:5-6").
class Reference
{
    public string Book { get; set; }
    public int StartChapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndChapter { get; set; }
    public int? EndVerse { get; set; }

    // Parameterless constructor for JSON serialization
    public Reference() {}

    public Reference(string reference)
    {
        string[] parts = reference.Split(' ');

        // Book name may contain multiple words, extract the last part as the verse reference
        string bookName = string.Join(" ", parts.Take(parts.Length - 1));
        string versePart = parts.Last();

        Book = bookName;
        string[] numbers = versePart.Split('-');
        string[] start = numbers[0].Split(':');
        StartChapter = int.Parse(start[0]);
        StartVerse = int.Parse(start[1]);

        if (numbers.Length > 1)
        {
            string[] end = numbers[1].Split(':');
            EndChapter = end.Length > 1 ? int.Parse(end[0]) : StartChapter;
            EndVerse = int.Parse(end.Length > 1 ? end[1] : numbers[1]);
        }
    }

    public override string ToString()
    {
        if (EndVerse.HasValue)
            return $"{Book} {StartChapter}:{StartVerse}-{EndVerse}";
        return $"{Book} {StartChapter}:{StartVerse}";
    }
}

/// Represents a single word in a verse, allowing hiding functionality.
class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    // Parameterless constructor for JSON serialization
    public Word() {}

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide() => IsHidden = true;
    public string GetDisplayText() => IsHidden ? new string('_', Text.Length) : Text;
}

/// Represents a verse with a reference and its words.
class Verse
{
    public Reference Reference { get; set; }
    public List<Word> Words { get; set; }

    // Parameterless constructor for JSON serialization
    public Verse() {}

    public Verse(string reference, string text)
    {
        Reference = new Reference(reference);
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        List<Word> visibleWords = Words.Where(w => !w.IsHidden).ToList();

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden() => Words.All(w => w.IsHidden);
    public string GetDisplayText() => Reference + " " + string.Join(" ", Words.Select(w => w.GetDisplayText()));
}

/// Manages a collection of verses.
class VerseCollection
{
    private List<Verse> verses = new List<Verse>();
    private const string FilePath = "verses.json";

    public void AddVerse(string reference, string text)
    {
        verses.Add(new Verse(reference, text));
        SaveToFile();
    }

    public void DisplayVerses()
    {
        foreach (var verse in verses)
        {
            Console.WriteLine(verse.GetDisplayText());
        }
    }

    public void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            try
            {
                var loadedVerses = JsonSerializer.Deserialize<List<Verse>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (loadedVerses != null)
                    verses = loadedVerses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading verses: {ex.Message}");
            }
        }
    }

    private void SaveToFile()
    {
        string json = JsonSerializer.Serialize(verses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public List<Verse> GetVerses() => verses;
}

/// Handles the memorization process.
class MemorizationSession
{
    public void Start(Verse verse)
    {
        while (!verse.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(verse.GetDisplayText() + "\n");
            Console.WriteLine("Press Enter to hide more words or type 'quit' to exit...");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit") break;
            verse.HideRandomWords(2);
        }

        Console.Clear();
        Console.WriteLine("All words are hidden!");
        Console.WriteLine(verse.GetDisplayText());
        Console.WriteLine("\nPress Enter to return to menu...");
        Console.ReadLine();
    }
}

/// Main program class.
class Program
{
    static void Main()
    {
        VerseCollection collection = new VerseCollection();
        collection.LoadFromFile();
        MemorizationSession memorizationSession = new MemorizationSession();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Add Verse\n2. Display Verses\n3. Start Memorization\n4. Exit");
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
                    Console.WriteLine("Press Enter to return...");
                    Console.ReadLine();
                    break;
                case "3":
                    if (collection.GetVerses().Count == 0)
                    {
                        Console.WriteLine("No verses available.");
                        Console.ReadLine();
                        break;
                    }

                    // Allow user to choose a verse
                    Console.WriteLine("Select a verse:");
                    for (int i = 0; i < collection.GetVerses().Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {collection.GetVerses()[i].Reference}");
                    }

                    Console.Write("Enter verse number: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= collection.GetVerses().Count)
                    {
                        memorizationSession.Start(collection.GetVerses()[index - 1]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                        Console.ReadLine();
                    }
                    break;

                case "4":
                    return;
            }
        }
    }
}
