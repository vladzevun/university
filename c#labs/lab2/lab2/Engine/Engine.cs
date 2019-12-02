using System;
using System.Collections.Generic;
using System.Text;
using lab2.Classes;

namespace lab2
{
    class Engine
    {
        public Engine(ILoger loger)
        {
            this.loger = loger;
            this.isEngineRunning = true;

            this.Catalogs = new List<Catalog>();
            this.Artists = new List<Artist>();
            this.Collections = new List<Collection>();

            this.unknownID = 1;

            this.loger.isTurnOn = false;

            this.initForDebug();
        }
        private ILoger loger { get; set; }

        private bool isEngineRunning;

        int unknownID;

        public List<Catalog> Catalogs { get; set; }

        public List<Artist> Artists { get; set; }

        public List<Collection> Collections { get; set; }

        private void initForDebug()
        {
            this.Catalogs.Add(new Catalog("old"));
            this.Catalogs.Add(new Catalog("new"));
            this.Artists.Add(new Artist("joji"));
            this.Artists.Add(new Artist("alla"));
            this.Artists.Add(new Artist("lev"));

            this.Artists[0].AtristJanra = Janra.Rock;
            this.Artists[1].AtristJanra = Janra.RussRock;
            this.Artists[2].AtristJanra = Janra.Classic;
            
            this.Catalogs[0].Alboms.Add(new Albom("sum"));
            this.Catalogs[0].Alboms.Add(new Albom("win"));
            this.Catalogs[0].Alboms.Add(new Albom("spr"));
            this.Catalogs[1].Alboms.Add(new Albom("top"));
            this.Catalogs[0].Alboms[0].Autrhor = this.Artists[0];
            this.Catalogs[0].Alboms[1].Autrhor = this.Artists[1];
            this.Catalogs[0].Alboms[2].Autrhor = this.Artists[2];
            this.Catalogs[1].Alboms[0].Autrhor = this.Artists[0];

            this.Catalogs[0].Alboms[0].songs.Add(new Song("s1"));
            this.Catalogs[0].Alboms[0].songs.Add(new Song("s2"));
            this.Catalogs[0].Alboms[0].songs.Add(new Song("s3"));
            this.Catalogs[0].Alboms[0].songs.Add(new Song("s4"));

            this.Catalogs[0].Alboms[0].songs[0].Year = "1999";
            this.Catalogs[0].Alboms[0].songs[1].Year = "1995";
            this.Catalogs[0].Alboms[0].songs[2].Year = "1993";
            this.Catalogs[0].Alboms[0].songs[3].Year = "2001";

            this.Catalogs[0].Alboms[0].songs[0].LinkToArtist = this.Catalogs[0].Alboms[0].Autrhor;
            this.Catalogs[0].Alboms[0].songs[1].LinkToArtist = this.Catalogs[0].Alboms[0].Autrhor;
            this.Catalogs[0].Alboms[0].songs[2].LinkToArtist = this.Catalogs[0].Alboms[0].Autrhor;
            this.Catalogs[0].Alboms[0].songs[3].LinkToArtist = this.Catalogs[0].Alboms[0].Autrhor;

            this.Collections.Add(new Collection("mysongs"));
            this.Collections.Add(new Collection("favorite"));

            this.Collections[0].songs.Add(this.Catalogs[0].Alboms[0].songs[0]);
            this.Collections[0].songs.Add(this.Catalogs[0].Alboms[0].songs[2]);
            this.Collections[0].songs.Add(this.Catalogs[0].Alboms[0].songs[3]);

            this.Collections[1].songs.Add(this.Catalogs[0].Alboms[0].songs[1]);

        }

        public void run()
        {
            while (this.isEngineRunning)
            {
                Console.Clear();
                this.printOptions();
                this.dispCurrentInfo();
                this.readCommand();
            }
            this.loger.log("Aborting...\nPress any key to continue...");
            Console.ReadKey();
        }

