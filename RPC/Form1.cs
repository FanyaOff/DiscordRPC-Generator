using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using TSL.ConfigLib;

namespace Login_Page_Design_UI
{

    public partial class DiscordRPC : Form
    {
        // Local Strings
        public static Config Config { get; private set; }
        public DiscordRpcClient client;
        public string filePath = Path.GetTempPath() + @"data.txt";
        private Config config;
        private FormSettings formSettings;
        private ContextMenu m_menu;

        public DiscordRPC()
        {
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
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





        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "" || guna2TextBox3.Text == "" || guna2TextBox4.Text == "" || guna2TextBox5.Text == "" || guna2TextBox5.Text == "")
            {
                MessageBox.Show("Проверьте, заполнили ли вы textbox");
            }
            else
            {
                List<string> data = new List<string>();
                data = File.ReadAllLines(filePath).ToList();
                data.Add("Client ID: " + guna2TextBox1.Text + "\n" + "Details: " + guna2TextBox2.Text + "," + "State: " + guna2TextBox3.Text + "," + "LargeImageKey: " + guna2TextBox4.Text + "," + "LargeImageText: " + guna2TextBox5.Text + "," + "SmallImageKey: " + guna2TextBox6.Text);
                File.WriteAllLines(filePath, data);
                MessageBox.Show("Значения сохранены!");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Process.Start(filePath);
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Process.Start("http://discordrpctutorial.getenjoyment.net/");
        }


        private void savedata_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
        }
    }
}
