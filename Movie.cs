using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        static int thumbNailHeight = 300;
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


            Label titleBox = new Label();
            titleBox.Text = this.title;
            titleBox.Dock = DockStyle.Bottom;

            output.Controls.Add(titleBox);

            return output;
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
