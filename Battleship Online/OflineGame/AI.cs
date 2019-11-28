using System;

namespace Battleship_Online.OflineGame
{
    internal class AI
    {
        internal static void Intialize()
        {
            //Spawn random pisitions....
            int x, y;

            Random rn = new Random();

            Instruments.GMMessage("Generazione navi random..."); 

            for(int i = 0; i < Dipendences.HOW_MANY_SHIPS; i++) //Genera le coordinate
            {
                rifai:
                    x = rn.Next(0, Dipendences.campoNemico.GetLength(0));
                    y = rn.Next(0, Dipendences.campoNemico.GetLength(1));

                if (Dipendences.campoNemico[x, y] == Dipendences.defStatus)
                    goto rifai;

                Dipendences.campoNemico[x, y] = Dipendences.defStatus; //Inserisci la posizione dentro il campo nemico
            }
        }
    }
}