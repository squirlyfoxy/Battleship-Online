using System;
using System.Windows.Forms;

namespace Battleship_Online.Game_Form
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            label1.Text = Dipendences.username;
            label2.Text = Dipendences.enemyUsername;

            //Pos navi
            Put p = new Put();
            p.ShowDialog();

            AspettaUtente(0);
            label7.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int x, y;

            if (int.TryParse(textBox2.Text, out x) && (x > -1 && x < 10))
            {
                if (int.TryParse(textBox3.Text, out y) && (y > -1 && y < 10))
                {
                    //Attack a boat



                    MossePosizioni.Visible = false;
                    AspettaUtente(1);

                    MossePosizioni.Visible = true;
                }
                else
                {
                     MessageBox.Show("Please insert a valid y");
                }
            }
            else
            {
                MessageBox.Show("Please insert a valid x");
            }
        }

        private void AspettaUtente(int cosa) //cosa = 0 (aspetta fine navi), cosa = 1 (Aspetta attacco)
        {
            throw new NotImplementedException();
        }
    }
}
