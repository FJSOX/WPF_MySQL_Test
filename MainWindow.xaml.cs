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

        /// <summary>
        /// 按钮事件，显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Show_Clk(object sender, RoutedEventArgs e)
        {
            if (LV.DataContext!=null)
            {
                return;
            }
            cmd.CommandText = "select * from fcs";//设置sql文本
            sda = new MySqlDataAdapter(cmd);
            sda.Fill(dataTable);

            LV.DataContext = dataTable.DefaultView;
        }



        /// <summary>
        /// 上传数据并刷新表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_CLk(object sender, RoutedEventArgs e)
        {
            dataTable.Rows.Add(Convert.ToInt32(Tbo_ID.Text), Tbo_NAME.Text, Convert.ToInt32(Tbo_AGE.Text));//new
            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(sda);//在透过dataadapter的selectcommand从数据源取回表结构后，自适应生成insert,delete,updata命令

            sda.Update(dataTable);//上传
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn = new MySqlConnection(connStr);//打开链接
            conn.Open();
            cmd = conn.CreateCommand();//

            dataTable = new DataTable();//
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            conn.Close();
            //dataTable.Clear();//清空datatable
        }
    }


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
