using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// I showed my creativity Exceding requirement by adding a gratitude activity.
// What i did was to create a gratitude class.Then in that GratitudeActivity class, I created two Lists to store all the prompting activities and also to store all the prompts that have been used, making sure that no random prompts are selected untill they are all used for atleast onece in every section.
// Also this Gratitude activity class constains method or function that will save and load log file as well as adding asterix* animations animations through calling a method that i named animation.


namespace MindfulnessApp

{
    class Program
    {
        static void Main(string[] args)
        {
            Activity.LoadActivityLog();

            Dictionary<int, Activity> activities = new Dictionary<int, Activity>
            {
                {1, new BreathingActivity()},
                {2, new ReflectionActivity()},
                {3, new ListingActivity()},
                {4, new GratitudeActivity()} // Creative enhancement
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Activities Menu");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Gratitude Activity"); // Creative enhancement
                Console.WriteLine("5. Exit");
                Console.Write("Choose an activity: ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(1000);
                    continue;
                }

                if (choice == 5)
                {
                    Activity.SaveActivityLog();
                    break;
                }

                activities[choice].Run();
            }
        }
    }

    // Class for Reflection Activity
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private List<string> _usedQuestions = new List<string>();

        public ReflectionActivity()
        {
            Name = "Reflection";
            Description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        }

        public override void Run()
        {
            DisplayStartingMessage();

            // Select a random prompt
            Random random = new Random();
            string prompt = _prompts[random.Next(_prompts.Count)];
            Console.WriteLine(prompt);
            Console.WriteLine();
            Console.WriteLine("When you have something in mind, press enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            animation(5);
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                if (_usedQuestions.Count == _questions.Count)
                {
                    _usedQuestions.Clear();
                }

                string question;
                do
                {
                    question = _questions[random.Next(_questions.Count)];
                } while (_usedQuestions.Contains(question));

                _usedQuestions.Add(question);

                Console.Write(question + " ");
                ShowSpinner(5);
                Console.WriteLine();
            }

            DisplayEndingMessage();
        }
    }

    //  Activity class that has pre-defined functionality
    public abstract class Activity
    {
        protected string Name { get; set; }
        protected string Description { get; set; }
        protected int Duration { get; set; }
        protected static int ActivityCount { get; set; } = 0;

        protected void DisplayStartingMessage()
        {
            ActivityCount++;
            Console.Clear();
            Console.WriteLine($"Welcome to the {Name} Activity");
            Console.WriteLine();
            Console.WriteLine(Description);
            Console.WriteLine();
            Console.Write("How long, in seconds, would you like for your session? ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Get ready to begin...");
            ShowSpinner(3);
        }

        protected void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Good job! You have completed another activity.");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine($"You have completed the {Name} Activity for {Duration} seconds.");
            ShowSpinner(3);
            Console.WriteLine();
        }

        protected void ShowSpinner(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write("|");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("/");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("-");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("\\");
                Thread.Sleep(250);
                Console.Write("\b \b");
            }
        }

        protected void animation(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        protected void ShowBreathingAnimation(int seconds, bool isBreathingIn)
        {
            string message = isBreathingIn ? "Breathe in..." : "Breathe out...";
            Console.Write(message);

            // Animate the growing/shrinking text
            for (int i = 0; i < seconds; i++)
            {
                double progress = (double)i / seconds;
                int size = isBreathingIn ?
                    (int)(10 * progress) :
                    (int)(10 * (1 - progress));

                string animation = new string('*', size);
                Console.Write($" {animation}");
                Thread.Sleep(1000);
                Console.Write("\b \b".Repeat(animation.Length + 1));
            }

            Console.WriteLine();
        }

        public abstract void Run();

        public static void SaveActivityLog()
        {
            File.WriteAllText("activity_log.txt", $"Total activities completed: {ActivityCount}");
        }

        public static void LoadActivityLog()
        {
            if (File.Exists("activity_log.txt"))
            {
                string log = File.ReadAllText("activity_log.txt");
                Console.WriteLine($"Previous session: {log}");
            }
        }
    }

    public static class StringExtensions
    {
        public static string Repeat(this string s, int count)
        {
            return string.Concat(Enumerable.Repeat(s, count));
        }
    }

    // Breathing Activity
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
        {
            Name = "Breathing";
            Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }

        public override void Run()
        {
            DisplayStartingMessage();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

            bool isBreathingIn = true;
            while (DateTime.Now < endTime)
            {
                ShowBreathingAnimation(isBreathingIn ? 4 : 6, isBreathingIn);
                isBreathingIn = !isBreathingIn;
            }

            DisplayEndingMessage();
        }
    }



    // Listing Activity
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private List<string> _usedPrompts = new List<string>();

        public ListingActivity()
        {
            Name = "Listing";
            Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        public override void Run()
        {
            DisplayStartingMessage();

            // Select a random prompt (ensuring all are used before repeating)
            Random random = new Random();
            string prompt;

            if (_usedPrompts.Count == _prompts.Count)
            {
                _usedPrompts.Clear();
            }

            do
            {
                prompt = _prompts[random.Next(_prompts.Count)];
            } while (_usedPrompts.Contains(prompt));

            _usedPrompts.Add(prompt);

            Console.WriteLine(prompt);
            Console.Write("You may begin in: ");
            animation(5);
            Console.WriteLine();

            List<string> items = new List<string>();
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item);
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {items.Count} items.");

            DisplayEndingMessage();
        }
    }

    // My creativity exceeding requirement
    public class GratitudeActivity : Activity
    {
        private List<string> _gratitudePrompts = new List<string>
        {
            "What small things are you grateful for today?",
            "Who has made a positive impact on your life recently?",
            "What opportunities are you grateful to have?",
            "What personal qualities are you thankful for?",
            "What comforts or conveniences are you grateful for?"
        };

        private List<string> _usedPrompts = new List<string>();

        public GratitudeActivity()
        {
            Name = "Gratitude";
            Description = "This activity will help you cultivate gratitude by focusing on specific things you're thankful for, which can improve your overall well-being.";
        }

        public override void Run()
        {
            DisplayStartingMessage();

            // Select a random prompt (ensuring all are used before repeating)
            Random random = new Random();
            string prompt;

            if (_usedPrompts.Count == _gratitudePrompts.Count)
            {
                _usedPrompts.Clear();
            }

            do
            {
                prompt = _gratitudePrompts[random.Next(_gratitudePrompts.Count)];
            } while (_usedPrompts.Contains(prompt));

            _usedPrompts.Add(prompt);

            Console.WriteLine(prompt);
            Console.WriteLine("Take a moment to reflect...");
            ShowSpinner(5);

            Console.WriteLine("Now write down as many things as you can think of:");
            Console.Write("You may begin in: ");
            animation(5);
            Console.WriteLine();

            List<string> items = new List<string>();
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("I'm grateful for: ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item);
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You expressed gratitude for {items.Count} things.");
            Console.WriteLine("Take a moment to appreciate these:");

            foreach (var item in items)
            {
                Console.WriteLine($"- {item}");
                ShowSpinner(1);
            }

            DisplayEndingMessage();
        }
    }
}