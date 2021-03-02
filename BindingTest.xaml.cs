using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data;
using System.Text.RegularExpressions;

namespace WPF_MySQL_Test
{
    /// <summary>
    /// BindingTest.xaml 的交互逻辑
    /// </summary>
    public partial class BindingTest : Window
    {
        Student student;
        Country country;
        DataTable dt;
        AMAN aMAN;

        public BindingTest()
        {
            InitializeComponent();

            InitializeLayOut();

            /*使用自定依赖属性*/
            aMAN = new AMAN();
            aMAN.SetBinding(AMAN.NameProperty, new Binding("Text") { Source = TBX_Source });
            TBK_Target.SetBinding(TextBlock.TextProperty, new Binding("Name") { Source = aMAN });
            //aMAN.SetValue(AMAN.AgeProperty, TBX_Source.Text);
            //TBK_Target.Text = (string)aMAN.GetValue(AMAN.AgeProperty);

            SetBinding();

            

            dt = new DataTable("Table_Data");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Rows.Add(1, "aaa", 24);
            dt.Rows.Add(2, "bbb", 35);
            dt.Rows.Add(3, "www", 44);
            dt.Rows.Add(4, "abb", 22);
            dt.Rows.Add(5, "aaaa", 264);

            Regex regex = new Regex(@"^[a-z]b*$");
            //regex.IsMatch("aaaa");

            /*对DataTable行进行正则筛选*/
            LV_New.ItemsSource =
                from row in dt.Rows.Cast<DataRow>()//将DataTable拆解成行
                where /*Convert.ToString(row["Name"]).StartsWith("aa")*/ regex.IsMatch(Convert.ToString(row["Name"]))//从中筛选出Name特性a字母开头的行
                select new Man()//创建新的Man列表
                {
                    Id = Convert.ToInt32(row["Id"].ToString()), 
                    Name = row["Name"].ToString(),
                    Age = Convert.ToInt32(row["Age"].ToString())
                };
            LV_Data.ItemsSource = dt.DefaultView;
            //LV_Data.DisplayMemberPath = "Name";

            //
            List<Man> men = new List<Man>()
            {
                new Man(){Id=1, Name="fhh", Age=13},
                new Man(){Id=2, Name="fjs", Age=25},
                new Man(){Id=3, Name="fy", Age=52}
            };

            LB_MName.ItemsSource = men;
            //LB_MName.DisplayMemberPath = "Name";

            TBX_MID.SetBinding(TextBox.TextProperty, new Binding("SelectedItem.Id") { Source = LB_MName });

            //数据源
            student = new Student();
            student.Name = "xff";

            //控件直接绑定
            this.TBK_Binding.SetBinding(TextBlock.TextProperty, new Binding("Name") { Source = student});//默认为单向绑定

            //TBK_SLD_1.SetBinding(TextBlock.TextProperty, new Binding("Value") { Source = SLD_1 });

            //Gecha gecha = new Gecha() { Sr = "sr", SSr = "ssr", Ur = "ur" };

            ////Binding
            //Binding binding = new Binding();
            //binding.Source = student;
            //binding.Path = new PropertyPath("Name");

            ////用Binding连接数据源和Binding目标
            //BindingOperations.SetBinding(this.TBK_Binding, TextBlock.TextProperty, binding);//source，property，binding实例

            //Binding binding_ = new Binding();
            //binding_.Source = student;
            //binding_.Path = new PropertyPath("Name");
            //BindingOperations.SetBinding(this.TBX_Name, TextBox.TextProperty, binding_);
            //TBX_Name.Text = student.Name;

            //country.Name = "中国";
            //country.ProvinceList = new List<Province>();
            City city = new City() { Name="武汉"};
            Province province = new Province() { Name = "湖北", CityList = new List<City>() { city } };
            country = new Country() { Name = "中国", ProvinceList = new List<Province>() { province } };

            TBK_Country.SetBinding(TextBlock.TextProperty, new Binding("Name") { Source = country });
            TBK_Province.SetBinding(TextBlock.TextProperty, new Binding("ProvinceList[0].Name") { Source = country });
            TBK_City.SetBinding(TextBlock.TextProperty, new Binding("ProvinceList[0].CityList[0].Name") { Source = country });
        }

