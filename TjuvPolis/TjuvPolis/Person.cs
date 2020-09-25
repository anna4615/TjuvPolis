using System.Collections.Generic;

namespace TjuvPolis
{
    class Person
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public Person(int xPosition, int yPosition, int xDirection, int yDirection)
        {
            XPosition = Program.GenerateRandomNumber(1, boardWidth + 1); ;
            YPosition = yPosition;
            XDirection = xDirection;
            YDirection = yDirection;
        }
        public static (int xDirection, int yDirection) GenerateDirection()
        {
            int xDirection = Program.GenerateRandomNumber(-1, 1 + 1);
            int yDirection = Program.GenerateRandomNumber(-1, 1 + 1);
            while (xDirection == 0 && yDirection == 0)
            {
                yDirection = Program.GenerateRandomNumber(-1, 1 + 1);
            }
            return (xDirection, yDirection);
        }
        public virtual void CreatePerson(int boardWidth, int boardHeight)
        {
            int xPosition = Program.GenerateRandomNumber(1, boardWidth + 1);
            int yPosition = Program.GenerateRandomNumber(1, boardHeight + 1);
            (int xDirection, int yDirection) = GenerateDirection();
            Citizen c = new Citizen(xPosition, yPosition, xDirection, yDirection);
        }
    }
    class Citizen : Person
    {
        public List<Inventory> Belongings { get; set; }

        public Citizen(int xPosition, int yPosition, int xDirection, int yDirection) : 
            base(xDirection,yDirection, xPosition, yPosition)
        {
            Belongings = new List<Inventory>();
            Belongings.Add(new Inventory("Keys", 1));
            Belongings.Add(new Inventory("Telephone", 1));
            Belongings.Add(new Inventory("Money", 1));
            Belongings.Add(new Inventory("Watch", 1));
        }
        public static void createCitizen(int boardWidth, int boardHeight)
        {
            int xPosition = Program.GenerateRandomNumber(1, boardWidth + 1);
            int yPosition = Program.GenerateRandomNumber(1, boardHeight + 1);
            Citizen c = new Citizen(xPosition, yPosition, xDirection, yDirection);
        }
    }
    class Police : Person
    {
        public List<Inventory> ConfiscatedItems { get; set; }
        public Police(int xPosition, int yPosition, int xDirection, int yDirection) :
            base(xDirection, yDirection, xPosition, yPosition)
        {
            ConfiscatedItems = new List<Inventory>
            {
                new Inventory("Keys", 0),
                new Inventory("Telephone", 0),
                new Inventory("Money", 0),
                new Inventory("Watch", 0)
            };
        }
        
    }
    class Thief : Person
    {
        public List<Inventory> Swag { get; set; }
        public Thief(int xPosition, int yPosition, int xDirection, int yDirection) :
            base(xDirection, yDirection, xPosition, yPosition)
        {
            Swag = new List<Inventory>
            {
                new Inventory("Keys", 0),
                new Inventory("Telephone", 0),
                new Inventory("Money", 0),
                new Inventory("Watch", 0)
            }; 
        }
    }
}
