using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship_Online.Game_Form
{
    class Game
    {
        private const char g1 = '╔';
        private const char g2 = '═';
        private const char g3 = '╚';
        private const char g4 = '╗';
        private const char g5 = '╝';
        private const char g6 = '║';

        private static int firstTime = 0; 

        internal static void Initialize()
        {
            int pos = 0;


            foreach (int n in Dipendences.x) //Insert my ships positions
            {
                MySqlCommand add = new MySqlCommand("INSERT INTO `Posizioni`(`username`, `x`, `y`, `Status`) VALUES (@usr, @x, @y, @Status)", MySql.Usr.conn); //Insert ships positions in Posizioni database
                add.Parameters.Add("@usr", MySqlDbType.VarChar).Value = Dipendences.username;
                add.Parameters.Add("@x", MySqlDbType.VarChar).Value = n;
                add.Parameters.Add("@y", MySqlDbType.VarChar).Value = Dipendences.y[pos];
                add.Parameters.Add("@Status", MySqlDbType.VarChar).Value = Dipendences.defStatus;

                add.ExecuteNonQuery();

                pos++;
            }

            //Scrivi le posizioni dentro la matrice del nostro campo (label = righe)
            pos = 0;

            foreach (int n in Dipendences.x)
            {
                Dipendences.campoNostro[n, Dipendences.y[pos]] = Dipendences.defStatus;

                pos++;
            }

            Aspetta(0); //Aspetta che finisca l'avversario di posizionare le navi

            //Metti le posizioni del nemico dentro un'altra matrice
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            pos = 0;

            for (int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro due array
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.enemyX[i] = Convert.ToInt32(MySql.Usr.table.Rows[i][1]);
                    Dipendences.enemyY[i] = Convert.ToInt32(MySql.Usr.table.Rows[i][2]);
                }
            }

            foreach (int n in Dipendences.enemyX)
            {
                Dipendences.campoNemicoDiNuovo[n, Dipendences.enemyY[pos]] = Dipendences.defStatus;

                pos++;
            }

            Aggiorna();

            //Thread t = new Thread(Gioca); //Aggiorna mentre chiedi all'utente le variabili

            Gioca();

            Aggiorna();

            Program.Continue();


        }

        private static void Gioca()
        {
            string x;
            string y;

            int conX = 0, conY = 0;

            for (; true ; ) //Giocaaaa
            {
                //Thread.Sleep(2000);

                do
                {
                _x:
                    Console.Write("Inserisci la coordinata x che vuoi attaccare: ");
                    x = Console.ReadLine();

                    if ((string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x)) || !int.TryParse(x, out conX))
                        goto _x;

                } while (conX < -1 || conX > Dipendences.campoNostro.GetLength(0));

                Console.WriteLine();

                do
                {
                _y:
                    Console.Write("Inserisci la coordinata y che vuoi attaccare: ");
                    y = Console.ReadLine();

                    if (string.IsNullOrEmpty(y) || string.IsNullOrWhiteSpace(y) || !int.TryParse(y, out conY))
                        goto _y;

                } while (conY < -1 || conY > Dipendences.campoNostro.GetLength(1));

                Console.WriteLine();

                MySqlCommand add = new MySqlCommand("INSERT INTO `Mosse`(`username`, `x`, `y`) VALUES (@usr,@x,@y)", MySql.Usr.conn); //Inserisci dentro il databse mysql
                add.Parameters.Add("@usr", MySqlDbType.VarChar).Value = Dipendences.username;
                add.Parameters.Add("@x", MySqlDbType.VarChar).Value = conX;
                add.Parameters.Add("@y", MySqlDbType.VarChar).Value = conY;

                add.ExecuteNonQuery();

                //Controlla se alla coordinata esiste una nave
                if (Dipendences.campoNemicoDiNuovo[conX, conY] != Dipendences.defStatus)
                {
                    //Non colpita :(
                    Console.Write("Non colpita");

                    MySql.Usr.command.CommandText = "UPDATE `Posizioni` SET `Status`='" + Dipendences.mancatoStatus + "' WHERE username='" + Dipendences.enemyUsername + "' AND x='" + conX + "' AND y='" + conY + "';";
                    MySqlDataReader upNonColpito = MySql.Usr.command.ExecuteReader();

                    Dipendences.campoNemico[conX, conY] = Dipendences.mancatoStatus; //Update matrice
                    upNonColpito.Close();

                    Dipendences.campoNemicoDiNuovo[conX, conY] = 'a';
                }
                else if (Dipendences.campoNemicoDiNuovo[conX, conY] == Dipendences.defStatus)
                {
                    //Colpita :)
                    Console.Write("Colpia :)");

                    MySql.Usr.command.CommandText = "UPDATE `Posizioni` SET `Status`='" + Dipendences.colpitoStatus + "' WHERE username='" + Dipendences.enemyUsername + "' AND x='" + conX + "' AND y='" + conY + "';";
                    MySqlDataReader upColpito = MySql.Usr.command.ExecuteReader();

                    Dipendences.campoNemico[conX, conY] = Dipendences.colpitoStatus; //Update matrice

                    Dipendences.remaningShips--;
                    Dipendences.sunkenShips++;
                    upColpito.Close();

                    Dipendences.campoNemicoDiNuovo[conX, conY] = 'a';
                }

                if (Dipendences.sunkenShips == Dipendences.HOW_MANY_SHIPS)
                {
                    Console.Clear();

                    Console.WriteLine("Hai vinto!!!");
                    Console.ReadKey();

                    break;
                }
                else
                {
                    if (Dipendences.remaningShips == 0)
                    {
                        Console.Clear();

                        Console.WriteLine("Hai perso :(");
                        Console.ReadKey();

                        break;
                    }
                }

                Console.ReadKey();

            }
        }

        private static void Aggiorna() //Aggiorna la gui
        {
            Console.Clear();

            Dipendences.remaningShips = Dipendences.HOW_MANY_SHIPS;
            Dipendences.sunkenShips = 0;

            //Gui Dimensions
            //Console.SetWindowSize(800, 600);

            Console.Write(Dipendences.username);
            for(int i = 0; i < (int)Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(Dipendences.enemyUsername);

            Console.Write(g1);
            for (int i = 0; i < Dipendences.campoNostro.GetLength(0); i++)
            {
                Console.Write(g2);
            }
            Console.WriteLine(g4);

            //Acquisisci la matrice
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.username + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            for (int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro due array e una matrice
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.campoNostro[Convert.ToInt32(MySql.Usr.table.Rows[i][1]), Convert.ToInt32(MySql.Usr.table.Rows[i][2])] = Convert.ToChar(MySql.Usr.table.Rows[i][3]);
                }
            }
            
            //Stampa la matrice (Mia)
            for (int i = 0; i < Dipendences.campoNostro.GetLength(0); i++)
            {
                Console.Write(g6);
                for (int x = 0; x < Dipendences.campoNostro.GetLength(1); x++)
                {
                    if(Dipendences.campoNostro[i, x] == Dipendences.colpitoStatus || Dipendences.campoNostro[i, x] == Dipendences.defStatus || Dipendences.campoNostro[i, x] == Dipendences.mancatoStatus)
                    {
                        Console.Write(Dipendences.campoNostro[i, x]);

                        if(Dipendences.campoNostro[i, x] == Dipendences.colpitoStatus)
                            Dipendences.remaningShips--;
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine(g6);
            }

            Console.Write(g3);
            for (int i = 0; i < Dipendences.campoNostro.GetLength(0); i++) //Dipendences.campoNostro.GetLength(0) = 10
            {
                
                Console.Write(g2);
            }
            Console.WriteLine(g5);

            //Scarica la matrice del mio avversario
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            for (int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro due array e una matrice
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.campoNemico[Convert.ToInt32(MySql.Usr.table.Rows[i][1]), Convert.ToInt32(MySql.Usr.table.Rows[i][2])] = Convert.ToChar(MySql.Usr.table.Rows[i][3]);
                }
            }

            MySql.Usr.table.Clear();

            //Stampa la matrice del mio avversario
            Console.SetCursorPosition(Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50, 1);
            Console.Write(g1);
            for (int i = 0; i < Dipendences.campoNemico.GetLength(0); i++)
            {
                Console.Write(g2);
            }
            Console.WriteLine(g4);

            Console.SetCursorPosition(Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50, 2);
            for (int i = 0; i < Dipendences.campoNemico.GetLength(0); i++)
            {
                Console.Write(g6);
                if (firstTime != 0)
                {
                    for (int x = 0; x < Dipendences.campoNemico.GetLength(1); x++)
                    {
                        if (Dipendences.campoNemico[i, x] == Dipendences.colpitoStatus || Dipendences.campoNemico[i, x] == Dipendences.mancatoStatus)
                        {
                            Console.Write(Dipendences.campoNemico[i, x]);

                            if (Dipendences.campoNemico[i, x] == Dipendences.colpitoStatus)
                                Dipendences.sunkenShips++;
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                }
                else
                {
                    for (int x = 0; x < Dipendences.campoNemico.GetLength(1); x++)
                        Console.Write(" ");

                    firstTime++;
                }
                Console.WriteLine(g6);
                Console.SetCursorPosition(Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50, 2 + i);
            }

            Console.Write(g3);
            for (int i = 0; i < Dipendences.campoNemico.GetLength(0); i++)
            {
                Console.Write(g2);
            }
            Console.WriteLine(g5);

            Console.SetCursorPosition(0, Dipendences.campoNemico.GetLength(1) + 3);

            Console.WriteLine("Sunken ships: " + Dipendences.sunkenShips);
            Console.WriteLine("Remaning ships: " + Dipendences.remaningShips);
        }

        private static void Aspetta(int v) //Aspetta qualcosa
        {
            if(v == 0) //Aspetta la posizione delle navi
            {
                int c = 0;

                Instruments.GMMessage("I'm waiting for the ships...");

                while (true)
                {
                    //Conta quante sono le posizioni
                    MySql.Usr.command.CommandText = "SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'";
                    MySqlDataReader readPos = MySql.Usr.command.ExecuteReader();

                    MySql.Usr.table.Load(readPos);

                    c = MySql.Usr.table.Rows.Count;

                    readPos.Close();

                    if (c == Dipendences.HOW_MANY_SHIPS)
                    {
                        MySql.Usr.table.Clear();


                        Console.Clear();
                        break;
                    }

                    MySql.Usr.table.Clear();
                }
            }
        }
    }
}
