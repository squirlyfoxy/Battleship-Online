using System.Windows;

namespace Battleship_Online
{
    class Dipendences
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
        * Company: OSS inc.
        * Program summary: Simulating Battleship Online via MYSQL Database
        * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
        * Class summary: Variables Class
        * 
        * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
        */

        internal static string version = "1";

        internal static string lastVersionFile = "https://pastebin.com/raw/MSCZ7Mqa";
        internal static string changelogFile = "https://pastebin.com/raw/NjDie2h1";
        internal static string connStrDownload = "https://pastebin.com/raw/3kUNdJbx";

        internal static string connStr; //Connection string for mysql

        internal static string username;
        internal static string password;
        internal static string enemyUsername;

        internal static bool logged = false;
    }
}