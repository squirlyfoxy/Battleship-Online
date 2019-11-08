using System;
using System.Net;
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

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Dispose(); //Close form
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) //Increment progressbar value
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBar1.Increment(1);
            label1.Text = progressBar1.Value.ToString();
        }

        private void Button2_Click(object sender, EventArgs e) //Start Update
        {
            string[] log = { "Downloading files", "Finish!", "Error: " }; //Log Array

            textBox1.AppendText(log[0] + Environment.NewLine);

            try
            {
                System.IO.File.Delete(@"C:\Battleship Online\Battleship Online.exe");
                System.IO.File.Delete(@"C:\Battleship Online\Battleship Online - Chat.exe");
                System.IO.File.Delete(@"C:\Battleship Online\Battleship Online - Me.exe");
                System.IO.File.Delete(@"C:\Battleship Online\Battleship Online - Other.exe");

                //Start Downloading
                var download = new WebClient();
                download.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

                download.DownloadFile(Dipendences.launcherDownload, @"C:\Battleship Online\Battleship Online.exe");

                download.DownloadFile(Dipendences.chatDownload, @"C:\Battleship Online\Battleship Online - Chat.exe");

                download.DownloadFile(Dipendences.meDownload, @"C:\Battleship Online\Battleship Online - Me.exe");

                download.DownloadFile(Dipendences.otherDownload, @"C:\Battleship Online\Battleship Online - Other.exe");

            }
            catch (Exception ex)
            {
                textBox1.AppendText(log[2] + ex);
            }

            textBox1.AppendText(log[1]);
        }
    }
}
