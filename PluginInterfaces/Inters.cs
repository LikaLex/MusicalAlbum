using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PluginInterfaces
{
    public class Inters
    {
        public static Type[] types { get; }
        static Inters()
        {
            //MessageBox.Show(Assembly.GetExecutingAssembly().GetTypes()[0].FullName);
            types = Assembly.GetExecutingAssembly().GetType("PluginInterfaces.Inters").GetNestedTypes().Where(x => x.IsInterface).ToArray();
        }
        public interface IStyle
        {
            Style GetStyle();
        }
        public interface IOneTimeMethod
        {
            void Method();
        }
    }
}
