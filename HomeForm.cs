using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;


namespace MovieOrganizer
{
    partial class HomeForm : Form
    {
        Form login;

        Panel pnlSearch;
        Panel pnlRec;
        Panel Profile;
        FlowLayoutPanel thumbNailHolder;


        FlowLayoutPanel pnlMovieInfo;
        //Variables for the search page.
        List<PictureBox> stars;
        int numberOfthumbNails;
        TextBox searchTitle;
        TextBox searchDirector;
        TextBox searchYear;
        TextBox searchActor;
        TextBox searchGenre;
        Movie currMovie;
        int movieRating;
        bool userRated;
        bool killForm;
        int ratingsDone;
        public static int UID;
        String userName;

        public static HomeForm self;

        public HomeForm(object sender,int loginUID, String uName)
        {
            login = (Form)sender;
            UID = loginUID;
            userName = uName;
            InitializeComponent();
            self = this;
            killForm = true;

        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (killForm)
            {
                login.Close();
            }
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
                    pnlContent.Controls.Add(pnlSearch);
                    String[] renamer = temp.Split(' ');
                    renamer[0] = renamer[0].Substring(0, (renamer[0].Length - 1));
                    String newTitle = temp;
                    if(renamer.Length>=2)
                        newTitle = renamer[0] + " Results for '" + renamer[1] + "'";
                    lblLocation.Text = newTitle;
                    

                }
                else if (temp.Contains("Profile"))
                {
                    changetoProfilePage();
                    lblLocation.Text = temp;
                }
                else
                {
                    //MessageBox.Show("MOVIE INFO LINK: " + loc + " + " + loc.Substring(0, 7));
                    pnlContent.Controls.Add(pnlMovieInfo);
                    lblLocation.Text = temp;
                }

                //FIND AND REMOVE EVERYTHING BEFORE THIS ONE
                //We record the location of it (farthest to the right)
                int location = breadcrumbsLayout.Controls.IndexOf((Button)sender)+1;

                int len = breadcrumbsLayout.Controls.Count; //then we see how many breacrumbs there are.
                int stopAt = len - location;

                for (int i = 0; i < stopAt; i++) //And remove all the ones after the one we just removed.
                    breadcrumbsLayout.Controls.RemoveAt(breadcrumbsLayout.Controls.Count - 1);
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
                btnCurrLocation.BackColor = Color.CornflowerBlue;
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
            currMovie = theMovie;
            DBConnect helper = new DBConnect();
            movieRating = (int)helper.getUserRating(theMovie.getMID(), UID);
           // MessageBox.Show("Rating:" + movieRating.ToString());
            userRated = true;
            if (movieRating == -1)
            {
                userRated = false;
                movieRating = (int)helper.AverageRating(theMovie.getMID());
            }
            
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

                oneStar.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                twoStar.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                threeStar.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                fourStar.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                fiveStar.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");

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
                if (helper.inWatchList(theMovie.getMID(),UID))//If in watchlist
                {
                    watchlist.Text = "Remove from Watchlist";
                    
                }
                else
                {
                    watchlist.Text = "Add to Watchlist";
                }
                watchlist.MouseClick += new MouseEventHandler(watchlist_MouseClick);
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

                changeAllStars(movieRating);
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

            /*
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
             */

            
            //Add the two items to the Movie Info Panel
            pnlMovieInfo.Controls.Add(pnlTopTable);
           // pnlMovieInfo.Controls.Add(pnlReviewHolder);
            //pnlMovieInfo.Controls.Add(pnlTopTable, 0, 0);
            //pnlMovieInfo.Controls.Add(pnlReviewHolder, 0, 1);

            //Add everything to the Content Panel
            pnlContent.Controls.Add(pnlMovieInfo);
        }

