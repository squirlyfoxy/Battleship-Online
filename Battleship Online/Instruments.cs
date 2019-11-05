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
        * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
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
    }
}
