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
    partial class HomeForm : Form
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
                    breadcrumbsLayout.Controls.RemoveAt(breadcrumbsLayout.Controls.Count - 1);
            }
            
        }

        void changeToSearchPage(List<Movie> found)
        {
            pnlContent.Controls.Clear();

            //Begin Building next panel

            FlowLayoutPanel thumbNailHolder = new FlowLayoutPanel();
            thumbNailHolder.BackColor = System.Drawing.Color.LightYellow;
            thumbNailHolder.Dock = DockStyle.Fill;

            for (int i = 0; i < found.Count; i++)
            {
                thumbNailHolder.Controls.Add(found[i].buildThumbnailPanel());
            }
            pnlContent.Controls.Add(thumbNailHolder);

            MessageBox.Show(found.Count.ToString());
        }

        void changeToMoviePage(Movie theMovie)
        {
            pnlContent.Controls.Clear();

            //build new panel

        }

        public List<Movie> Search(String searchTerm)
        {
            DBConnect helper = new DBConnect();
            List<Movie> movies = helper.SelectMovie("SELECT * FROM Movies WHERE title LIKE '%"+searchTerm+"%'");
            /*
            if(movies.Count != 0)
                MessageBox.Show(movies[0].ToString());
            else
                MessageBox.Show("Nothing found...?");
            */
            return movies;

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
            if (e.KeyChar == (char)Keys.Enter)
            {
                List<Movie> found = Search(txtSearch.Text);
                changeToSearchPage(found);
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            List<Movie> found = Search("Amazon");
            changeToSearchPage(found);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Movie> found = Search( txtSearch.Text);
            changeToSearchPage(found);

        }

    }
}
