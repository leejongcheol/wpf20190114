using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataContextDataBinding
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClicked(object sender, RoutedEventArgs e)
        {
            Emp e1 = Grid1.DataContext as Emp;
            System.Console.WriteLine(e1.Ename);
            System.Console.WriteLine(e1.City);
        }
    }

    public class Emp
    {
        public string Ename
        {
            get;
            set;
        }
        public string City
        {
get;
            set;
        }
    }
}
