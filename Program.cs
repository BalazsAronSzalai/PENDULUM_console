using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PENDULUM_console
{
    class Program
    {
        public const string ALBUMS = "albums";
        public const string TRACKS = "tracks";
        private static string albumOrTrack;
        private static int trackId = 0;


        public struct album
        {
            public string id;
            public string artist;
            public string title;
            public DateTime relase;

        }

        public struct track
        {
            public int id;
            public string title;
            public DateTime length;
            public string album;
            public string url;
        }

        static void Main(string[] args)
        {
            List<album> albums = new List<album>();
            List<track> tracks = new List<track>();

            albumOrTrack = ALBUMS;

            FileStream fs = new FileStream("..\\..\\res\\pendulum.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.StartsWith("[" + ALBUMS + "]") || line.StartsWith("[" + TRACKS + "]"))
                {
                    if (line.StartsWith("[" + ALBUMS + "]"))
                    {
                        albumOrTrack = ALBUMS;
                    }
                    else
                    {
                        albumOrTrack = TRACKS;
                    }
                }
                else
                {
                    if (albumOrTrack.Equals(ALBUMS))
                    {
                        string[] albumRowElements;
                        album a;

                        albumRowElements = line.Split(';');
                        a.id = albumRowElements[0];
                        a.artist = albumRowElements[1];
                        a.title = albumRowElements[2];
                        a.relase = DateTime.Parse(albumRowElements[3]);
                        albums.Add(a);
                    }
                    if (albumOrTrack.Equals(TRACKS))
                    {
                        string[] trackRowElements;
                        track t;

                        trackRowElements = line.Split(';');
                        t.id = trackId;
                        t.title = trackRowElements[0];
                        t.length = DateTime.Parse(trackRowElements[1]);
                        t.album = trackRowElements[2];
                        t.url = trackRowElements[3];
                        tracks.Add(t);
                        trackId++;
                    }
                }
            }

            fs.Close();
            sr.Close();

            FileStream fs2 = new FileStream("adatbazis.sql", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs2);

            Console.WriteLine("A fajlba irashoz nyomjon meg egy gombot!");
            Console.ReadKey();
            sw.Write("create database music;\nuse music;\n\n" +
                "create table Albums(\n" +
                "id varchar(4) primary key,\n" +
                "artist varchar(255) not null,\n" +
                "title varchar(255) not null,\n" +
                "relase date);\n\n" +
                "create table Tracks(\n" +
                "id int primary key identity\n" +
                "title varchar(255) not null,\n" +
                "length time\nalbum varchar(4) foreign key(Albums id),\n" +
                "url varchar(30));");

            for (int i = 0; i < albums.Count; i++)
            {
                album a = albums[i];
                sw.WriteLine(a.id+"," +
                    a.artist+"," +
                    a.title+","+
                    a.relase);
            }

            for (int i = 0; i < tracks.Count; i++)
            {
                track tr = tracks[i];
                sw.WriteLine(tr.id + "," +
                    tr.title+"," +
                    tr.length+"," +
                    tr.album+","+
                    tr.url);
            }

            sw.Close();
            fs2.Close();

            //hibas SQL kimenet
            /*FileStream fs3 = new FileStream("..\\..\\res\\tankcsapda.txt", FileMode.Open);
            StreamReader sr2 = new StreamReader(fs3);
            while (!sr2.EndOfStream)
            {
                string line = sr2.ReadLine();
                if (line.StartsWith("[" + ALBUMS + "]") || line.StartsWith("[" + TRACKS + "]"))
                {
                    if (line.StartsWith("[" + ALBUMS + "]"))
                    {
                        albumOrTrack = ALBUMS;
                    }
                    else
                    {
                        albumOrTrack = TRACKS;
                    }
                }
                else
                {
                    if (albumOrTrack.Equals(ALBUMS))
                    {
                        string[] albumRowElements;
                        album a;

                        albumRowElements = line.Split(';');
                        a.id = albumRowElements[0];
                        a.artist = albumRowElements[1];
                        a.title = albumRowElements[2];
                        a.relase = DateTime.Parse(albumRowElements[3]);
                        albums.Add(a);
                    }
                    if (albumOrTrack.Equals(TRACKS))
                    {
                        string[] trackRowElements;
                        track t;

                        trackRowElements = line.Split(';');
                        t.id = trackId;
                        t.title = trackRowElements[0];
                        t.length = DateTime.Parse(trackRowElements[1]);
                        t.album = trackRowElements[2];
                        t.url = trackRowElements[3];
                        tracks.Add(t);
                        trackId++;
                    }
                }
            }

            fs3.Close();
            sr2.Close();

            FileStream fs4 = new FileStream("adatbazis_2.sql", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw2 = new StreamWriter(fs4);

            Console.WriteLine("A fajlba irashoz nyomjon meg egy gombot!");
            Console.ReadKey();
            sw2.Write("create database music;\nuse music;\n\n" +
                "create table Albums(\n" +
                "id varchar(4) primary key,\n" +
                "artist varchar(255) not null,\n" +
                "title varchar(255) not null,\n" +
                "relase date);\n\n" +
                "create table Tracks(\n" +
                "id int primary key identity\n" +
                "title varchar(255) not null,\n" +
                "length time\nalbum varchar(4) foreign key(Albums id),\n" +
                "url varchar(30));");

            for (int i = 0; i < albums.Count; i++)
            {
                album a = albums[i];
                sw2.WriteLine(a.id + "," +
                    a.artist + "," +
                    a.title + "," +
                    a.relase);
            }

            for (int i = 0; i < tracks.Count; i++)
            {
                track tr = tracks[i];
                sw2.WriteLine(tr.id + "," +
                    tr.title + "," +
                    tr.length + "," +
                    tr.album + "," +
                    tr.url);
            }

            sw2.Close();
            fs4.Close();*/
        }
    }
}
