using System;

namespace Battleship_Online
{
    class Program
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: Main Class
         * 
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */
        static void Main(string[] args)
        {
            //Layout
            Console.Title = "Battleship Online v. " + Dipendences.version;

            InitializingWorkshop.Check();

            Console.ReadKey();
        }
    }
}
