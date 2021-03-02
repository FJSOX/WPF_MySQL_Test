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

namespace WPF_MySQL_Test
{
    /// <summary>
    /// 自定义命令目标
    /// </summary>
    public partial class MiniView : UserControl, IView
    {
        //构造器
        public MiniView()
        {
            InitializeComponent();
        }

        //继承自IView的成员们
        public bool IsChanged { get; set; }
        public void SetBinding() { }
        public void Refresh() { }
        public void Save() { }

        //用于清除内容的业务逻辑
        public void Clear()
        {
            TBX_1.Clear();
            TBX_2.Clear();
            TBX_3.Clear();
            TBX_4.Clear();
        }

    }
}
