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
                txtboxConfirmPassword.Clear();
                txtboxPassword.Clear();
            }
        }
    }
}
