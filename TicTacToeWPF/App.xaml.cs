using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                // ...
            }
            else
            {
                var app = new App();
                app.InitializeComponent();
                StartWindow game = new StartWindow();
                game.Show();
                app.Run();
            }
        }

        private void InitializeComponent()
        {
            //throw new NotImplementedException();
        }
    }
}
