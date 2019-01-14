using System;
using System.Windows;
namespace HelloWorld1
{
    class Main2
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run( new MainWindow() );
        }
    }
}
