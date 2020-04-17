using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20200325_Mastermind_LucaKeller
{
    class Spielinfos
    {
        const int maxVersuche = 12;
        public static string gameOver(int anzahlVersuche, int schwarz)
        {
            if(schwarz == 5)
            {
                return $"Saubere sache, du hast alles nach {anzahlVersuche} erraten !";
            }
            else if(anzahlVersuche == maxVersuche)
            {
                return $"Schade aber auch, du hast den letzten Versuch verbraucht. {schwarz} von 5 waren richtig !";
            }
            return null;
        }
    }
}
