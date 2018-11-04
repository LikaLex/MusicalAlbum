using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace AquaStylePlugin
{
    public class BlueStyle : PluginInterfaces.Inters.IStyle, PluginInterfaces.Inters.IOneTimeMethod
    {
        public Style GetStyle()
        {
            Style s = new Style();
            s.Setters.Add(new Setter() { Property = Control.BackgroundProperty, Value = Brushes.Blue});
            s.Setters.Add(new Setter() { Property = Control.ForegroundProperty, Value = Brushes.Gold});
            s.Setters.Add(new Setter() {Property = Control.BorderBrushProperty, Value = Brushes.SeaGreen});
            return s;
        }

        public void Method()
        {
            MessageBox.Show("Blue theme activated");
        }
    }
}
