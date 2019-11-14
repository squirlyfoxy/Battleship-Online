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

        internal static string chatDownload = "https://onedrive.live.com/download?cid=0C7B692900D659EC&resid=C7B692900D659EC%2114379&authkey=ABHi-O9n225mLdo";
        internal static string meDownload = "https://onedrive.live.com/download?cid=0C7B692900D659EC&resid=C7B692900D659EC%2114384&authkey=ACQ-dxB3bZmwe4M";
        internal static string otherDownload = "https://onedrive.live.com/download?cid=0C7B692900D659EC&resid=C7B692900D659EC%2114391&authkey=AJNorS109sVtLrA";
        internal static string launcherDownload = "https://onedrive.live.com/download?cid=0C7B692900D659EC&resid=C7B692900D659EC%2114354&authkey=AIzPOqxddxfnms8";

        internal static string connStr; //Connection string for mysql

        internal static string username;
        internal static string password;
        internal static string enemyUsername;

        internal static int[] x = new int[HOW_MANY_SHIPS]; //Coordinate nostre
        internal static int[] y = new int[HOW_MANY_SHIPS];

        internal static int[] enemyX = new int[HOW_MANY_SHIPS]; //Coordinate nemiche
        internal static int[] enemyY = new int[HOW_MANY_SHIPS];

        internal static char[,] campoNostro = new char[10, 10]; //Campo matrix
        internal static char[,] campoNemico = new char[10, 10]; //Campo Nemico matrix

        internal static int remaningShips = HOW_MANY_SHIPS;
        internal static int sunkenShips = 0;

        internal static bool logged = false;

        internal const int HOW_MANY_SHIPS = 7;

        internal static char defStatus = 'N';
        internal static char mancatoStatus = '0';
        internal static char colpitoStatus = 'X';
    }
}