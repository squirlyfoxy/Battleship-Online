using System;

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
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */
        internal static void Check()
        {
            bool isChecked = true;

            if(isChecked)
            {
                //Show update form
                Changelog ch = new Changelog();
                ch.ShowDialog();
            }
        }
    }
}
