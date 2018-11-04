using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace NightStylePlugin
{
    public class NightStyle : PluginInterfaces.Inters.IStyle, PluginInterfaces.Inters.IOneTimeMethod
    {
        public Style GetStyle()
        {
            Style s = new Style();
            s.Setters.Add(new Setter() { Property = Control.BackgroundProperty, Value = Brushes.Gray});
            s.Setters.Add(new Setter() { Property = Control.ForegroundProperty, Value = Brushes.AntiqueWhite});
            return s;
        }

        public void Method()
        {
            MessageBox.Show("Night theme activated");
        }
    }
}
