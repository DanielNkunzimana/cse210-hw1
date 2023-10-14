using System;
using System.Collections.Generic;
using System.Threading;

// Base Activity class
class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Start()
    {
        Console.WriteLine($"Starting {name} Activity");
        Console.WriteLine(description);
        Console.Write("Enter duration (in seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(2000); // Pause with animation
    }

    public void End()
    {
        Console.WriteLine($"You have completed the {name} Activity");
        Console.WriteLine($"Duration: {duration} seconds");
        Console.WriteLine("You did a good job!");
        Thread.Sleep(2000); // Pause with animation
    }
}

// Breathing Activity derived from Activity
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void StartBreathing()
    {
        Start();
        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine(i % 2 == 0 ? "Breathe in..." : "Breathe out...");
            Thread.Sleep(1000); // Pause with animation
        }
        End();
    }
}

// Reflection Activity derived from Activity
class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        // Add more questions
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public void StartReflecting()
    {
        Start();
        Random random = new Random();
        foreach (string prompt in prompts)
        {
            Console.WriteLine(prompt);
            Thread.Sleep(2000); // Pause with animation
            foreach (string question in questions)
            {
                Console.WriteLine(question);
                Thread.Sleep(2000); // Pause with animation
            }
        }
        End();
    }
}

// Listing Activity derived from Activity
class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        // Add more prompts
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public void StartListing()
    {
        Start();
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(2000); // Pause with animation
        Console.WriteLine("Start listing items...");
        Thread.Sleep(duration * 1000); // Simulate user input duration
        Console.WriteLine($"Number of items entered: {duration}");
        End();
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Select an Activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.StartBreathing();
                    break;

                case 2:
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartReflecting();
                    break;

                case 3:
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.StartListing();
                    break;

                case 4:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
