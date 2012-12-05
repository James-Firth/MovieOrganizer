using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieOrganizer
{
    class WishList
    {
        List<Movie> movies;

        WishList(List<Movie> Movies)
        {
            this.movies = Movies;
        }



        // Builds a WishList for the given UID. 
        // The Mysql request sent and recived from here.
        static WishList buildWishList(int UID)
        {
            List<Movie> movies = new List<Movie>();

            //TODO: Build list of Movies so wishlist can be filled.


            WishList outPut = new WishList(movies);
            return outPut;
        }

        //================================Gets and Setters=========================

        //When a user would like to add a movie to their watchlist.
        Boolean AddToWishList(Movie newMovie)
        {

            //TODO: Add new Movie to DB.

            movies.Insert(0,newMovie);

            //TODO: Check is successful.
            return false;
        }

        //=================================BUILDING=================================

        //Display Type One
        public Panel buildWishList(int UID)
        {
            Panel outPanel = null;

            //TODO: Design Layout of WishList Panel
            //NOTE: Don't forget to display a message if the WishList is empty.


            return outPanel;
        }

        

        
    }
}
