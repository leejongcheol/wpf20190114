using System.Windows;
using System.Windows.Input;
using System.Text;
namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        string s = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            Title = "사용자 입력을 Window의 Content 속성에 매핑하기";
            Content = s;
        }

        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            string str = Content.ToString();
            if (args.Text == "\b")
            {
                if (str.Length > 0)
                {
                    //s = s.Substring(0, s.Length - 1);
                    s = s.Remove(s.Length - 1, 1);
                }
            }
            else
            {
                s += args.Text;
            }
            Content = s;
        }
    }
}