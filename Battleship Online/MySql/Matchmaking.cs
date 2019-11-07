using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;

namespace Battleship_Online.MySql
{
    class Matchmaking
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: Matchmaking Class
         * 
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */
        internal static void Start()
        {
            string[] usrs; //Usernames array
            Random rand = new Random();
            int randomized;

            MySqlCommand com = new MySqlCommand("INSERT INTO `matchmaking`(`username`) VALUES ('" + Dipendences.username + "')", Usr.conn); //Insert user in matchmaking database
            com.ExecuteNonQuery();

            while (true)
            {
                Usr.adapter = new MySqlDataAdapter("SELECT * FROM `matchmaking` WHERE 1", Usr.conn);
                Usr.adapter.Fill(Usr.table);

                usrs = new string[Usr.table.Rows.Count];

                for (int i = 0; i < Usr.table.Rows.Count; i++) //Put query in usrs
                {
                    usrs[i] = Usr.table.Rows[i][0].ToString();
                }

                if(usrs.Length >= 1)
                {
                    randomized = rand.Next(0, usrs.Length);
                    Dipendences.enemyUsername = usrs[randomized]; //Select a random enemy from table

                    if (Dipendences.enemyUsername != Dipendences.username)
                        break; //Matched
                }

                Console.Clear();
                Console.WriteLine("Matchmaking....");
            }

            Thread.Sleep(10000);

            MySqlCommand del = new MySqlCommand("DELETE FROM `matchmaking` WHERE `username` = '" + Dipendences.username + "'", Usr.conn); //Delete usrer from matchmaking table
            del.ExecuteNonQuery();

            Usr.table.Clear();
            Console.WriteLine("The enemy is: " + Dipendences.enemyUsername);
        }
    }
}
