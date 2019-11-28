using System;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Battleship_Online.MySql
{
    class Usr
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: User login / Signup
         * 
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */

        internal static MySqlConnection conn;
        internal static MySqlDataAdapter adapter;
        internal static DataTable table = new DataTable();
        internal static MySqlCommand command;

        internal static void Connect() //Connect to MySql
        {
            /*
            //Download Connection string (enrypted)
            var downloadClient = new WebClient();
            downloadClient.DownloadFile(Dipendences.connStrDownload, @"C:\Battleship Online\connStr.txt");

            string[] cont = System.IO.File.ReadAllLines(@"C:\Battleship Online\connStr.txt"); //Read connStr file

            System.IO.File.Delete(@"C:\Battleship Online\connStr.txt"); //Delete connStr file

            //Decript cont
            Encoding en = Encoding.Default;

            Dipendences.connStr = Security.AES.Decrypt(cont[0], en.GetBytes("!e2e4ab3a960c3!a"), en.GetBytes("128"));
            Dipendences.connStr= Dipendences.connStr.Remove(Dipendences.connStr.Length - 3, 3); //Cut last 3 special char
            */
            Ping ping = new Ping();

            try
            {
                PingReply pingReply = ping.Send("51.83.46.129");

                if (pingReply.Status == IPStatus.Success)
                {
                    Console.Title = Console.Title + " - Online";

                    Dipendences.connStr = "Server=51.83.46.129;Database=gioco;Uid=dio;Pwd=leonardo1;";

                    conn = new MySqlConnection(Dipendences.connStr);
                    conn.Open(); //Open connection

                    command = conn.CreateCommand();
                }
                else
                {
                    //Offline Mode
                    Console.Title = Console.Title + " - Offline";

                    Dipendences.offMode = true;
                }
            } catch
            {
                //Offline Mode
                Console.Title = Console.Title + " - Offline";

                Dipendences.offMode = true;
            }
        }

        internal static void Login() //Login in mysql database
        {
            do
            {
                Console.Write("Username? ");
                Dipendences.username = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(Dipendences.username) || string.IsNullOrEmpty(Dipendences.username));

            do
            {
                Console.Write("Password? ");
                Dipendences.password = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(Dipendences.password) || string.IsNullOrEmpty(Dipendences.password));

            Dipendences.password = Security.AES.CalculateMD5Hash(Dipendences.password);

            //Check if Exist on mysql
            adapter = new MySqlDataAdapter("SELECT * FROM usr WHERE username = '" + Dipendences.username +  "' AND password = '" + Dipendences.password + "'", conn);
            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                //Invalid Login
                Instruments.ErrMessage("Username or Password invalid");

                Console.WriteLine("Press Any Key To Continue...");
                Console.ReadKey();
                Console.Clear();

                Login();
            }
            else
            {
                //Valid Login
                Dipendences.logged = true;
            }

            table.Clear();

            Del(); //Delete all posizioni and mosse rows
        }

        private static void Del()
        {
            MySqlCommand delP = new MySqlCommand("DELETE FROM `Posizioni` WHERE `username` = '" + Dipendences.username + "'", Usr.conn); //Delete in Posizioni table
            delP.ExecuteNonQuery();

            MySqlCommand delM = new MySqlCommand("DELETE FROM `Mosse` WHERE `username` = '" + Dipendences.username + "'", Usr.conn); //Delete in Mosse table
            delM.ExecuteNonQuery();

            MySqlCommand delMa = new MySqlCommand("DELETE FROM `matchmaking` WHERE `username` = '" + Dipendences.username + "'", Usr.conn); //Delete in matchmaking table
            delMa.ExecuteNonQuery();

            MySqlCommand delTu = new MySqlCommand("DELETE FROM `Turno` WHERE `username` = '" + Dipendences.username + "'", Usr.conn); //Delete in matchmaking table
            delTu.ExecuteNonQuery();
        }

        internal static void Signup() //Signup in mysql database
        {
            bool usrAlrealyExist = false;

            do
            {
                usrAlrealyExist = false;

                reg:
                Console.Write("Username? ");
                Dipendences.username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(Dipendences.username) || string.IsNullOrEmpty(Dipendences.username))
                    goto reg;

                //Check if usr alrealy exist on mysql database
                adapter = new MySqlDataAdapter("SELECT * FROM usr WHERE username = '" + Dipendences.username + "'", conn);
                adapter.Fill(table);

                if (!(table.Rows.Count <= 0))
                {
                    usrAlrealyExist = true;
                }

                table.Clear();

            } while (usrAlrealyExist);

            do
            {
                Console.Write("Password? ");
                Dipendences.password = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(Dipendences.password) || string.IsNullOrEmpty(Dipendences.password));

            Dipendences.password = Security.AES.CalculateMD5Hash(Dipendences.password);

            MySqlCommand com = new MySqlCommand("INSERT INTO `usr`(`username`, `password`) VALUES (@usr, @pass)", conn); //Puts username and password(md5 hash) in usr table

            com.Parameters.Add("@usr", MySqlDbType.VarChar).Value = Dipendences.username;
            com.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Dipendences.password;

            if (com.ExecuteNonQuery() == 1) //Execute query
                Dipendences.logged = true;

            Del(); //Delete all posizioni and mosse rows
        }
    }
}
