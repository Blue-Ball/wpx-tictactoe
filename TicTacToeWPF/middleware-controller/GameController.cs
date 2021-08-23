using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TicTacToe_WPF.back_end;
using TicTacToe_WPF.game_logic;

namespace TicTacToe_WPF.middleware_controller
{
    public class GameController
    {
        public static int index = 0;
        public static GameWindow gameWindow;
        public static int start_mode = Constants.PEER_TO_COMPUTER;
        public static int opponent_player_mode = Constants.COMPUTER;
        public static bool TURN = true;        // true = X / false = 0

        public static List<Button> gameButtons;

        public static string Player1_Symbol = Constants.X_SYMBOL;
        public static string Player2_Symbol = Constants.O_SYMBOL;

        public static void RestoreStep(string pos, string value)
        {
            Button btn = GetButton(pos);
            if (btn != null)
            {
                btn.Content = value;
                btn.IsEnabled = false;

                //gameWindow.gameAction_Click(btn, null);
            }
        }
        private static Button GetButton(string pos)
        {
            switch (pos)
            {
                case "00": return gameButtons[0];
                case "01": return gameButtons[1];
                case "02": return gameButtons[2];
                case "10": return gameButtons[3];
                case "11": return gameButtons[4];
                case "12": return gameButtons[5];
                case "20": return gameButtons[6];
                case "21": return gameButtons[7];
                case "22": return gameButtons[8];
            }
            return null;
        }

        public static void SetPlayerAction(Button pressedButton)
        {
            index++;
            string type = "user";
            if (opponent_player_mode == Constants.COMPUTER &&
                (
                    ArtificialIntelligence.AI_SYMBOL == Constants.X_SYMBOL && GameController.TURN ||
                    ArtificialIntelligence.AI_SYMBOL == Constants.O_SYMBOL && !GameController.TURN
                ))
            { 
            
            }


            if (GameController.TURN)
            {
                pressedButton.Content = Player1_Symbol;
                pressedButton.IsEnabled = false;
                GameController.TURN = false;
            }
            else
            {
                pressedButton.Content = Player2_Symbol;
                pressedButton.IsEnabled = false;
                GameController.TURN = true;
            }
            XmlModel.AddActionXml(GetPosId(pressedButton), pressedButton.Content.ToString(), index, type);
            if (checkGameStatus())
            {
                return;
            }

            // Move AI if necessary
            if (GameController.opponent_player_mode == Constants.COMPUTER)
            {
                GameController.TURN = true;
                Button btnAI = ArtificialIntelligence.performHardMove(gameButtons);
                if (btnAI != null)
                {
                    index++;
                    type = "computer";
                    XmlModel.AddActionXml(GetPosId(btnAI), btnAI.Content.ToString(), index, type);
                }
            }

            checkGameStatus();
            //throw new NotImplementedException();
        }

        public static bool checkGameStatus()
        {
            GameCheck.checkHorizontal(gameButtons);
            if (GameStatus.isGameOver())
            {
                gameWindow.disableGame();
                MessageBox.Show("Player " + GameStatus.winner + " has won the game!");
                return true;
            }

            GameCheck.checkVertical(gameButtons);
            if (GameStatus.isGameOver())
            {
                gameWindow.disableGame();
                MessageBox.Show("Player " + GameStatus.winner + " has won the game!");
                return true;
            }

            GameCheck.checkDiagonal(gameButtons);
            if (GameStatus.isGameOver())
            {
                gameWindow.disableGame();
                MessageBox.Show("Player " + GameStatus.winner + " has won the game!");
                return true;
            }

            if (GameCheck.checkForTie(gameButtons))
            {
                gameWindow.disableGame();
                MessageBox.Show("The game ended in a tie!");
                return true;
            }

            return false;
        }

         public static void StartGameByPlayer1(){
            ArtificialIntelligence.AI_SYMBOL = Constants.O_SYMBOL;
            ArtificialIntelligence.ENEMY_SYMBOL = Constants.X_SYMBOL;

            Player1_Symbol = Constants.X_SYMBOL;
            Player2_Symbol = Constants.O_SYMBOL;
            // Reset member variables
            TURN = true;
            XmlModel.CreateGameXml();

            index = 0;
         }
         public static void StartGameByPlayer2()
         {
            XmlModel.CreateGameXml();

            index = 0;

             ArtificialIntelligence.AI_SYMBOL = Constants.X_SYMBOL;
             ArtificialIntelligence.ENEMY_SYMBOL = Constants.O_SYMBOL;

             Player1_Symbol = Constants.O_SYMBOL;
             Player2_Symbol = Constants.X_SYMBOL;

             // Reset member variables
             TURN = false;

             if (opponent_player_mode == Constants.COMPUTER)
             {
                 Button btnAI = ArtificialIntelligence.performHardMove(gameButtons);
                TURN = true;
                if (btnAI != null)
                {
                    index++;
                    string type = "computer";
                    XmlModel.AddActionXml(GetPosId(btnAI), btnAI.Content.ToString(), index, type);
                }
             }
             else if(opponent_player_mode == Constants.HUMAN)
            {
                TURN = true;
            }

             index = 0;
         }

        private static string GetPosId(Button pressedButton)
        {
            string strRet = "";
            for (int i = 0; i < gameButtons.Count; i++)
            {
                if (gameButtons[i].Equals(pressedButton))
                //if (gameButtons == pressedButton)
                {
                    switch (i)
                    { 
                        case 0: strRet ="00" ; break;
                        case 1: strRet ="01" ; break;
                        case 2: strRet ="02" ; break;
                        case 3: strRet ="10" ; break;
                        case 4: strRet ="11" ; break;
                        case 5: strRet ="12" ; break;
                        case 6: strRet ="20" ; break;
                        case 7: strRet ="21" ; break;
                        case 8: strRet ="22" ; break;
                        default: break;
                    }
                    return strRet;
                }
            }
            return "";
        }
    }
}
