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
        RegisterForm reg;
        bool userValid;
        bool passValid;
        public LoginForm()
        {
            InitializeComponent();
            txtboxUserName.Select();
            userValid = false;
            passValid = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (userValid && passValid) //txtboxUserName.Text.Equals("username") && txtboxPassword.Text.Equals("Password");
            {
                HomeForm main = new HomeForm(this);
                main.Show();
                this.Hide();
                txtboxPassword.Text = "";
                txtboxUserName.Text = "";
            }
            else
            {
                if (txtboxPassword.Text.Equals("") || txtboxUserName.Text.Equals(""))
                {
                    lblErrorMsg.Text = "Please enter information into the empty field(s)";
                }
                else
                {
                    lblErrorMsg.Text = "Please fix Errors before continuing";
                }

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
                userValid = true;
            }
            else
            {
                errUserName.SetError(txtboxUserName, "No Blanks allowed!");
                lblErrorMsg.Text = "No blanks allowed!";
                userValid = false;
            }
        }

        private void txtboxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!txtboxPassword.Text.Equals(""))//everythin's cool
            {
                errPassword.Clear();
                lblErrorMsg.Text = "";
                passValid = true;
            }
            else
            {
                errUserName.SetError(txtboxPassword, "No Blanks allowed!");
                lblErrorMsg.Text = "No blanks allowed!";
                passValid = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (reg == null)
            {
                reg = new RegisterForm(this);
                reg.Show();
            }
            else
            {
                reg.Show();
            }

        }

        private void txtboxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)System.ConsoleKey.Enter)
            {
                txtboxPassword_Validating(this, null);
                btnLogin_Click(this, null);
            }
        }

    }
}
