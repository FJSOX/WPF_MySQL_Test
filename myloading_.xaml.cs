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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_MySQL_Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class myloading_ : Window
    {
        public myloading_()
        {
            InitializeComponent();
        }

        static ANi aNi;

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            //var task = Task.Delay(2000);
            //await task;

            //this.rectangle.Fill = Brushes.Red;

            //MiAN(sender,e);

            //Thread td = new Thread(NewANi);
            //td.IsBackground = false;
            //td.Start();

            App.Current.Dispatcher.Invoke((Action)(() =>//开始异步执行，原理正在搞
            {
                NewANi();
            }));

            for (int i = 0; i < 100; i++)//no.3
            {
                Thread.Sleep(100);//主线程需要做的事
                Console.WriteLine("action {0}",i);
            }
            

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                ClzANi();
            }));
        }


        public delegate void NewWin();
        static void NewThread(object sender, RoutedEventArgs e)
        {
            Thread td = new Thread(NewANi);
            td.Start();
        }

        static void NewANi()
        {
            aNi = new ANi();
            aNi.Show();
        }

        static void ClzANi()
        {
            aNi.Close();
        }

        public delegate void DoWork(object sender, RoutedEventArgs e);

        static void MiAN(object sender, RoutedEventArgs e)
        {
            DoWork d = new DoWork(WorkPro);//no.1

            var a= d.BeginInvoke(sender, e, CallBack, d);//no.2
            //var b = d.EndInvoke(a);
            //Console.WriteLine(b);
            for (int i = 0; i < 100; i++)//no.3
            {
                Thread.Sleep(10);//主线程需要做的事
            }
            Console.WriteLine("主线程done");
            //Console.ReadKey();
        }

        public static void WorkPro(object sender, RoutedEventArgs e)//静态方法，在类没有实例化之前即可使用
        {
            //int sum = 0;
            //做一些耗时的工作
            aNi = new ANi();
            aNi.Show();
            for (int i = 0; i < 120; i++)
            {
                //sum += i;
                Thread.Sleep(10);
            }
            //return sum;
        }

        public static void CallBack(IAsyncResult asyncResult)
        {
            DoWork d = (DoWork)asyncResult.AsyncState;
            d.EndInvoke(asyncResult);
            aNi.Close();
            Console.WriteLine("over!");
        }
    }
}
