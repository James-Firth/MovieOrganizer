using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieOrganizer
{
    class Genre
    {
        int GID;
        String name;

        public Genre(int GID, String name)
        {
            this.GID = GID;
            this.name = name;

        }

        public String getName()
        {
            return name;
        }
    }
}
