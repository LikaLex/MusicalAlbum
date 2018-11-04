using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    internal class AudioRent : IEnumerable<Album>, IEnumerable<Track>, IEnumerable<Library>
    {
        internal List<Album> Albums;
        internal List<Track> Tracks;
        internal List<Library> Libraries;


        IEnumerator<Album> IEnumerable<Album>.GetEnumerator()
        {
            foreach (Album F in Albums) yield return F;
        }
        IEnumerator<Track> IEnumerable<Track>.GetEnumerator()
        {
            foreach (Track A in Tracks) yield return A;
        }
        IEnumerator<Library> IEnumerable<Library>.GetEnumerator()
        {
            foreach (Library D in Libraries) yield return D;
        }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }

        public AudioRent()
        {
            Albums = new List<Album>();
            Tracks = new List<Track>();
            Libraries = new List<Library>();
        }

        public void ObjToConsole<T>() where T : Obj
        {
            int i = 0;
            foreach (var F in (IEnumerable<T>)this)
            {
                Console.Write("\n" + ++i + ". " + F.Name);
            }
        }

        public Obj GetObjWithIndex<T>(int index) where T : Obj
        {
            if (index >= 0 && index < ((IEnumerable<T>)this).Count())
            {
                return ((IEnumerable<T>)this).ElementAt(index);
            }
            return null;
        }

        public void DelObjWithIndex<T>(int index) where T : Obj
        {
            Obj o = GetObjWithIndex<T>(index);
            if (typeof(T) == typeof(Album))
            {
                Albums.Remove(o as Album);
            }
            if(typeof(T) == typeof(Track))
            {
                Tracks.Remove(o as Track);
            }
            if(typeof(T) == typeof(Library))
            {
                Libraries.Remove(o as Library);
            }
            o?.DeleteObj();
        }

        public Obj GetObjWithName<T>(string name) where T : Obj
        {
            foreach (var O in (IEnumerable<T>)this)
            {
                if (O.Name == name) return O;
            }
            return null;
        }
        public static AudioRent Initialize() // предварительно созданная фонотека
        {
            AudioRent V = new AudioRent();

            V.Libraries.Add(new Library("iTunes"));
            List<Track> l2 = new List<Track>(3);
            V.Tracks.Add(new Track("Пока мы молоды", "2013", "his1"));
            V.Tracks.Add(new Track("Вечность", "2014", "ист2"));
            V.Tracks.Add(new Track("Beautiful Life", "2015", "his3"));
            l2.Add(V.Tracks[0]);
            l2.Add(V.Tracks[1]);
            l2.Add(V.Tracks[2]);
            V.Albums.Add(new Album("Пока мы молоды", "Лирика", 12, 2015, V.Libraries[0], l2, "Images/PMM.jpg"));

            V.Libraries.Add(new Library("VK Music"));
            List<Track> l3 = new List<Track>(4);
            V.Tracks.Add(new Track("Forever", "2018", "his4"));
            V.Tracks.Add(new Track("That's How You Write a song", "2018", "his5"));
            V.Tracks.Add(new Track("Mercy", "2018", "his6"));
            V.Tracks.Add(new Track("Stones", "2018", "his7"));
            l3.Add(V.Tracks[3]);
            l3.Add(V.Tracks[4]);
            l3.Add(V.Tracks[5]);
            l3.Add(V.Tracks[6]);
            V.Albums.Add(new Album("Eurovision 2018", "Pop", 6, 2018, V.Libraries[1], l3, "Images/Eurovision.jpg"));


            V.Libraries.Add(new Library("MediaFire"));
            List<Track> l4 = new List<Track>(4);
            V.Tracks.Add(new Track("Sing", "2015", "his9"));
            V.Tracks.Add(new Track("Let It Go", "2013", "his10"));
            V.Tracks.Add(new Track("Daft Punk", "2012", "his11"));
            V.Tracks.Add(new Track("Aha!", "2014", "his12"));
            l4.Add(V.Tracks[7]);
            l4.Add(V.Tracks[8]);
            l4.Add(V.Tracks[9]);
            l4.Add(V.Tracks[10]);

            V.Albums.Add(new Album("Pentatonix", "A-kapella", 12, 2016, V.Libraries[2], l4, "Images/PTX.png"));
            return V;
        }

        public void DeleteAlbum(Album F)
        {
            F.DeleteObj();
            Albums.Remove(F);
        }

        public void AssignLibrary(Album F, string LibName)
        {
            foreach(Library D in Libraries)
            {
                if (D.Name == LibName)
                {
                    D.AlbumsList.Add(F);
                    F.Library = D;
                    return;
                }
            }
            Library Di = new Library(LibName, F);
            F.Library = Di;
            Libraries.Add(Di);
        }

        public void AssignTrack(Album F, string LibName)
        {
            foreach (Track D in Tracks)
            {
                if (D.Name == LibName)
                {
                    F.AssignTrack(D);
                    return;
                }
            }
            Track Di = new Track(LibName, "unknown", "unknown", F);
            F.TracksList.Add(Di);
            Tracks.Add(Di);
        }

        public Album CreateEmptyAlbum()
        {
            Album F = new Album("Новый альбом", "Жанр", 0, 2017, null, new List<Track>(0));
            Albums.Add(F);
            return F;
        }
        public Track CreateEmptyTrack()
        {
            Track a = new Track("", "2018", "", new Album[0]);
            Tracks.Add(a);
            return a;
        }

        
    }
}
