using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Page_Design_UI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (guna2RadioButton1.Checked)
            {
                label2.Text = "Пункт 1:";
                label1.Text = "Зайдите на сайт\nDiscord Developers";
            }
            else
            if (guna2RadioButton2.Checked)
            {
                label2.Text = "Пункт 1:";
                label1.Text = "Авторизируетесь\nи жмете на кнопке\nNew Application";
                guna2Button1.Visible = false;
            }
            if (guna2RadioButton3.Checked)
            {

            }
            else
            if (guna2RadioButton4.Checked)
            {

            }
            if (guna2RadioButton5.Checked)
            {

            }
            else
            if (guna2RadioButton6.Checked)
            {

            }
            if (guna2RadioButton7.Checked)
            {

            }
            else
            if (guna2RadioButton8.Checked)
            {

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.com/developers/applications");
        }

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
