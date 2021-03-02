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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_MySQL_Test
{
    /// <summary>
    /// UCForTemplateTest1.xaml 的交互逻辑
    /// </summary>
    public partial class UCForTemplateTest1 : UserControl
    {
        public UCForTemplateTest1()
        {
            InitializeComponent();
        }

        private Car car;
        public Car Car
        {
            get { return car; }
            set
            {
                car = value;
                TBK_Name.Text = car.Name;
                TBK_Year.Text = car.Year;
                string uriStr = @"Resurce/Image/CAR/LamborghiniLogo.jpg";
                IMG_Logo.Source = new BitmapImage(new Uri(uriStr, UriKind.Relative));
            }
        }
    }

    public class Car
    {
        public string Automaker { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string TopSpeed { get; set; }     
    }

}
