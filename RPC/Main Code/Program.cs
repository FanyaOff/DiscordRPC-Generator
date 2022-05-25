using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSL.ConfigLib;

namespace Login_Page_Design_UI
{
    static class Program
    {
        public static DiscordRPC f1;

        public static Config Config { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //SoundPlayer sp = new SoundPlayer();
            //sp.Stream = Properties.Resources.blya;
            //WebClient client = new WebClient();
            //string ver = client.DownloadString(""); // link to version file
            //string log = client.DownloadString(""); // link to changelog file
            //string desktop = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads");
            //string dwnload = ""; // link to download file

            // uncomment this, if you want turn on auto-update
            //if (client.DownloadString("http://discordrpctutorial.getenjoyment.net/version.txt").Contains("0.6"))
            //{
                Config = new Config();
                Config.Load(Path.GetTempPath() + "config.xml");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(f1 = new DiscordRPC());
                Config.Save();
            //}
            //else
            //{
            //    sp.Play();
            //    MessageBox.Show("Найдено обновление! Подождите примерно 10-15 секунд\nВерсия на сервере: " + ver + "\nВерсия вашего клиента: " + Application.ProductVersion + "\nИзменения: " + "\n" + log);
            //    client.DownloadFile(dwnload, desktop + "/" + "v_" + ver + "_Discord RPC.exe");
            //    sp.Play();
            //    Process.Start("explorer.exe", desktop);
            //    Application.Exit();
            //}

        }


    }
}
