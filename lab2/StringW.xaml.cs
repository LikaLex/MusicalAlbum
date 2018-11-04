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
    public partial class StringW : Window
    {
        bool t=false;

        internal static string GetString(string Title, IEnumerable<Obj> Collection)
        {
            StringW w = new StringW(Collection);
            w.Title = Title;
            w.Resources["Style"] = Application.Current.MainWindow.Resources["Style"];
            w.ShowDialog();
            if (!w.t) return null;
            if (string.IsNullOrWhiteSpace((string)w.CB.SelectedItem)) return null;
            return (string)w.CB.SelectedItem;
        }

        private StringW(IEnumerable<Obj> Collection)
        {
            InitializeComponent();
            foreach (Obj o in Collection) CB.Items.Add(o.Name);
            ((MainWindow)Application.Current.MainWindow).Reopening += () => { Close(); };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            t = true;
        }

        private void CB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Button_Click(sender,e);
        }
    }
}
