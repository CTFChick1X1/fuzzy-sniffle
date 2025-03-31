using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Reference
{
    public string Book { get; set; }
    public int StartChapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndChapter { get; set; }
    public int? EndVerse { get; set; }

    public Reference() { }

    public Reference(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference) || !reference.Contains(':'))
            throw new ArgumentException("Invalid reference format. Use 'Book Chapter:Verse' or 'Book Chapter:Verse-Verse'.");

        string[] parts = reference.Split(' ');
        if (parts.Length < 2)
            throw new ArgumentException("Invalid reference format. Ensure the book name and chapter:verse are included.");

        string bookName = string.Join(" ", parts.Take(parts.Length - 1));
        string versePart = parts.Last();
        string[] numbers = versePart.Split('-');

        if (!numbers[0].Contains(':'))
            throw new ArgumentException("Invalid reference format. Ensure chapter and verse are separated by a colon.");

        string[] start = numbers[0].Split(':');

        if (!int.TryParse(start[0], out int startChap) || !int.TryParse(start[1], out int startVers))
            throw new ArgumentException("Chapter and verse must be valid numbers.");

        Book = bookName;
        StartChapter = startChap;
        StartVerse = startVers;

        if (numbers.Length > 1)
        {
            string[] end = numbers[1].Split(':');
            if (end.Length > 1)
            {
                if (!int.TryParse(end[0], out int endChap) || !int.TryParse(end[1], out int endVers))
                    throw new ArgumentException("Invalid end chapter or verse format.");

                EndChapter = endChap;
                EndVerse = endVers;
            }
            else
            {
                if (!int.TryParse(numbers[1], out int endVers))
                    throw new ArgumentException("Invalid end verse format.");

                EndChapter = StartChapter;
                EndVerse = endVers;
            }
        }
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {StartChapter}:{StartVerse}-{EndVerse}" : $"{Book} {StartChapter}:{StartVerse}";
    }
}

class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    public Word() { }
    public Word(string text) { Text = text; IsHidden = false; }
    public void Hide() => IsHidden = true;
    public void Unhide() => IsHidden = false;
    public string GetDisplayText() => IsHidden ? new string('_', Text.Length) : Text;
}

class Verse
{
    public Reference Reference { get; set; }
    public List<Word> Words { get; set; }
    private static Random random = new Random();

    public Verse() { }
    public Verse(string reference, string text)
    {
        Reference = new Reference(reference);
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        var visibleWords = Words.Where(w => !w.IsHidden).ToList();
        var hiddenIndices = new HashSet<int>();

        while (hiddenIndices.Count < count && hiddenIndices.Count < visibleWords.Count)
        {
            int index;
            do { index = random.Next(visibleWords.Count); } while (hiddenIndices.Contains(index));

            visibleWords[index].Hide();
            hiddenIndices.Add(index);
        }
    }

    public void UnhideAllWords()
    {
        foreach (var word in Words) word.Unhide();
    }

    public bool AllWordsHidden() => Words.All(w => w.IsHidden);

    public string GetDisplayText()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(Reference + " ");
        Console.ResetColor();

        foreach (var word in Words)
        {
            Console.ForegroundColor = word.IsHidden ? ConsoleColor.DarkGray : ConsoleColor.Green;
            Console.Write(word.GetDisplayText() + " ");
            Console.ResetColor();
        }

        return "";
    }
}

class VerseCollection
{
    private List<Verse> verses = new List<Verse>();
    private const string FilePath = "verses.json";
    private static Random random = new Random();

    public void AddVerse(string reference, string text)
    {
        verses.Add(new Verse(reference, text));
        SaveToFile();
    }

    public void DisplayVerses()
    {
        foreach (var verse in verses)
        {
            verse.GetDisplayText();
            Console.WriteLine();
        }
    }

    public void LoadFromFile()
    {
        if (!File.Exists(FilePath)) return;
        try
        {
            string json = File.ReadAllText(FilePath);
            verses = JsonSerializer.Deserialize<List<Verse>>(json) ?? new List<Verse>();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading verses: {ex.Message}");
            Console.ResetColor();
            verses = new List<Verse>();
        }
    }

    private void SaveToFile()
    {
        string json = JsonSerializer.Serialize(verses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public Verse GetRandomVerse()
    {
        return verses.Count > 0 ? verses[random.Next(verses.Count)] : null;
    }
}

class MemorizationSession
{
    public void Start(Verse verse)
    {
        if (verse == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No verses available for memorization.");
            Console.ResetColor();
            Console.ReadLine();
            return;
        }

        Console.Write("\nHow many words would you like to hide per round? ");
        if (!int.TryParse(Console.ReadLine(), out int wordsToHide) || wordsToHide < 1)
        {
            wordsToHide = 2;
        }

        while (!verse.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine();
            verse.GetDisplayText();
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Enter to hide more words or type 'unhide' to reveal them...");
            Console.ResetColor();

            string input = Console.ReadLine();

            if (input.ToLower() == "unhide") verse.UnhideAllWords();
            else verse.HideRandomWords(wordsToHide);
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("All words are hidden!");
        verse.GetDisplayText();
        Console.ResetColor();
        Console.WriteLine("\nPress Enter to return to menu...");
        Console.ReadLine();
    }
}

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
            Console.WriteLine("1. Add Scripture");
            Console.WriteLine("2. Display Scripture");
            Console.WriteLine("3. Start Memorization");
            Console.WriteLine("4. Exit");
            Console.Write("Enter Your Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Reference: ");
                    string reference = Console.ReadLine();
                    
                    Console.WriteLine("Enter the Verse (press Enter twice to finish):");
                    string text = "";
                    string line;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        text += line + " ";
                    }

                    collection.AddVerse(reference, text.Trim());
                    break;
                case "2":
                    collection.DisplayVerses();
                    Console.ReadLine();
                    break;
                case "3":
                    memorizationSession.Start(collection.GetRandomVerse());
                    break;
                case "4":
                    return;
            }
        }
    }
}
