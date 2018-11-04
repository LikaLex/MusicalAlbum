using System.Windows;
using System.Windows.Controls;

namespace lab2
{
    /// <summary>
    /// Логика взаимодействия для PosterW.xaml
    /// </summary>
    public partial class PosterW : Window
    {
        public PosterW(Image im)
        {
            InitializeComponent();
            imag.Source = im.Source;
            this.ShowDialog();
            ((MainWindow)Application.Current.MainWindow).Reopening += () => { Close(); };
        }
    }
}
