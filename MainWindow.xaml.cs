//数据流：insert data -> DataTable <-> MySQL table 双向
//11点26分 2020年12月28日 by Fjsox
//11点33分 2020年12月28日 新建分支 by Fjsox

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
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace WPF_MySQL_Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string connStr = "server=192.168.1.103;port=3306;database=test;user=fox;password=`1234";//链接，账户设置
        MySqlConnection conn;//声明远程连接
        DataTable dataTable;//
        MySqlCommand cmd;//
        MySqlDataAdapter sda;//
        //DataTable dataTable = new DataTable();
        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Otest()
        {
            List<object> list=new List<object>();
            list.Add(1);
            list.Add(2);
            list.Add("str");


        }

        /// <summary>
        /// 按钮事件，显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Show_Clk(object sender, RoutedEventArgs e)
        {
            //cmd.CommandText = "select * from conntest";
            //MySqlDataReader reader=new MySqlDataReader()

            //cmd.CommandText = "select * from fcs";//设置sql文本 
            cmd.CommandText = "select * from fcs limit 1";//只读取第一行


            //MySqlDataReader reader = cmd.ExecuteReader();
            //try
            //{
            //    MySqlDataReader reader = cmd.ExecuteReader();               
            //}
            //catch (MySql.Data.MySqlClient.MySqlException ep)
            //{
            //    MessageBox.Show(ep.Message);

            //    cmd.CommandText = "create table CANData(time1 datetime)";//新建table
            //    int rt = cmd.ExecuteNonQuery();

            //    Console.WriteLine(rt);

            //    return;
            //}

            sda = new MySqlDataAdapter(cmd);
            if (LV.DataContext!=null)
            {
                dataTable.Clear();
            }
            sda.Fill(dataTable);//填充

            Tbo_ID.Text = (Convert.ToInt32(dataTable.Compute("max(ID)", ""))+1).ToString();

            LV.DataContext = dataTable.DefaultView;
        }



        /// <summary>
        /// 上传数据并刷新表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_CLk(object sender, RoutedEventArgs e)
        {
            //
            DataRow dr = dataTable.NewRow();
            object[] obarr = new object[3];
            List<object> list = new List<object>();
            
            for (int i = 0, j=0; i < StkP.Children.Count; i++)
            {
                UIElement element = StkP.Children[i];//uielement使用
                if (element is TextBox)
                {
                    TextBox tb = element as TextBox;
                    string tbtext = tb.Text;
                    if (IsNumber(tbtext))
                    {
                        //添加int
                        obarr[j]= Convert.ToInt32(tbtext);
                    }
                    else
                    {
                        //添加string
                        obarr[j] = tbtext;
                    }

                    ++j;
                }
            }

            //obarr.Append(1);

            for (int i=0;i<obarr.Length;i++)
            {
                list.Add(obarr[i]);
            }

            //dataTable.Rows.Add(Convert.ToInt32(Tbo_ID.Text), Tbo_NAME.Text, Convert.ToInt32(Tbo_AGE.Text));//new

            dr.ItemArray = list.ToArray();//obarr//datarow使用
            dataTable.Rows.Add(dr);//更新datatable
            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(sda);//在透过dataadapter的selectcommand从数据源取回表结构后，自适应生成insert,delete,updata命令

            sda.Update(dataTable);//上传

            Tbo_ID.Text = (Convert.ToInt32(dataTable.Compute("max(ID)", "")) + 1).ToString();
        }


        delegate void PrintSth(string s);
        private void Prs(string s)
        {
            Console.WriteLine(s);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn = new MySqlConnection(connStr);//打开链接
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("数据库连接失败，请检查服务器参数设置");
                return;
            }

            //conn.Open();

            //MessageBox.Show("数据库连接成功");
            //return true;
            cmd = conn.CreateCommand();//

            dataTable = new DataTable();//

            /*lambda表达式*/
            PrintSth pr = x => { Console.WriteLine(x); };
            pr("hello\n");

            /*匿名函数*/
            PrintSth pr2 = delegate (string xx) { Console.WriteLine(xx); };
            pr2("c sharp\n");

            /*委托*/
            PrintSth pr3 = Prs;
            Prs("Doom!");

            //2
            PrintSth pr4 = new PrintSth(Prs);
            Prs("OK!");
        }

        /// <summary>
        /// 使用正则表达式判断是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsNumber(string str)
        {
            //string rule = "^-?[0-9]*$";
            //Regex rg = new Regex(rule);
            Regex rg = new Regex(@"^-?[0-9].?[0-9]*$");
            Match m = rg.Match(str);
            string re = m.Value;

            if (m.Value==str)
            {
                return true;
            } else
            {
                return false;
            }
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            conn.Close();
            //dataTable.Clear();//清空datatable
        }

        private async Task Btn_New_WindowASYNC(object sender, RoutedEventArgs e)
        {
            ANi aNi = new ANi();
            await Task.Run(() => { 
                aNi.Show(); 
                
            });
            Task.Delay(6000).Wait();
        }

        private async void Btn_New_Window(object sender, RoutedEventArgs e)
        {
            //ANi aNi = new ANi();
            //await Task.Run(() => { aNi.Show(); });
            await Btn_New_WindowASYNC(sender, e);
        }
    }

    /// <summary>
    /// 颜色变换
    /// </summary>
    public class BackgroundConverter: IValueConverter//BackgroundConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = new Color();
            int num = int.Parse(value.ToString());
            if (num > 100)
                color = Colors.Yellow;
            else if (num < 50)
                color = Colors.LightGreen;
            else
                color = Colors.LightPink;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
