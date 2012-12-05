using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
