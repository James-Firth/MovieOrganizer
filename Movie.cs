﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace MovieOrganizer
{
    class Movie
    {
        //Data
        int MID;
        String title;
        String length;
        String director;
        String year;
        List<Actor> Actors;
        List<Genre> Genres;

        //Static Data For Building
        static int thumbNailWidth = 150;
        static int thumbNailHeight = 200;

        static int thumbNailWatchWidth = 150;
        static int thumbNailWatchHeight = 250;
        static string addTo = "Add to Watchlist";
        static string removeFrom = "Remove from Watchlist";

        static int thumbNailPadding = 25;

        public Movie(int MID, String title, String length, String director, String year, List<Actor> Actors, List<Genre> Genres)
        {
            this.MID = MID;
            this.title = title;
            this.length = length;
            this.director = director;
            this.year = year;
            this.Actors = Actors;
            this.Genres = Genres;
        }

        public String ToString()
        {
            String toReturn = "";
            toReturn = MID.ToString() + " " + title + " " + length + " " + director + " " + year + " ";
            for (int i = 0; i < Actors.Count; i++)
            {
                toReturn = toReturn + Actors[i].getName() + " ";
            }
            for (int j = 0; j < Genres.Count; j++)
            {
                toReturn = toReturn + Genres[j].getName() + " ";
            }
            return toReturn;
        }


        public Panel buildThumbnailPanel()
        {
            Panel output = new Panel();


            output.Size = new System.Drawing.Size(thumbNailWidth, thumbNailHeight);

            output.BackColor = System.Drawing.Color.AntiqueWhite;
            output.Margin = new Padding(thumbNailPadding);


            PictureBox poster = new PictureBox();
            poster.Padding = new Padding(10);
            poster.Dock = DockStyle.Fill;
            poster.SizeMode = PictureBoxSizeMode.StretchImage;

            Image image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/image-not-found.gif"));
            poster.Image = image;


            Label titleBox = new Label();
            titleBox.Text = this.title;
            titleBox.Dock = DockStyle.Bottom;
            titleBox.TextAlign = ContentAlignment.MiddleCenter;

            output.Controls.Add(poster);
            output.Controls.Add(titleBox);


            poster.Click += new EventHandler(clickedThumbNail);
            output.Click += new EventHandler(clickedThumbNail);
            titleBox.Click += new EventHandler(clickedThumbNail);
            return output;
        }

        public Panel bildThumbnailWatchPanel()
        {
            Panel output = new Panel();

            output.Size = new System.Drawing.Size(thumbNailWatchWidth, thumbNailWatchHeight);

            output.BackColor = System.Drawing.Color.AntiqueWhite;

            output.Margin = new Padding(thumbNailPadding);


            PictureBox poster = new PictureBox();
            //poster.BackColor = Color.AntiqueWhite;
            poster.Padding = new Padding(10);
            poster.Dock = DockStyle.Fill;
            poster.SizeMode = PictureBoxSizeMode.StretchImage;

            Image image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Graphics/image-not-found.gif"));
            poster.Image = image;


            Label titleBox = new Label();
            titleBox.Text = this.title;
            titleBox.Dock = DockStyle.Bottom;
            titleBox.TextAlign = ContentAlignment.MiddleCenter;
            

            Button BTNWatchList = new Button();
            BTNWatchList.Dock = DockStyle.Bottom;
            BTNWatchList.AutoSize = true;
            BTNWatchList.FlatStyle = FlatStyle.Flat;
            BTNWatchList.BackColor = Color.LightSkyBlue;
            BTNWatchList.TextAlign = ContentAlignment.MiddleCenter;
            BTNWatchList.MouseClick += new MouseEventHandler(recItemAdd_MouseClick);

            DBConnect checker = new DBConnect();
            if (!checker.inWatchList(MID, HomeForm.UID))
                BTNWatchList.Text = addTo;
            else
                BTNWatchList.Text = removeFrom;

            output.Controls.Add(poster);
            output.Controls.Add(titleBox);
            output.Controls.Add(BTNWatchList);

            poster.Click += new EventHandler(clickedThumbNail);
            output.Click += new EventHandler(clickedThumbNail);
            titleBox.Click += new EventHandler(clickedThumbNail);
            return output;
        }


        public void recItemAdd_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            DBConnect checker = new DBConnect();
            if (btn.Text.Equals( addTo))
            {
                DBConnect adder = new DBConnect();
                adder.addToWatchList(this.MID, HomeForm.UID);
                btn.Text = removeFrom;

            }
            else
            {
                DBConnect remover = new DBConnect();
                remover.removeFromWatchList(this.MID, HomeForm.UID);//Remove from watchlist
                btn.Text = addTo;

            }
        }
        void clickedThumbNail(object sender, EventArgs e)
        {
            //Console.Out.WriteLine("test MID:" + MID + " Name:" + title);
            //TODO: make this change the page 
            HomeForm.self.changeToMoviePage(this);
        }

        public static int getNailWidth()
        {
            return thumbNailWidth + 2 * thumbNailPadding;
        }

        internal static int getNailHeight()
        {
            return thumbNailHeight + 2 * thumbNailPadding;
        }

        //GETTERS
        public String getTitle()
        {
            return title;
        }

        public List<Actor> getActors()
        {
            return Actors;
        }

        public String getInfo()
        {
            String info = "Runtime: " + length + System.Environment.NewLine + System.Environment.NewLine + "Director: " + director + " " + System.Environment.NewLine + System.Environment.NewLine + "Year: " + year + System.Environment.NewLine + System.Environment.NewLine + "Genre: ";
            for (int i = 0; i < Genres.Count; i++)
            {
                if (i == (Genres.Count - 1))
                {
                    info += Genres[i].getName();
                }
                else
                {
                    info += Genres[i].getName() + ", ";
                }
            }

            return info;
        }

        public String getLength()
        {
            return length;
        }

        public String getDirector()
        {
            return director;
        }

        public String getYear()
        {
            return year;
        }

        public List<Genre> getGenres()
        {
            return Genres;
        }

        public int getMID()
        {
            return MID;
        }

        public static int getNailWatchWidth()
        {
            return thumbNailWatchWidth + 2 * thumbNailPadding;
        }

        public static int getNailWatchHeight()
        {
            return thumbNailWatchHeight + 2 * thumbNailPadding;
        }
    
    }
}
