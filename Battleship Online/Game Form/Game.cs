using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
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

            /*
            //Qual'è il primo turno?
            Random rand = new Random();
            int whatsTurnSl = rand.Next(3000, 5000) *2;

            Thread.Sleep(whatsTurnSl * 2);

            //Controlla se non è stato ancora deciso il turno (controlla se il nome avversario esiste già)
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Turno` WHERE `username` = '" + Dipendences.enemyUsername + "'", MySql.Usr.conn);

            Thread.Sleep(whatsTurnSl + 1000);

            MySql.Usr.adapter.Fill(MySql.Usr.table);

            Console.WriteLine("Ciao");

            if (MySql.Usr.table.Rows.Count > 0)
            {
                Aspetta(1); //Aspetta fine del turno

                Instruments.GMMessage("Il primo turno è di: " + Dipendences.enemyUsername);
            }
            else
            {
                //Inserisci il mio nome utente dentro la tabella
                MySqlCommand add = new MySqlCommand("INSERT INTO `Turno`(`username``) VALUES (@usr)", MySql.Usr.conn);
                add.Parameters.Add("@usr", MySqlDbType.VarChar).Value = Dipendences.username;

                add.ExecuteNonQuery();

                Instruments.GMMessage("Il primo turno è di: " + Dipendences.username);
            } //Its my turn!!!!

            MySql.Usr.table.Clear();

            */

            label7.Visible = false;
            MossePosizioni.Visible = true;
        }

        private void Aggiorna()
        {
            //Aggiorna il mio campo
            Instruments.GMMessage("Aggiorno le matrici");

            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.username + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            for (int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro due array e una matrice
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.campoNostro[Convert.ToInt32(MySql.Usr.table.Rows[i][1]), Convert.ToInt32(MySql.Usr.table.Rows[i][2])] = Convert.ToChar(MySql.Usr.table.Rows[i][3]);
                }
            }

            MySql.Usr.table.Clear();


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

            Dipendences.meRemaningShips = Dipendences.HOW_MANY_SHIPS;

            for (int x = 0; x < Dipendences.campoNostro.GetLength(0); x++)
            {
                for (int y = 0; y < Dipendences.campoNostro.GetLength(1); y++)
                {
                    if(Dipendences.campoNostro[x, y] == Dipendences.colpitoStatus)
                    {
                        Dipendences.meRemaningShips--;
                    }
                }
            }

            //Aggiorna il database del campo avversario
            MySql.Usr.adapter = new MySqlDataAdapter("SELECT * FROM `Posizioni` WHERE `username` = '" + Dipendences.enemyUsername + "'", MySql.Usr.conn);
            MySql.Usr.adapter.Fill(MySql.Usr.table);

            for (int i = 0; i < MySql.Usr.table.Rows.Count; i++) //Scrivi dentro due array e una matrice
            {
                if (MySql.Usr.table.Rows[i][1] != DBNull.Value && MySql.Usr.table.Rows[i][2] != DBNull.Value)
                {
                    Dipendences.campoNemico[Convert.ToInt32(MySql.Usr.table.Rows[i][1]), Convert.ToInt32(MySql.Usr.table.Rows[i][2])] = Convert.ToChar(MySql.Usr.table.Rows[i][3]);
                }
            }

            MySql.Usr.table.Clear();

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
                        label37.Text += (Dipendences.campoNemico[x, 9] + "           ");
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
            label4.Text = "Sunken Ships: " + Dipendences.sunkenShips;
        }

        private void Aspetta(int v) //v == 0 (posizioni), v == 1(attacchi)
        {
            int c = 0;

            if (v == 0)
            {
                label7.Visible = true;
                MossePosizioni.Visible = false;

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

                MossePosizioni.Visible = false;


                while (true)
                {
                    //Aspetta che l'avversario abbia finito

                    Thread.Sleep(2500);

                    MySql.Usr.command.CommandText = "SELECT * FROM `Mosse` WHERE `username` = '" + Dipendences.enemyUsername + "'";
                    MySqlDataReader readPos = MySql.Usr.command.ExecuteReader();

                    MySql.Usr.table.Load(readPos);

                    if(MySql.Usr.table.Rows.Count == Dipendences.mosseAvversarie + 1)
                    {
                        Dipendences.mosseAvversarie++;

                        break;
                    }

                }

                label7.Visible = false;

                MossePosizioni.Visible = true;
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
                    MySqlCommand add = new MySqlCommand("INSERT INTO `Mosse`(`username`, `x`, `y`) VALUES (@usr,@x,@y)", MySql.Usr.conn);
                    add.Parameters.Add("@usr", MySqlDbType.VarChar).Value = Dipendences.username;
                    add.Parameters.Add("@x", MySqlDbType.VarChar).Value = x;
                    add.Parameters.Add("@y", MySqlDbType.VarChar).Value = y;

                    add.ExecuteNonQuery();

                    if (Array.IndexOf(Dipendences.enemyX, x) < 1 && Array.IndexOf(Dipendences.enemyY, y) < 1)
                    {
                        //Non colpita :(
                        MySql.Usr.command.CommandText = "UPDATE `Posizioni` SET `Status`='" + Dipendences.mancatoStatus + "' WHERE username='" + Dipendences.enemyUsername + "', x='" + x + "' AND y='" + y + "';";
                        MySqlDataReader upNonColpito = MySql.Usr.command.ExecuteReader();

                        Instruments.GMMessage("Nave non Colpita");

                        Dipendences.campoNemico[x, y] = Dipendences.mancatoStatus; //Update matrice

                    } else if (Array.IndexOf(Dipendences.enemyX, x) >= 0 && Array.IndexOf(Dipendences.enemyY, y) >= 0)
                    {
                        //Colpita :)
                        MySql.Usr.command.CommandText = "UPDATE `Posizioni` SET `Status`='" + Dipendences.colpitoStatus + "' WHERE username='" + Dipendences.enemyUsername + "', x='" + x + "' AND y='" + y + "';";
                        MySqlDataReader upColpito = MySql.Usr.command.ExecuteReader();

                        Dipendences.campoNemico[x, y] = Dipendences.colpitoStatus; //Update matrice
                        Instruments.GMMessage("Nave Colpita");

                        Dipendences.remaningShips--;
                        Dipendences.sunkenShips++;
                    }

                    //This is the lasts boat?
                    if(Dipendences.sunkenShips == Dipendences.HOW_MANY_SHIPS)
                    {
                        Instruments.GMMessage("Finito, hai vinto");

                        MessageBox.Show("Hai vinto!!!");

                        this.Dispose();
                        Program.Continue();
                    }

                    MossePosizioni.Visible = false;

                    Aggiorna();

                    Aspetta(1);

                    Aggiorna();

                    if(Dipendences.meRemaningShips == 0)
                    {
                        Instruments.GMMessage("Finito, hei perso");

                        MessageBox.Show("Hai perso :(");

                        this.Dispose();
                        Program.Continue();
                    }

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
