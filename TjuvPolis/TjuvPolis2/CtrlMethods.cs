using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvPolis2
{
    class CtrlMethods
    {
        //Skapade klass för metoder som inte ingår i spelet men är bra att ha för att kolla vad som har hänt.
        //Är det vettigt att ha en klass med bara metoder eller finns det ett annat sätt att gruppera metoder?
        
        public static string PrintCitizen(Citizen c)
        {
            string s = $"xdir: {c.XDirection}, ydir: {c.YDirection}, " +
                    $"xpos: {c.XPosition}, ypos: {c.YPosition}, ";

            for (int i = 0; i < c.Belongings.Count; i++)
            {
                s += $"{c.Belongings[i]}, ";
            }
            return s;
        }
        public static string PrintCitizenList(List<Citizen> c)
        {
            int x = 1;
            string s = "";
            foreach (Citizen citizen in c)
            {
                s += $"Medborgare {x}: xdir: {citizen.XDirection}, ydir: {citizen.YDirection}, " +
                    $"xpos: {citizen.XPosition}, ypos: {citizen.YPosition}, ";

                for (int i = 0; i < citizen.Belongings.Count; i++)
                {
                    s += $"{citizen.Belongings[i]}, ";
                }
                s += "\n";
                x++;
            }
            return s;
        }
        public static string PrintPoliceList(List<Police> p)
        {
            int x = 1;
            string s = "";
            foreach (Police police in p)
            {
                s += $"Medborgare {x}: xdir: {police.XDirection}, ydir: {police.YDirection}, " +
                    $"xpos: {police.XPosition}, ypos: {police.YPosition}, ";

                for (int i = 0; i < police.ConfiscatedItems.Count; i++)
                {
                    s += $"{police.ConfiscatedItems[i]}, ";
                }
                s += "\n";
                x++;
            }
            return s;
        }
        public static string PrintThiefList(List<Thief> t)
        {
            int x = 1;
            string s = "";
            foreach (Thief police in t)
            {
                s += $"Medborgare {x}: xdir: {police.XDirection}, ydir: {police.YDirection}, " +
                    $"xpos: {police.XPosition}, ypos: {police.YPosition}, ";

                for (int i = 0; i < police.Swag.Count; i++)
                {
                    s += $"{police.Swag[i]}, ";
                }
                s += "\n";
                x++;
            }
            return s;
        }
    }
}