        private void dispCurrentInfo()
        {
            Console.Write("Artist list: ");
            if (this.Artists.Count == 0)
            {
                Console.Write("\nNo artists found, but you can add some.");
            }
            foreach (var artist in this.Artists)
            {
                Console.Write($"{artist.Name}({artist.AtristJanra}), ");
            }
            Console.Write("\n\nCollection list:\n");
            if (this.Collections.Count == 0)
            {
                Console.WriteLine("No collections found, but you can add some.");
            }
            foreach (var collection in this.Collections)
            {
                Console.Write($"{collection.Name}:\n");
                foreach (var song in collection.songs)
                {
                    Console.WriteLine($"\t\t{song.Name} by {song.LinkToArtist.Name} year({song.Year})");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\nCatalogs:");
            if (this.Catalogs.Count == 0)
            {
                Console.WriteLine("No catalogs found, but you can create them.");
                return;
            }
            foreach (var catalog in this.Catalogs)
            {
                this.printCatalog(catalog);
                //Console.WriteLine("");
            }
        }

        private void printCatalog(Catalog catalog)
        {
            Console.Write($"{catalog.Name} has {catalog.Alboms.Count} Albom(s):\n");
            if (catalog.Alboms.Count > 0)
            {
                foreach (var item in catalog.Alboms)
                {
                    Console.Write($"\t{item.Name} by {item.Autrhor.Name} janra {item.Autrhor.AtristJanra}. Songs:\n");
                    if (item.songs.Count > 0)
                    {
                        foreach (var song in item.songs)
                        {
                            Console.Write($"\t\t{song.Name} year({song.Year}) janra {item.Autrhor.AtristJanra}\n");
                        }
                    }
                }

            }
        }

        public void printOptions()
        {
            Console.WriteLine("Options are:\n" +
                "1. Add New Catalog\n" +
                "2. Add New Artist\n" +
                "3. Add New Albom To Catalog\n" +
                "4. Add New Song to Albom\n" +
                "5. Add New Collection\n" +
                "6. Add Song To Collection\n" +
                "7. Search\n" +
                "9. Exit\n");
        }

        public void readCommand()
        {

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    this.loger.log("AddNewCatalog();");
                    Console.Clear();
                    this.AddNewCatalog();
                    break;
                case '2':
                    this.loger.log("AddNewArtistToCatalog();");
                    Console.Clear();
                    this.AddNewArtist();
                    break;
                case '3':
                    this.loger.log("AddNewAlbomToCatalog();");
                    Console.Clear();
                    this.AddNewAlbomToCatalog();
                    break;
                case '4':
                    this.loger.log("AddNewCollection();");
                    Console.Clear();
                    this.AddNewSongToAlbom();
                    break;
                case '5':
                    this.loger.log("AddNewSongToCollection();");
                    Console.Clear();
                    this.AddNewCollection();
                    break;
                case '6':
                    this.loger.log("AddNewSongToCollection();");
                    Console.Clear();
                    this.AddNewSongToCollection();
                    break;
                case '7':
                    this.loger.log("Search();");
                    Console.Clear();
                    this.Search();
                    break;
                case '9':
                    this.loger.log("Engine turn off;");
                    this.isEngineRunning = false;
                    break;


                default:
                    this.loger.log("Default state;");
                    break;
            }
            Console.WriteLine("\nPress any key to back to menu...");
            Console.ReadKey();
            //Console.WriteLine("\nPress any key to try again...");
            //Console.ReadKey();
        }

        private void Search()
        {
            Console.WriteLine("" +
                "1. Search for song Janra\n" +
                "2. Search for Artist\n" +
                "3. Search for Albom\n" +
                "4. Search for Collection\n" +
                "5. Search for song Year");
            char command = Console.ReadKey().KeyChar;
            Console.WriteLine("");
            switch (command)
            {
                case '1':
                    searchJanra();
                    break;
                case '2':
                    searchArtist();
                    break;
                case '3':
                    Console.Write("Alboms: ");
                    foreach (var catalog in this.Catalogs)
                    {
                        foreach (var alb in catalog.Alboms)
                        {
                            Console.Write($"{alb.Name}, ");
                        }
                    }
                    Console.Write($"\nEnter albom name: ");
                    string albomName = Console.ReadLine();
                    Albom albom = null;
                    foreach (var catalog in this.Catalogs)
                    {
                        albom = this.GetAlbom(albomName, catalog.Alboms);
                        if (albom != null)
                            break;
                    }
                    if (albom == null)
                    {
                        Console.WriteLine($"Albom with name {albomName} was not found.\n");
                        return;
                    }
                    Console.WriteLine($"{albom.Name}:");
                    foreach (var song in albom.songs)
                    {
                        Console.Write($"{song.Name} by {song.LinkToArtist.Name} year{song.Year}\n");
                    }
                    break;
                case '4':
                    Console.Write($"Collections: ");
                    foreach (var collectionT in this.Collections)
                    {
                        Console.Write($"{collectionT.Name}, ");
                    }
                    Console.Write("\nEnter collection: ");
                    string colName = Console.ReadLine();
                    var collection = this.GetCollection(colName);
                    if (collection == null)
                    {
                        Console.WriteLine($"There is no collection with name {colName}");
                        return;
                    }
                    foreach (var song in collection.songs)
                    {
                        Console.WriteLine($"\t\t{song.Name} by {song.LinkToArtist.Name} year({song.Year}) janra({song.LinkToArtist.AtristJanra})");
                    }
                    break;
                case '5':
                    Console.Write("Enter year: ");
                    string year = Console.ReadLine();
                    foreach (var catalog in this.Catalogs)
                    {
                        foreach (var albm in catalog.Alboms)
                        {
                            foreach (var song in albm.songs)     
                            {
                                if (song.Year == year)
                                    Console.WriteLine($"\t\t{song.Name} by {song.LinkToArtist.Name} year({song.Year}) janra({song.LinkToArtist.AtristJanra})");
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void searchArtist()
        {
            Console.Write("Artists:");
            if (this.Artists.Count == 0)
            {
                Console.WriteLine("There are no artist. First add some.\nPress any key to back to menu...");
                Console.ReadKey();
                return;
            }
            foreach (var artst in this.Artists)
            {
                Console.Write($"{artst.Name}, ");
            }
            Console.Write($"\nEnter Artist name: ");
            string artistName = Console.ReadLine();
            var artist = GetArtist(artistName);
            if (artist == null)
            {
                Console.WriteLine($"There is no artist with name {artistName}");
                return;
            }
            foreach (var catalog in this.Catalogs)
            {
                foreach (var albom in catalog.Alboms)
                {
                    if (albom.Autrhor == artist)
                    {
                        foreach (var song in albom.songs)
                        {
                            Console.WriteLine($"{song.Name} by {song.LinkToArtist.Name} year({song.Year}) from albom({albom.Name})");
                        }
                    }
                }
            }
        }

        private void searchJanra()
        {
            Console.Write("Choose janra: ");

            Janra janra = Janra.Other;
            this.printJanras();
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    janra = Janra.Pop;
                    break;
                case '2':
                    janra = Janra.Rock;
                    break;
                case '3':
                    janra = Janra.AltRock;
                    break;
                case '4':
                    janra = Janra.RussRock;
                    break;
                case '5':
                    janra = Janra.Classic;
                    break;
                case '6':
                    janra = Janra.DubStep;
                    break;
                case '7':
                    janra = Janra.Other;
                    break;
                default:
                    break;
            }
            Console.WriteLine("");
            foreach (var catalog in this.Catalogs)
            {
                foreach (var albom in catalog.Alboms)
                {
                    if (albom.Autrhor.AtristJanra == janra || Song.isSubJanra(janra,albom.Autrhor.AtristJanra))
                        foreach (var song in albom.songs)
                        {
                            Console.WriteLine($"{song.Name} by {song.LinkToArtist.Name} year({song.Year}) from albom({albom.Name}) janra({albom.Autrhor.AtristJanra})");
                        }
                }
            }
        }
    

        private void AddNewSongToCollection()
        {
            Console.Write("Collections: ");
            foreach (var item in this.Collections)
            {
                Console.Write($"{item.Name}, ");
            }
            Console.Write("\nEnter name of Collection in which you what to add song: ");
            string collectionName = Console.ReadLine();
            Collection collection = this.GetCollection(collectionName);
            if (collection == null)
            {
                Console.WriteLine($"Can not find Collection with name {collectionName}");
                return;
            }
            Console.Write("Alboms: ");
            foreach (var catalog1 in this.Catalogs)
            {
                foreach (var albom1 in catalog1.Alboms)
                {
                    Console.Write($"{albom1.Name}, ");
                }
            }
            Console.Write("\nEnter name of Albom which contains song you want to add: ");
            string albomName = Console.ReadLine();
            Albom albom = null;
            foreach (var catalog in this.Catalogs)
            {
                albom = this.GetAlbom(albomName, catalog.Alboms);
                if (albom != null)
                    break;
            }
            if (albom == null)
            {
                Console.WriteLine($"Can not find Albom with name {albomName}");
                return;
            }
            Console.Write($"Songs in {albom.Name}: ");
            foreach (var ssong in albom.songs)
            {
                Console.Write($"{ssong.Name}, ");
            }
            Console.WriteLine("\nEnter name of new song you want to add: ");
            string songName = Console.ReadLine();
            Song song = null;
            foreach (var tsong in albom.songs)
            {
                if (tsong.Name == songName)
                    song = tsong;
            }
            if (song == null)
            {
                Console.WriteLine($"Song {songName} was not found.");
                return;
            }
            collection.songs.Add(song);
        }

        private void AddNewSongToAlbom()
        {
            Console.Write("Catalogs: ");
            foreach (var item in this.Catalogs)
            {
                Console.Write($"{item.Name}, ");
            }
            Console.Write("\nEnter name of Catalog in which you what to add new Song: ");
            string catalogName = Console.ReadLine();
            Catalog catalog = this.GetCatalog(catalogName);
            if (catalog == null)
            {
                Console.WriteLine($"Can not find Catalog with name {catalogName}");
                return;
            }
            Console.Write($"Alboms in {catalog.Name}: ");
            foreach (var item in catalog.Alboms)
            {
                Console.Write($"{item.Name} by {item.Autrhor.Name}, ");
            }
            Console.Write("\nEnter name of Albom: ");
            string albomName = Console.ReadLine();
            Albom albom = this.GetAlbom(albomName, catalog.Alboms);
            if (albom == null)
            {
                Console.WriteLine($"Can not find Albom with name {albomName}");
                return;
            }
            Console.Write("Enter New Song name: ");
            string songName = Console.ReadLine();
            if (songName == String.Empty)
                songName = ("(unknown_song" +
                    "" + this.unknownID++.ToString() + ")");
            var newSong = new Song(songName);
            Console.Write("Enter Song year: ");
            string songYear = Console.ReadLine();
            if (songYear == String.Empty)
                songYear = ("(empty)");
            newSong.LinkToArtist = albom.Autrhor;
            newSong.Year = songYear;
            albom.songs.Add(newSong);
        }

        private void AddNewCollection()
        {
            Console.Write("Enter name of collection you want to add: ");
            string collectionName = Console.ReadLine();
            if (collectionName == String.Empty)
                collectionName = ("(unknown_collection" +
                    "" + this.unknownID++.ToString() + ")");
            var newColletion = new Collection(collectionName);
            this.Collections.Add(newColletion);
        }

        private void AddNewAlbomToCatalog()
        {
            Console.Write("Catalogs:");
            foreach (var cata in this.Catalogs)
            {
                Console.Write($" {cata.Name},");
            }
            Console.Write("\nEnter name of Catalog in which you what to add new Almob: ");
            string catalogName = Console.ReadLine();
            Catalog catalog = this.GetCatalog(catalogName);
            if (catalog == null)
            {
                Console.WriteLine($"Can not find Catalog with name {catalogName}");
                return;
            }
            Console.Write("Artist: ");
            foreach (var artst in this.Artists)
            {
                Console.Write($"{artst.Name}, ");
            }
            Console.Write("\nEnter name of Actist: ");
            string artistName = Console.ReadLine();
            Artist artist = this.GetArtist(artistName);
            if (artist == null)
            {
                Console.WriteLine($"Can not find Artist with name {artistName}");
                return;
            }
            Console.Write("Enter New Albom name: ");
            string albomName = Console.ReadLine();
            if (albomName == String.Empty)
                albomName = ("(unknown_albom" + this.unknownID++.ToString() + ")");
            var newAlbom = new Albom(albomName);
            newAlbom.Autrhor = artist;
            catalog.Alboms.Add(newAlbom);
        }

        private void AddNewArtist()
        {
            Console.Write("Enter Artist name: ");
            string artistName = Console.ReadLine();
            if (artistName == String.Empty)
                artistName = ("(unknown_artst" + this.unknownID++.ToString() + ")");
            var newArtist = new Artist(artistName);
            this.Artists.Add(newArtist);

            Console.Write("Choose Artist janra: ");
            this.printJanras();
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    newArtist.AtristJanra = Janra.Pop;
                    break;
                case '2':
                    newArtist.AtristJanra = Janra.Rock;
                    break;
                case '3':
                    newArtist.AtristJanra = Janra.AltRock;
                    break;
                case '4':
                    newArtist.AtristJanra = Janra.RussRock;
                    break;
                case '5':
                    newArtist.AtristJanra = Janra.Classic;
                    break;
                case '6':
                    newArtist.AtristJanra = Janra.DubStep;
                    break;
                case '7':
                    newArtist.AtristJanra = Janra.Other;
                    break;
                default:
                    newArtist.AtristJanra = Janra.Other;
                    break;
            }
        }

        private Collection GetCollection(string collectionName)
        {
            Collection collection = null;
            foreach (var item in this.Collections)
                if (item.Name == collectionName)
                    collection = item;
            return collection;
        }

        private Catalog GetCatalog(string catalogName)
        {
            Catalog catalog = null;
            foreach (var item in this.Catalogs)
                if (item.Name == catalogName)
                    catalog = item;
            return catalog;
        }
        
        private Artist GetArtist(string artistName)
        {
            Artist catalog = null;
            foreach (var item in this.Artists)
                if (item.Name == artistName)
                    catalog = item;
            return catalog;
        }

        private Albom GetAlbom(string albomName, List<Albom> alboms)
        {
            if (alboms.Count == 0)
                return null;
            Albom albom = null;
            foreach (var item in alboms)
                if (item.Name == albomName)
                    albom = item;
            return albom;
        }
        private void AddNewCatalog()
        {
            Console.Write("Enter Catalog name: ");
            string catalogName = Console.ReadLine();
            if (catalogName == String.Empty)
                catalogName = ("(unknown_catlg" + this.unknownID++.ToString() + ")");
            this.Catalogs.Add(new Catalog(catalogName));
        }

        private void printJanras()
        {
            Console.WriteLine("\n" +
                "\t1. Pop \n" +
                "\t2. Rock \n" +
                "\t3. AltRock \n" +
                "\t4. RussRock\n" +
                "\t5. Classic \n" +
                "\t6. DubStep \n" +
                "\t7. Other\n");
        }
    }
}