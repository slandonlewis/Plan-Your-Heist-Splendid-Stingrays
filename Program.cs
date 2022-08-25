using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    public class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank(100);

            // enter team member information
            Console.WriteLine("Plan Your Heist!");
            TeamMember.CreatePrompt();

            //Console.WriteLine("\nYour Team:");
            //TeamMember.Team.ForEach((tm) => {tm.displayInfo();});

            if (bank.RequiredSkill <= TeamMember.TeamSkill)
            {
                Console.WriteLine("Yay!!");
            }
            else
            {
                Console.WriteLine("You are now in jail.");
            }

        }

    }

    public class Bank
    {
        public int RequiredSkill;

        

        public Bank(int difficulty)
        {
            RequiredSkill = difficulty;
        }
    }

    public class TeamMember
    {
        public static List<TeamMember> Team = new List<TeamMember>();

        public static int TeamSkill {
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
                if (String.IsNullOrWhiteSpace(name)){break;}
                Console.WriteLine("Enter skill level (1 - 20): ");
                int skillLevel = 0;
                while (skillLevel < 1 || skillLevel > 50)
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