        void watchlist_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text.Equals("Add to Watchlist"))//(((Button)sender).Text.Equals(""))//If we want to add it
            {
                DBConnect adder = new DBConnect();
                adder.addToWatchList(currMovie.getMID(), UID);
                btn.Text = "Remove from Watchlist";

            }
            else 
            {
                DBConnect adder = new DBConnect();
                adder.removeFromWatchList(currMovie.getMID(), UID);//Remove from watchlist
                btn.Text = "Add to Watchlist";

            }

        }

        void fiveStar_MouseClick(object sender, MouseEventArgs e)
        {
            DBConnect rater = new DBConnect();
            rater.addRating(currMovie.getMID(), UID, 5);
            userRated = true;
            movieRating = 5;
        }

        void fourStar_MouseClick(object sender, MouseEventArgs e)
        {
            DBConnect rater = new DBConnect();
            rater.addRating(currMovie.getMID(), UID, 4);
            userRated = true;
            movieRating = 4;
        }

        void threeStar_MouseClick(object sender, MouseEventArgs e)
        {
            DBConnect rater = new DBConnect();
            rater.addRating(currMovie.getMID(), UID, 3);
            userRated = true;
            movieRating = 3;
        }

        void twoStar_MouseClick(object sender, MouseEventArgs e)
        {
            DBConnect rater = new DBConnect();
            rater.addRating(currMovie.getMID(), UID, 2);
            userRated = true;
            movieRating = 2;
        }

        void oneStar_MouseClick(object sender, MouseEventArgs e)
        {
            DBConnect rater = new DBConnect();
            rater.addRating(currMovie.getMID(),UID, 1);
            userRated = true;
            movieRating = 1;
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
            //blankAllStars();
            changeAllStars(movieRating);
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
                stars[i].ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/yellowstar.png");

            }

            if (curr + 1 < stars.Count)
            {
                for (int i = curr + 1; i < stars.Count; i++)
                {
                    stars[i].ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                }
            }
        }

        void changeAllStars(int newRating)
        {
            int curr = newRating;
            for (int i = 0; i < curr; i++)
            {
                stars[i].ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/yellowstar.png");

            }

            if (curr + 1 < stars.Count)
            {
                for (int i = curr + 1; i < stars.Count; i++)
                {
                    stars[i].ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                }
            }
        }

        void blankAllStars()
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
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
        void changeToSearchPage(String title, String director,String year,String genre,String actor)
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
            addBreadrumbs(pnlSearch, "Search: " + title);
            Label loading = new Label();
            loading.Text = "Loading...";
            pnlContent.Controls.Add(loading);
            //System.Threading.Thread.Sleep(500);
        }
        void changeToSearchPageStage2(List<Movie> found,
                                        String title = "",
                                        String director = "",
                                        String year = "",
                                        String genre = "",
                                        String actor = "")
        {
            pnlContent.Controls.Clear();
            //Begin Building next panel

            if (found.Count > 0)
            {
                pnlSearch = new Panel();
                pnlSearch.Dock = DockStyle.Fill;
                pnlSearch.AutoScroll = true;

                thumbNailHolder = new FlowLayoutPanel();
                thumbNailHolder.BackColor = System.Drawing.Color.LightYellow;
                thumbNailHolder.Dock = DockStyle.Top;
                thumbNailHolder.Height = 1000;

                this.Resize -= new EventHandler(searchSizeChange);
                this.Resize -= new EventHandler(recSizeChange);
                this.Resize -= new EventHandler(watchSizeChange);

                this.Resize += new EventHandler(searchSizeChange);

                FlowLayoutPanel topAdvanceBar = new FlowLayoutPanel();
                topAdvanceBar.BackColor = Color.Red;
                topAdvanceBar.Height = 40;

                searchTitle = new TextBox();
                searchDirector = new TextBox();
                searchYear = new TextBox();
                searchActor = new TextBox();
                searchGenre = new TextBox();

                KeyPressEventHandler KeyPress = new KeyPressEventHandler(advance_KeyPress);

                searchTitle.KeyPress += KeyPress;
                searchDirector.KeyPress += KeyPress;
                searchYear.KeyPress += KeyPress;
                searchActor.KeyPress += KeyPress;
                searchGenre.KeyPress += KeyPress;


                Label LBLtitle = new Label();
                Label LBLdirector = new Label();
                Label LBLyear = new Label();
                Label LBLactor = new Label();
                Label LBLgenre = new Label();
                Button BTNsubmit = new Button();
                BTNsubmit.Text = "Advanced Search";
                BTNsubmit.FlatStyle = FlatStyle.Flat;
                BTNsubmit.AutoSize = true;

                //Listeners
                BTNsubmit.Click += new EventHandler(submitAdvanceSearch);
                searchTitle.Text = title;
                searchDirector.Text = director;
                searchYear.Text = year;
                searchActor.Text = actor;
                searchGenre.Text = genre;


                LBLtitle.Width = 35;
                LBLdirector.Width = 60;
                LBLyear.Width = 35;
                LBLactor.Width = 35;
                LBLgenre.Width = 40;

                Padding pad = new Padding(5, 10, 0, 10);
                LBLtitle.Margin = pad;
                LBLdirector.Margin = pad;
                LBLyear.Margin = pad;
                LBLactor.Margin = pad;
                LBLgenre.Margin = pad;

                searchTitle.Margin = pad;
                searchDirector.Margin = pad;
                searchYear.Margin = pad;
                searchActor.Margin = pad;
                searchGenre.Margin = pad;

                BTNsubmit.Margin = pad;
                LBLtitle.Text = "Title:";
                LBLdirector.Text = "Director:";
                LBLyear.Text = "Year:";
                LBLactor.Text = "Actor:";
                LBLgenre.Text = "Genre:";

                topAdvanceBar.Controls.Add(LBLtitle);
                topAdvanceBar.Controls.Add(searchTitle);

                topAdvanceBar.Controls.Add(LBLdirector);
                topAdvanceBar.Controls.Add(searchDirector);

                topAdvanceBar.Controls.Add(LBLyear);
                topAdvanceBar.Controls.Add(searchYear);

                topAdvanceBar.Controls.Add(LBLactor);
                topAdvanceBar.Controls.Add(searchActor);

                topAdvanceBar.Controls.Add(LBLgenre);
                topAdvanceBar.Controls.Add(searchGenre);

                topAdvanceBar.Controls.Add(BTNsubmit);

                topAdvanceBar.Dock = DockStyle.Top;

                for (int i = 0; i < found.Count; i++)
                {
                    thumbNailHolder.Controls.Add(found[i].buildThumbnailPanel());
                }
                numberOfthumbNails = found.Count;


                pnlSearch.Controls.Add(thumbNailHolder);
                pnlSearch.Controls.Add(topAdvanceBar);

                pnlContent.Controls.Add(pnlSearch);
                //MessageBox.Show(found.Count.ToString());
                searchSizeChange();
            }
            else
            {
                Panel temp = new Panel();
                temp.Height = 400;
                temp.Width = 800;
                Label error = new Label();
                error.Height = 400;
                error.Width = 800;
                error.TextAlign = ContentAlignment.MiddleCenter;
                error.ForeColor = Color.White;
                error.Text = "No results found";
                lblLocation.Text = "No Search Results found";
                temp.Controls.Add(error);
                pnlContent.Controls.Add(temp);
            }
        }
        public void submitAdvanceSearch(object sender, EventArgs ee)
        {
            changeToSearchPage(
            searchTitle.Text,
            searchDirector.Text,
            searchYear.Text,
            searchGenre.Text,
            searchActor.Text);
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
            List<Movie> movies;
            if (searchTerm.Length > 2)
            {
                movies = helper.SelectMovie("SELECT * FROM Movies WHERE title LIKE '%" + searchTerm + "%'");
            }
            else
            {
                movies = new List<Movie>();
            }
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
            if (genre.Length > 2)
            {
                List<Movie> fromGenre = helper.SelectMovieByGenre(genre);
                movies.AddRange(fromGenre);
            }
            if (actor.Length > 2)
            {
                List<Movie> fromActor = helper.SelectMovieByActor(actor);
                movies.AddRange(fromActor);
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
                if(txtSearch.Text.Length > 0)
                    changeToSearchPage(txtSearch.Text);
            }
        }

        private void advance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                changeToSearchPage(searchTitle.Text, searchDirector.Text, searchYear.Text, searchGenre.Text, searchActor.Text);
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

        private void button8_Click(object sender, EventArgs e)
        {
            DBConnect helper = new DBConnect();
            Console.Out.WriteLine(helper.SelectMovieByGenre("History").Count);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            login.Show();
            killForm = false;
            this.Close();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            changetoProfilePage();
        }

        private void btnGetRecommends_Click(object sender, EventArgs e)
        {
            DBConnect helper = new DBConnect();
            changeToRecPage(helper.selectRecomendations(UID));
        }

        void changeToRecPage(List<Movie> found)
        {
            if (found.Count > 0)
            {
                pnlContent.Controls.Clear();
                //Begin Building next panel

                pnlRec = new Panel();
                pnlRec.Dock = DockStyle.Fill;
                pnlRec.AutoScroll = true;

                thumbNailHolder = new FlowLayoutPanel();
                thumbNailHolder.BackColor = System.Drawing.Color.LightYellow;
                thumbNailHolder.Dock = DockStyle.Top;
                thumbNailHolder.Height = 1000;


                this.Resize -= new EventHandler(searchSizeChange);
                this.Resize -= new EventHandler(recSizeChange);
                this.Resize -= new EventHandler(watchSizeChange);

                this.Resize += new EventHandler(recSizeChange);

                for (int i = 0; i < found.Count; i++)
                {
                    thumbNailHolder.Controls.Add(found[i].bildThumbnailWatchPanel());
                }
                numberOfthumbNails = found.Count;


                pnlRec.Controls.Add(thumbNailHolder);

                pnlContent.Controls.Add(pnlRec);
                //MessageBox.Show(found.Count.ToString());
                recSizeChange();
            }
            else
            {
                pnlContent.Controls.Clear();
                Panel temp = new Panel();
                temp.Height = 400;
                temp.Width = 800;
                Label error = new Label();
                error.Height = 400;
                error.Width = 800;
                error.TextAlign = ContentAlignment.MiddleCenter;
                error.ForeColor = Color.White;
                error.Text = "No Recommendations found. \r\n Please Rate some movies (button to the left).";
                lblLocation.Text = "No Recommendations found";
                temp.Controls.Add(error);
                pnlContent.Controls.Add(temp);
            }
        }


        public void recSizeChange(object sender, EventArgs ee)
        {
            recSizeChange();
        }
        public void recSizeChange()
        {
            int numNails = numberOfthumbNails;
            int width = thumbNailHolder.Width;
            int numPerLine = width / Movie.getNailWatchWidth();
            Console.Out.WriteLine("W=" + width + " numPer = " + numPerLine);
            int numLines = (int)Math.Ceiling(numNails / (double)numPerLine);
            thumbNailHolder.Height = numLines * Movie.getNailWatchHeight();
        }

        public void changetoProfilePage()
        {
            pnlContent.Controls.Clear();

            TableLayoutPanel profile = new TableLayoutPanel();
            removeBreadcrumbs(null, "");
            addBreadrumbs(profile, "Profile");
            lblLocation.Text = "Profile";
            

            //styling
            profile.Dock = DockStyle.Fill;
            profile.RowCount = 1;
            profile.ColumnCount = 2;
            profile.BackColor = Color.CornflowerBlue;
            //profile.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, (float)500.0);

            //left panel
            Panel userInfo = new Panel();
            userInfo.Width = 425;
            userInfo.Dock = DockStyle.Fill;
            Label name = new Label();
            name.ForeColor = Color.White;
            name.AutoSize = true;
            name.Font = new Font("Arial", (float)14.0, FontStyle.Bold);
            name.Margin = new Padding(180, 20, 0, 0);
            userInfo.Padding = new Padding(50);
            name.Text = "Username: "+userName;
            userInfo.BackColor = Color.SteelBlue;
            userInfo.Controls.Add(name);

            

            //right panel
            Panel watchlist = new Panel();
            watchlist.Dock = DockStyle.Fill;
            watchlist.BackColor = Color.CornflowerBlue;
            watchlist.AutoScroll = true;
            //Adding stuff
            profile.Controls.Add(userInfo, 0, 0);
            profile.Controls.Add(watchlist, 1, 0);

                //ThumbNail

                thumbNailHolder = new FlowLayoutPanel();
                thumbNailHolder.BackColor = Color.CornflowerBlue;
                thumbNailHolder.Dock = DockStyle.Top;
                thumbNailHolder.Height = 1000;


                this.Resize -= new EventHandler(searchSizeChange);
                this.Resize -= new EventHandler(recSizeChange);
                this.Resize -= new EventHandler(watchSizeChange);

                this.Resize += new EventHandler(watchSizeChange);
                watchlist.Controls.Add(thumbNailHolder);

                






            //Add all this to the content panel
            pnlContent.Controls.Add(profile);




            //Populate Wish List
            
            DBConnect helper = new DBConnect();
            List<Movie> found = helper.selectMoviesFromWatchList(UID);
            
            for (int i = 0; i < found.Count; i++)
            {
                thumbNailHolder.Controls.Add(found[i].bildThumbnailWatchPanel());
            }
            numberOfthumbNails = found.Count;

            //MessageBox.Show(found.Count.ToString());
            watchSizeChange();


        }


        public void watchSizeChange(object sender, EventArgs ee)
        {
            watchSizeChange();
        }

        public void watchSizeChange()
        {
            int numNails = numberOfthumbNails;
            int width = thumbNailHolder.Width;
            int numPerLine = width / Movie.getNailWatchWidth();
            Console.Out.WriteLine("W=" + width + " numPer = " + numPerLine);
            int numLines = (int)Math.Ceiling(numNails / (double)numPerLine);
            thumbNailHolder.Height = numLines * Movie.getNailWatchHeight();
        }

        private void btnNavRate_Click(object sender, EventArgs e)
        {
            ratingsDone = 5;
            changetoRateMovieList();
        }

        public void changetoRateMovieList()
        {
            if (ratingsDone > 0)
            {
                removeBreadcrumbs(null, "");
                lblLocation.Text = "Rating Movies";
                pnlContent.Controls.Clear();


                DBConnect helper = new DBConnect();
                Movie theMovie = helper.selectRandomMovies(1)[0];
                currMovie = theMovie;

                //Panel pnlRate = new Panel();
                //pnlRate.Controls.Add(theMovie.buildThumbnailPanel());
                // pnlRate.Dock = DockStyle.Fill;

                //Add left column contents
                FlowLayoutPanel leftCol = new FlowLayoutPanel();
                leftCol.Padding = new Padding(0);
                leftCol.Dock = DockStyle.Fill;
                leftCol.FlowDirection = FlowDirection.TopDown;


                //Create area for ratings
                FlowLayoutPanel ratings = new FlowLayoutPanel();
                ratings.Margin = new Padding(22, 0, 0, 0);
                //ratings.Height = 550;
                //ratings.Width = 550;
                ratings.FlowDirection = FlowDirection.LeftToRight;
                ratings.Padding = new Padding(0);
                ratings.BackColor = Color.SteelBlue;
                PictureBox starOne = new PictureBox();
                PictureBox starTwo = new PictureBox();
                PictureBox starThree = new PictureBox();
                PictureBox starFour = new PictureBox();
                PictureBox starFive = new PictureBox();
                starOne.Size = new Size(25, 26);
                starTwo.Size = new Size(25, 26);
                starThree.Size = new Size(25, 26);
                starFour.Size = new Size(25, 26);
                starFive.Size = new Size(25, 26);

                starOne.Margin = new Padding(3);
                starTwo.Margin = new Padding(3);
                starThree.Margin = new Padding(3);
                starFour.Margin = new Padding(3);
                starFive.Margin = new Padding(3);

                starOne.Padding = new Padding(0);
                starTwo.Padding = new Padding(0);
                starThree.Padding = new Padding(0);
                starFour.Padding = new Padding(0);
                starFive.Padding = new Padding(0);

                starOne.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                starTwo.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                starThree.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                starFour.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");
                starFive.ImageLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/blankstar.png");

                stars = new List<PictureBox>();
                stars.Add(starOne);
                stars.Add(starTwo);
                stars.Add(starThree);
                stars.Add(starFour);
                stars.Add(starFive);


                //Event listeners
                ratings.MouseLeave += new EventHandler(ratings_MouseLeave);

                starOne.MouseEnter += new EventHandler(oneStar_MouseEnter);
                starTwo.MouseEnter += new EventHandler(twoStar_MouseEnter);
                starThree.MouseEnter += new EventHandler(threeStar_MouseEnter);
                starFour.MouseEnter += new EventHandler(fourStar_MouseEnter);
                starFive.MouseEnter += new EventHandler(fiveStar_MouseEnter);

                starOne.MouseClick += new MouseEventHandler(starOne_MouseClick);
                starTwo.MouseClick += new MouseEventHandler(starTwo_MouseClick);
                starThree.MouseClick += new MouseEventHandler(starThree_MouseClick);
                starFour.MouseClick += new MouseEventHandler(starFour_MouseClick);
                starFive.MouseClick += new MouseEventHandler(starFive_MouseClick);

                Button btnSkip = new Button();
                btnSkip.MouseClick += new MouseEventHandler(btnSkip_MouseClick);
                btnSkip.Text = "Skip";
                btnSkip.BackColor = Color.LightSkyBlue;
                btnSkip.ForeColor = Color.White;
                btnSkip.Width = 150;
                btnSkip.FlatStyle = FlatStyle.Flat;

                Label lblRatingsLeft = new Label();
                lblRatingsLeft.ForeColor = Color.White;
                lblRatingsLeft.Text = (ratingsDone).ToString() + " ratings to go.";


                //Add to panels
                ratings.Controls.Add(starOne);
                ratings.Controls.Add(starTwo);
                ratings.Controls.Add(starThree);
                ratings.Controls.Add(starFour);
                ratings.Controls.Add(starFive);
                ratings.Controls.Add(btnSkip);



                leftCol.Controls.Add(theMovie.buildThumbnailPanel()); //add thumbnails

                leftCol.Controls.Add(ratings);

                leftCol.Controls.Add(lblRatingsLeft);

                pnlContent.Controls.Add(leftCol);
                //pnlRate.Controls.Add(leftCol);
            }

        }

        void starFive_MouseClick(object sender, MouseEventArgs e)
        {
            fiveStar_MouseClick(sender, e);
            ratingsDone--;
            changetoRateMovieList();
        }

        void starFour_MouseClick(object sender, MouseEventArgs e)
        {
            fourStar_MouseClick(sender, e);
            ratingsDone--;
            changetoRateMovieList();

        }

        void starThree_MouseClick(object sender, MouseEventArgs e)
        {
            threeStar_MouseClick(sender, e);
            ratingsDone--;
            changetoRateMovieList();
        }

        void starTwo_MouseClick(object sender, MouseEventArgs e)
        {
            twoStar_MouseClick(sender, e);
            ratingsDone--;
            changetoRateMovieList();
        }

        void starOne_MouseClick(object sender, MouseEventArgs e)
        {
            oneStar_MouseClick(sender, e);
            ratingsDone--;
            changetoRateMovieList();

        }

        void btnSkip_MouseClick(object sender, MouseEventArgs e)
        {
            changetoRateMovieList();
        }

    }
    
        

}
