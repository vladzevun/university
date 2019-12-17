using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Classes
{
    class Artist : INameble
    {
        public Artist(string Name)
        {
            this.Name = Name;

            this.alboms = new List<Albom>();
        }
        public List<Albom> alboms{ get; set; }
        public string Name { get; set; }

        public Genre AtristGenre { get; set; }

    }
}
