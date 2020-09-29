using System;
using System.Collections.Generic;
using System.Threading;

namespace TjuvPolis2
{
    class Program
    {
        static void Main(string[] args)
        {
            int cityWidth = 100;
            int cityHeight = 25;

            int noOfCitizens = 30;
            int noOfPolice = 50;
            int noOfThiefs = 20;


            int robberies = 0;
            int captures = 0;

            List<Citizen> citizenList = new List<Citizen>();
            CreateCitizenList(noOfCitizens, cityWidth, cityHeight, citizenList);
            //Console.WriteLine(CtrlMethods.PrintCitizenList(citizenList));

            List<Police> policeList = new List<Police>();
            CreatePoliceList(noOfPolice, cityWidth, cityHeight, policeList);
            //Console.WriteLine(CtrlMethods.PrintPoliceList(policeList));

            List<Thief> thiefList = new List<Thief>();
            CreateThiefList(noOfThiefs, cityWidth, cityHeight, thiefList);
            //Console.WriteLine(CtrlMethods.PrintThiefList(thiefList));


            while (true)
            {
                Console.Clear();
                bool robberyEvent = false;
                bool captureEvent = false;
                for (int y = 0; y < cityHeight; y++)
                {
                    for (int x = 0; x < cityWidth; x++)
                    {
                        if (CheckIfThief(x, y, thiefList))
                        {
                            if (CheckIfCitizen(x, y, citizenList) == false &&
                                CheckIfPolice(x, y, policeList) == false)
                            {
                                Console.Write("T");
                            }
                            if (CheckIfCitizen(x, y, citizenList))
                            {
                                if (CheckBelongings(x, y, citizenList))
                                {
                                    PerformTheft(x, y, citizenList, thiefList);
                                }
                                robberies++; // Jag tycker det skall räknas som rån i statistiken även om medborgaren inte har ngt att stjäla
                                robberyEvent = true;
                                Console.Write("!");
                            }
                            if (CheckIfPolice(x, y, policeList)) // Om alla tre kommer på samma ställe sker först rånet och sedan tar polisen genast tjuven
                            {
                                if (CheckSwag(x, y, thiefList))
                                {
                                    ConfiscateSwag(x, y, thiefList, policeList);
                                    Console.Write("#");
                                    captures++; // Polisen tar bara tjuven om hen har stulit något
                                    captureEvent = true;
                                }
                            }
                        }
                        else if (CheckIfPolice(x, y, policeList))
                        {
                            if (CheckIfCitizen(x, y, citizenList))
                            {
                                Console.Write("*");
                            }
                            else
                            {
                                Console.Write("P");
                            }
                        }
                        else if (CheckIfCitizen(x, y, citizenList))
                        {
                            Console.Write("M");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                //Thread.Sleep(200);

                if (robberyEvent || captureEvent)
                {
                    if (robberyEvent)
                    {
                        Console.WriteLine("Medborgare rånad av tjuv");
                    }
                    if (captureEvent)
                    {
                        Console.WriteLine("Tjuv fångad av polis");
                    }
                    Console.WriteLine($"Antal rånade medborgare: {robberies}");
                    Console.WriteLine($"Antal gripna tjuvar: {captures}");
                    Thread.Sleep(5000);
                }

                foreach (Citizen citizen in citizenList)
                {
                    Move(cityWidth, cityHeight, citizen);
                }
                foreach (Police police in policeList)
                {
                    Move(cityWidth, cityHeight, police);
                }
                foreach (Person thief in thiefList)  // Testar att använda typen Person istället för Thief, det verkar gå lika bra
                {
                    Move(cityWidth, cityHeight, thief);
                }
            }
        }

        private static void Move(int width, int height, Person person)
        {
            person.XPosition += person.XDirection;
            if (person.XPosition > width - 1 && person.XDirection == 1)
            {
                person.XPosition = 0;
            }
            if (person.XPosition < 0 && person.XDirection == -1)
            {
                person.XPosition = width - 1;
            }

            person.YPosition += person.YDirection;
            if (person.YPosition > height - 1 && person.YDirection == 1)
            {
                person.YPosition = 0;
            }
            if (person.YPosition < 0 && person.YDirection == -1)
            {
                person.YPosition = height - 1;
            }
        }

        private static void ConfiscateSwag(int x, int y, List<Thief> thiefList, List<Police> policeList)
        {
            foreach (Thief thief in thiefList)
            {
                if (thief.XPosition == x && thief.YPosition == y)
                {
                    foreach (Police police in policeList)
                    {
                        if (police.XPosition == x && police.YPosition == y)
                        {
                            for (int i = 0; i < thief.Swag.Count; i++)
                            {
                                police.ConfiscatedItems[i].NoOfItems += thief.Swag[i].NoOfItems;
                                thief.Swag[i].NoOfItems = 0;
                            }
                        }
                    }
                }
            }
        }

        private static void PerformTheft(int x, int y, List<Citizen> citizenList, List<Thief> thiefList)
        {
            foreach (Citizen citizen in citizenList)
            {
                if (citizen.XPosition == x && citizen.YPosition == y)
                {
                    int selectedItem = GetRandom(0, 3);
                    while (citizen.Belongings[selectedItem].NoOfItems < 1)
                    {
                        selectedItem = GetRandom(0, 3);
                    }
                    foreach (Thief thief in thiefList)
                    {
                        if (thief.XPosition == x && thief.YPosition == y)
                        {
                            thief.Swag[selectedItem].NoOfItems++;
                            citizen.Belongings[selectedItem].NoOfItems--;
                        }
                    }
                }
            }
        }
        // Försökte göra en gemensam metod för att kolla om person har items men lyckades inte lösa hur jag skall komma åt item-listan eftersom den inte finns i basklassen
        //private static bool CheckItems(int x, int y, List<Person> personList)
        //{
        //    bool hasItem = false;
        //    foreach (Person p in personList)
        //    {
        //        if (p.XPosition == x && p.YPosition == y)
        //        {
        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (p.Belongings[i].NoOfItems > 0)
        //                {
        //                    hasItem = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return hasItem;
        //}
        private static bool CheckBelongings(int x, int y, List<Citizen> citizenList)
        {
            bool hasBelonging = false;
            foreach (Citizen citizen in citizenList)
            {
                if (citizen.XPosition == x && citizen.YPosition == y)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (citizen.Belongings[i].NoOfItems > 0)
                        {
                            hasBelonging = true;
                            break;
                        }
                    }
                }
            }
            return hasBelonging;
        }
        private static bool CheckSwag(int x, int y, List<Thief> theifList)
        {
            bool hasSwag = false;
            foreach (Thief thief in theifList)
            {
                if (thief.XPosition == x && thief.YPosition == y)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (thief.Swag[i].NoOfItems > 0)
                        {
                            hasSwag = true;
                            break;
                        }
                    }
                }
            }
            return hasSwag;
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
        private static void CreateCitizenList(int noOfCitizens, int width, int height, List<Citizen> citizens)
        {

            for (int i = 0; i < noOfCitizens; i++)
            {
                int xPos = GetRandom(0, width);
                int yPos = GetRandom(0, height);
                (int xDir, int yDir) = CreateDirection();
                List<Item> belongings = new List<Item>();
                belongings.Add(new Item("Keys", 1));
                belongings.Add(new Item("Telephone", 1));
                belongings.Add(new Item("Money", 1));
                belongings.Add(new Item("Watch", 1));
                citizens.Add(new Citizen(xPos, yPos, xDir, yDir, belongings));
            }
        }
        private static void CreatePoliceList(int noOfPolice, int width, int height, List<Police> policeOfficers)
        {
            for (int i = 0; i < noOfPolice; i++)
            {
                int xPos = GetRandom(0, width);
                int yPos = GetRandom(0, height);
                (int xDir, int yDir) = CreateDirection();
                List<Item> confiscatedItems = new List<Item>
                {
                    new Item("Keys", 0),
                    new Item("Telephone", 0),
                    new Item("Money", 0),
                    new Item("Watch", 0)
                };
                policeOfficers.Add(new Police(xPos, yPos, xDir, yDir, confiscatedItems));
            }
        }
        private static void CreateThiefList(int noOfThiefs, int width, int height, List<Thief> thiefs)
        {
            for (int i = 0; i < noOfThiefs; i++)
            {
                int xPos = GetRandom(0, width);
                int yPos = GetRandom(0, height);
                (int xDir, int yDir) = CreateDirection();
                List<Item> swag = new List<Item>
                {
                    new Item("Keys", 0),
                    new Item("Telephone", 0),
                    new Item("Money", 0),
                    new Item("Watch", 0)
                };
                thiefs.Add(new Thief(xPos, yPos, xDir, yDir, swag));
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
