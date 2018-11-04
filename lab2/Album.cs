using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    internal class Album : Obj, IEnumerable<Track>, IDataErrorInfo, INotifyPropertyChanged
    {
        public string Genre { get; set; }
        public int Age { get; set; }
        public int Year { get; set; }
        public Library Library { get; set; }
        internal List<Track> TracksList { get; set; }
        [NonSerialized]
        private int copies;
        public int CopiesAmount { get { return copies; } set { copies = value; } }
        private string _poster;
        public string Poster { get { return _poster; } set { _poster = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Poster")); } }

        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerator<Track> GetEnumerator()
        {
            foreach (Track A in TracksList) yield return A;
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator();}

        public override void InfoToConsole()
        {
            if (Library != null)
            {
                Console.Write("Album: " + Name + "\n\nGenre: " + Genre +
                "\n\nYear: " + Year.ToString() + "\n\n1." + Library.Name + " - Library" + "\n\nTracks:");
                int i = 1;
                foreach (Track A in TracksList) Console.Write("\n" + ++i + ". " + A.Name);
                Console.WriteLine("\n" + ++i + ". Edit");
            }
            else
            {
                Console.Write("Album: " + Name + "\n\nGenre: " + Genre +
                "\n\nYear: " + Year.ToString() + "\n\nTracks:");
                int i = 0;
                foreach (Track A in TracksList) Console.Write("\n" + ++i + ". " + A.Name);
                Console.WriteLine("\n" + ++i + ". Edit");
            }
        }

        public override Obj NextObj(int index)
        {
            if (Library != null)
            {
                if (index == 1)
                {
                    return Library;
                }
                if (index > 0 && index < TracksList.Count + 2)
                {
                    return TracksList[index - 2];
                }
            }
            else
            {
                if (index > 0 && index < TracksList.Count + 1)
                {
                    return TracksList[index - 1];
                }
            }
            return null;
        }

        public Album(string name, string genre, int age, int year, Library library, List<Track> list, string poster="", int amount = 1):base(name)
        {
            Genre = genre;
            Age = age;
            Year = year;
            Library = library;
            TracksList = list;
            Library?.AddAlbum(this);
            foreach (Track A in list) A.AddAlbum(this);
            Poster = poster;
            CopiesAmount = amount;
        }

        public void NameToConsole() { Console.Write(Name); }

        public override void DeleteObj()
        {
            foreach (Track A in TracksList) A.RemoveAlbum(this);
            Library?.RemoveAlbum(this);
        }

        public void RemoveTrack(Track A)
        {
            TracksList.Remove(A);
        }

        public void RemoveLibrary()
        {
            Library = null;
        }

        public void UnassignLibrary()
        {
            Library?.RemoveAlbum(this);
            Library = null;
        }

        public void UnassignTrack(int index)
        {
            TracksList[index].RemoveAlbum(this);
            TracksList.RemoveAt(index);
        }

        public void AssignTrack(Track A)
        {
            if (!TracksList.Contains(A))
            {
                TracksList.Add(A);
                A.AlbumsList.Add(this);
            }
        }

        public override string this[string columnName]
        {
            get
            {
                string err = base[columnName];
                switch (columnName)
                {
                    case "Age":
                        if (Age < 0) err = "Age must be >= 0 ";
                        if (Age > 35) err = "Age should be < 100";
                        break;
                    case "Year":
                        if (Year < 1895) err = "Year must be > 1900";
                        if (Year >= 2040) err = "Year should be < 2040";
                        break;
                    case "Genre":
                        if (string.IsNullOrWhiteSpace(Genre)) err = "Genre must not be empty";
                        break;
                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name)) err = "Name must not be empty";
                        break;
                }
                return err;
            }
        }

        public new string Error
        {
            get
            {
                return "AlbumValidationFails";
            }
        }
    }
}
