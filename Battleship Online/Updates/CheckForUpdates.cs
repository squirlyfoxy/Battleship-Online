using System;
using System.Net;

namespace Battleship_Online.Updates
{
    class CheckForUpdates
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: Check for update, if true show progress bar and download
         *  
         * Copyright (c) 2018-20 - All Rights Reserved
         */
        internal static void Check()
        {
            bool isChecked = true;

            /*
            //Download lastest version file
            var downloadClient = new WebClient();
            downloadClient.DownloadFile(Dipendences.lastVersionFile, @"C:\Battleship Online\ver.txt");

            string[] versionDownloaded = System.IO.File.ReadAllLines(@"C:\Battleship Online\ver.txt");

            if(versionDownloaded[0] == Dipendences.version) //Check if this is the lastes version
            {
                isChecked = false;
            }

            */

            if (!isChecked)
            {
                //Show update form
                Changelog ch = new Changelog();
                ch.ShowDialog();
            }
            else
            {
                Instruments.GMMessage("This is the lastest version!");
            }
        }
    }
}
