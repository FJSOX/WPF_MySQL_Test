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
    /// CommandTest.xaml 的交互逻辑
    /// </summary>
    public partial class CommandTest : Window
    {
        public CommandTest()
        {
            InitializeComponent();

            ClearCommand clearCommand = new ClearCommand();
            ctrlClear.Command = clearCommand;
            ctrlClear.CommandTarget = miniView;


            InitializeCommand();
            //ApplicationCommands.Save;
        }

        //声明并定义命令
        private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(CommandTest));

        private void InitializeCommand()
        {
            //把命令赋值给命令源（发送者）并制定快捷键
            BTN_Clear.Command = clearCmd;
            clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            //指定命令目标
            BTN_Clear.CommandTarget = TBX1;

            //创建命令关联
            CommandBinding commandBinding = new CommandBinding();
            commandBinding.Command = clearCmd;
            commandBinding.CanExecute += new CanExecuteRoutedEventHandler(commandBinding_CanExecute);
            commandBinding.Executed += new ExecutedRoutedEventHandler(commandBinding_Executed);

            //把命令关联安置在外围空间上
            SP1.CommandBindings.Add(commandBinding);
        }

        void commandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBX1.Text))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }

            // 避免继续上传而降低程序性能
            e.Handled = true;
        }

        void commandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TBX1.Clear();

            //避免继续上传而降低程序性能
            e.Handled = true;
        }

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBX_Right.Text))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name = TBX_Right.Text;
            if (e.Parameter.ToString()=="Teacher")
            {
                LB_NewItems.Items.Add(string.Format("New Teacher:{0}, 学而不厌，诲人不倦", name));
            }
            if (e.Parameter.ToString() == "Student")
            {
                LB_NewItems.Items.Add(string.Format("New Teacher:{0}, 好好学习，天天向上", name));

            }
        }   
    }

    /// <summary>
    /// 自定义接口
    /// </summary>
    public interface IView
    {
        //属性
        bool IsChanged { get; set; }

        //方法
        void SetBinding();
        void Refresh();
        void Clear();
        void Save();
    }

    /// <summary>
    /// 自定义命令
    /// </summary>
    public class ClearCommand:ICommand
    {
        //当命令可执行状态发生改变时，应当被激发
        public event EventHandler CanExecuteChanged;

        //用于判断命令是否可以执行（咱不实现）
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        //命令执行，带有业务相关的Clear逻辑
        public void Execute(object parameter)
        {
            IView view = parameter as IView;
            if (view != null)
            {
                view.Clear();
            }
        }
    }

    /// <summary>
    /// 自定义命令源
    /// </summary>
    public class MyCommmandSource: UserControl,ICommandSource
    {
        //继承自ICommandSource的三个属性
        public ICommand Command { set; get; }
        public object CommandParameter { get; set; }
        public IInputElement CommandTarget { get; set; }

        //在组建被单击时连带执行命令
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            //在命令目标上执行命令，或称让命令作用域命令目标
            if (CommandTarget!=null)
            {
                Command.Execute(CommandTarget);
            }
        }
    }

   
}
