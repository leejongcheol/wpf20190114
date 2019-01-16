using System.Windows;
using System.Windows.Controls;

namespace EventSetterTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            MessageBox.Show($"{btn.Content}({btn.Name}) has been clicked {Title}");
        }
    }
}
