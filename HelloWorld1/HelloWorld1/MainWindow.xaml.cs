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

namespace HelloWorld1
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

        //이벤트 핸들러, 이벤트 가입자
        private void btnSayHello_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"안녕~{txtName.Text}");
            MessageBox.Show(string.Format("안녕~ {0}", txtName.Text));
            MessageBox.Show("안녕~ " + txtName.Text);
        }
    }
}
