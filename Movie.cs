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
    }
}
