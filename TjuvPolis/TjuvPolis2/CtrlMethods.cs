using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace TjuvPolis2
{
    class CtrlMethods
    {
        //Flytta till klass, använd polymorfism
        
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
        public static string PrintPolice(Police p)
        {
            string s = $"xdir: {p.XDirection}, ydir: {p.YDirection}, " +
                    $"xpos: {p.XPosition}, ypos: {p.YPosition}, ";

            for (int i = 0; i < p.ConfiscatedItems.Count; i++)
            {
                s += $"{p.ConfiscatedItems[i]}, ";
            }
            return s;
        }
        public static string PrintThief(Thief t)
        {
            string s = $"xdir: {t.XDirection}, ydir: {t.YDirection}, " +
                    $"xpos: {t.XPosition}, ypos: {t.YPosition}, ";

            for (int i = 0; i < t.Swag.Count; i++)
            {
                s += $"{t.Swag[i]}, ";
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
                s += $"Polis {x}: xdir: {police.XDirection}, ydir: {police.YDirection}, " +
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
            //int x = 1;
            string s = "";
            foreach (Thief thief in t)
            {
                s += $"Tjuv {thief.IdNumber}: xdir: {thief.XDirection}, ydir: {thief.YDirection}, " +
                    $"xpos: {thief.XPosition}, ypos: {thief.YPosition}, fängelse: {thief.IsInPrison}, ";

                for (int i = 0; i < thief.Swag.Count; i++)
                {
                    s += $"{thief.Swag[i]}, ";
                }
                s += "\n";
                //x++;
            }
            return s;
        }
        public static void StartTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            
            Console.WriteLine($"Starttid: {DateTime.Now}");

        }
    }
}
