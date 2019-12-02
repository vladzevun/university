using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Classes
{
    enum Janra
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

        //Very bad realization of janras and subjanran, but I hope it is enough...
        public static bool isSubJanra(Janra parent, Janra son)
        {
            if (parent == Janra.Rock)
                if (son == Janra.AltRock || son == Janra.RussRock)
                    return true;
            return false;
        }
    }
}
