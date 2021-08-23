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
using System.Windows.Shapes;
using TicTacToe_WPF.middleware_controller;
using System.IO;
namespace TicTacToe_WPF
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void P2P_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            GameWindow gameWindow = new GameWindow();
            GameController.start_mode = Constants.PEER_TO_PEER; gameWindow.parent = this;
            if (File.Exists("game.xml"))
            {
                File.Delete("game.xml");
            }
            gameWindow.Show();
        }

        private void P2C_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            GameWindow gameWindow = new GameWindow();
            GameController.start_mode = Constants.PEER_TO_COMPUTER; gameWindow.parent = this;
            if (File.Exists("game.xml"))
            {
                File.Delete("game.xml");
            }
            gameWindow.Show();
        }

        private void RESUME_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            GameWindow gameWindow = new GameWindow();
            GameController.start_mode = Constants.RESUME_GAME; gameWindow.parent = this;
            gameWindow.Show();
        }
    }
}
