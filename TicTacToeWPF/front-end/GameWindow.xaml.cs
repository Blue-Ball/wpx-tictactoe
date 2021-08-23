using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using TicTacToe_WPF.back_end;
using TicTacToe_WPF.game_logic;
using TicTacToe_WPF.middleware_controller;

namespace TicTacToe_WPF
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public StartWindow parent = null;

        public XElement gamexml = null;

        private List<Button> gameButtons;

        public GameWindow()
        {
            InitializeComponent();

            gameButtons = new List<Button>();

            gameButtons.Add(A1);
            gameButtons.Add(A2);
            gameButtons.Add(A3);

            gameButtons.Add(B1);
            gameButtons.Add(B2);
            gameButtons.Add(B3);

            gameButtons.Add(C1);
            gameButtons.Add(C2);
            gameButtons.Add(C3);

            GameController.gameButtons = gameButtons;
            GameController.gameWindow = this;
        }

        public void gameAction_Click(object sender, RoutedEventArgs e)
        {
            // Make move
            Button pressedButton = (Button)sender;
            GameController.SetPlayerAction(pressedButton);
        }
        

        public void disableGame()
        {
            foreach(Button button in gameButtons)
            {
                button.IsEnabled = false;
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //case resume mode
            if (GameController.start_mode == Constants.RESUME_GAME)
            {
                this.LoadGame.Visibility = System.Windows.Visibility.Visible;
            }
            if (GameController.start_mode == Constants.PEER_TO_COMPUTER)
            {
                GameController.opponent_player_mode = Constants.COMPUTER;
            }
            if (GameController.start_mode == Constants.PEER_TO_PEER)
            {
                GameController.opponent_player_mode = Constants.HUMAN;
            }
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ( openFileDialog.ShowDialog() == false )
            {
                MessageBox.Show("could not resume game, because not found game.xml");
                return;
            }
            for (int i = 0; i < gameButtons.Count; i++)
            {
                gameButtons[i].Content = "";
                gameButtons[i].IsEnabled = true;
            }
            XmlModel.LoadGameXml(openFileDialog.FileName);

        }

        private void onPlayerX_Click(object sender, RoutedEventArgs e)
        {
            // Start the game
            // Activate all buttons and reset their texts
            foreach (Button button in gameButtons)
            {
                button.IsEnabled = true;button.Content = "";
            }

            GameController.StartGameByPlayer1();
            
        }

        private void onPlayerO_Click(object sender, RoutedEventArgs e)
        {
            // Start the game
            // Activate all buttons and reset their texts
            foreach (Button button in gameButtons)
            {
                button.IsEnabled = true; button.Content = "";
            }

            GameController.StartGameByPlayer2();
           
        }
        
    }
}
