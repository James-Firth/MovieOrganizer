using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieOrganizer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (true) //txtboxUserName.Text.Equals("username") && txtboxPassword.Text.Equals("Password");
            {
                HomeForm main = new HomeForm();
                main.Show();
                this.Hide();
            }
            else
            {
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
