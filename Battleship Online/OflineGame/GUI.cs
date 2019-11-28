using System;

namespace Battleship_Online.OflineGame
{
    class GUI
    {
        private const char g1 = '╔';
        private const char g2 = '═';
        private const char g3 = '╚';
        private const char g4 = '╗';
        private const char g5 = '╝';
        private const char g6 = '║';

        internal static void Initialize()
        {
            Console.Clear();

            int pos = 0;

            //Clear matrix
            Array.Clear(Dipendences.campoNostro, 0, Dipendences.campoNostro.GetLength(0)* Dipendences.campoNostro.GetLength(1));
            Array.Clear(Dipendences.campoNemico, 0, Dipendences.campoNemico.GetLength(0)* Dipendences.campoNemico.GetLength(1));

            Game_Form.Put p = new Game_Form.Put(); //Chiedi le posizioni
            p.ShowDialog();

            foreach (int n in Dipendences.x) //Inserisci le posizioni dentro la matrice
            {
                Dipendences.campoNostro[n, Dipendences.y[pos]] = Dipendences.defStatus;

                pos++;
            }

            Instruments.GMMessage("Initializing AI.....");

            AI.Intialize();

            Console.Clear();

            Aggiorna();

            Console.ReadKey();
        }

        private static void Aggiorna() //Aggiorna la gui
        {
            Console.Clear();

            Dipendences.remaningShips = Dipendences.HOW_MANY_SHIPS;
            Dipendences.sunkenShips = 0;

            Console.Write(Dipendences.username); //Scrivi gli username6
            for (int i = 0; i < (int)Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50; i++)
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

            //Stampa la matrice (Mia)
            for (int i = 0; i < Dipendences.campoNostro.GetLength(0); i++)
            {
                Console.Write(g6);
                for (int x = 0; x < Dipendences.campoNostro.GetLength(1); x++)
                {
                    if (Dipendences.campoNostro[i, x] == Dipendences.colpitoStatus || Dipendences.campoNostro[i, x] == Dipendences.defStatus)
                    {
                        Console.Write(Dipendences.campoNostro[i, x]);
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

            //Stampa la matrice del mio avversario
            Console.SetCursorPosition(Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50, 1);
            Console.Write(g1);
            for (int i = 0; i < Dipendences.campoNemico.GetLength(0); i++)
            {
                Console.Write(g2);
            }
            Console.WriteLine(g4);

            Console.SetCursorPosition(Console.WindowWidth - (Dipendences.username.Length + Dipendences.enemyUsername.Length) - 50, 2);
            for (int i = 1; i <= Dipendences.campoNemico.GetLength(0); i++)
            {
                Console.Write(g6);
                for (int x = 0; x < Dipendences.campoNemico.GetLength(1); x++)
                {
                    if (Dipendences.campoNemico[i - 1, x] == Dipendences.colpitoStatus)
                    {
                        Console.Write(Dipendences.colpitoStatus);
                    }
                    else if (Dipendences.campoNemico[i - 1, x] == Dipendences.mancatoStatus)
                    {
                        Console.Write(Dipendences.mancatoStatus);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
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
    }
}
