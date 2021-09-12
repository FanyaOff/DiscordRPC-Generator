using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.SelectedPath + @"\" + $"{file_name.Text}" + @".txt";
                if (!File.Exists(file))
                {
                    var myFile = File.CreateText(file);
                    myFile.Close();
                }
                using (StreamWriter incdate = File.AppendText(file))
                {
                    // запись в файл
                    incdate.WriteLine(Program.f1.guna2TextBox1.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox2.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox3.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox4.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox5.Text, '\n');
                    incdate.WriteLine(Program.f1.guna2TextBox6.Text, '\n');
                }
                // проигрывание звука
                sp.Play();
                MessageBox.Show("Done");
                credits cd = new credits();
                cd.Close();
            }
        }
    }
}
