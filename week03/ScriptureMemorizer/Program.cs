
/* In my creativity exceeding requirment, I did the following

    I created a class called ScriptureLibrary which contains the cods that create a liberary of scriptures and also the codes that Choose scriptures at random to present to the user and a menu system to navigate between options.

    1. I created a Program class in which in the Main methos, I prompted the user with options to either Practice a random scripture by pressing 1 or Add a new scripture by pressing 2 or exit by pressing 3
    or choose option. This is what the program will first show the user when the user first lunch the program. Then when the user selects an option, the user can now continue with program.    
    
*/
using System;
using System.Collections.Generic;
using System.IO;

// Single word in the script
public class ScriptureWord
{
    private string _text;
    private bool _isHidden;
    public void Show()
    {
        _isHidden = false;
    }

    public ScriptureWord(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetOriginalText()
    {
        return _text;
    }
}

// Class to represent the scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6")
public class ScriptureReference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int? _verseEnd;

    // Constructor for single verse
    public ScriptureReference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verse;
        _verseEnd = null;
    }

    // Constructor for verse range
    public ScriptureReference(string book, int chapter, int verseStart, int verseEnd)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    // Constructor that parses from string (e.g., "John 3:16" or "Proverbs 3:5-6")
    public ScriptureReference(string reference)
    {
        var parts = reference.Split(' ', ':', '-');
        _book = parts[0];
        _chapter = int.Parse(parts[1]);
        _verseStart = int.Parse(parts[2]);
        _verseEnd = parts.Length > 3 ? int.Parse(parts[3]) : null;
    }

    public string GetDisplayText()
    {
        return _verseEnd.HasValue ?
            $"{_book} {_chapter}:{_verseStart}-{_verseEnd}" :
            $"{_book} {_chapter}:{_verseStart}";
    }
}

// Class to represent the entire scripture
public class Scripture
{
    private ScriptureReference _reference;
    private List<ScriptureWord> _words;
    private List<int> _hiddenWordIndices;

    public Scripture(ScriptureReference reference, string text)
    {
        _reference = reference;
        _words = new List<ScriptureWord>();
        _hiddenWordIndices = new List<int>();

        // Split text into words (handling punctuation)
        var wordArray = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in wordArray)
        {
            _words.Add(new ScriptureWord(word));
        }
    }

    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText() + "\n\n";
        foreach (var word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }
        return displayText.Trim();
    }

    public bool HideRandomWords(int count = 3)
    {
        var visibleIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                visibleIndices.Add(i);
            }
        }

        if (visibleIndices.Count == 0)
            return false;

        Random random = new Random();
        for (int i = 0; i < Math.Min(count, visibleIndices.Count); i++)
        {
            int randomIndex = random.Next(visibleIndices.Count);
            _words[visibleIndices[randomIndex]].Hide();
            visibleIndices.RemoveAt(randomIndex);
        }

        return true;
    }

    public bool IsCompletelyHidden()
    {
        foreach (var word in _words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }

    public void Reset()
    {
        foreach (var word in _words)
        {
            word.Show();
        }
    }

    public string GetReference()
    {
        return _reference.GetDisplayText();
    }
}

// Class to manage a library of scriptures
public class ScriptureLibrary
{
    private List<Scripture> _scriptures;
    private Random _random;

    public ScriptureLibrary()
    {
        _scriptures = new List<Scripture>();
        _random = new Random();
    }

    public void AddScripture(Scripture scripture)
    {
        _scriptures.Add(scripture);
    }

    public Scripture GetRandomScripture()
    {
        if (_scriptures.Count == 0)
            return null;

        return _scriptures[_random.Next(_scriptures.Count)];
    }

    public void LoadFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length >= 2)
            {
                var reference = new ScriptureReference(parts[0].Trim());
                var scripture = new Scripture(reference, parts[1].Trim());
                _scriptures.Add(scripture);
            }
        }
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create scripture library
        ScriptureLibrary library = new ScriptureLibrary();

        // Load scriptures from file (if exists)
        string filePath = "scriptures.txt";
        if (File.Exists(filePath))
        {
            library.LoadFromFile(filePath);
        }
        else
        {
            // Add some default scriptures if file doesn't exist
            library.AddScripture(new Scripture(
                new ScriptureReference("John 3:16"),
                "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."
            ));

            library.AddScripture(new Scripture(
                new ScriptureReference("Proverbs 3:5-6"),
                "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."
            ));

            library.AddScripture(new Scripture(
                new ScriptureReference("Philippians 4:13"),
                "I can do all this through him who gives me strength."
            ));
        }

        // Main program loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Scripture Memorization Helper");
            Console.WriteLine("----------------------------");
            Console.WriteLine("1. Practice a random scripture");
            Console.WriteLine("2. Add a new scripture");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                PracticeScripture(library);
            }
            else if (choice == "2")
            {
                AddNewScripture(library);
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }

    static void PracticeScripture(ScriptureLibrary library)
    {
        Scripture scripture = library.GetRandomScripture();
        if (scripture == null)
        {
            Console.WriteLine("No scriptures available in the library.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nAll words are hidden! Well done!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("\nPress ENTER to hide words or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }
            else
            {
                scripture.HideRandomWords(3);
            }
        }
    }

    static void AddNewScripture(ScriptureLibrary library)
    {
        Console.Clear();
        Console.WriteLine("Add New Scripture");
        Console.WriteLine("-----------------");

        Console.Write("Enter reference (e.g., 'John 3:16' or 'Proverbs 3:5-6'): ");
        string reference = Console.ReadLine();

        Console.Write("Enter scripture text: ");
        string text = Console.ReadLine();

        try
        {
            library.AddScripture(new Scripture(new ScriptureReference(reference), text));
            Console.WriteLine("Scripture added successfully!");

            // Option to save to file
            Console.Write("Would you like to save this to the scriptures file? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                File.AppendAllText("scriptures.txt", $"{reference}|{text}\n");
                Console.WriteLine("Scripture saved to file!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding scripture: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

