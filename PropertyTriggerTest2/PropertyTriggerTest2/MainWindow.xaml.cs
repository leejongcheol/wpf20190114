using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PropertyTriggerTest2
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
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Thread.Sleep(1000);
            Button btn = args.Source as Button;
            MessageBox.Show(btn.Content + " has been clicked", Title);
        }

    }
}
