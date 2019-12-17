using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Classes
{
    enum Genre
    {
        Pop, Rock, AltRock, RussRock, Classic, DubStep, Other
    }

    class Song : INameble
    {
        public Song(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }

        public string Year { get; set; }

        public Artist LinkToArtist { get; set; }

        //Very bad realization of genres and subgenren, but I hope it is enough...
        public static bool isSubGenre(Genre parent, Genre son)
        {
            if (parent == Genre.Rock)
                if (son == Genre.AltRock || son == Genre.RussRock)
                    return true;
            return false;
        }
    }
}
