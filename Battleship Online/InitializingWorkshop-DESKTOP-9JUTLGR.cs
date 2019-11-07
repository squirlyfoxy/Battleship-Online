using System;

namespace Battleship_Online
{
    class InitializingWorkshop
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: Check if it's the first time, call check for updates (if true first update, after download), download from internet other programs to run
         *  
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */

        internal static void Check()
        {
            Updates.CheckForUpdates.Check();

            Instruments.GMMessage("I'm checking if this is the first time you run me ...");

            if (CheckDir())
            {
                //It's not the first time
                Instruments.GMMessage("This is not the first time you run me, welcome!");
            }
            else
            {
                //It's the first time :(, creating directory and downloading files....
                Instruments.GMMessage("This is the first time that I'm running in this machine, I will have to download some essential files for my execution...");

                Instruments.GMMessage("Creating directory in: C:/");
                System.IO.Directory.CreateDirectory(@"C:\Battleship Online\");
                Instruments.GMMessage("DONE");
            }
        }

        private static bool CheckDir()
        {
            bool toReturn = false;

            if (System.IO.Directory.Exists(@"C:\Battleship Online\") && System.IO.File.Exists(@"C:\Battleship Online\configuration.conf") && System.IO.File.Exists(@"C:\Battleship Online\Battleship Online - Chat.exe") && System.IO.File.Exists(@"C:\Battleship Online\Battleship Online - Me.exe") && System.IO.File.Exists(@"C:\Battleship Online\Battleship Online - Other.exe"))
            {
                toReturn = true;
            }

            return toReturn;
        }
    }
}
