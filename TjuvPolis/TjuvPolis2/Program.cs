using System;
using System.Collections.Generic;

namespace TjuvPolis2
{
    class Program
    {
        static void Main(string[] args)
        {
            int cityWidth = 100;
            int cityHeight = 25;
            char[,] city = new char[cityWidth, cityHeight];

            int noOfCitizens = 30;
            int noOfPolice = 10;
            int noOfThiefs = 20;

            List<Citizen> citizens = new List<Citizen>();
            CreateCitizens(noOfCitizens, cityWidth, cityHeight, citizens);
            //Console.WriteLine(CtrlMethods.PrintCitizenList(citizens));

            List<Police> policeOfficers = new List<Police>();
            CreatePoliceOfficers(noOfPolice, cityWidth, cityHeight, policeOfficers);
            //Console.WriteLine(CtrlMethods.PrintPoliceList(policeOfficers));

            List<Thief> thiefs = new List<Thief>();
            CreateThiefs(noOfThiefs, cityWidth, cityHeight, thiefs);
            //Console.WriteLine(CtrlMethods.PrintThiefList(thiefs));


            //while (true)
            //{
            for (int y = 0; y < cityHeight; y++)
            {
                for (int x = 0; x < cityWidth; x++)
                {
                    if (CheckIfCitizen(x, y, citizens))
                    {
                        Console.Write("C");
                    }
                    else if (CheckIfPolice(x, y, policeOfficers))
                    {
                        Console.Write("P");
                    }
                    else if (CheckIfThief(x, y, thiefs))
                    {
                        Console.Write("T");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            //}

        }


        private static bool CheckIfCitizen(int x, int y, List<Citizen> citizens)
        {
            bool isCitizen = false;
            foreach (Citizen citizen in citizens)
            {
                if (x == citizen.XPosition && y == citizen.YPosition)
                {
                    isCitizen = true;
                }
            }
            return isCitizen;
        }
        private static bool CheckIfPolice(int x, int y, List<Police> policeOfficers)
        {
            bool isPolice = false;
            foreach (Police police in policeOfficers)
            {
                if (x == police.XPosition && y == police.YPosition)
                {
                    isPolice = true;
                }
            }
            return isPolice;
        }
        private static bool CheckIfThief(int x, int y, List<Thief> thiefs)
        {
            bool isThief = false;
            foreach (Thief thief in thiefs)
            {
                if (x == thief.XPosition && y == thief.YPosition)
                {
                    isThief = true;
                }
            }
            return isThief;
        }

        private static void CreateCitizens(int noOfCitizens, int width, int height, List<Citizen> citizens)
        {

            for (int i = 0; i < noOfCitizens; i++)
            {
                int xPos = GetRandom(0, width);
                int yPos = GetRandom(0, height);
                (int xDir, int yDir) = CreateDirection();
                citizens.Add(new Citizen(xPos, yPos, xDir, yDir));
            }
        }
        private static void CreatePoliceOfficers(int noOfPolice, int width, int height, List<Police> policeOfficers)
        {
            for (int i = 0; i < noOfPolice; i++)
            {
                int xPos = GetRandom(0, width);
                int yPos = GetRandom(0, height);
                (int xDir, int yDir) = CreateDirection();
                policeOfficers.Add(new Police(xPos, yPos, xDir, yDir));
            }
        }
        private static void CreateThiefs(int noOfThiefs, int width, int height, List<Thief> thiefs)
        {
            for (int i = 0; i < noOfThiefs; i++)
            {
                int xPos = GetRandom(0, width);
                int yPos = GetRandom(0, height);
                (int xDir, int yDir) = CreateDirection();
                thiefs.Add(new Thief(xPos, yPos, xDir, yDir));
            }
        }

        private static (int xDir, int yDir) CreateDirection()
        {
            int xDir = GetRandom(-1, 1);
            int yDir = GetRandom(-1, 1);
            while (xDir == 0 && yDir == 0)
            {
                yDir = GetRandom(-1, 1);
            }
            return (xDir, yDir);
        }

        public static int GetRandom(int low, int high)
        {
            Random r = new Random(); // Är det en dålig ide att skapa en ny random varje gång metoden körs?
            return r.Next(low, high + 1);
        }
    }
}
