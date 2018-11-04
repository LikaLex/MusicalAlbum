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
    /// Логика взаимодействия для LibW.xaml
    /// </summary>
    public partial class LibW : Window
    {
        Library D;
        internal LibW(Library d)
        {
            InitializeComponent();
            D = d;
            DataContext = D;
            foreach (Album F in d) FL.Items.Add(F.Name);
            Closing += Window_Closing;
            ((MainWindow)Application.Current.MainWindow).Reopening += () => { Closing -= Window_Closing; Close(); };
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FL_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FL.SelectedIndex == -1) return;
            if (MainWindow.ElementsChildrenHaveValidationErrors(GRID) != null) return;
            Album f = D.AlbumsList[FL.SelectedIndex];
            Close();
            AlbumW F = new AlbumW(f);
            F.Resources["Style"] = Resources["Style"];
            F.ShowDialog();
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
    }
}
