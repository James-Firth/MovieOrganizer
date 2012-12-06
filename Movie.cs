using System;
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
        static int thumbNailPadding = 25;

        public Movie(int MID,String title,String length,String director,String year,List<Actor> Actors, List<Genre> Genres)
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
                toReturn = toReturn + Actors[i].getName()+" ";
            }
            for(int j=0; j < Genres.Count; j++)
            {
                toReturn = toReturn + Genres[j].getName()+" ";
            }
            return toReturn;
        }


        public Panel buildThumbnailPanel()
        {
            Panel output = new Panel();


            output.Size = new System.Drawing.Size(thumbNailWidth, thumbNailHeight);

            output.BackColor = System.Drawing.Color.LimeGreen;

            output.Margin = new Padding(thumbNailPadding);


            PictureBox poster = new PictureBox();
            poster.BackColor = Color.AntiqueWhite;
            poster.Padding = new Padding(10);
            poster.Dock = DockStyle.Fill;
            poster.SizeMode = PictureBoxSizeMode.StretchImage;

            Image image = Image.FromFile(Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly().Location),"Graphics/image-not-found.gif"));
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

        void clickedThumbNail(object sender, EventArgs e)
        {
            Console.Out.WriteLine("test MID:" + MID + " Name:"+title);
            //TODO: make this change the page 
        }

        public static int getNailWidth()
        {
            return thumbNailWidth + 2*thumbNailPadding ;
        }

        internal static int getNailHeight()
        {
            return thumbNailHeight + 2 * thumbNailPadding;
        }
    }
}
