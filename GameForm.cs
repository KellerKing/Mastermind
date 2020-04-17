using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace _20200325_Mastermind_LucaKeller
{
    public partial class GameForm : Form
    {
        private Master master;
        private Soundplayer player;

        private Panel[] panelFarbwahl;
        private Panel[,] panelVersuche;
        private Panel[,] panelSchwarzWeiss;

        private Resultpanel loesungsbildschirm;

        private GroupBox versuchsbox = new GroupBox();
        private GroupBox farbwahlbox = new GroupBox();

        private Button auswertenButton;
        private Button soundButton;
        private Button cheatButton;


        //Default größen der Panels
        const int SQUARE = 50;
        const int VERSUCHSQUARE = 30;
        const int RESULTSQUARE = 20;

        private Color pickedColor = Color.Transparent; //Speichert ausgewählte Farbe Transparent = nichts ausgewählt

        private int[] auswahl;//Array mit Farben(Werten) des aktuellen Versuchs

        private int versuch; //Der aktuelle Versuch

        public GameForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            versuch = 0;
            auswahl = new int[] { -1, -1, -1, -1, -1 };

            panelFarbwahl = new Panel[8];
            panelVersuche = new Panel[12, 5]; //Spalten,Zeilen
            panelSchwarzWeiss = new Panel[12, 5];
            auswertenButton = new Button();
            soundButton = new Button();
            cheatButton = new Button();

            master = new Master();
            player = new Soundplayer(@"..\..\Asset\Please_01.wav");

            //Plaziert die Elemente
            initFarbwahl();
            initPanelVersuche();
            initPanelSchwarzWeiss();
            AddButton();
        }




        //______________________________________Zeichnen der Panels________________________

        private void initPanelSchwarzWeiss()
        {
            for (int i = 0; i < panelSchwarzWeiss.GetLength(0); i++)
            {
                for (int j = 0; j < panelSchwarzWeiss.GetLength(1); j++)
                {
                    panelSchwarzWeiss[i, j] = new Panel();
                    panelSchwarzWeiss[i, j].Width = RESULTSQUARE;
                    panelSchwarzWeiss[i, j].Height = RESULTSQUARE;

                    panelSchwarzWeiss[i, j].Left = panelVersuche[i, 4].Location.X + RESULTSQUARE + 10 + (j * 25) + 50;
                    //TODO: dieErgebnisse[i, j].Left = farbwahlbox.Location.X + (farbwahlbox.Width -  (i*RESULTSQUARE) - 10);
                    panelSchwarzWeiss[i, j].Top = panelVersuche[i, 4].Location.Y + ((panelVersuche[i, j].Height - RESULTSQUARE) / 2);

                    panelSchwarzWeiss[i, j].BackColor = Color.LightGray;

                    panelSchwarzWeiss[i, j].Parent = this;
                    panelSchwarzWeiss[i, j].BringToFront();
                }
            }
        }
        /// <summary>
        /// Zeichnen der Panele, Groupboxen für die versuche
        /// </summary>
        private void initPanelVersuche()
        {
            for (int i = 0; i < panelVersuche.GetLength(0); i++) //Spalten
            {
                for (int j = 0; j < panelVersuche.GetLength(1); j++) //Zeilen
                {
                    panelVersuche[i, j] = new Panel();
                    panelVersuche[i, j].Width = VERSUCHSQUARE;
                    panelVersuche[i, j].Height = VERSUCHSQUARE;

                    panelVersuche[i, j].Left = (j * 52) + 20;
                    panelVersuche[i, j].Top = this.Height - 200 - (i * 47);

                    panelVersuche[i, j].BackColor = Color.LightGray;

                    panelVersuche[i, j].Parent = this;
                    panelVersuche[i, j].BringToFront();

                    panelVersuche[i, j].Click += versuch_Click;
                }
            }

            /////////////////////////////////////Grouboxen/////////////////////////////////
            for (int i = 0; i < panelVersuche.GetLength(0); i++)
            {
                versuchsbox = new GroupBox();
                versuchsbox.Text = "Versuch " + (i + 1);
                versuchsbox.Font = new Font("Calibri", 10, FontStyle.Italic);
                versuchsbox.ForeColor = Color.White;
                versuchsbox.Location = new Point(10, panelVersuche[i, 1].Location.Y - 16);
                versuchsbox.Width = 270;
                versuchsbox.Height = 55;
                this.Controls.Add(versuchsbox);
                versuchsbox.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Erzeugt die Panels und Groupbox der Farbwahl
        /// </summary>
        private void initFarbwahl()
        {
            //////////////////Rahmen//////////////////////////////////////
            farbwahlbox = new GroupBox();
            farbwahlbox.Text = "Farbwahl";
            farbwahlbox.ForeColor = Color.White;
            farbwahlbox.Font = new Font("Calibri", 12, FontStyle.Italic);
            farbwahlbox.Location = new Point(10, this.Height - SQUARE - 95);
            farbwahlbox.Width = 460;
            farbwahlbox.Height = 80;
            this.Controls.Add(farbwahlbox);
            farbwahlbox.BackColor = Color.Transparent;

            //////////////Panels zu Farbauswahl////////////////////////////
            for (int i = 0; i < panelFarbwahl.Length; i++)
            {
                panelFarbwahl[i] = new Panel();
                //////////////groesse//////////
                panelFarbwahl[i].Width = SQUARE;
                panelFarbwahl[i].Height = SQUARE;
                //////////////Positions//////////
                panelFarbwahl[i].Top = this.Height - SQUARE - 75;
                panelFarbwahl[i].Left = (i * SQUARE) + (i * 5) + 20;

                panelFarbwahl[i].BackColor = ColorPick.farbe(i); //Es wird jede Farbe 1x ausgewäht da i als index fungiert
                panelFarbwahl[i].Parent = this;
                panelFarbwahl[i].BringToFront();
                panelFarbwahl[i].Click += panelFarbwahl_Click;

            }
        }


        //___________________________________Zeichnen der Buttons_______________________
        /// <summary>
        /// Button gestalten und zeichnen
        /// </summary>
        private void AddButton()
        {
            auswertenButton.Text = "Auswerten";
            auswertenButton.Font = new Font("Calibri", 20, FontStyle.Regular);
            auswertenButton.FlatStyle = FlatStyle.Flat;
            auswertenButton.FlatAppearance.BorderColor = Color.LightGray;
            auswertenButton.FlatAppearance.BorderSize = 1;
            auswertenButton.MouseEnter += Button_MouseEnter;
            auswertenButton.MouseLeave += Button_MouseLeave;
            auswertenButton.Location = new Point(panelFarbwahl[7].Location.X + SQUARE + 50, panelFarbwahl[7].Location.Y - 10);
            auswertenButton.Width = 150;
            auswertenButton.Height = 75;
            auswertenButton.Click += AuswertenButton_Click;

            this.Controls.Add(auswertenButton);

            soundButton.Text = "Sound einschalten";
            soundButton.TextAlign = ContentAlignment.MiddleCenter;
            soundButton.Font = new Font("Calibri", 12, FontStyle.Regular);
            soundButton.FlatStyle = FlatStyle.Flat;
            soundButton.FlatAppearance.BorderColor = Color.LightGray;
            soundButton.FlatAppearance.BorderSize = 1;
            soundButton.MouseEnter += Button_MouseEnter;
            soundButton.MouseLeave += Button_MouseLeave;
            soundButton.Width = 100;
            soundButton.Height = 50;
            soundButton.Location = new Point(this.Width - soundButton.Width - 20, 15);
            soundButton.Click += buttonSound_Click;

            this.Controls.Add(soundButton);

            cheatButton.Text = "cheat";
            cheatButton.TextAlign = ContentAlignment.MiddleCenter;
            cheatButton.Font = new Font("Calibri", 12, FontStyle.Regular);
            cheatButton.FlatStyle = FlatStyle.Flat;
            cheatButton.FlatAppearance.BorderColor = Color.LightGray;
            cheatButton.FlatAppearance.BorderSize = 1;
            cheatButton.MouseEnter += Button_MouseEnter;
            cheatButton.MouseLeave += Button_MouseLeave;
            cheatButton.Width = 100;
            cheatButton.Height = 50;
            cheatButton.Location = new Point(this.Width - soundButton.Width - 20, 30 + soundButton.Height);
            cheatButton.Click += CheatButton_Click;

            this.Controls.Add(cheatButton);
        }

        private void CheatButton_Click(object sender, EventArgs e)
        {
            if (versuch < 5)
            {
                MessageBox.Show("Fürs Cheaten musst du erst noch selbst etwas probieren !");
                return;
            }
            loesungsbildschirm = new Resultpanel(master.zahl);
            loesungsbildschirm.Show();
        }

        private void fillResult(int schwarz, int weiss)
        {
            for (int i = 0; i < schwarz; i++) //Zuerst die schwarzen Felder ausfüllen
            {
                panelSchwarzWeiss[versuch, i].BackColor = ColorPick.schwarz();
            }
            for (int i = schwarz; i < schwarz + weiss; i++) //i wird auf den Wert von Schwarz gesetzt, da die weißen Felder direkt danach sein sollen
            {                                      //Bei 0 würden die schwarzen überschrieben.
                panelSchwarzWeiss[versuch, i].BackColor = ColorPick.white();
            }
        }

        private void checkWin(int schwarz)
        {

            string result = Spielinfos.gameOver(versuch, schwarz); //Prüft ob das Spiel vorbei ist.

            if (String.IsNullOrEmpty(result))
            {
                return;
            }
            else
            {
                if (loesungsbildschirm == null) //Zeigt die Lösungen an wenn kein Lösungsbildschirm offen ist
                {
                    loesungsbildschirm = new Resultpanel(master.zahl);
                    loesungsbildschirm.Show(); 
                }


                result = result + " Möchten Sie ein neues Spiel starten ?";

                if (MessageBox.Show(result, "", MessageBoxButtons.YesNo) == DialogResult.Yes) //Startet das Spiel neu, oder schließt es
                {
                    loesungsbildschirm.Close();
                    init();
                }
                else
                {
                    this.Close();
                }
            }
        }


        private void Button_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.White;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.LightGreen;
        }
        /// <summary>
        /// Zeichnen der Panele die schwarz / weiss gefärbt werden sollen
        /// </summary>

        private void AuswertenButton_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < auswahl.Length; i++)
            {
                if (auswahl[i] == -1) //Abfangen, das dass komplette Array einen Wert hat (Jedes Feld hat eine Farbe)
                {
                    MessageBox.Show($"Bitte fülle alle Felder vom {versuch + 1}. aus");
                    return;
                }
            }
            int schwarz = master.getSchwarz(auswahl);

            fillResult(schwarz, master.getWeiss(auswahl));

            Array.Copy(new int[] { -1, -1, -1, -1, -1 }, 0, auswahl, 0, auswahl.Length); //Setzt das array wieder auf 0 zurück, da der nächste Zug beginnt.
            versuch++;

            checkWin(schwarz); //Prüft ob das Spiel vorbei ist

        }

        private void versuch_Click(object sender, EventArgs e)
        {
            Panel current = (Panel)sender;

            if (pickedColor == Color.Transparent)
            {
                return;
            }

            for (int i = 0; i < panelVersuche.GetLength(1); i++)
            {
                if(ColorPick.farbe(pickedColor) == auswahl[i]) //Wenn die Farbe schon gewählt wurde
                {
                    MessageBox.Show("Keine doppelten Farben erlaubt !");
                    goto ende;
                }
            }

            for (int i = 0; i < panelVersuche.GetLength(1); i++)
            {           
                if (current == panelVersuche[versuch, i]) //Wenn der Sender in der Reihe des aktuellen versuchs ist.
                {
                    current.BackColor = pickedColor;
                    auswahl[i] = ColorPick.farbe(current.BackColor);
                }
            }
        ende:
            pickedColor = Color.Transparent;
        }


        private void panelFarbwahl_Click(object sender, EventArgs e)
        {
            pickedColor = ((Panel)sender).BackColor; //Angecklickte Farbe wird in pickedColor gespeicert
        }

        private void buttonSound_Click(object sender, EventArgs e)
        {
            if (player.playing == true)
            {
                player.stop();
                soundButton.Text = "Sound einschalten";
            }
            else
            {
                player.play();
                soundButton.Text = "Sound deaktivieren";
            }
        }

    }
}