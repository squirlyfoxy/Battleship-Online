using System;
using System.Net;

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
            CreateDir();

            Updates.CheckForUpdates.Check();

            Instruments.GMMessage("I'm checking if this is the first time you run me ...");

            if (CheckDir())
            {
                //It's not the first time
                Instruments.GMMessage("This is not the first time you run me, welcome!");
            }
            else
            {
                //It's the first time :(, creating directory and downloading files.... (Not working)
                Instruments.GMMessage("This is the first time that I'm running in this machine, I will have to download some essential files for my execution...");

                /*
                var download = new WebClient();

                Instruments.GMMessage("Downloading Battleship Online - Chat.exe...");
                download.DownloadFile(Dipendences.chatDownload, @"C:\Battleship Online\Battleship Online - Chat.exe");

                Instruments.GMMessage("Downloading Battleship Online - Me.exe...");
                download.DownloadFile(Dipendences.meDownload, @"C:\Battleship Online\Battleship Online - Me.exe");

                Instruments.GMMessage("Downloading Battleship Online - Other.exe...");
                download.DownloadFile(Dipendences.otherDownload, @"C:\Battleship Online\Battleship Online - Other.exe");
                */

                Instruments.GMMessage("DONE");
            }
        }

        private static void CreateDir()
        {
            //Create Battleship Directory
            if(!System.IO.Directory.Exists(@"C:\Battleship Online\"))
            {
                Instruments.GMMessage("Creating directory in: C:/");
                System.IO.Directory.CreateDirectory(@"C:\Battleship Online\");
            }
        }

        private static bool CheckDir() //Check if game files alrealy exist
        {
            bool toReturn = false;

            if (System.IO.File.Exists(@"C:\Battleship Online\Battleship Online - Chat.exe") && System.IO.File.Exists(@"C:\Battleship Online\Battleship Online - Me.exe") && System.IO.File.Exists(@"C:\Battleship Online\Battleship Online - Other.exe"))
            {
                toReturn = true;
            }

            return toReturn;
        }
    }
}
