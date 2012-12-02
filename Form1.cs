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
                HomeForm main = new HomeForm(this);
                main.Show();
                this.Hide();
                txtboxPassword.Text = "";
                txtboxUserName.Text = "";
            }
            else
            {

            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void txtboxUserName_Validated(object sender, EventArgs e)
        {

        }

        private void txtboxUserName_Validating(object sender, CancelEventArgs e)
        {
            if (!txtboxUserName.Text.Equals(""))//everythin's cool
            {
                errUserName.Clear();
                lblErrorMsg.Text = "";
            }
            else
            {
                errUserName.SetError(txtboxUserName, "No Blanks allowed!");
                lblErrorMsg.Text = "No blanks allowed!";
            }

        }

        private void txtboxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!txtboxPassword.Text.Equals(""))//everythin's cool
            {
                errPassword.Clear();
                lblErrorMsg.Text = "";
            }
            else
            {
                errUserName.SetError(txtboxPassword, "No Blanks allowed!");
                lblErrorMsg.Text = "No blanks allowed!";
            }
        }
    }
}
