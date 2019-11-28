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
         * Per modificare questi file prima bisogna essere provvisti dell'estensione di GitHub per visual studio per poter caricare i cambiamenti e avere sempre un backup
         * 
         * Prima di avviare compilare in modalità release
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

                int a;

                string inp;

                if (!Dipendences.offMode)
                {
                    Console.Clear();
                    Console.WriteLine("Type: ");
                    Console.WriteLine("1    -    LogIn");
                    Console.WriteLine("2    -    SignUp");

                    do
                    {
                    ret:
                        Console.Write(": ");
                        inp = Console.ReadLine();

                        if (string.IsNullOrEmpty(inp) || string.IsNullOrWhiteSpace(inp) || !int.TryParse(inp, out a))
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

                    if (Dipendences.logged)
                    {
                        //Continue
                        ContinueOnline();
                    }
                }
                else
                {
                    a:
                        Console.Clear();

                        chUsr:
                            Console.Write("Inserisci il tuo username: ");
                            Dipendences.username = Console.ReadLine();

                        if (string.IsNullOrEmpty(Dipendences.username) || string.IsNullOrWhiteSpace(Dipendences.username))
                            goto chUsr;

                        Dipendences.enemyUsername = "PC";


                        OflineGame.GUI.Initialize();

                    goto a;

                }
            } catch(Exception ex)
            {
                Instruments.ErrMessage(ex.ToString()); //Error
            }

            Console.ReadKey();
        }

        internal static void ContinueOnline() //Continue Main method execution
        {
            string inp;
            int a;

            Console.Clear();

            Console.WriteLine("Type: ");
            Console.WriteLine("1    -    Matchmaking");
            Console.WriteLine("2    -    Offline");
            Console.WriteLine("3    -    Manual");

            do
            {
                ex:
                Console.Write(": ");
                inp = Console.ReadLine();

                if (string.IsNullOrEmpty(inp) || string.IsNullOrWhiteSpace(inp) || !int.TryParse(inp, out a))
                    goto ex;
            } while (int.Parse(inp) < 0 || int.Parse(inp) > 3);

            switch (inp)
            {
                case "1":
                    MySql.Matchmaking.Start(); //Start matchmaking
                    break;

                case "2":
                    OflineGame.GUI.Initialize(); //Initialize offline game
                    break;

                case "3":
                    Instruments.Manual(); //Show manual
                    break;

                default:
                    break;
            }
        }
    }
}
