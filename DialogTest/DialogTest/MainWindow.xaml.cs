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

namespace DialogTest
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

        //OpenFileDialogBox
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dl1 = new Microsoft.Win32.OpenFileDialog();
            dl1.FileName = "MYFileSave";
            dl1.DefaultExt = ".*";
            dl1.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            Nullable<bool> result = dl1.ShowDialog();
            if (result == true)
            {
                tb.Text = dl1.FileName;
            }
        }
        //SaveFileDialogBox
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dl1 = new Microsoft.Win32.SaveFileDialog();
            dl1.FileName = "MYFileSave";
            dl1.DefaultExt = ".txt";
            dl1.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dl1.ShowDialog();
            if (result == true)
            {
                tb.Text = dl1.FileName;
            }
        }
        //PrintFileDialogBox
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog dl1 = new
           System.Windows.Controls.PrintDialog();
            dl1.PageRangeSelection = PageRangeSelection.AllPages;
            dl1.UserPageRangeEnabled = true;
            Nullable<bool> result = dl1.ShowDialog();
            if (result == true)
            {
                tb.Text = "프린트 다이얼로그...";
            }
        }
    }
}
