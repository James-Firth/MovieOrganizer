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
    public partial class RegisterForm : Form
    {
        Form parent;
        bool userValid; //used to check if username is valid without doing a second check after clicking register button
        bool passValid;
        public RegisterForm(Form sender)
        {
            parent = sender;
            InitializeComponent();
            userValid = true; ////@TODO: Need to check db for the current username entered.
            passValid = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtboxUsername.Text = "";
            txtboxPassword.Text = "";
            txtboxConfirmPassword.Text = "";
            this.Hide();
            parent.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            parent.Close();
        }

        private void txtboxConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            
            if (txtboxPassword.Text.Equals(txtboxConfirmPassword.Text))
            {
                //all good
                errMistmatch.Clear();
                lblMismatch.Visible = false;
                passValid = true;
            }
            else
            {
                //error!
                lblMismatch.Visible = true;
                passValid = false;
                errMistmatch.SetError(txtboxPassword, "ERROR");
                errMistmatch.SetError(txtboxConfirmPassword, "ERROR");
                //txtboxConfirmPassword.Clear();
                //txtboxPassword.Clear();
            }
             
        }

        private void txtboxConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((char)Keys.Enter == e.KeyChar)
                btnRegister_Click(this, null);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            if (passValid)
            {
                DBConnect connector = new DBConnect();
                List<String>[] temp = connector.SelectUsers("SELECT * FROM Users WHERE name='" + txtboxUsername.Text + "'");

               
                if (temp[0].Count == 0)
                {
                   
                    connector.Insert("INSERT INTO Users (name,pass) VALUES ('" + txtboxUsername.Text + "','" + txtboxPassword.Text + "')");
                    txtboxPassword.Clear();
                    txtboxConfirmPassword.Clear();
                    txtboxUsername.Clear();
                    this.Hide();
                    parent.Show();
                }
                else
                {
                    MessageBox.Show("Error user already exists");
                }
            }
            else
            {
                lblMismatch.Text = "Passwords do no match!";
            }

        }

        
        private void txtboxPassword_Validating(object sender, CancelEventArgs e)
        {
           // txtboxConfirmPassword_Validating(this, null);
        }
         

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Close();
        }

        private void txtboxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtboxConfirmPassword_KeyPress(sender, e);
        }

        private void txtboxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtboxConfirmPassword_KeyPress(sender, e);
        }

        private void txtboxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtboxPassword.Text.Length >= txtboxConfirmPassword.Text.Length && 
                txtboxPassword.Text.Substring(0,txtboxConfirmPassword.Text.Length).Equals(txtboxConfirmPassword.Text))
            {
                //all good
                errMistmatch.Clear();
                lblMismatch.Visible = false;
                passValid = true;
            }
            else
            {
                //error!
                lblMismatch.Visible = true;
                passValid = false;
                errMistmatch.SetError(txtboxPassword, "ERROR");
                errMistmatch.SetError(txtboxConfirmPassword, "ERROR");
                //txtboxConfirmPassword.Clear();
                //txtboxPassword.Clear();
            }
        }

    }
}