        private void Click_ChangeText(object sender, RoutedEventArgs e)
        {
            student.Name = TBX_Name.Text;
        }

        /**/
        private void SetBinding()
        {
            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Calculator();
            odp.MethodName = "Add";
            odp.MethodParameters.Add("0");
            odp.MethodParameters.Add("0");
            //MessageBox.Show(odp.Data.ToString());

            Binding binding1 = new Binding("MethodParameters[0]")//Path为第一个参数
            {
                Source = odp,
                BindsDirectlyToSource = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged

            };

            Binding binding2 = new Binding("MethodParameters[1]")//Path为第二个参数
            {
                Source = odp,
                BindsDirectlyToSource = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged

            };

            Binding bindingResult = new Binding(".") { Source=odp};//Path为odp本身

            TBX_Num1.SetBinding(TextBox.TextProperty, binding1);
            TBX_Num2.SetBinding(TextBox.TextProperty, binding2);
            TBX_Num3.SetBinding(TextBox.TextProperty, bindingResult);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            

            ANi aNi = new ANi();
            aNi.ShowDialog();
            

            await Task.Delay(5000);

            aNi.Close();

            
            return;
        }

        private void BTN_Get_Click(object sender, RoutedEventArgs e)
        {
            

            //aMAN.Name = TBX_Source.Text;
            //TBK_Target.Text = (string)aMAN.Name;

            //aMAN.SetValue(AMAN.NameProperty, TBX_Source.Text);
            //TBK_Target.Text = (string)aMAN.GetValue(AMAN.NameProperty);

            //aMAN.Name = "1";
            //MessageBox.Show(aMAN.Name);

        }

        private void BTN_Get_Grade_Click(object sender, RoutedEventArgs e)
        {
            AMAN am = new AMAN();
            School.SetGrade(am, 6);
            MessageBox.Show(School.GetGrade(am).ToString());
        }

        private void InitializeLayOut()
        {
            Grid grid = new Grid() { ShowGridLines=true};
            SP3.Children.Add(grid);

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Button button = new Button() { Content = "OKK" };
            Grid.SetRow(button, 1);
            Grid.SetColumn(button, 1);

            grid.Children.Add(button);
        }
    }

    public class School:DependencyObject
    {
        public static int GetGrade(DependencyObject obj)
        {
            return (int)obj.GetValue(GradeProperty);
        }

        public static void SetGrade(DependencyObject obj, int value)
        {
            obj.SetValue(GradeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Grade.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.RegisterAttached("Grade", typeof(int), typeof(School), new PropertyMetadata(0));
    }

    public class AMAN : DependencyObject
    {
        /*依赖属性*/
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(AMAN));

        /*CLR包装*/
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set
            {
                if (value != "")
                {
                    SetValue(NameProperty, value);
                }
                else
                { throw new Exception("Value is empty!"); }
                //SetValue(NameProperty, value);
            }
        }
        
        /*setbinding包装*/
        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
        {
            return BindingOperations.SetBinding(this, dp, binding);
        }



        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Age.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register("Age", typeof(int), typeof(AMAN));
    }

    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                /*触发事件*/
                if (this.PropertyChanged!=null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
    }

    class Calculator
    {
        public string Add(string arg1, string arg2)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            if (double.TryParse(arg1, out x)&& double.TryParse(arg2, out y))
            {
                z = x + y;
                return z.ToString();
            }
            
            return "Input Error";
        }
    }
    
    enum Sex
    {
        Female,
        Male,
    }

    class City
    {
        public string Name {
            get;
            set;
        }
    }

    class Province:City
    {
        public List<City> CityList { get; set; }
    }

    class Country : City
    {
        public List<Province> ProvinceList { get; set; }
    }

    class Gecha
    {
        public string Ur { get; set; }
        public string SSr { get; set; }
        public string Sr { get; set; }
    }

    class Man
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { set; get; }
    }
    

}
