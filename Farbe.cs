using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace _20200325_Mastermind_LucaKeller
{
    public class ColorPick
    {
        public static Color farbe(int wert)
        {
            switch (wert)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Lime;
                case 3:
                    return Color.Pink;
                case 4:
                    return Color.Yellow;
                case 5:
                    return Color.Green;
                case 6:
                    return Color.Orange;
                case 7:
                    return Color.Cyan;
                default:
                    return Color.White;
            }
        }
        public static int farbe(Color color)
        {

            if (color == Color.Red)
                return 0;
            else if (color == Color.Blue)
                return 1;
            else if (color == Color.Lime)
                return 2;
            else if (color == Color.Pink)
                return 3;
            else if (color == Color.Yellow)
                return 4;
            else if (color == Color.Green)
                return 5;
            else if (color == Color.Orange)
                return 6;
            else if (color == Color.Cyan)
                return 7;
            else
                return -1;
        }

        public static Color schwarz() => Color.Black;
        public static Color white() => Color.White;
    }
}
