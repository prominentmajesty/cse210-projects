using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// From line 72 to 75, i showed my creativity and exeeding requirement by prompting to the user if the user would like to add anything to the journal and if there is still any space or time to write.
// From line 78 to line 89, i created an instance of my entry, add the documents, and then save the documents.
// I created a method called DisplayJournal from line 92 to line 103 to display my documents
// Also from line 123 to 161, i created a method called LoadJournal to load the saved journal or documents

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Welcome to the Journal Program!");
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteNewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    journal.LoadJournal();
                    break;
                case "4":
                    journal.SaveJournal();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void WriteNewEntry()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        
        // Additional ideas to address journaling challenges
        Console.WriteLine("Do you feel like adding anything to your journal today? (yes/no)");
        string motivation = Console.ReadLine();
        Console.WriteLine("Is there still any space or time to write today? (yes/no)");
        string time = Console.ReadLine();

        Entry entry = new Entry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            Motivation = motivation,
            Time = time
        };

        entries.Add(entry);
        Console.WriteLine("Entry saved successfully!");
    }

    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine($"Motivation: {entry.Motivation}");
            Console.WriteLine($"Time: {entry.Time}");
            Console.WriteLine();
        }
    }

    public void SaveJournal()
    {
        Console.Write("Enter a filename to save the journal: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Date,Prompt,Response,Motivation,Time");
            foreach (var entry in entries)
            {
                writer.WriteLine($"\"{entry.Date}\",\"{entry.Prompt}\",\"{entry.Response}\",\"{entry.Motivation}\",\"{entry.Time}\"");
            }
        }

        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadJournal()
    {
        Console.Write("Enter a filename to load the journal: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            entries.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                // Skip header
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');

                    Entry entry = new Entry
                    {
                        Date = parts[0].Trim('"'),
                        Prompt = parts[1].Trim('"'),
                        Response = parts[2].Trim('"'),
                        Motivation = parts[3].Trim('"'),
                        Time = parts[4].Trim('"')
                    };

                    entries.Add(entry);
                }
            }

            Console.WriteLine("Journal loaded successfully!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Motivation { get; set; }
    public string Time { get; set; }
}