using System;
using System.Windows.Forms;

namespace Battleship_Online.Updates
{
    public partial class Changelog : Form
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: Show changelog of the new version
         *  
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */
        public Changelog()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Dispose(); //Close Form

            //Start updating
            UpdateProgess up = new UpdateProgess();
            up.ShowDialog();
        }
    }
}
