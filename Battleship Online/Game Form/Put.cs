using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship_Online.Game_Form
{
    public partial class Put : Form
    {
        private static int putted = 0;

        public Put()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (putted != Dipendences.HOW_MANY_SHIPS)
            {
                Putting();
            }
            else
            {
                Putting();

                this.Dispose();
            }
        }

        private void Putting()
        {
            if (putted != Dipendences.HOW_MANY_SHIPS)
            {
                if (int.TryParse(textBox2.Text, out Dipendences.x[putted]) && (Dipendences.x[putted] > -1 && Dipendences.x[putted] < 10))
                {
                    if (int.TryParse(textBox3.Text, out Dipendences.y[putted]) && (Dipendences.y[putted] > -1 && Dipendences.y[putted] < 10))
                    {
                        label1.Text = (putted + 1) + " Ships";
                        putted++;
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
            else
            {
                if (int.TryParse(textBox2.Text, out Dipendences.x[putted - 1]) && (Dipendences.x[putted - 1] > -1 && Dipendences.x[putted - 1] < 10))
                {
                    if (int.TryParse(textBox3.Text, out Dipendences.y[putted - 1]) && (Dipendences.y[putted - 1] > -1 && Dipendences.y[putted - 1] < 10))
                    {
                        label1.Text = (putted + 1) + " Ships";
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
        }

        private void Put_Load(object sender, EventArgs e)
        {

        }
    }
}
