using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HelloWorld2
{
    class MyMain : Application
    {
        [STAThread]
        public static void Main()
        {
            MyMain app = new MyMain();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run();
            Console.WriteLine("hello");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
  
            Window mainWindow = new Window();
            mainWindow.Title = "Main Window";
            mainWindow.MouseDown += WinMouseDown;
            mainWindow.Show();

            for(int i=0; i <2; i++)
            {
                Window win = new Window();
                win.Title = $"Extra Window No.{i+1}";
                win.ShowInTaskbar = false;
                //this.MainWindow = win;
                win.Owner = mainWindow;
                win.Show();
            }
        }

        void WinMouseDown(Object sender, MouseEventArgs e)
        {
            Window win = new Window();
            win.Title = "Modal Win";

            Button b = new Button();
            b.Content = "Click Me~";
            b.Click += Button_Click;

            win.Content = b;
            win.ShowDialog();  //Modal
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Button click{sender.ToString()}");
        }
    }
}
