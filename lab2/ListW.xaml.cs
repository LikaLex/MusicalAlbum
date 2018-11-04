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
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Логика взаимодействия для ListW.xaml
    /// </summary>
    public partial class ListW : Window
    {
        List<Obj> List;
        Type T;
        AudioRent Rent;
        internal ListW(IEnumerable<Obj> list, Type t, AudioRent rent)
        {
            InitializeComponent();
            List = list.ToList<Obj>();
            Rent = rent;
            T = t;
            foreach (Obj o in list) LBox.Items.Add(o.Name);
            ((MainWindow)Application.Current.MainWindow).Reopening += () => { Close(); };
        }

        private void LBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LBox.SelectedIndex == -1) return;
            if (T == typeof(Album))
            {
                Album F =(Album)List[LBox.SelectedIndex];
                Close();
                AlbumW w = new AlbumW(F);
                w.Resources["Style"] = Resources["Style"];
                w.ShowDialog();
            }
            if (T == typeof(Track))
            {
                Track F = (Track)List[LBox.SelectedIndex];
                Close();
                TrackW w = new TrackW(F);
                w.Resources["Style"] = Resources["Style"];
                w.ShowDialog();

            }
            if (T == typeof(Library))
            {
                Library F = (Library)List[LBox.SelectedIndex];
                Close();
                LibW w = new LibW(F);
                w.Resources["Style"] = Resources["Style"];
                w.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (LBox.SelectedIndex == -1) return;
            if (T == typeof(Album))
            {
                Rent.DelObjWithIndex<Album>(LBox.SelectedIndex);
            }
            if (T == typeof(Track))
            {
                Rent.DelObjWithIndex<Track>(LBox.SelectedIndex);
            }
            if (T == typeof(Library))
            {
                Rent.DelObjWithIndex<Track>(LBox.SelectedIndex);
            }
            LBox.Items.RemoveAt(LBox.SelectedIndex);
        }
    }
}
