using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

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

        ///REMOVE DURING PRODUCTION PHASE ///////////////////
        private void LoginForm_Load(object sender, EventArgs e)
        {

            ///// JURY-RIGGED FOR NOW MAKE SURE TO REMOVE IN FINAL PRODUCT! //////
            //HomeForm main = new HomeForm(this);
            //main.Show();
            //this.Hide();
            /////// NEEEEEEEDS TO BE REMOVED IN FINAL PRODUCT OTHERWISE YOU WILL NOT BE ABLE TO LOG IN ///////
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String givenUserName = Regex.Replace(txtboxUserName.Text, @"[\r\n\x00\x1a\\'""]", @"\$0");
            String givenPassword = Regex.Replace(txtboxPassword.Text, @"[\r\n\x00\x1a\\'""]", @"\$0");


            DBConnect connector = new DBConnect();
            List<String>[] temp = connector.SelectUsers("SELECT * FROM Users WHERE name='" + givenUserName + "' AND pass='" + givenPassword + "'");
            ValidateLogin();
            if (temp[0].Count == 1) //Pass
            {
                userValid = true;
                passValid = true;
            }
            else //Fail because of wrong password
            {
                userValid = false;
                passValid = false;
                txtboxPassword.Text = "";
                MessageBox.Show("Error: username/password do not match");
                lblErrorMsg.Visible = true;
            }


            if (userValid && passValid) //txtboxUserName.Text.Equals("username") && txtboxPassword.Text.Equals("Password");
            {
                MessageBox.Show("UID:" + temp[0][0]);
                HomeForm main = new HomeForm(this, int.Parse(temp[0][0]));
                main.Show();
                this.Hide();
                txtboxPassword.Text = "";
                txtboxUserName.Text = "";
            }
            else
            {
                lblErrorMsg.Visible = true;

            }
        }

        private void ValidateLogin()
        {
            if (!txtboxUserName.Text.Equals(""))//everythin's cool
            {
                errUserName.Clear();
                lblErrorMsg.Visible = false;
                userValid = true;
            }
            else
            {
                errUserName.SetError(txtboxUserName, "No Blanks allowed!");
                lblErrorMsg.Visible = true;
                userValid = false;
            }
        }
  

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            txtboxUserName.Text = "";
            txtboxPassword.Text = "";
            lblErrorMsg.Visible = false;
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

        private void txtboxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)System.ConsoleKey.Enter)
            {
                ValidateLogin();
                btnLogin_Click(this, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnect helper = new DBConnect();
            Console.Out.WriteLine(helper.AverageRating(1));
        }



    }
}
