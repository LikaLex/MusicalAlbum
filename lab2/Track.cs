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
    internal class Track : Library, IDataErrorInfo
    {
        public string ReleaseDate { get; set; }
        public string Description { get; set; }


        public override void InfoToConsole()
        {
            Console.Write("Name: " + Name + "\n\nDate of release: " + ReleaseDate + "\n\nDescription: " + Description
                + "\n\nAlbums:");
            int i = 0;
            foreach (Album F in AlbumsList) Console.Write("\n" + ++i + ". " + F.Name);
            Console.WriteLine("\n" + ++i + ". Edit");
        }

        protected override void Edit()
        {
            Console.Clear();
            string s;
            Console.WriteLine("Type new Track's name: ");
            s = Console.ReadLine();
            if (s.Length > 0) Name = s;
            Console.WriteLine("Type new Track's ReleaseDate: ");
            s = Console.ReadLine();
            if (s.Length > 0) ReleaseDate = s;
            Console.WriteLine("Type new Track's Description: ");
            s = Console.ReadLine();
            if (s.Length > 0) Description = s;
        }

        public Track(string name, string releasedate, string description, Album[] Albumslist):base(name, Albumslist)
        {
            ReleaseDate = releasedate;
            Description = description;
        }

        public Track(string name, string releasedate, string description, Album Album) : base(name, Album)
        {
            ReleaseDate = releasedate;
            Description = description;
        }

        public Track(string name, string releasedate, string description) : base(name)
        {
            ReleaseDate = releasedate;
            Description = description;
        }

        public override void DeleteObj()
        {
            foreach(Album F in AlbumsList) { F.RemoveTrack(this); }
        }

        public new string Error
        {
            get
            {
                return "TrackValidationFails";
            }
        }

        public override string this[string columnName]
        {
            get
            {
                string err = base[columnName];
                switch (columnName)
                {

                    case "Description":
                        if (string.IsNullOrWhiteSpace(Description)) err = "Description must not be empty";
                        break;
                    case "ReleaseDate":
                        if (string.IsNullOrWhiteSpace(ReleaseDate)) err = "ReleaseDate must not be empty";
                        if (ReleaseDate.Length < 0) err = "Impossible realise date";

                        if (Convert.ToInt32(ReleaseDate) > 2020) err = "Too large ReleaseDate";
                        if (Convert.ToInt32(ReleaseDate) < 1830) err = "Too short ReleaseDate";
                        if (ReleaseDate.Length > 4) err = "Too large ReleaseDate";
                        break;
                }
                return err;
            }
        }
    }
}
