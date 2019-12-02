using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Classes
{
    class Albom : INameble
    {
        public Albom(string Name)
        {
            this.Name = Name;

            this.songs = new List<Song>();
        }
        public List<Song> songs { get; set; }

        public string Name { get; set; }

        public Artist Autrhor { get; set; }
    }
}
