using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DiscordRPC;
using TSL.ConfigLib;
using Timer = System.Windows.Forms.Timer;

namespace Login_Page_Design_UI
{

    public partial class DiscordRPC : Form
    {
        // Local Strings
        System.Windows.Forms.Timer timer = new Timer();
        public static Config Config { get; private set; }
        public DiscordRpcClient client;
        public string filePath = Path.GetTempPath() + @"data.txt";
        private Config config;
        private FormSettings formSettings;
        private ContextMenu m_menu;
        public DiscordRPC()
        {
            timer.Tick += new EventHandler(timer1_Tick);
            SoundPlayer sp = new SoundPlayer();
            sp.Stream = Properties.Resources.blya;
            InitializeComponent();
            if (!File.Exists(filePath))
            {
                var myFile = File.CreateText(filePath);
                myFile.Close();
            }
            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0,
                new MenuItem("Show", new System.EventHandler(Show)));
            m_menu.MenuItems.Add(1,
                new MenuItem("Turn Off RPC", new System.EventHandler(Off)));
            m_menu.MenuItems.Add(2,
                new MenuItem("Close", new System.EventHandler(Close)));
        }



        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }

        #region Settings

        private void LoadSettings()
        {
            config = Program.Config;

            // настройки положения формы
            formSettings = new FormSettings();
            formSettings.LoadFromConfig(config, this.Name);
            formSettings.AttachToForm(this);

            // прочие настройки
            guna2TextBox1.Text = config.Settings["TextBox1_Value"];
            guna2TextBox2.Text = config.Settings["TextBox2_Value"];
            guna2TextBox3.Text = config.Settings["TextBox3_Value"];
            guna2TextBox4.Text = config.Settings["TextBox4_Value"];
            guna2TextBox5.Text = config.Settings["TextBox5_Value"];
            guna2TextBox6.Text = config.Settings["TextBox6_Value"];
        }


        private void SaveSettings()
        {
            // прочие настройки
            config.Settings["TextBox1_Value"] = guna2TextBox1.Text;
            config.Settings["TextBox2_Value"] = guna2TextBox2.Text;
            config.Settings["TextBox3_Value"] = guna2TextBox3.Text;
            config.Settings["TextBox4_Value"] = guna2TextBox4.Text;
            config.Settings["TextBox5_Value"] = guna2TextBox5.Text;
            config.Settings["TextBox6_Value"] = guna2TextBox6.Text;
        }

        #endregion

        private void Timer(object sender, ElapsedEventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadSettings();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            SaveSettings();
        }

        private void DiscordRPC_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        protected void Show(Object sender, System.EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        protected void Off(Object sender, System.EventArgs e)
        {
            base.OnClosed(e);
            SaveSettings();
            Application.Restart();
            MessageBox.Show("RPC Успешно отключён!\nДля повторного включение запустите его заного");
        }

        protected void Close(Object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "PNG Image|*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "" || guna2TextBox3.Text == "")
            {
                MessageBox.Show("Кажется, вы не заполнили одно из полей\nПервый Или Второй Текст");
            }
            else
            {
                guna2HtmlLabel20.Text = guna2TextBox2.Text;
                guna2HtmlLabel21.Text = guna2TextBox3.Text;
            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            base.OnClosed(e);
            SaveSettings();
            Application.Restart();
            MessageBox.Show("RPC Успешно отключён!\nДля повторного включение запустите его заного");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (guna2Button1.Text == "Start RPC")
            {
                guna2Button1.Text = "Stop RPC";
                guna2Button1.FillColor = Color.FromArgb(237, 66, 69);
                client = new DiscordRpcClient($"{guna2TextBox1.Text}");
                client.Initialize();
                client.SetPresence(new global::DiscordRPC.RichPresence()
                {
                    Details = $"{guna2TextBox2.Text}",
                    State = $"{guna2TextBox3.Text}",
                    Timestamps = Timestamps.Now,
                    Assets = new Assets()
                    {
                        LargeImageKey = $"{guna2TextBox4.Text}",
                        LargeImageText = $"{guna2TextBox5.Text}",
                        SmallImageKey = $"{guna2TextBox6.Text}",
                    }
                });
                MessageBox.Show("RPC Запущен и свернут в трей");
                this.Visible = false; /* скроем форму */
                notifyIcon1.Visible = true; /* покажем икону в трее */
                notifyIcon1.ContextMenu = m_menu;
            }
            else
            {
                guna2Button1.Text = "Start RPC";
                base.OnClosed(e);
                SaveSettings();
                Application.Restart();
            }
            
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "TXT File|*.txt";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string r = File.ReadLines(openFileDialog1.FileName).Skip(0).First();
                string d = File.ReadLines(openFileDialog1.FileName).Skip(1).First();
                string t = File.ReadLines(openFileDialog1.FileName).Skip(2).First();
                string c = File.ReadLines(openFileDialog1.FileName).Skip(3).First();
                string p = File.ReadLines(openFileDialog1.FileName).Skip(4).First();
                string s = File.ReadLines(openFileDialog1.FileName).Skip(5).First();
                guna2TextBox1.Text = r;
                guna2TextBox2.Text = d;
                guna2TextBox3.Text = t;
                guna2TextBox4.Text = c;
                guna2TextBox5.Text = p;
                guna2TextBox6.Text = s;
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.Stream = Properties.Resources.blya;
            MessageBox.Show("Создайте TXT файл в любой папке\nи нажмите OK для записи текста туда");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "TXT File|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter incdate = File.AppendText(openFileDialog1.FileName))
                {
                    incdate.WriteLine(guna2TextBox1.Text, '\n');
                    incdate.WriteLine(guna2TextBox2.Text, '\n');
                    incdate.WriteLine(guna2TextBox3.Text, '\n');
                    incdate.WriteLine(guna2TextBox4.Text, '\n');
                    incdate.WriteLine(guna2TextBox5.Text, '\n');
                    incdate.WriteLine(guna2TextBox6.Text, '\n');
                }
                sp.Play();
                MessageBox.Show("Done");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;
            string time = "";

            if (h < 10)
            {
                time += "0" + h;
            }
            else
            {
                time += h;
            }

            time += ":";

            if (m < 10)
            {
                time += "0" + m;
            }
            else
            {
                time += m;
            }

            time += ":";

            if (s < 10)
            {
                time += "0" + s;
            }
            else
            {
                time += s;
            }

            guna2HtmlLabel25.Text = time;
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            this.Visible = false; /* скроем форму */
            notifyIcon1.Visible = true; /* покажем икону в трее */
            notifyIcon1.ContextMenu = m_menu;
        }

        private void guna2HtmlLabel26_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Process.Start("http://discordrpctutorial.getenjoyment.net/");
        }
    }
}
