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

        FlowLayoutPanel thumbNailHolder;
        FlowLayoutPanel pnlMovieInfo;
        int numberOfthumbNails;

        public HomeForm(object sender)
        {
            login = (Form)sender;
            InitializeComponent();

        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Close();
        }

        private void btnCurrLocation_Click(object sender, EventArgs e)
        {
            String loc = ((Button)sender).Text;
            pnlContent.Controls.Clear();
            if (loc.Substring(0,7).Equals("Search:"))
            {
                pnlContent.Controls.Add(thumbNailHolder);
            }
            else if (loc.Contains("Blah"))
            {

            }
            else
            {

            }
        }
        //Adds breadcrumbs to the navigation bar.
        private void addBreadrumbs(object sender, String label)
        {
            //Setup button
            Button btnCurrLocation = new Button();
            btnCurrLocation.AutoEllipsis = true;
            btnCurrLocation.Text = label + " -->";
            btnCurrLocation.FlatStyle = FlatStyle.Flat;
            btnCurrLocation.BackColor = Color.LightSkyBlue;
            btnCurrLocation.ForeColor = Color.White;
            btnCurrLocation.FlatAppearance.BorderSize = 0;
            btnCurrLocation.Name = label;
            btnCurrLocation.Click += new EventHandler(btnCurrLocation_Click);
            btnCurrLocation.AutoSize = true;


            //Add buttons to the breadcrumbs
            breadcrumbsLayout.Controls.Add(btnCurrLocation);
        }

        //removes breadcrumbs from navigation
        private void removeBreadcrumbs(object sender, String label)
        {
            if (label.Equals(""))
            {
                breadcrumbsLayout.Controls.Clear();
            }
            else
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
                    int stopAt = len - location;

                    for (int i = 0; i < stopAt; i++) //And remove all the ones after the one we just removed.
                        breadcrumbsLayout.Controls.RemoveAt(breadcrumbsLayout.Controls.Count - 1);
                }
            }
            
        }

        void changeToMoviePage(Movie theMovie)
        {
            pnlContent.Controls.Clear();


            //build new Full panel
            pnlMovieInfo = new FlowLayoutPanel();
            pnlMovieInfo.BackColor = Color.Wheat;
            pnlMovieInfo.Dock = DockStyle.Fill;
            pnlMovieInfo.FlowDirection = FlowDirection.TopDown;

            //Add Table panel to divide Movie thumbnail with info
            TableLayoutPanel pnlTopTable = new TableLayoutPanel();
                pnlTopTable.RowCount = 1;
                pnlTopTable.ColumnCount = 2;
                pnlTopTable.BackColor = Color.Red;
                pnlTopTable.Dock = DockStyle.Top;
            
                //Add left column contents
                pnlTopTable.Controls.Add(theMovie.buildThumbnailPanel(), 0, 0);

                    //Create right column contents
                    FlowLayoutPanel pnlMovieDetails = new FlowLayoutPanel();
                        Label title = new Label();
                        Button help = new Button();
                        //title.Text = theMovie.getTitle();
                        title.Text = "TEXT ONE TWO THREE";
                        TabControl details = new TabControl();

                        pnlMovieDetails.Controls.Add(help);
                    pnlMovieDetails.Controls.Add(title);
                    pnlMovieDetails.Controls.Add(details);
                
                //add right column contents to table
                    pnlTopTable.Controls.Add(pnlMovieDetails, 1, 0);
            //End of Table Panel

            //Below add another FlowPanel for Reviews
            FlowLayoutPanel pnlReviewHolder = new FlowLayoutPanel();
            pnlReviewHolder.FlowDirection = FlowDirection.TopDown;
            
                FlowLayoutPanel pnlReviewHeader = new FlowLayoutPanel();
                Label lblReview = new Label();
                lblReview.Text = "REVIEWS";
                Button toggle = new Button();
                toggle.Text = "Show/Hide";
                /////////////////////////// REMEMBER TO ADD BUTTON LISTENER
                pnlReviewHeader.Controls.Add(lblReview);
                pnlReviewHeader.Controls.Add(toggle);
            //Add stuff to the review holder panel
            pnlReviewHolder.Controls.Add(pnlReviewHeader);

            
            //Add the two items to the Movie Info Panel
            pnlMovieInfo.Controls.Add(pnlTopTable);
            pnlMovieInfo.Controls.Add(pnlReviewHolder);

            //Add everything to the Content Panel
            pnlContent.Controls.Add(pnlMovieInfo);
        }

        void changeToSearchPage(String searchTerm)
        {
            
            lblLocation.Text = "Search Results for '" + searchTerm + "'";

            pnlContent.Controls.Clear();


            //Large amount of Lag here
            List<Movie> found = Search(txtSearch.Text);
                
            //Begin Building next panel
            pnlContent.AutoScroll = true;
            thumbNailHolder = new FlowLayoutPanel();
            thumbNailHolder.BackColor = System.Drawing.Color.LightYellow;
            thumbNailHolder.Dock = DockStyle.Top;
            thumbNailHolder.Height = 1000;
            this.Resize += new EventHandler(searchSizeChange);


            for (int i = 0; i < found.Count; i++)
            {
                thumbNailHolder.Controls.Add(found[i].buildThumbnailPanel());
            }

            numberOfthumbNails = found.Count;

            pnlContent.Controls.Add(thumbNailHolder);

            
            //MessageBox.Show(found.Count.ToString());
            removeBreadcrumbs(null, "");
            addBreadrumbs(thumbNailHolder, "Search: " + searchTerm);
            searchSizeChange();
        }

        public void searchSizeChange(object sender, EventArgs ee)
        {
            searchSizeChange();
        }
        public void searchSizeChange()
        {
            int numNails = numberOfthumbNails;

            int width = thumbNailHolder.Width;
            int numPerLine = width / Movie.getNailWidth();
            Console.Out.WriteLine("W=" + width + " numPer = " + numPerLine);

            int numLines = (int)Math.Ceiling(numNails / (double)numPerLine);

            thumbNailHolder.Height = numLines * Movie.getNailHeight();
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
                
                changeToSearchPage(txtSearch.Text);
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
            changeToSearchPage("Amazon");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            changeToSearchPage(txtSearch.Text);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<Movie> found = Search("Amazon");
            MessageBox.Show(found[0].ToString());
            changeToMoviePage(found[0]);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
        }

    }
}
