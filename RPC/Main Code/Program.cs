using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSL.ConfigLib;

namespace Login_Page_Design_UI
{
    static class Program
    {
        public static Config Config { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config = new Config();
            Config.Load(Path.GetTempPath() + "config.xml");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DiscordRPC());
            Config.Save();
        }


    }
}
