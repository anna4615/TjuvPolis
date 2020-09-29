using System;
using System.Collections.Generic;

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
        public Citizen(int xPosition, int yPosition, int xDirection, int yDirection, List<Item> belongings) :
            base(xPosition, yPosition, xDirection, yDirection)
        {
            Belongings = belongings;
        }
    }
    class Police : Person
    {
        public List<Item> ConfiscatedItems { get; set; }
        public Police(int xPosition, int yPosition, int xDirection, int yDirection, List<Item> confiscatedItems) :
            base(xPosition, yPosition, xDirection, yDirection)
        {
            ConfiscatedItems = confiscatedItems;
        }
    }
    class Thief : Person
    {
        public List<Item> Swag { get; set; }
        public Thief(int xPosition, int yPosition, int xDirection, int yDirection, List<Item> swag) :
            base(xPosition, yPosition, xDirection, yDirection)
        {
            Swag = swag;
        }
    }
}
