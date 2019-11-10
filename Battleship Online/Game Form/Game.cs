using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
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
            int pos = 0;

            label1.Text = Dipendences.username;
            label2.Text = Dipendences.enemyUsername;

            //Pos navi
            Put p = new Put();
            p.ShowDialog();

            foreach(int n in Dipendences.x) //Insert my ships positions
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
                Dipendences.campoNostro[Dipendences.x[pos], Dipendences.y[pos]] = Dipendences.defStatus;
            }

            for(int i = 0; i < Dipendences.HOW_MANY_SHIPS; i++)
            {
                for (int x = 0; x < Dipendences.HOW_MANY_SHIPS; i++)
                {
                    textBox3.AppendText(Dipendences.campoNostro[x, i].ToString() + "   ");
                }
            }

            Aspetta(0);

            //Metti le posizioni del nemico dentro un'altra matrice
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            pos = 0;

            for(int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro una matrice
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.campoNemico[Convert.ToInt32(MySql.Usr.table.Rows[i][1]), Convert.ToInt32(MySql.Usr.table.Rows[i][2])] = Dipendences.defStatus;
                }
            }

            MySql.Usr.table.Clear();

            label7.Visible = false;
        }

        private void Aspetta(int v) //v == 0 (posizioni), v == 1(attacchi)
        {
            int c = 0;

            if (v == 0)
            {
                label7.Visible = true;

                while (true)
                {
                    c = Count("SELECT Count(*) FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'");

                    if (c == Dipendences.HOW_MANY_SHIPS)
                    {
                        label7.Visible = false;

                        break;
                    }
                }
            }
            else
            {
                label7.Visible = true;

            }
        }

        private int Count(string v) //conta righe
        {
            int recCount = 0;

            MySql.Usr.adapter = new MySqlDataAdapter(v, MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            if (MySql.Usr.table.Rows.Count >= 0)
            {
                recCount = MySql.Usr.table.Rows.Count;
            }

            return recCount;
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

                    Aspetta(1);

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

        private void Me_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
