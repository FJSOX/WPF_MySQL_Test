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
using System.Windows.Shapes;

namespace WPF_MySQL_Test
{
    /// <summary>
    /// ResourceTest.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceTest : Window
    {
        public ResourceTest()
        {
            InitializeComponent();

            Uri imguri = new Uri(@"Resurce/Image/mei.png",UriKind.Relative);
            IMG.Source = new BitmapImage(imguri);

            //添加资源
            TBK_Age.Text = Properties.Resources.Age;

            GetResource();


        }

        public void GetResource()
        {
            string t1 = (string)FindResource("str");
            Console.WriteLine(t1);
            string t2 = (string)GD.Resources["str1"];
            Console.WriteLine(t2);
        }
    }
}
