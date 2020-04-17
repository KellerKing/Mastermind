using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20200325_Mastermind_LucaKeller
{
    class Master
    {
        public int[] zahl { get; private set; } //Array mit den Lösung

        public Master()
        {
            start();
        }

        private void start()
        {
            int counter = 5;
            int ubound = 8; //Da es nur 8 Farben gibt
            int number;

            zahl = new int[counter];
            Random r = new Random();

            for (int i = 0; i < counter; i++)
            {
                number = r.Next(0, ubound);

                if (zahl.Contains(number)) //Wenn die Zahl im Array vorhanden ist
                {
                    i--; //Damit dieser Array eintrag nochmal durgegangen wird
                }
                else
                {
                    zahl[i] = number; //Speichert die Zahl
                }
            }
        }

        public int getSchwarz(int[] versuch)
        {
            int erraten = 0;

            for (int i = 0; i < zahl.Length; i++)
            {
                if(zahl[i] == versuch[i]) //Wenn gleiche stellen den selben wert besitzen
                {
                    erraten++;
                }
            }
            return erraten;
        }

        public int getWeiss(int[] versuch)
        {
            int erraten = 0;

            //Geht beide Arrays durch, und verleicht jeden Wert ob er vorhanden ist.
            for (int i = 0; i < zahl.Length; i++)
            { 

                for (int j = 0; j < versuch.Length; j++)
                {
                    if (i == j) //Es soll nur geprüft werdeb ob gleiche Werte an anderer Stelle sind. Darum interessiert die selbe position nicht
                    {
                        continue;
                    }
                    if (zahl[i] == versuch[j]) //Es wird jeder Wert auf gleichheit geprüft (außer die gleichen stellen)
                    {
                        erraten++;                    
                    }
                } //end inner for
            }// end outer for
            return erraten;
        }
    }
}
