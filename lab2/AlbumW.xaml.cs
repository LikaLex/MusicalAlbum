using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    [ValueConversion(typeof(double),typeof(double))]
    public class HConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if((parameter as string)=="10") return (object)((value as double?).Value / 10);
            if ((parameter as string) == "1") return (object)((value as double?).Value / 7.5);
            return (object)((value as double?).Value / 1.7);
        }
    }
    
    public partial class AlbumW : Window
    {
        Album F;
        internal AlbumW(Album f)
        {
            InitializeComponent();
            F = f;
            DataContext = f;
            foreach (Track A in f) AL.Items.Add(A.Name);
            Closing += Window_Closing;
            ((MainWindow)Application.Current.MainWindow).Reopening += () => { Closing -= Window_Closing; Close(); };
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AL_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AL.SelectedIndex == -1) return;
            if (MainWindow.ElementsChildrenHaveValidationErrors(GRID) != null) return;
            Track a = F.TracksList[AL.SelectedIndex];
            Close();
            TrackW A = new TrackW(a);
            A.Resources["Style"] = Resources["Style"];
            A.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ElementsChildrenHaveValidationErrors(GRID) == null && F.Library!=null) {
                Library d = F.Library;
                Close();
                LibW D = new LibW(d);
                D.Resources["Style"] = Resources["Style"];
                D.ShowDialog();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string name = StringW.GetString("Library's name", MainWindow.rent.Libraries);
            if (string.IsNullOrEmpty(name)) return;
            F.UnassignLibrary();
            MainWindow.rent.AssignLibrary(F, name);
            this.LibName.Text = F.Library.Name;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AL.SelectedIndex == -1) return;
            F.UnassignTrack(AL.SelectedIndex);
            AL.Items.RemoveAt(AL.SelectedIndex);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string name = StringW.GetString("Track's name", MainWindow.rent.Tracks);
            if (string.IsNullOrEmpty(name)) return;
            MainWindow.rent.AssignTrack(F, name);
            AL.Items.Add(name);
        }

        private void button_Click_3(object sender, RoutedEventArgs e)
        {
            var dial = new OpenFileDialog();
            dial.Filter = "Image files|*.png; *.jpg; *.jpeg; *.gif";
            if (dial.ShowDialog().GetValueOrDefault(false) == true) F.Poster = dial.FileName;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string trig = MainWindow.ElementsChildrenHaveValidationErrors(GRID);
            if (trig != null)
            {
                MessageBox.Show(trig, "Error");
                e.Cancel = true;
            }
            else { Closing -= Window_Closing; }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PosterW pw = new PosterW(IMG);
        }
    }
}
