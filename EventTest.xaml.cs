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
    /// EventTest.xaml 的交互逻辑
    /// </summary>
    public partial class EventTest : Window
    {
        public EventTest()
        {
            InitializeComponent();

            //为外城Grid添加路由事件侦听器
            StudentA.AddNameChangedHandler(GD20,new RoutedEventHandler(StudentANameChangedHandler));            //this.GD20.AddHandler(StudentA.NameChangedEvent,new RoutedEventHandler(this.StudentANameChangedHandler));

            this.GD1.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonClicked));
            //此事件对GD1下所有控件有用，Button.ClickEvent是RouteEvent类型的静态成员变量（private），Button还有
            //一个CLR包装的.Click事件用于对外部暴露。//相当于Xaml中的Button.Click="ButtonClicked"。(写在GD1中)

        }

        //Click事件处理器
        private void BTN1_Click(object sender, RoutedEventArgs e)
        {
            StudentA student = new StudentA() { Id = 101, Name = "Temmo" };
            student.Name = "Kass";

            //准备事件并发送路由消息
            RoutedEventArgs args = new RoutedEventArgs(StudentA.NameChangedEvent, student);
            BTN1.RaiseEvent(args);
        }

        //Grid捕捉到NameChangedEvent后的处理器
        private void StudentANameChangedHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((e.OriginalSource as StudentA).Id.ToString());
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {            
            MessageBox.Show((e.OriginalSource as FrameworkElement).Name);
            //此时的sender实质上是GD1，使用.OriginalSource属性获取事件的源头。
        }

        private void ReportTimeHandler(object sender, ReportTimeEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            string timeStr = e.ClickTime.ToLongTimeString();
            string content = string.Format("{0} 到达 {1}", timeStr, element.Name);
            this.LB1.Items.Add(content);

            //当路由消息到达GD12时，终止路由事件（路由事件已被处理）
            if (element==this.GD12)
            {
                e.Handled = true;//表示事件已处理
            }
        }
    }


    class ReportTimeEventArgs:RoutedEventArgs
    {
        //派生类可以调用父类的有参构造函数
        public ReportTimeEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source) { }

        public DateTime ClickTime { get; set; }

    }

    public class TimeButton:Button
    {
        //声明和注册路由事件
        public static readonly RoutedEvent ReportTimeEvent = EventManager.RegisterRoutedEvent
            ("ReportTime", RoutingStrategy.Tunnel, typeof(EventHandler<ReportTimeEventArgs>), typeof(TimeButton));

        //CLR事件包装器
        public event RoutedEventHandler ReportTime
        {
            add { this.AddHandler(ReportTimeEvent, value); }
            remove { this.RemoveHandler(ReportTimeEvent, value); }
        }

        //激发路由事件，借用Click事件的激发方法
        protected override void OnClick()
        {
            base.OnClick();  //保证Button原有功能正常使用，Click事件能被激发

            ReportTimeEventArgs args = new ReportTimeEventArgs(ReportTimeEvent, this);
            args.ClickTime = DateTime.Now;
            this.RaiseEvent(args);
        }
    }

    public class StudentA
    {
        //声明并定义路由事件
        public static readonly RoutedEvent NameChangedEvent = EventManager.RegisterRoutedEvent
            ("NameChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Student));

        //为界面元素添加路由事件侦听
        public static void AddNameChangedHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            if(e!=null)
            {
                e.AddHandler(StudentA.NameChangedEvent, h);
            }
        }

        //移除侦听
        public static void RemoveNameChangedHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            if(e!=null)
            {
                e.RemoveHandler(StudentA.NameChangedEvent, h);
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}

