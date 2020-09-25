using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvPolis2
{
    class Person
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public Person(int xPosition, int yPosition, int xDirection, int yDirection)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            XDirection = xDirection;
            YDirection = yDirection;
        }
    }
    class Citizen : Person
    {
        public List<Item> Belongings { get; set; }
        public Citizen(int xPosition, int yPosition, int xDirection, int yDirection) :
            base (xPosition, yPosition, xDirection, yDirection)
        {
            Belongings = new List<Item>();
            Belongings.Add(new Item("Keys", 1));
            Belongings.Add(new Item("Telephone", 1));
            Belongings.Add(new Item("Money", 1));
            Belongings.Add(new Item("Watch", 1));
        }
    }
    class Police : Person
    {
        public List<Item> ConfiscatedItems { get; set; }
        public Police(int xPosition, int yPosition, int xDirection, int yDirection) :
            base (xPosition, yPosition, xDirection, yDirection)
        {
            ConfiscatedItems = new List<Item>
            {
                new Item("Keys", 0),
                new Item("Telephone", 0),
                new Item("Money", 0),
                new Item("Watch", 0)
            };

        }
    }
    class Thief : Person
    {
        public List<Item> Swag { get; set; }
        public Thief(int xPosition, int yPosition, int xDirection, int yDirection) :
            base(xPosition, yPosition, xDirection, yDirection)
        {
            Swag = new List<Item>
            {
                new Item("Keys", 0),
                new Item("Telephone", 0),
                new Item("Money", 0),
                new Item("Watch", 0)
            };
        }
    }
}
