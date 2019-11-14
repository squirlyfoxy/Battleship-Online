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

        private static int firstTime = 0;

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

            Aspetta(0);

            Aggiorna();

            //Metti le posizioni del nemico dentro un'altra matrice
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            pos = 0;

            for(int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro due array e una matrice
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.enemyX[i] = Convert.ToInt32(MySql.Usr.table.Rows[i][1]);
                    Dipendences.enemyY[i] = Convert.ToInt32(MySql.Usr.table.Rows[i][2]);
                }
            }

            //Qual'è il primo turno?


            MySql.Usr.table.Clear();

            label7.Visible = false;
        }

        private void Aggiorna()
        {
            //Aggiorna il mio campo

            riga0me.Text = ""; //Resetta la labels del mio campo
            riga1me.Text = "";
            riga2me.Text = "";
            riga3me.Text = "";
            riga4me.Text = "";
            riga5me.Text = "";
            riga6me.Text = "";
            riga7me.Text = "";
            riga8me.Text = "";
            riga9me.Text = "";

            for (int x = 0; x < Dipendences.campoNostro.GetLength(0); x++)
            {
                if (Dipendences.campoNostro[x, 0] == Dipendences.defStatus)
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
            }

            //Aggiorna le labels del campo avversario
            label28.Text = "";
            label29.Text = "";
            label30.Text = "";
            label31.Text = "";
            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
            label35.Text = "";
            label36.Text = "";
            label37.Text = "";

            if(!(firstTime == 0))
            {
                //Aggiorna le label
                for (int x = 0; x < Dipendences.campoNemico.GetLength(0); x++)
                {
                    if (Dipendences.campoNemico[x, 0] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 0] == Dipendences.mancatoStatus)
                    {
                        label28.Text += (Dipendences.campoNemico[x, 0] + "           ");
                    }
                    else
                    {
                        label28.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 1] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 1] == Dipendences.mancatoStatus)
                    {
                        label29.Text += (Dipendences.campoNemico[x, 1] + "           ");
                    }
                    else
                    {
                        label29.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 2] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 2] == Dipendences.mancatoStatus)
                    {
                        label30.Text += (Dipendences.campoNemico[x, 2] + "           ");
                    }
                    else
                    {
                        label30.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 3] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 3] == Dipendences.mancatoStatus)
                    {
                        label31.Text += (Dipendences.campoNemico[x, 3] + "           ");
                    }
                    else
                    {
                        label31.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 4] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 4] == Dipendences.mancatoStatus)
                    {
                        label32.Text += (Dipendences.campoNemico[x, 4] + "           ");
                    }
                    else
                    {
                        label32.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 5] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 5] == Dipendences.mancatoStatus)
                    {
                        label33.Text += (Dipendences.campoNemico[x, 5] + "           ");
                    }
                    else
                    {
                        label33.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 6] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 6] == Dipendences.mancatoStatus)
                    {
                        label34.Text += (Dipendences.campoNemico[x, 6] + "           ");
                    }
                    else
                    {
                        label34.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 7] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 7] == Dipendences.mancatoStatus)
                    {
                        label35.Text += (Dipendences.campoNemico[x, 7] + "           ");
                    }
                    else
                    {
                        label35.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 8] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 8] == Dipendences.mancatoStatus)
                    {
                        label36.Text += (Dipendences.campoNemico[x, 8] + "           ");
                    }
                    else
                    {
                        label36.Text += ("            ");
                    }
                    //
                    if (Dipendences.campoNemico[x, 9] == Dipendences.colpitoStatus || Dipendences.campoNemico[x, 9] == Dipendences.mancatoStatus)
                    {
                        label37.Text += (Dipendences.campoNostro[x, 9] + "           ");
                    }
                    else
                    {
                        label37.Text += ("            ");
                    }
                }

                firstTime++;
            }

            //Update remaning and sunken ships
            label3.Text = "Remaining Ships: " + Dipendences.remaningShips;
            label3.Text = "Sunken Ships: " + Dipendences.sunkenShips;
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

                    Aggiorna();

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
