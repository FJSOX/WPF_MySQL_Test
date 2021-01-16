using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// ANi.xaml 的交互逻辑
    /// </summary>
    public partial class ANi : Window
    {
        public ANi()
        {
            InitializeComponent();
        }

        public void WindowLoaded(object sender, RoutedEventArgs e)
        {
            new Thread(p => { DataBinding(); }).Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DataBinding()
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                //编写获取数据并显示在界面的代码
            }));
        }
    }
}
