using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Classes
{
    class Collection : INameble
    {
        public Collection(string Name)
        {
            this.Name = Name;

            this.songs = new List<Song>();
        }
        public string Name { get; set; }

        public List<Song> songs { get; set; }
    }
}
