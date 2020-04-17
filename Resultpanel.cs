using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _20200325_Mastermind_LucaKeller
{
    public partial class Resultpanel : Form
    {
        private int size = 30;

        private int[] res;
        private Panel[] resultPanel = new Panel[5];

        public Resultpanel(int[] loesung)
        {
            InitializeComponent();
            this.res = loesung;
        }

        private void Resultpane_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < res.Length; i++)
            {
                resultPanel[i] = new Panel();
                resultPanel[i].Width = size;
                resultPanel[i].Height = size;
                resultPanel[i].Top = 100;
                resultPanel[i].Left = (i * size) + (i * 10) + this.Width/4;

                resultPanel[i].BackColor = ColorPick.farbe(res[i]);
                resultPanel[i].Parent = this;
                resultPanel[i].BringToFront();
            }
        }
    }
}
