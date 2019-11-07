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
            try
            {
                //Layout
                Console.Title = "Battleship Online v. " + Dipendences.version;

                InitializingWorkshop.Check();
                MySql.Usr.Connect(); //Start connection to mysql database

                string inp;

                Console.Clear();
                Console.WriteLine("Type: ");
                Console.WriteLine("1    -    LogIn");
                Console.WriteLine("2    -    SignUp");

                do
                {
                  ret:
                    Console.Write(": ");
                    inp = Console.ReadLine();

                    if (string.IsNullOrEmpty(inp) || string.IsNullOrWhiteSpace(inp))
                        goto ret;
                } while (int.Parse(inp) < 0 || int.Parse(inp) > 2);

                Console.Clear();

                switch (inp)
                {
                    case "1":
                        MySql.Usr.Login();
                        break;

                    case "2":
                        MySql.Usr.Signup();
                        break;

                    default:
                        break;
                }

                if(Dipendences.logged)
                {
                    //Continue
                    Continue();
                }
            } catch(Exception ex)
            {
                Instruments.ErrMessage(ex.ToString()); //Error
            }

            Console.ReadKey();
        }

        internal static void Continue()
        {
            string inp;

            Console.Clear();

            Console.WriteLine("Type: ");
            Console.WriteLine("1    -    Matchmaking");
            Console.WriteLine("2    -    Manual");

            do
            {
                ex:
                Console.Write(": ");
                inp = Console.ReadLine();

                if (string.IsNullOrEmpty(inp) || string.IsNullOrWhiteSpace(inp))
                    goto ex;
            } while (int.Parse(inp) < 0 || int.Parse(inp) > 2);

            switch (inp)
            {
                case "1":
                    MySql.Matchmaking.Start();
                    break;

                case "2":
                    Instruments.Manual();
                    break;

                default:
                    break;
            }
        }
    }
}
