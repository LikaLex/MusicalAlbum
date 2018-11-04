using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StylePlugin
{
    public class SimpleStyle1 : PluginInterfaces.Inters.IStyle
    {
        public Style GetStyle()
        {
            Style s = new Style();
            s.Setters.Add(new Setter() {Property=Control.FontFamilyProperty, Value=new FontFamily("Segoe Print") });
            return s;
        }
    }
}
