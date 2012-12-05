using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieOrganizer
{
    class Actor
    {
        int AID;
        String name;

        public Actor(int AID, String name)
        {
            this.AID = AID;
            this.name = name;

        }

        public String getName()
        {
            return name;
        }
    }
}
