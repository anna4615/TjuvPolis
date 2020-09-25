using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace TjuvPolis
{
    class Program
    {
        static void Main(string[] args)
        {
            int boardWidth = 100;
            int boardHeight = 25;
            int xDirection;
            int yDirection;

            Citizen[] Citizens = new Citizen[GenerateRandomNumber(1, 30 + 1)];
            for (int i = 0; i < Citizens.Length; i++)
            {
                (xDirection, yDirection) = Person.GenerateDirection();

                Citizens[i] = new Citizen(GenerateRandomNumber(1, boardWidth), GenerateRandomNumber(1, boardHeight),
                    xDirection, yDirection);
            }
        }
        public static int GenerateRandomNumber(int low, int high)
        {
            Random r = new Random();
            return r.Next(low, high + 1);
        }
        
        
    }
}
   