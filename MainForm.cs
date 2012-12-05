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
    public partial class HomeForm : Form
    {
        Form login;
        public HomeForm(object sender)
        {
            login = (Form)sender;
            InitializeComponent();
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Close();
        }

        private void addBreadCrumbs(object sender, String label)
        {
            Button btnCurrLocation = new Button();
            btnCurrLocation.Text = label;

            Button btnDivider = new Button();
            btnDivider.Text = "-->";

            breadcrumbsLayout.Controls.Add(btnCurrLocation);
            breadcrumbsLayout.Controls.Add(btnDivider);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addBreadCrumbs(this, "Test");
        }
    }
}
