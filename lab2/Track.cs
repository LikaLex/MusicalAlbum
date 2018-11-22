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
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        protected override void Edit()
        {
            Console.Clear();
            string s;
            Console.WriteLine("Type new Track's name: ");
            s = Console.ReadLine();
            if (s.Length > 0) Name = s;
            Console.WriteLine("Type new Track's ReleaseDate: ");
            int year = int.Parse(Console.ReadLine());
            int month = int.Parse(Console.ReadLine());
            int day = int.Parse(Console.ReadLine());
            DateTime RealiseDate = new DateTime(year, month, day);
            Console.WriteLine("Type new Track's Description: ");
            s = Console.ReadLine();
            if (s.Length > 0) Description = s;
        }

        public Track(string name, DateTime releasedate, string description, Album[] Albumslist):base(name, Albumslist)
        {
            ReleaseDate = releasedate;
            Description = description;
        }

        public Track(string name, DateTime releasedate, string description, Album Album) : base(name, Album)
        {
            ReleaseDate = releasedate;
            Description = description;
        }

        public Track(string name, DateTime releasedate, string description) : base(name)
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
                }
                return err;
            }
        }
    }
}
