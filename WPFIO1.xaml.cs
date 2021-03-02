using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

namespace WPF_MySQL_Test
{
    /// <summary>
    /// WPFIO1.xaml 的交互逻辑
    /// </summary>
    public partial class WPFIO1 : Window
    {
        public WPFIO1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Human h = (Human)this.FindResource("human");
            MessageBox.Show(h.Child.Name);
        }
    }

    [TypeConverter(typeof(StringToHumanTypeConvert))]//TypeConverterAttribute
    public class Human {
        public string Name { get; set; }
        public Human Child { get; set; }
    }

    public class StringToHumanTypeConvert:TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                Human h = new Human();
                h.Name = value as string;
                return h;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
