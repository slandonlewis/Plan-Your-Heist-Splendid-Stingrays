using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    public class Program
    {
        static void Main(string[] args)
        {

            // enter team member information
            Console.WriteLine("Plan Your Heist!");
            TeamMember.CreatePrompt();

            //Console.WriteLine("\nYour Team:");
            //TeamMember.Team.ForEach((tm) => {tm.displayInfo();});

            Console.WriteLine("Enter number of trial runs:");
            int numberOfTrialRuns = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter bank's difficulty:");
            int bankDifficulty = int.Parse(Console.ReadLine());
            Bank bank = new Bank(numberOfTrialRuns, bankDifficulty);

            int wins = 0;
            for (int i = numberOfTrialRuns; i > 0; i--)
            {
                Console.WriteLine($"-----------Robbery #{numberOfTrialRuns - (i - 1)}-----------");
                Console.WriteLine($"Your team's skill level: {TeamMember.TeamSkill}");
                if (bank.Rob())
                {
                    wins++;
                }
            }
            Console.WriteLine($"\nNumber of Successes: {wins}");
            Console.WriteLine($"Number of Failures: {numberOfTrialRuns - wins}");

        }

    }

    public class Bank
    {
        public int RequiredSkill;
        public int NumOfScenarios;
        public Bank(int numOfScenarios, int requiredSkill)
        {
            RequiredSkill = requiredSkill;
            NumOfScenarios = numOfScenarios;
        }

        public bool Rob()
        {
            int skillToPass = new Random().Next(-10, 11) + RequiredSkill;

            Console.WriteLine($"Heist difficulty level: {skillToPass}");
            if (skillToPass <= TeamMember.TeamSkill)
            {
                Console.WriteLine("Yay!!\n");
                return true;
            }
            else
            {
                Console.WriteLine("You are now in jail.\n");
                return false;
            }
        }
    }

    public class TeamMember
    {
        public static List<TeamMember> Team = new List<TeamMember>();

        public static int TeamSkill
        {
            get
            {
                int skill = 0;
                Team.ForEach((member) => skill += member.SkillLevel);
                return skill;
            }
        }


        public string Name { get; }
        public int SkillLevel { get; }
        public decimal CourageFactor { get; }


        public static void CreatePrompt()
        {
            while (true)
            {
                Console.WriteLine($"\nMember #{Team.Count + 1}:");
                Console.WriteLine("Enter team member name: (blank to continue) ");
                string name = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(name)) { break; }
                Console.WriteLine("Enter skill level (1 - 20): ");
                int skillLevel = 0;
                while (skillLevel < 1 || skillLevel > 20)
                {
                    if (skillLevel != 0)
                    {
                        Console.WriteLine("Input out of range, please enter level between 1 and 20");
                    }
                    skillLevel = int.Parse(Console.ReadLine());
                }

                Console.Write("Enter courage factor (0.0 - 2.0): ");
                decimal courageFactor = -1;
                while (courageFactor < (decimal)0.0 || courageFactor > (decimal)2.0)
                {
                    if (courageFactor != -1)
                    {
                        Console.WriteLine("Input out of range, please enter factor between 0.0 and 2.0");
                    }
                    courageFactor = decimal.Parse(Console.ReadLine());
                }

                // create new team member based off above info
                TeamMember newMember = new TeamMember(name, skillLevel, courageFactor);
                Team.Add(newMember);
            }
        }
        public TeamMember(string name, int skillLevel, decimal courageFactor)
        {
            Name = name;
            SkillLevel = skillLevel;
            CourageFactor = courageFactor;
        }
        public void displayInfo()
        {
            Console.WriteLine($"\nName: {Name}, \nSkill Lvl: {SkillLevel}, \nCourage Factor: {CourageFactor}");
        }
    }
}
