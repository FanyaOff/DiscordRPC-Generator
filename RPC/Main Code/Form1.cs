using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DiscordRPC;
using Login_Page_Design_UI.Properties;
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
        private ContextMenu m_menu;
        public DiscordRPC()
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
            SoundPlayer sp = new SoundPlayer();
            sp.Stream = Properties.Resources.blya;
            string cf = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + @"DiscordRPC Presets";
            if (Directory.Exists(cf))
            {

            }
            else
            {
                Directory.CreateDirectory(cf);
            }
            InitializeComponent();
            this.Load += new System.EventHandler(this.UpdateList1);
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



        #region Settings

        private void LoadSettings()
        {
            config = Program.Config;
            guna2TextBox1.Text = config.Settings["TextBox1_Value"];
            guna2TextBox2.Text = config.Settings["TextBox2_Value"];
            guna2TextBox3.Text = config.Settings["TextBox3_Value"];
            guna2TextBox4.Text = config.Settings["TextBox4_Value"];
            guna2TextBox5.Text = config.Settings["TextBox5_Value"];
            guna2TextBox6.Text = config.Settings["TextBox6_Value"];
        }


        private void SaveSettings()
        {
            config.Settings["TextBox1_Value"] = guna2TextBox1.Text;
            config.Settings["TextBox2_Value"] = guna2TextBox2.Text;
            config.Settings["TextBox3_Value"] = guna2TextBox3.Text;
            config.Settings["TextBox4_Value"] = guna2TextBox4.Text;
            config.Settings["TextBox5_Value"] = guna2TextBox5.Text;
            config.Settings["TextBox6_Value"] = guna2TextBox6.Text;
        }

        #endregion


        protected override void OnLoad(EventArgs e) // при загрузке формы
        {
            // загрузка настроек
            base.OnLoad(e);
            LoadSettings();
        }

        protected override void OnClosed(EventArgs e) // при закрытии формы
        {
            // сохранение настроек
            base.OnClosed(e);
            SaveSettings();
        }

        private void DiscordRPC_Resize(object sender, EventArgs e) // разворачивание приложения
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        // кнопка Show в трее
        protected void Show(Object sender, System.EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        // кнопка Off в трее
        protected void Off(Object sender, System.EventArgs e)
        {
            base.OnClosed(e);
            SaveSettings();
            Application.Restart();
            MessageBox.Show("RPC Успешно отключён!\nДля повторного включение запустите его заного");
        }

        // кнопка Close в трее
        protected void Close(Object sender, System.EventArgs e)
        {
            Application.Exit();
        }

       

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e) // тоже дабл клик
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            this.Show();
        }


        // закрытие с сохранением настроек 1
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            base.OnClosed(e);
            SaveSettings();
            Application.Restart();
            MessageBox.Show("RPC Успешно отключён!\nДля повторного включение запустите его заного");
        }


        private void guna2Button1_Click_1(object sender, EventArgs e) // закрытие с сохранением настроек 2
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
                this.Visible = false; // Скрывается форма
                notifyIcon1.Visible = true; // Сворачиваем приложение в трек
                notifyIcon1.ContextMenu = m_menu; // присваниваем констекстное меню
            }
            else
            {
                guna2Button1.Text = "Start RPC";
                base.OnClosed(e);
                SaveSettings();
                Application.Restart();
            }

        }



 

      

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // очистка текста
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
        }


        private void UpdateList1(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + @"DiscordRPC Presets";
            var dir = new DirectoryInfo(path);
            var files = new List<string>();
            foreach (FileInfo file in dir.GetFiles("*.txt"))
            {
                files.Add(Path.GetFileName(file.FullName));
            }
            foreach (string str in files)
            {
                guna2ComboBox1.Items.Add(str);
            }
        }



        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // скрытие в трей
            this.Visible = false;
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenu = m_menu;
        }

        private void guna2HtmlLabel26_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            // переход на сайт с тутором
            Process.Start("http://discordrpctutorial.getenjoyment.net/");
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            // открыть форму с кредитами
            credits cd = new credits();
            cd.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2HtmlLabel20.Text = guna2TextBox2.Text;
            guna2HtmlLabel21.Text = guna2TextBox3.Text;
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

 

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Items.Clear();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + @"DiscordRPC Presets";
            var dir = new DirectoryInfo(path);
            var files = new List<string>();
            foreach (FileInfo file in dir.GetFiles("*.txt"))
            {
                files.Add(Path.GetFileName(file.FullName));
            }
            foreach (string str in files)
            {
                guna2ComboBox1.Items.Add(str);
            }
        }
   

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "PNG File|*.png";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fd.FileName);
            }
        }

        private void guna2Button4_Click_2(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + @"DiscordRPC Presets" + @"\";
            string on = File.ReadLines(path + guna2ComboBox1.SelectedItem.ToString()).Skip(0).First();
            string tu = File.ReadLines(path + guna2ComboBox1.SelectedItem.ToString()).Skip(1).First();
            string th = File.ReadLines(path + guna2ComboBox1.SelectedItem.ToString()).Skip(2).First();
            string fo = File.ReadLines(path + guna2ComboBox1.SelectedItem.ToString()).Skip(3).First();
            string fv = File.ReadLines(path + guna2ComboBox1.SelectedItem.ToString()).Skip(4).First();
            string sx = File.ReadLines(path + guna2ComboBox1.SelectedItem.ToString()).Skip(5).First();
            guna2TextBox1.Text = on;
            guna2TextBox2.Text = tu;
            guna2TextBox3.Text = th;
            guna2TextBox4.Text = fo;
            guna2TextBox5.Text = fv;
            guna2TextBox6.Text = sx;
        }

        private void guna2Button2_Click_2(object sender, EventArgs e)
        {
            filename nf = new filename();
            nf.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + @"DiscordRPC Presets" + @"\";
            File.Delete(path + guna2ComboBox1.SelectedItem.ToString());
            guna2ComboBox1.Items.Clear();
            var dir = new DirectoryInfo(path);
            var files = new List<string>();
            foreach (FileInfo file in dir.GetFiles("*.txt"))
            {
                files.Add(Path.GetFileName(file.Name));
            }
            foreach (string str in files)
            {
                guna2ComboBox1.Items.Add(str);
            }

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}

        
