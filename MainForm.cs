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

        //Adds breadcrumbs to the navigation bar.
        private void addBreadrumbs(object sender, String label)
        {
            Button btnCurrLocation = new Button();
            btnCurrLocation.Text = label + " -->";
            btnCurrLocation.FlatStyle = FlatStyle.Flat;
            btnCurrLocation.BackColor = Color.LightSkyBlue;
            btnCurrLocation.FlatAppearance.BorderSize = 0;
            btnCurrLocation.Name = label;

            breadcrumbsLayout.Controls.Add(btnCurrLocation);
        }

        //removes breadcrumbs from navigation
        private void removeBreadcrumbs(object sender, String label)
        {
            //Finds all the breadcrumbs with that label
            Control[] items = breadcrumbsLayout.Controls.Find(label, false);

            //if the item exists
            if (items.Length != 0)
            {
                //We record the location of it (farthest to the right)
                int location = breadcrumbsLayout.Controls.IndexOf(items[items.Length - 1]);
                breadcrumbsLayout.Controls.Remove(items[items.Length - 1]); //and remove it.
                
                int len = breadcrumbsLayout.Controls.Count; //then we see how many breacrumbs there are.
                int stopAt = len-location;
                
                for(int i=0; i < stopAt; i++) //And remove all the ones after the one we just removed.
                    breadcrumbsLayout.Controls.RemoveAt(len - 1);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addBreadrumbs(this, "Test");
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
                Search(txtSearch.Text);
        }

        private void Search(String term)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            removeBreadcrumbs(this, "Test");
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            addBreadrumbs(this, "Something");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DBConnect helper = new DBConnect();
            List<Genre> test = helper.SelectGenres("SELECT * FROM Genres WHERE GID='1'");
            Genre blah = test[0];
            MessageBox.Show(blah.getName());

        }

    }
}
