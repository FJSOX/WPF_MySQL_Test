//数据流：insert data -> MySQL table -> DataTable
//11点26分 2020年12月28日 by Fjsox

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
            conn = new MySqlConnection(connStr);//打开链接
           
            conn.Open();

            Show_Table();

            conn.Close();
        }

        /// <summary>
        /// 绘制表格
        /// </summary>
        private void Show_Table()
        {
            DataTable dataTable = new DataTable();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from fcs";//设置sql文本
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(dataTable);

            LV.DataContext = dataTable.DefaultView;
        }

        //上传数据并刷新表格
        private void Btn_Add_CLk(object sender, RoutedEventArgs e)
        {
            //List<string> 

            conn = new MySqlConnection(connStr);
            conn.Open();
            //dataTable = new DataTable();


            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "insert into fcs value('"+Convert.ToInt32(Tbo_ID.Text)+ "', " +
                "'" + Tbo_NAME.Text + "','" + Convert.ToInt32(Tbo_AGE.Text) + "')";//设置sql文本 //@id，@name，@age
            //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Tbo_ID.Text));//通常方式
            //cmd.Parameters.AddWithValue("@name", Tbo_NAME.Text);
            //cmd.Parameters.AddWithValue("@age", Convert.ToInt32(Tbo_AGE.Text));
            cmd.ExecuteNonQuery();//写入

            Show_Table();

            conn.Close();
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
