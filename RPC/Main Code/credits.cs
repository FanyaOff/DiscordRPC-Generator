using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Page_Design_UI
{
    public partial class credits : Form
    {
        public credits()
        {
            InitializeComponent();
            WebClient client = new WebClient();
            //string ver = client.DownloadString("http://discordrpctutorial.getenjoyment.net/version.txt");*/ // change to you version file
            //guna2HtmlLabel27.Text = ver;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/FanyaOff/DiscordRPC-Generator");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/fan9_a");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCDSKBT5wHdM4zhmElIsrHeQ");
        }
    }
}
