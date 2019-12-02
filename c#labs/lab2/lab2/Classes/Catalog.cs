using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Classes
{
    class Catalog : INameble
    {
        public Catalog(string Name)
        {
            this.Name = Name;

            this.Alboms = new List<Albom>();
        }
        public List<Albom> Alboms { get; set; }
        public string Name { get ; set; }
    }
}
