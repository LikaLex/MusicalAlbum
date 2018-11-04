using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO.Compression;
using System.Reflection;

namespace lab2
{
    public partial class MainWindow : Window
    {
        internal static AudioRent rent;
        private FileSystemWatcher watcher = null;
        private string FileName = null;
        private bool ReopenDialogShown = false;
        Dictionary<Type, List<Type>> Plugins;

        private void FindPlugins()//метод для плагинов
        {
            var col = Directory.EnumerateFiles(System.IO.Path.Combine(Environment.CurrentDirectory, "plugins"), "*.dll");
            Dictionary<Type, List<Type>> plugs = new Dictionary<Type, List<Type>>();
            foreach (string path in col)
            {
                var asembly = Assembly.LoadFile(path);
                var types = asembly.GetTypes();
                foreach(var t in types)
                {
                    var intersection = t.GetInterfaces().Intersect(PluginInterfaces.Inters.types);
                    if (intersection.Count() > 0)
                    {
                        plugs.Add(t, intersection.ToList());
                    }
                }
            }
            LV.ItemsSource = plugs.Select(x=>x.Key);
            Plugins = plugs;
        } 

        public MainWindow()
        {
            InitializeComponent();
            rent = AudioRent.Initialize();
            FindPlugins();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dial = new OpenFileDialog()
            {
                AddExtension = true,//можно не указывать расширение
                CheckFileExists = true,//нельзя указывать несуществующие файлы
                InitialDirectory = Environment.CurrentDirectory,//откуда начинать
                Filter = "AudioRent files(.rent)|*.rent"
            };
            var t = dial.ShowDialog();
            if (t.GetValueOrDefault(false))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var stream = File.Open(dial.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (DeflateStream deflstr = new DeflateStream(stream, CompressionMode.Decompress))//разжимаем
                    {
                        try
                        {
                            rent = (AudioRent)formatter.Deserialize(deflstr);
                        }
                        catch
                        {
                            MessageBox.Show("Файл поврежден!!!");
                            return;
                        }
                    }
                }
                watcher = new FileSystemWatcher(System.IO.Path.GetDirectoryName(dial.FileName), "*.rent");
                FileName = System.IO.Path.GetFileName(dial.FileName);
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += new FileSystemEventHandler(OnWrote);
                watcher.EnableRaisingEvents = true;
            }
        }

        public delegate bool AskDel(string asd, string aasf);
        public delegate void VVDel();
        public event VVDel Reopening;

        private void OnWroteSTAThread(object s, FileSystemEventArgs a)
        {
            if (YNDialogW.Ask("Открыть файл заново", "Файл был изменен. Открыть новый файл??"))
            {
                Reopening?.Invoke();
                BinaryFormatter formatter = new BinaryFormatter();
                /*     var stream = File.Open(System.IO.Path.Combine(watcher.Path, FileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                     DeflateStream deflstr = new DeflateStream(stream, CompressionMode.Decompress);
                     rent = (AudioRent)formatter.Deserialize(deflstr);
                     deflstr.Dispose();
                     stream.Dispose();*/
                using (var stream = File.Open(System.IO.Path.Combine(watcher.Path, FileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (DeflateStream deflstr = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        try
                        {
                            rent = (AudioRent)formatter.Deserialize(deflstr);
                        }
                        catch
                        {
                            MessageBox.Show("Файл поврежден!!!");
                            return;
                        }
                    }
                }
            }
        }

        internal void OnWrote(object s, FileSystemEventArgs a)
        {
            if (ReopenDialogShown) return; 
            ReopenDialogShown = true;
            if(a.ChangeType == WatcherChangeTypes.Changed && a.Name == FileName)
            {
                watcher.EnableRaisingEvents = false;

                var d = Dispatcher.BeginInvoke(new FileSystemEventHandler(OnWroteSTAThread),s,a);
                d.Wait();

     
                watcher.EnableRaisingEvents = true;
            }
            ReopenDialogShown = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListW w = new ListW(rent.Albums, typeof(Album),rent);
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ListW w = new ListW(rent.Tracks, typeof(Track),rent);
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ListW w = new ListW(rent.Libraries, typeof(Library),rent);
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string name = StringW.GetString("Album's title", rent.Albums);
            if (string.IsNullOrEmpty(name)) return;
            Album F = (Album)rent.GetObjWithName<Album>(name);
            if (F == null) return;
            AlbumW w = new AlbumW(F);
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string name = StringW.GetString("Track's name", rent.Tracks);
            if (string.IsNullOrEmpty(name)) return;
            Track A = (Track)rent.GetObjWithName<Track>(name);
            if (A == null) return;
            TrackW w = new TrackW(A);
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string name = StringW.GetString("Library's name", rent.Libraries);
            if (string.IsNullOrEmpty(name)) return;
            Library D = (Library)rent.GetObjWithName<Library>(name);
            if (D == null) return;
            LibW w = new LibW(D);
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dial = new SaveFileDialog
            {
                AddExtension = true,
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "AudioRent files(.rent)|*.rent"
            };
            var t = dial.ShowDialog();
            if (t.GetValueOrDefault(false))
            {
                if (watcher != null) { watcher.EnableRaisingEvents = false; }
                BinaryFormatter formatter = new BinaryFormatter();
                using (var stream = File.Open(dial.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (DeflateStream deflstr = new DeflateStream(stream, CompressionLevel.Optimal))
                    {
                        formatter.Serialize(deflstr, rent);
                    }
                }
                if (watcher != null) { watcher.EnableRaisingEvents = true; }
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AlbumW w = new AlbumW(rent.CreateEmptyAlbum());
            w.Resources["Style"] = Resources["Style"];
            w.ShowDialog();
        }



        public static string ElementsChildrenHaveValidationErrors(FrameworkElement el)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(el))
            {
                TextBox element = child as TextBox;
                if (element == null) continue;
                if (Validation.GetHasError(element))
                {
                    return Validation.GetErrors(element)[0].ErrorContent.ToString();
                }
            }
            return null;
        }

        private void LV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LV.SelectedItem == null) return;
            Type t = (Type)LV.SelectedItem;
            List<Type> interfaces;
            object ob = t.Assembly.CreateInstance(t.FullName);
            Plugins.TryGetValue(t, out interfaces);
            if (interfaces.Contains(typeof(PluginInterfaces.Inters.IStyle)))
            {
                Style s = ((PluginInterfaces.Inters.IStyle)ob).GetStyle(); this.Resources["Style"] = s;
            }
            if (interfaces.Contains(typeof(PluginInterfaces.Inters.IOneTimeMethod)))
            {
                ((PluginInterfaces.Inters.IOneTimeMethod)ob).Method();
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            TrackW w = new TrackW(rent.CreateEmptyTrack());
            w.ShowDialog();
        }

        
    }
}
