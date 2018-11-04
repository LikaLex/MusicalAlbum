using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AquaStylePlugin
{
    public class GreenStyle : PluginInterfaces.Inters.IStyle, PluginInterfaces.Inters.IOneTimeMethod
    {
        public Style GetStyle()
        {
            Style s = new Style();
            s.Setters.Add(new Setter() {Property=Control.FontFamilyProperty, Value=new FontFamily("Segoe Print") });
            s.Setters.Add(new Setter() { Property = Control.BackgroundProperty, Value = Brushes.AliceBlue });
            s.Setters.Add(new Setter() { Property = Control.ForegroundProperty, Value = Brushes.ForestGreen });
            return s;
        }

        public void Method()
        {
            MessageBox.Show("Green theme activated");
        }
    }
}
