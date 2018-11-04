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
    class Library : Obj, IEnumerable<Album>, IDataErrorInfo
    {
        public List<Album> AlbumsList { get; private set; }


        public void NameToConsole() { Console.WriteLine(Name);}

        public void AddAlbum(Album item)
        {
            if (AlbumsList != null)
            {
                foreach (Album F in AlbumsList)
                {
                    if (F.Equals(item)) return;
                }
                AlbumsList.Add(item);
            }
            else
            {
                AlbumsList = new List<Album>(1) { item };
            }
        }

        public override void InfoToConsole()
        {
            Console.Write("Name: " + Name + "\n\nAlbums:");
            int i = 0;
            foreach (Album F in AlbumsList) Console.Write("\n"+ ++i + ". " + F.Name);
            Console.WriteLine("\n" + ++i + ". Edit");
        }

        public override Obj NextObj(int index)
        {
            if (index > 0 && index < AlbumsList.Count + 1)
            {
                return AlbumsList[index - 1];
            }
            if(index == AlbumsList.Count + 1)
            {
                this.Edit();
            }
            return null;
        }

        protected virtual void Edit()
        {
            Console.Clear();
            string s;
            Console.WriteLine("Type new Library's name: ");
            s = Console.ReadLine();
            if (s.Length > 0) Name = s;
        }

        public IEnumerator<Album> GetEnumerator() { return AlbumsList.GetEnumerator();}
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        protected Library(string name, Album[] albums):base(name)
        {
            AlbumsList = albums.ToList();
        }

        public Library(string name, Album album):base(name)
        {
            AlbumsList = new List<Album>(1) { album };
        }

        public Library(string name) : base(name) { }

        public void RemoveAlbum(Album F)
        {
            AlbumsList.Remove(F);
        }

        public override void DeleteObj()
        {
            foreach (Album F in AlbumsList) F.RemoveLibrary();
        }

        public new string Error
        {
            get
            {
                return "LibraryValidationFails";
            }
        }
    }
}
