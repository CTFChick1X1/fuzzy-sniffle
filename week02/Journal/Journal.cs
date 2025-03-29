using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\n📖 Your journal is empty!\n");
            return;
        }

        foreach (var entry in _entries)
        {
            entry.Display();
        }
    }

    // ✅ Save journal entries to CSV without CsvHelper
    public void SaveToCsv(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                // Write header
                writer.WriteLine("Date,Prompt,Response");

                // Write each entry
                foreach (var entry in _entries)
                {
                    writer.WriteLine($"{entry.Date},{EscapeCsv(entry.Prompt)},{EscapeCsv(entry.Response)}");
                }
            }
            Console.WriteLine("\n✅ Journal successfully saved to CSV!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error saving file: {ex.Message}");
        }
    }

    // ✅ Load journal entries from CSV without CsvHelper
    public void LoadFromCsv(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("\n❌ File not found. Please check the filename and try again.\n");
            return;
        }

        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                _entries.Clear();
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine) // Skip header row
                    {
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');
                    if (values.Length >= 3)
                    {
                        Entry entry = new Entry
                        {
                            Date = values[0],
                            Prompt = UnescapeCsv(values[1]),
                            Response = UnescapeCsv(values[2])
                        };
                        _entries.Add(entry);
                    }
                }
            }
            Console.WriteLine("\n✅ Journal successfully loaded from CSV!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error loading file: {ex.Message}");
        }
    }

    // ✅ Handle commas in CSV fields by wrapping them in quotes
    private string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = "\"" + field.Replace("\"", "\"\"") + "\""; // Escape double quotes
        }
        return field;
    }

    // ✅ Handle removing extra quotes when reading CSV
    private string UnescapeCsv(string field)
    {
        if (field.StartsWith("\"") && field.EndsWith("\""))
        {
            field = field.Substring(1, field.Length - 2).Replace("\"\"", "\"");
        }
        return field;
    }
}
