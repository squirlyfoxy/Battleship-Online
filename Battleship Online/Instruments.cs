using System;

namespace Battleship_Online
{
    class Instruments
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
        * Company: OSS inc.
        * Program summary: Simulating Battleship Online via MYSQL Database
        * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
        * Class summary: Console Utilities Class
        * 
        * Copyright (c) 2018-20 - All Rights Reserved
        */

        internal static void MysqlMessage(string mesg) //Print Mysql Message
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[MYSQ]: ");
            Console.ResetColor();
            Console.WriteLine(mesg);
        }

        internal static void ErrMessage(string mesg) //Print an error message
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERR]: ");
            Console.ResetColor();
            Console.WriteLine(mesg);
        }

        internal static void GMMessage(string mesg) //Print a message from game engine
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[GM ENGINE]: ");
            Console.ResetColor();
            Console.WriteLine(mesg);
        }

        internal static void Manual() //Print game manual
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            for(int i = 0; i < Console.LargestWindowWidth / 2 - 2; i++) //Write banner 1
            {
                Console.Write("*");
            }

            Console.WriteLine();
            Console.WriteLine("**                                                 Battleship Online                                                **");

            for (int i = 0; i < Console.LargestWindowWidth / 2 - 2; i++) //Write banner 2
            {
                Console.Write("*");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("Battleship Online is an Open Source online game written in c# based on MySql Database");
            Console.WriteLine("Programmers: Leonardo Baldazzi (@leonardobaldazzi_) && Tommaso Brandinelli (@tommib.117)");

            Console.WriteLine();
            Console.Write("Press Any Key To Continue.....");
            Console.ReadKey();


        }
    }
}
