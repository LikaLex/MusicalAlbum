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
    /// Логика взаимодействия для YNDialogW.xaml
    /// </summary>
    public partial class YNDialogW : Window
    {
        private bool ans=false;

        private YNDialogW()
        {
            InitializeComponent();
        }

        internal static bool Ask(string title, string text)
        {
            YNDialogW w = new YNDialogW();
            w.Title = title;
            w.TB.Text = text;
            w.Resources["Style"] = Application.Current.MainWindow.Resources["Style"];
            w.ShowDialog();
            return w.ans;
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            ans = true;
            Close();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
