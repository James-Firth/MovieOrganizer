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
        //Variables for the search page.
        List<PictureBox> stars;
        //TableLayoutPanel pnlMovieInfo;
        int numberOfthumbNails;
        TextBox searchTitle;
        TextBox searchDirector;
        TextBox searchYear;

        public static HomeForm self;

        public HomeForm(object sender)
        {
            login = (Form)sender;
            InitializeComponent();
            self = this;

        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Close();
        }

        private void btnCurrLocation_Click(object sender, EventArgs e)
        {
            String temp = ((Button)sender).Text; 
            pnlContent.Controls.Clear();

            //Don't bother moving if we're in the right place
            if (temp.Equals(lblLocation.Text))
            {
                //Stay here
            }
            else //otherwise change location
            {
                pnlContent.Controls.Clear();

                if (temp.Length >= 7 && temp.Substring(0, 7).Equals("Search:"))
                {
                    //MessageBox.Show("SEARCH TYPE LINK: "+loc+" + "+ temp);
                    pnlContent.Controls.Add(thumbNailHolder);
                    String[] renamer = temp.Split(' ');
                    renamer[0] = renamer[0].Substring(0, (renamer[0].Length - 1));
                    String newTitle = temp;
                    if(renamer.Length>=2)
                        newTitle = renamer[0] + " Results for '" + renamer[1] + "'";
                    lblLocation.Text = newTitle;

                }
                else
                {
                    //MessageBox.Show("MOVIE INFO LINK: " + loc + " + " + loc.Substring(0, 7));
                    pnlContent.Controls.Add(pnlMovieInfo);
                    lblLocation.Text = temp;
                }
            }
        }
        //Adds breadcrumbs to the navigation bar.
        private void addBreadrumbs(object sender, String label)
        {
            //Make sure we actually need to add breadcrumbs
            if (!label.Equals(lblLocation.Text))
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

        public void changeToMoviePage(Movie theMovie)
        {
            addBreadrumbs(this, theMovie.getTitle());
            lblLocation.Text = theMovie.getTitle();
            pnlContent.Controls.Clear();


            //build new big panel
            
            pnlMovieInfo = new FlowLayoutPanel();
            //pnlMovieInfo = new TableLayoutPanel();
            //pnlMovieInfo.Anchor = AnchorStyles.Top;
            //pnlMovieInfo.Anchor = AnchorStyles.Bottom;
            //pnlMovieInfo.Anchor = AnchorStyles.Left;
            //pnlMovieInfo.Anchor = AnchorStyles.Right;
            //pnlMovieInfo.RowCount = 2;
           // pnlMovieInfo.ColumnCount = 1;
            pnlMovieInfo.BackColor = Color.SteelBlue;
            pnlMovieInfo.Dock = DockStyle.Fill;
           // this.pnlMovieInfo.RowStyles[0].Height = 50F;
            //this.pnlMovieInfo.RowStyles[1].Height = 50F;
            //this.pnlMovieInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

           pnlMovieInfo.FlowDirection = FlowDirection.TopDown;
            
            //Add Table panel to divide Movie thumbnail with info
            TableLayoutPanel pnlTopTable = new TableLayoutPanel();
                //pnlTopTable.AutoSize = true;
                pnlTopTable.RowCount = 1;
                pnlTopTable.ColumnCount = 2;
                pnlTopTable.Padding = new Padding(0);
                pnlTopTable.Width = 1000;
                pnlTopTable.Height = 360;
                //pnlTopTable.Dock = DockStyle.Fill;

                pnlTopTable.BackColor = Color.SteelBlue;
               
                
            
                //Add left column contents
                FlowLayoutPanel leftCol = new FlowLayoutPanel();
                leftCol.Padding = new Padding(0);
                leftCol.Dock = DockStyle.Fill;
                leftCol.FlowDirection = FlowDirection.TopDown;
                leftCol.Controls.Add(theMovie.buildThumbnailPanel()); //add thumbnails

                //Create area for ratings
                FlowLayoutPanel ratings = new FlowLayoutPanel();
                ratings.Margin = new Padding(22, 0, 0, 0);
                //ratings.Height = 550;
                //ratings.Width = 550;
                ratings.FlowDirection = FlowDirection.LeftToRight;
                ratings.Padding = new Padding(0);
                ratings.BackColor = Color.SteelBlue;
                PictureBox oneStar = new PictureBox();
                PictureBox twoStar = new PictureBox();
                PictureBox threeStar = new PictureBox();
                PictureBox fourStar = new PictureBox();
                PictureBox fiveStar = new PictureBox();
                oneStar.Size = new Size(25, 26);
                twoStar.Size = new Size(25, 26);
                threeStar.Size = new Size(25, 26);
                fourStar.Size = new Size(25, 26);
                fiveStar.Size = new Size(25, 26);

                oneStar.Margin = new Padding(3);
                twoStar.Margin = new Padding(3);
                threeStar.Margin = new Padding(3);
                fourStar.Margin = new Padding(3);
                fiveStar.Margin = new Padding(3);

                oneStar.Padding = new Padding(0);
                twoStar.Padding = new Padding(0);
                threeStar.Padding = new Padding(0);
                fourStar.Padding = new Padding(0);
                fiveStar.Padding = new Padding(0);

                oneStar.ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";
                twoStar.ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";
                threeStar.ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";
                fourStar.ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";
                fiveStar.ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";

                stars = new List<PictureBox>();
                stars.Add(oneStar);
                stars.Add(twoStar);
                stars.Add(threeStar);
                stars.Add(fourStar);
                stars.Add(fiveStar);


            //Event listeners
                ratings.MouseLeave += new EventHandler(ratings_MouseLeave);

                oneStar.MouseEnter += new EventHandler(oneStar_MouseEnter);
                twoStar.MouseEnter += new EventHandler(twoStar_MouseEnter);
                threeStar.MouseEnter += new EventHandler(threeStar_MouseEnter);
                fourStar.MouseEnter += new EventHandler(fourStar_MouseEnter);
                fiveStar.MouseEnter += new EventHandler(fiveStar_MouseEnter);

                oneStar.MouseClick += new MouseEventHandler(oneStar_MouseClick);
                twoStar.MouseClick += new MouseEventHandler(twoStar_MouseClick);
                threeStar.MouseClick += new MouseEventHandler(threeStar_MouseClick);
                fourStar.MouseClick += new MouseEventHandler(fourStar_MouseClick);
                fiveStar.MouseClick += new MouseEventHandler(fiveStar_MouseClick);

                Button watchlist = new Button();
                watchlist.Text = "Add to Watchlist";
                //watchlist.AutoSize = true;
                watchlist.Width = 150;
                watchlist.FlatStyle = FlatStyle.Flat;
                //watchlist.Dock = DockStyle.Fill;

                ratings.Controls.Add(oneStar);
                ratings.Controls.Add(twoStar);
                ratings.Controls.Add(threeStar);
                ratings.Controls.Add(fourStar);
                ratings.Controls.Add(fiveStar);
                ratings.Controls.Add(watchlist);

                leftCol.Controls.Add(ratings); //add ratings

                pnlTopTable.Controls.Add(leftCol, 0, 0);
                
                
                //RIGHT column contents
                FlowLayoutPanel pnlMovieDetails = new FlowLayoutPanel();
                pnlMovieDetails.Margin = new Padding(0, 25, 0, 0);
                
                pnlMovieDetails.Dock = DockStyle.Fill;
                //pnlMovieDetails.Width = 400;
                //pnlMovieDetails.Width = pnlMovieInfo.Width;
                //pnlMovieDetails.Height = pnlMovieInfo.Height / 2;
                pnlMovieDetails.FlowDirection = FlowDirection.TopDown;

                    //Create Tab Info
                    TabControl details = new TabControl();
                    
                    details.Width = 600;
                    details.Height = 285;
                    //details.Dock = DockStyle.Fill;

                    //Create tab pages
                    TabPage pgActors = new TabPage("Actors");
                    //Fill info into the tab page

                    //Actors Tab
                    FlowLayoutPanel flowActors = new FlowLayoutPanel();
                    flowActors.FlowDirection = FlowDirection.TopDown;
                    pgActors.Controls.Add(flowActors);
                    flowActors.Dock = DockStyle.Fill;

                    List<Actor> actorList = theMovie.getActors();
                    for (int i = 0; i < actorList.Count; i++)
                    {
                        Label lblActor = new Label();
                        lblActor.Text = actorList[i].getName();
                        flowActors.Controls.Add(lblActor);
                    }

                    //Info Tab
                    TabPage pgInfo = new TabPage("Info");
                    //FlowLayoutPanel flowInfo = new FlowLayoutPanel();
                    //flowInfo.FlowDirection = FlowDirection.TopDown;
                    //pgInfo.Controls.Add(flowInfo);
                    //flowInfo.Dock = DockStyle.Fill;
                    Label lblInfo = new Label();
                    lblInfo.Dock = DockStyle.Fill;
                    lblInfo.Text = theMovie.getInfo();
                    pgInfo.Controls.Add(lblInfo);
                    //flowInfo.Controls.Add(lblInfo);
                        
                    //Add tab pages
                    details.TabPages.Add(pgActors);
                    details.TabPages.Add(pgInfo);
                        
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
                toggle.FlatStyle = FlatStyle.Flat;
                /////////////////////////// REMEMBER TO ADD BUTTON LISTENER
                pnlReviewHeader.Controls.Add(lblReview);
                pnlReviewHeader.Controls.Add(toggle);
            //Add stuff to the review holder panel
            pnlReviewHolder.Controls.Add(pnlReviewHeader);

            
            //Add the two items to the Movie Info Panel
            pnlMovieInfo.Controls.Add(pnlTopTable);
            pnlMovieInfo.Controls.Add(pnlReviewHolder);
            //pnlMovieInfo.Controls.Add(pnlTopTable, 0, 0);
            //pnlMovieInfo.Controls.Add(pnlReviewHolder, 0, 1);

            //Add everything to the Content Panel
            pnlContent.Controls.Add(pnlMovieInfo);
        }

        void fiveStar_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fourStar_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void threeStar_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void twoStar_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void oneStar_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fiveStar_MouseEnter(object sender, EventArgs e)
        {
            changeAllStars((PictureBox)sender);
        }

        void fourStar_MouseEnter(object sender, EventArgs e)
        {
            changeAllStars((PictureBox)sender);
        }

        void threeStar_MouseEnter(object sender, EventArgs e)
        {
            changeAllStars((PictureBox)sender);
        }

        void ratings_MouseLeave(object sender, EventArgs e)
        {
            blankAllStars();
        }

        void twoStar_MouseEnter(object sender, EventArgs e)
        {
            changeAllStars((PictureBox)sender);
        }

        void changeAllStars(PictureBox sender)
        {
            int curr = stars.IndexOf(sender);
            for (int i = 0; i <= curr; i++)
            {
                stars[i].ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\yellowstar.png";

            }

            if (curr + 1 < stars.Count)
            {
                for (int i = curr + 1; i < stars.Count; i++)
                {
                    stars[i].ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";
                }
            }
        }

        void blankAllStars()
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].ImageLocation = "C:\\Users\\Casey\\Documents\\GitHub\\MovieOrganizer\\Graphics\\blankstar.png";
            }
        }
        void oneStar_MouseEnter(object sender, EventArgs e)
        {
            changeAllStars((PictureBox)sender);
        }

        void changeToSearchPage(String searchTerm)
        {
            cleanUpBeforeSearch(searchTerm);
            //Large amount of Lag here
            List<Movie> found = Search(searchTerm);
            changeToSearchPageStage2(found);
        }
        void changeToSearchPage(String title,
                                    String director,
                                    String year,
                                    String genre,
                                    String actor)
        {
            String name = generateName(title, director, year, genre, actor);

            cleanUpBeforeSearch(name);
            //Large amount of Lag here
            List<Movie> found = Search(title, director, year, genre, actor);
            changeToSearchPageStage2(found,title,director,year,genre,actor);
        }
        private string generateName(string title, string director, string year, string genre, string actor)
        {
            if (title != "")
                return title;
            if (director != "")
                return director;
            if (year != "")
                return year;
            if (genre != "")
                return genre;
            if (actor != "")
                return actor;
            return "Advanced";

        }

        void cleanUpBeforeSearch(String title)
        {
            pnlContent.Controls.Clear();
            lblLocation.Text = "Search Results for '" + title + "'";
            removeBreadcrumbs(null, "");
            addBreadrumbs(thumbNailHolder, "Search: " + title);
        }
        void changeToSearchPageStage2(List<Movie> found,
                                        String title = "",
                                        String director = "",
                                        String year = "",
                                        String genre = "",
                                        String actor = "")
        {
            //Begin Building next panel
            pnlContent.AutoScroll = true;
            thumbNailHolder = new FlowLayoutPanel();
            thumbNailHolder.BackColor = System.Drawing.Color.LightYellow;
            thumbNailHolder.Dock = DockStyle.Top;
            thumbNailHolder.Height = 1000;
            this.Resize += new EventHandler(searchSizeChange);
            FlowLayoutPanel topAdvanceBar = new FlowLayoutPanel();
            topAdvanceBar.BackColor = Color.Red;
            topAdvanceBar.Height = 40;

            searchTitle = new TextBox();
            searchDirector = new TextBox();
            searchYear = new TextBox();
            Label LBLtitle = new Label();
            Label LBLdirector = new Label();
            Label LBLyear = new Label();
            Button BTNsubmit = new Button();
            BTNsubmit.Text = "Submit";

            BTNsubmit.Click += new EventHandler(submitAdvanceSearch);
            searchTitle.Text = title;
            searchDirector.Text = director;
            searchYear.Text = year;


            LBLtitle.Width = 40;
            LBLdirector.Width = 60;
            LBLyear.Width = 40;

            Padding pad = new Padding(10, 10, 0, 10);
            LBLtitle.Margin = pad;
            LBLdirector.Margin = pad;
            LBLyear.Margin = pad;
            searchTitle.Margin = pad;
            searchDirector.Margin = pad;
            searchYear.Margin = pad;
            BTNsubmit.Margin = pad;
            LBLtitle.Text = "Title:";
            LBLdirector.Text = "Director:";
            LBLyear.Text = "Year:";

            topAdvanceBar.Controls.Add(LBLtitle);
            topAdvanceBar.Controls.Add(searchTitle);
            topAdvanceBar.Controls.Add(LBLdirector);
            topAdvanceBar.Controls.Add(searchDirector);
            topAdvanceBar.Controls.Add(LBLyear);
            topAdvanceBar.Controls.Add(searchYear);
            topAdvanceBar.Controls.Add(BTNsubmit);

            topAdvanceBar.Dock = DockStyle.Top;

            for (int i = 0; i < found.Count; i++)
            {
                thumbNailHolder.Controls.Add(found[i].buildThumbnailPanel());
            }
            numberOfthumbNails = found.Count;
            pnlContent.Controls.Add(thumbNailHolder);
            pnlContent.Controls.Add(topAdvanceBar);

            //MessageBox.Show(found.Count.ToString());
            searchSizeChange();
        }
        public void submitAdvanceSearch(object sender, EventArgs ee)
        {
            changeToSearchPage(
            searchTitle.Text,
            searchDirector.Text,
            searchYear.Text,
            "",
            "");
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
            List<Movie> movies = helper.SelectMovie("SELECT * FROM Movies WHERE title LIKE '%" + searchTerm + "%'");
            /*
            if(movies.Count != 0)
                MessageBox.Show(movies[0].ToString());
            else
                MessageBox.Show("Nothing found...?");
            */
            return movies;
        }
        private List<Movie> Search(string title, string director, string year, string genre, string actor)
        {
            DBConnect helper = new DBConnect();
            List<Movie> movies = new List<Movie>();
            if (title.Length > 2)
            {
                List<Movie> fromTitle = helper.SelectMovie("SELECT * FROM Movies WHERE title LIKE '%" + title + "%'");
                movies.AddRange(fromTitle);
            }
            if (director.Length > 2)
            {
                List<Movie> fromDirector = helper.SelectMovie("SELECT * FROM Movies WHERE director LIKE '%" + director + "%'");
                movies.AddRange(fromDirector);
            }
            if (year.Length > 2)
            {
                List<Movie> fromYear = helper.SelectMovie("SELECT * FROM Movies WHERE year LIKE '%" + year + "%'");
                movies.AddRange(fromYear);
            }

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
            //MessageBox.Show(found[0].ToString());
            changeToMoviePage(found[0]);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
        }

    }
}
