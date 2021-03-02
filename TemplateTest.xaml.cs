using System;
using System.Collections.Generic;
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
    /// TemplateTest.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateTest : Window
    {
        public TemplateTest()
        {
            InitializeComponent();
            InitialCarList();
            
            ////开始时显示Logo
            //detailView.IMG_Photo.Source = new BitmapImage
            //        (new Uri(@"Resurce/Image/CAR/LamborghiniLogo.jpg", UriKind.Relative));
            //detailView.IMG_Photo.Stretch = Stretch.Uniform;
        }

        //初始化ListBox
        private void InitialCarList()
        {
            List<Car> carList = new List<Car>()
            {
                new Car(){Automaker="Lamborghini",Name="DiaBlo",Year="1990", TopSpeed="340"},
                new Car(){Automaker="Lamborghini",Name="Murcielago",Year="2001", TopSpeed="353"},
                new Car(){Automaker="Lamborghini",Name="Gallardo",Year="2003", TopSpeed="325"},
                new Car(){Automaker="Lamborghini",Name="Rventom",Year="2008", TopSpeed="356"},
            };

            //填充数据源
            LB_Cars.ItemsSource = carList;
            //默认显示第一页
            LB_Cars.SelectedItem = LB_Cars.Items[0];

            //foreach (Car car in carList)
            //{
            //    UCForTemplateTest1 view = new UCForTemplateTest1();
            //    view.Car = car;
            //    LB_Cars.Items.Add(view);
            //}
        }

        ////选项变化事件处理器
        //private void LB_Cars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    UCForTemplateTest1 view = e.AddedItems[0] as UCForTemplateTest1;
        //    if (view!=null)
        //    {
        //        detailView.Car = view.Car;
        //    }
        //}  
    }

    //厂商名称转换成Logo路径
    public class AutomakerToLogoPathConverter : IValueConverter
    {
        //正向转换
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string uriStr = string.Format(@"/Resurce/Image/CAR/{0}Logo.jpg", (string)value);
            return new BitmapImage(new Uri(uriStr, UriKind.Relative));
        }

        //反向转换(未被用到)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //汽车名称转换为图片路径
    public class NameToPhotoPathConverter : IValueConverter
    {
        //正向转换
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string uriStr = string.Format(@"/Resurce/Image/CAR/{0}.jpg", (string)value);
            return new BitmapImage(new Uri(uriStr, UriKind.Relative));
        }

        //反向转换(未被用到)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
