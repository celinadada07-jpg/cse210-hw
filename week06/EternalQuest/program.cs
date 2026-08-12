using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("        ETERNAL QUEST");
            Console.WriteLine("=================================");
            Console.WriteLine($"Score: {score}");
            Console.WriteLine($"Level: {GetLevel()}");
            Console.WriteLine();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.WriteLine();

            Console.Write("Select a choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;

                case "2":
                    ListGoals();
                    break;

                case "3":
                    SaveGoals();
                    break;

                case "4":
                    LoadGoals();
                    break;

                case "5":
                    RecordEvent();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Thank you for playing Eternal Quest!");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.Clear();

        Console.WriteLine("Create New Goal");
        Console.WriteLine("----------------");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine();

        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine() ?? "";

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "";

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? "";

        Console.Write("How many points is this goal worth? ");
        int points = int.Parse(Console.ReadLine() ?? "0");

        if (type == "1")
        {
            SimpleGoal goal = new SimpleGoal(
                name,
                description,
                points
            );

            goals.Add(goal);

            Console.WriteLine("Simple goal created!");
        }
        else if (type == "2")
        {
            EternalGoal goal = new EternalGoal(
                name,
                description,
                points
            );

            goals.Add(goal);

            Console.WriteLine("Eternal goal created!");
        }
        else if (type == "3")
        {
            Console.Write("How many times must this goal be completed? ");
            int target = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("How many bonus points are awarded when completed? ");
            int bonus = int.Parse(Console.ReadLine() ?? "0");

            ChecklistGoal goal = new ChecklistGoal(
                name,
                description,
                points,
                target,
                bonus
            );

            goals.Add(goal);

            Console.WriteLine("Checklist goal created!");
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }

        Pause();
    }

    static void ListGoals()
    {
        Console.Clear();

        Console.WriteLine("Your Goals");
        Console.WriteLine("----------");

        if (goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals yet.");
        }
        else
        {
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
            }
        }

        Pause();
    }

    static void RecordEvent()
    {
        Console.Clear();

        Console.WriteLine("Record Event");
        Console.WriteLine("------------");

        if (goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals.");
            Pause();
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
        }

        Console.WriteLine();

        Console.Write("Which goal did you accomplish? ");
        int choice = int.Parse(Console.ReadLine() ?? "0");

        if (choice < 1 || choice > goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            Pause();
            return;
        }

        Goal selectedGoal = goals[choice - 1];

        int pointsEarned = selectedGoal.RecordEvent();

        if (pointsEarned > 0)
        {
            score += pointsEarned;

            Console.WriteLine();
            Console.WriteLine($"Congratulations!");
            Console.WriteLine($"You earned {pointsEarned} points.");
            Console.WriteLine($"Your total score is now {score}.");

            CheckForLevelUp();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("This goal has already been completed.");
        }

        Pause();
    }

    static void SaveGoals()
    {
        Console.Clear();

        using (StreamWriter outputFile = new StreamWriter("goals.txt"))
        {
            outputFile.WriteLine(score);

            foreach (Goal goal in goals)
            {
                outputFile.WriteLine(goal.GetSaveString());
            }
        }

        Console.WriteLine("Goals saved successfully.");
        Pause();
    }

    static void LoadGoals()
    {
        Console.Clear();

        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No saved goals were found.");
            Pause();
            return;
        }

        string[] lines = File.ReadAllLines("goals.txt");

        goals.Clear();

        if (lines.Length > 0)
        {
            score = int.Parse(lines[0]);
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts[0] == "SimpleGoal")
            {
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);
                bool isComplete = bool.Parse(parts[4]);

                goals.Add(
                    new SimpleGoal(
                        name,
                        description,
                        points,
                        isComplete
                    )
                );
            }
            else if (parts[0] == "EternalGoal")
            {
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);

                goals.Add(
                    new EternalGoal(
                        name,
                        description,
                        points
                    )
                );
            }
            else if (parts[0] == "ChecklistGoal")
            {
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);
                int target = int.Parse(parts[4]);
                int amountCompleted = int.Parse(parts[5]);
                int bonus = int.Parse(parts[6]);

                goals.Add(
                    new ChecklistGoal(
                        name,
                        description,
                        points,
                        target,
                        amountCompleted,
                        bonus
                    )
                );
            }
        }

        Console.WriteLine("Goals loaded successfully.");
        Console.WriteLine($"Current score: {score}");

        Pause();
    }

    static string GetLevel()
    {
        if (score >= 2000)
        {
            return "Eternal Champion";
        }
        else if (score >= 1000)
        {
            return "Dedicated Disciple";
        }
        else if (score >= 500)
        {
            return "Faithful Disciple";
        }
        else
        {
            return "Beginner";
        }
    }

    static void CheckForLevelUp()
    {
        if (score == 500 ||
            score == 1000 ||
            score == 2000)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("          LEVEL UP!");
            Console.WriteLine("=================================");
            Console.WriteLine($"You are now a {GetLevel()}!");
        }
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }
}

/*
 * CREATIVITY / EXCEEDING REQUIREMENTS:
 *
 * I added a leveling system to my Eternal Quest program.
 * The user's level changes based on their total score.
 * The levels are Beginner, Faithful Disciple,
 * Dedicated Disciple, and Eternal Champion.
 *
 * I also added a level-up message when the user reaches
 * 500, 1000, or 2000 points.
 *
 * This gamification feature goes beyond the core requirements
 * by giving the user additional motivation to continue
 * completing their goals.
 */