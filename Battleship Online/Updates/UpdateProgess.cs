using System;
using System.Windows.Forms;

namespace Battleship_Online.Updates
{
    public partial class UpdateProgess : Form
    {
        public UpdateProgess()
        {
            InitializeComponent();
        }

        private void UpdateProgess_Load(object sender, EventArgs e)
        {
            string[] log = { "Downloading .zip file", "Unzipping .zip file", "Finish!" ,"Error: " }; //Log Array

            textBox1.AppendText(log[0] + Environment.NewLine);

            try
            {
                //Start Downloading

                //Start Unzipping
                textBox1.AppendText(log[1] + Environment.NewLine);

            } catch (Exception ex)
            {
                textBox1.AppendText(log[3] + ex);
            }

            textBox1.AppendText(log[2]);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Dispose(); //Close form
        }
    }
}
