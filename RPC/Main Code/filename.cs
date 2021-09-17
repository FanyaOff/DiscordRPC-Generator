using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Page_Design_UI
{
    public partial class filename : Form
    {
        public filename()
        {
            InitializeComponent();
            WebClient client = new WebClient();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void filename_Load(object sender, EventArgs e)
        {
           
        }

        private void test(object sender, EventArgs e)
        {
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.Stream = Properties.Resources.blya;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + @"DiscordRPC Presets" + @"\";
            string file1 = path + @"\" + $"{file_name.Text}" + @".txt";
                if (!File.Exists(file1))
                {
                    var myFile = File.CreateText(file1);
                    myFile.Close();
                }
                using (StreamWriter incdate = File.AppendText(file1))
                {
                    // запись в файл
                    incdate.WriteLine(Program.f1.guna2TextBox1.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox2.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox3.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox4.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox5.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox6.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox7.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox8.Text, '\n');
            }
                // проигрывание звука
                sp.Play();
                MessageBox.Show("Done");
                Program.f1.guna2ComboBox1.Items.Clear();
                var dir = new DirectoryInfo(path);
                var files = new List<string>();
                foreach (FileInfo file in dir.GetFiles("*.txt"))
                {
                   files.Add(Path.GetFileName(file.FullName));
                }
                foreach (string str in files)
                {
                   Program.f1.guna2ComboBox1.Items.Add(str);
                }
                this.Close();
            }
        }
    }

