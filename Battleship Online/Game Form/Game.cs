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

            for(int x = 0; x < Dipendences.campoNostro.GetLength(0); x++) 
            { 
                if(Dipendences.campoNostro[x,0] == Dipendences.defStatus) 
                {
                    riga0me.Text += (Dipendences.campoNostro[x, 0] + "           ");
                }
                else 
                {
                    riga0me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 1] == Dipendences.defStatus)
                {
                    riga1me.Text += (Dipendences.campoNostro[x, 1] + "           ");
                }
                else
                {
                    riga1me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 2] == Dipendences.defStatus)
                {
                    riga2me.Text += (Dipendences.campoNostro[x, 2] + "           ");
                }
                else
                {
                    riga2me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 3] == Dipendences.defStatus)
                {
                    riga3me.Text += (Dipendences.campoNostro[x, 3] + "           ");
                }
                else
                {
                    riga3me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 4] == Dipendences.defStatus)
                {
                    riga4me.Text += (Dipendences.campoNostro[x, 4] + "           ");
                }
                else
                {
                    riga4me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 5] == Dipendences.defStatus)
                {
                    riga5me.Text += (Dipendences.campoNostro[x, 5] + "           ");
                }
                else
                {
                    riga5me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 6] == Dipendences.defStatus)
                {
                    riga6me.Text += (Dipendences.campoNostro[x, 6] + "           ");
                }
                else
                {
                    riga6me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 7] == Dipendences.defStatus)
                {
                    riga7me.Text += (Dipendences.campoNostro[x, 7] + "           ");
                }
                else
                {
                    riga7me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 8] == Dipendences.defStatus)
                {
                    riga8me.Text += (Dipendences.campoNostro[x, 8] + "           ");
                }
                else
                {
                    riga8me.Text += ("            ");
                }
                //
                if (Dipendences.campoNostro[x, 9] == Dipendences.defStatus)
                {
                    riga9me.Text += (Dipendences.campoNostro[x, 9] + "           ");
                }
                else
                {
                    riga9me.Text += ("            ");
                }
                //

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
                    //Conta quante sono le posizioni
                    MySql.Usr.command.CommandText = "SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'";
                    MySqlDataReader readPos = MySql.Usr.command.ExecuteReader();

                    MySql.Usr.table.Load(readPos);

                    c = MySql.Usr.table.Rows.Count;

                    readPos.Close();

                    if (c == Dipendences.HOW_MANY_SHIPS)
                    {
                        label7.Visible = false;
                        MySql.Usr.table.Clear();



                        break;
                    }

                    MySql.Usr.table.Clear();
                }
            }
            else
            {
                label7.Visible = true;

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int x, y;

            if (int.TryParse(textBox2.Text, out x) && (x > -1 && x < 10))
            {
                if (int.TryParse(textBox3.Text, out y) && (y > -1 && y < 10))
                {
                    //Attack a boat


                    //This is the lasts boat?

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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label67_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
