using System;
using System.Collections.Generic;
using System.Threading;

namespace TjuvPolis2
{
    class Program
    {

        static void Main(string[] args)
        {
            int cityWidth = 3;
            int cityHeight = 3;

            int noOfCitizens = 4;
            int noOfPolice = 6;
            int noOfThiefs = 4;

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
            //Console.ReadKey();

            List<Thief> prison = new List<Thief>();

            while (true)
            {
                Console.Clear();
                bool robberyEvent = false;
                bool captureEvent = false;
                bool freedomEvent = false;
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
                                //if (CheckBelongings(x, y, citizenList))
                                //{
                                    PerformTheft2(x, y, citizenList, thiefList); // Om det finns två tjuvar här samtidigt tar båda M:s item som då får antal -1.
                                //}
                                robberies++; // Om två tjuvar rånar samma medborgare samtidigt räknas det som ett rån vilket stämmer med texten som skrivs ut, dvs "Antal rånade medborgare, 
                                //vad händer om två medborgare finns på samma ställe?

                                robberyEvent = true;
                                if (CheckIfPolice(x, y, policeList) == false)
                                {
                                    Console.Write("!");
                                }
                            }
                            if (CheckIfPolice(x, y, policeList)) // Om alla tre kommer på samma ställe sker först rånet och sedan tar polisen genast tjuven
                            {
                                if (CheckSwag(x, y, thiefList))
                                {
                                    ConfiscateSwag(x, y, thiefList, policeList);
                                    Console.Write("#");
                                    captures++;                      // Polisen tar bara tjuven om hen har stulit något
                                    SendToPrison(x, y, thiefList, prison);
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
                            Console.Write("-");
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();



                Thread.Sleep(200);

                foreach (Thief thief in prison)
                {
                    if (GetPrisonTime(thief) >= 30)
                    {
                        Console.WriteLine($"Tjuv med ID {thief.IdNumber} frigavs från fängelset.");
                        freedomEvent = true;
                    }
                }

                if (robberyEvent || captureEvent || freedomEvent)
                {
                    if (robberyEvent)
                    {
                        Console.WriteLine("Medborgare rånad av tjuv");
                    }
                    if (captureEvent)
                    {
                        Console.WriteLine("Tjuv fångad av polis");
                    }
                    Console.WriteLine("\nStatistik:");
                    Console.WriteLine($"Antal rånade medborgare: {robberies}");
                    Console.WriteLine($"Antal gripna tjuvar: {captures}");
                    Console.WriteLine();

                    while (freedomEvent)
                    {
                        freedomEvent = false;
                        foreach (Thief thief in prison)
                        {
                            if (GetPrisonTime(thief) >= 30)
                            {
                                SendToFreedom(thief, prison);
                                //Console.WriteLine($"Tjuv med ID {thief.IdNumber} frigavs från fängelset.");
                                freedomEvent = true;
                                break; // Börjar om med uppdaterad prisonlista, annars kraschar SendToFreedom eftersom listan ändras under tiden metoden loopar genom listan
                            }
                        }
                    }
                    switch (prison.Count)
                    {
                        case 0:
                            Console.WriteLine("Fängelset är tomt.");
                            break;
                        case 1:
                            Console.WriteLine($"{prison.Count} tjuv sitter i fängelse.");
                            break;
                        default:
                            Console.WriteLine($"{prison.Count} tjuvar sitter i fängelse.");
                            break;
                    }

                    if (prison.Count > 0)
                    {
                        Console.WriteLine("Fängelset har just nu följande interner:");
                        Console.WriteLine(PrintPrisonList(prison));
                    }
                    //Thread.Sleep(5000);
                    Console.WriteLine(CtrlMethods.PrintCitizenList(citizenList));
                    Console.WriteLine(CtrlMethods.PrintPoliceList(policeList));
                    Console.WriteLine(CtrlMethods.PrintThiefList(thiefList));
                    Console.ReadKey();
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

        private static string PrintPrisonList(List<Thief> prison)
        {
            string s = "";
            foreach (Thief thief in prison)
            {
                s += $"Tjuv med ID {thief.IdNumber } har suttit i fängelse {Math.Round(GetPrisonTime(thief))} sekunder.\n";
            }
            return s;
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

        private static void SendToPrison(int x, int y, List<Thief> thiefs, List<Thief> prison)
        {
            foreach (Thief thief in thiefs)
            {
                if (thief.XPosition == x && thief.YPosition == y)
                {
                    thief.TimeOfCapture = DateTime.Now;
                    prison.Add(thief);
                    thief.IsInPrison = true;
                    //StartPrisonTime(thief);
                    //thiefs.Remove(thief); //Hade tänkt ta bort tjuven från den här listan när hen sattinne men det orsakar problem för foreach eftersom listan ändras i loopen
                }
            }
        }
        private static double GetPrisonTime(Thief thief)
        {
            return (DateTime.Now - thief.TimeOfCapture).TotalSeconds;
            //thief.TimeOfCapture = DateTime.Now;
            //thief.TimeInPrison = (DateTime.Now - thief.TimeOfCapture).TotalSeconds;
            //thief.TimeOfRelease = thief.TimeOfCapture.AddSeconds(30);
        }

        private static void SendToFreedom(Thief thief, List<Thief> prison)
        {
            //thiefs.Add(thief); // Tjuven får vara kvar i den här listan inderfängelsetiden eftersom det annars blir problem i SendToPrison
            //thief.TimeInPrison = 0;
            prison.Remove(thief);
            thief.IsInPrison = false; // Samma tjuv som jag skickade hit genom prison-listan finns väl även i Thief-listan ? dvs båda listorna pekar på samma tjuv?

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

        private static void PerformTheft2(int x, int y, List<Citizen> citizenList, List<Thief> thiefList)
        {
            foreach (Thief thief in thiefList)
            {
                if (thief.XPosition == x && thief.YPosition == y)
                {
                    foreach (Citizen citizen in citizenList)
                    {
                        if (citizen.XPosition == x && citizen.YPosition == y)
                        {
                            if (CheckBelongings2(citizen))
                            {
                                int selectedItem = r.Next(0, citizen.Belongings.Count);
                                while (citizen.Belongings[selectedItem].NoOfItems < 1)
                                {
                                    selectedItem = r.Next(0, citizen.Belongings.Count);
                                }
                                thief.Swag[selectedItem].NoOfItems++;
                                citizen.Belongings[selectedItem].NoOfItems--;
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
                    int selectedItem = r.Next(0, citizen.Belongings.Count);
                    while (citizen.Belongings[selectedItem].NoOfItems < 1)
                    {
                        selectedItem = r.Next(0, citizen.Belongings.Count);
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

        private static readonly Random r = new Random();

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
        private static bool CheckBelongings2(Citizen citizen)
        {
            bool hasBelonging = false;

            for (int i = 0; i < 4; i++)
            {
                if (citizen.Belongings[i].NoOfItems > 0)
                {
                    hasBelonging = true;
                    break;
                }
            }
            return hasBelonging;
        }
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
                if (x == thief.XPosition && y == thief.YPosition && thief.IsInPrison == false)
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
                int xPos = r.Next(0, width);
                int yPos = r.Next(0, height);
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
                int xPos = r.Next(0, width);
                int yPos = r.Next(0, height);
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
                int idNumber = i + 1;
                int xPos = r.Next(0, width);
                int yPos = r.Next(0, height);
                (int xDir, int yDir) = CreateDirection();
                List<Item> swag = new List<Item>
                {
                    new Item("Keys", 0),
                    new Item("Telephone", 0),
                    new Item("Money", 0),
                    new Item("Watch", 0)
                };
                bool isInPrison = false;
                DateTime timeOfCapture = new DateTime();
                double timeInPrison = 0;
                //DateTime timeOfRelease = new DateTime();
                thiefs.Add(new Thief(idNumber, xPos, yPos, xDir, yDir, swag, isInPrison, timeOfCapture, timeInPrison/*, timeOfRelease*/));
            }
        }

        private static (int xDir, int yDir) CreateDirection()
        {
            int xDir = /*GetRandom*/ r.Next(-1, 1 + 1);
            int yDir = r.Next(-1, 1 + 1);
            while (xDir == 0 && yDir == 0)
            {
                yDir = r.Next(-1, 1 + 1);
            }
            return (xDir, yDir);
        }


        //public static int GetRandom(int low, int high)
        //{

        //    //Random r = new Random(); // Är det en dålig ide att skapa en ny random varje gång metoden körs?
        //    // Flyttade Random utanför metoden 
        //    return r.Next(low, high + 1);
        //}
    }
}
