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
    /// UCForTemplateTest2.xaml 的交互逻辑
    /// </summary>
    public partial class UCForTemplateTest2 : UserControl
    {
        public UCForTemplateTest2()
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
                TBK_Automaker.Text = car.Automaker;
                TBK_TopSpeed.Text = car.TopSpeed;
                string uriStr = string.Format(@"Resurce/Image/CAR/{0}.jpg",car.Name);
                IMG_Photo.Source = new BitmapImage(new Uri(uriStr, UriKind.Relative));
            }
        }
    }
}
