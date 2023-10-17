using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        User user = new User("Daniel");

        GoalManager goalManager = new GoalManager();
        goalManager.AddGoal(new SimpleGoal("Run a marathon", 1000));
        goalManager.AddGoal(new EternalGoal("Read scriptures", 100));
        goalManager.AddGoal(new ChecklistGoal("Attend the temple", 10, 50, 500));

        while (true)
        {
            Console.WriteLine($"Welcome, {user.Name}!");
            Console.WriteLine($"Current Score: {user.Score}");
            Console.WriteLine("Hi those are your Goals");
            goalManager.DisplayGoals();

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Record Goal");
            Console.WriteLine("2. Create New Goal");
            Console.WriteLine("3. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the goal number to record: ");
                    int goalNumber = int.Parse(Console.ReadLine());
                    goalManager.RecordGoal(user, goalNumber);
                    break;
                case 2:
                    CreateNewGoal(goalManager);
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateNewGoal(GoalManager goalManager)
    {
        Console.WriteLine("Choose the goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Enter the name of the goal: ");
                string name = Console.ReadLine();
                Console.Write("Enter the value: ");
                int value = int.Parse(Console.ReadLine());
                goalManager.AddGoal(new SimpleGoal(name, value));
                break;
            default:
                  Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

class User
{
    public string Name { get; }
    public int Score { get; set; }

    public User(string name)
    {
        Name = name;
        Score = 0;
    }
}

class Goal
{
    public string Name { get; }
    public bool Completed { get; protected set; }

    public Goal(string name)
    {
        Name = name;
        Completed = false;
    }

    public virtual void RecordGoal(User user)
    {
        Completed = true;
    }
}

class SimpleGoal : Goal
{
    public int Value { get; }

    public SimpleGoal(string name, int value) : base(name)
    {
        Value = value;
    }

    public override void RecordGoal(User user)
    {
        base.RecordGoal(user);
        user.Score += Value;
    }
}

class EternalGoal : Goal
{
    public int Value { get; }

    public EternalGoal(string name, int value) : base(name)
    {
        Value = value;
    }

    public override void RecordGoal(User user)
    {
        base.RecordGoal(user);
        user.Score += Value;
    }
}

class ChecklistGoal : Goal
{
    public int TotalRequired { get; }
    public int ValuePerCompletion { get; }
    public int BonusValue { get; }
    public int TimesCompleted { get; private set; }

    public ChecklistGoal(string name, int totalRequired, int valuePerCompletion, int bonusValue) : base(name)
    {
        TotalRequired = totalRequired;
        ValuePerCompletion = valuePerCompletion;
        BonusValue = bonusValue;
        TimesCompleted = 0;
    }

    public override void RecordGoal(User user)
    {
        TimesCompleted++;
        if (TimesCompleted < TotalRequired)
            user.Score += ValuePerCompletion;
        else
            user.Score += BonusValue;

        if (TimesCompleted >= TotalRequired)
            Completed = true;
    }
}

class GoalManager
{
    private List<Goal> goals;

    public GoalManager()
    {
        goals = new List<Goal>();
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            string status = goals[i].Completed ? "[X]" : "[ ]";
            string goalDetails = $"{status} {goals[i].Name}";

            if (goals[i] is ChecklistGoal checklistGoal)
            {
                goalDetails += $" (Completed {checklistGoal.TimesCompleted}/{checklistGoal.TotalRequired} times)";
            }

            Console.WriteLine($"{i + 1}. {goalDetails}");
        }
    }

    public void RecordGoal(User user, int goalNumber)
    {
        if (goalNumber >= 1 && goalNumber <= goals.Count)
        {
            Goal goal = goals[goalNumber - 1];
            goal.RecordGoal(user);
        }
        else
        {
            Console.WriteLine("Invalid goal number. Please try again.");
        }
    }
}
