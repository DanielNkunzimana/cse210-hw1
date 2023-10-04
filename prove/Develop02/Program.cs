using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public DateTime Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private List<string> prompts = new List<string>
    {
        "What is something that distracted me or slowed my progress today?",
        "What are you proud of yourself for today?",
        "What is something kind you have seen today?",
        "Did you try to pray for your day when you wake up?",
        "How can I make tomorrow better?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void WriteEntry()
    {
        Console.WriteLine("Writing a new entry...");

        Random random = new Random();
        int randomIndex = random.Next(prompts.Count);

        Console.WriteLine("Prompt: " + prompts[randomIndex]);
        Console.Write("Your Response: ");
        string response = Console.ReadLine();

        JournalEntry entry = new JournalEntry
        {
            Date = DateTime.Now,
            Prompt = prompts[randomIndex],
            Response = response
        };

        entries.Add(entry);
        Console.WriteLine("Entry added successfully!");
    }

    public void DisplayJournal()
    {
        Console.WriteLine("\nJournal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"Date: {entry.Date}");
                writer.WriteLine($"Prompt: {entry.Prompt}");
                writer.WriteLine($"Response: {entry.Response}");
                writer.WriteLine();
            }
        }
        Console.WriteLine($"Journal saved to {filename} successfully!");
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            entries.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    JournalEntry entry = new JournalEntry();
                    entry.Date = DateTime.Parse(reader.ReadLine().Substring(6)); // Skip "Date: "
                    entry.Prompt = reader.ReadLine().Substring(8); // Skip "Prompt: "
                    entry.Response = reader.ReadLine().Substring(10); // Skip "Response: "
                    entries.Add(entry);
                    reader.ReadLine(); // Skip empty line
                }
            }
            Console.WriteLine($"Journal loaded from {filename} successfully!");
        }
        else
        {
            Console.WriteLine($"File {filename} not found.");
        }
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        string filename = "journal.txt";

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a New Entry");
            Console.WriteLine("2. Display the Journal");
            Console.WriteLine("3. Save the Journal to a File");
            Console.WriteLine("4. Load the Journal from a File");
            Console.WriteLine("5. Quit");

            Console.Write("please what would you like to do (1/2/3/4/5)?: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter the filename to save the journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter the filename to load the journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, 3, 4, or 5.");
                    break;
            }
        }
    }
}
