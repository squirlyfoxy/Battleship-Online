using System;
using System.Net;
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

        private void Changelog_Load(object sender, EventArgs e)
        {
            //Download changelog file
            var downloadClient = new WebClient();
            downloadClient.DownloadFile(Dipendences.changelogFile, @"C:\Battleship Online\ch.txt");

            string[] ch = System.IO.File.ReadAllLines(@"C:\Battleship Online\ch.txt");                      //Write "ch.txt" contents in textBox1

            foreach (string r in ch)
            {
                textBox1.AppendText(r);
            }
        }
    }
}
