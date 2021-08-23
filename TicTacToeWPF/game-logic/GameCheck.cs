using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicTacToe_WPF.game_logic
{
    public class GameCheck
    {
        public static void checkHorizontal(List<Button> gameButtons)
        {
            bool gameOver = false;
            string winner = "";
            // Horizontal check
            if (gameButtons[0].Content.Equals(gameButtons[1].Content)
                && gameButtons[0].Content.Equals(gameButtons[2].Content)
                && gameButtons[1].Content.Equals(gameButtons[2].Content)
                && !gameButtons[0].Content.Equals(""))
            {
                // Top row won
                //MessageBox.Show("top row");
                gameOver = true;
                winner = Convert.ToString(gameButtons[0].Content);
            }
            else if (gameButtons[3].Content.Equals(gameButtons[4].Content)
                    && gameButtons[3].Content.Equals(gameButtons[5].Content)
                    && gameButtons[4].Content.Equals(gameButtons[5].Content)
                    && !gameButtons[3].Content.Equals(""))
            {
                // Middle row won
                //MessageBox.Show("middle row");
                gameOver = true;
                winner = Convert.ToString(gameButtons[3].Content);
            }
            else if (gameButtons[6].Content.Equals(gameButtons[7].Content)
                    && gameButtons[6].Content.Equals(gameButtons[8].Content)
                    && gameButtons[7].Content.Equals(gameButtons[8].Content)
                    && !gameButtons[6].Content.Equals(""))
            {
                // Bottom row won
                //MessageBox.Show("bottom row");
                gameOver = true;
                winner = Convert.ToString(gameButtons[6].Content);
            }

            GameStatus.SetGameStatus(gameOver, winner, false); 
        }

        public static void checkVertical(List<Button> gameButtons)
        {
            bool gameOver = false;
            string winner = "";
            // Vertical check
            if (gameButtons[0].Content.Equals(gameButtons[3].Content)
                && gameButtons[0].Content.Equals(gameButtons[6].Content)
                && gameButtons[3].Content.Equals(gameButtons[6].Content)
                && !gameButtons[0].Content.Equals(""))
            {
                // Left column won
                //MessageBox.Show("left column");
                gameOver = true;
                winner = Convert.ToString(gameButtons[0].Content);
            }
            else if (gameButtons[1].Content.Equals(gameButtons[4].Content)
                    && gameButtons[1].Content.Equals(gameButtons[7].Content)
                    && gameButtons[4].Content.Equals(gameButtons[7].Content)
                    && !gameButtons[1].Content.Equals(""))
            {
                // Middle column won
                //MessageBox.Show("middle column");
                gameOver = true;
                winner = Convert.ToString(gameButtons[1].Content);
            }
            else if (gameButtons[2].Content.Equals(gameButtons[5].Content)
                    && gameButtons[2].Content.Equals(gameButtons[8].Content)
                    && gameButtons[5].Content.Equals(gameButtons[8].Content)
                    && !gameButtons[2].Content.Equals(""))
            {
                // Right column won
                //MessageBox.Show("right column");
                gameOver = true;
                winner = Convert.ToString(gameButtons[2].Content);
            }

            GameStatus.SetGameStatus(gameOver, winner, false);
        }

        public static void checkDiagonal(List<Button> gameButtons)
        {
            bool gameOver = false;
            string winner = "";
            // Diagonal check
            if (gameButtons[0].Content.Equals(gameButtons[4].Content)
                && gameButtons[0].Content.Equals(gameButtons[8].Content)
                && gameButtons[4].Content.Equals(gameButtons[8].Content)
                && !gameButtons[0].Content.Equals(""))
            {
                // Top left to bottom right won
                //MessageBox.Show("tl-br");
                gameOver = true;
                winner = Convert.ToString(gameButtons[0].Content);
            }
            else if (gameButtons[6].Content.Equals(gameButtons[4].Content)
                    && gameButtons[6].Content.Equals(gameButtons[2].Content)
                    && gameButtons[4].Content.Equals(gameButtons[2].Content)
                    && !gameButtons[6].Content.Equals(""))
            {
                // Bottom left to top right won
                //MessageBox.Show("bl-tr");
                gameOver = true;
                winner = Convert.ToString(gameButtons[6].Content);
            }

            GameStatus.SetGameStatus(gameOver, winner, false);
        }

        public static bool checkForTie(List<Button> gameButtons)
        {
            bool tie = true;
            foreach (Button button in gameButtons)
            {
                if (button.IsEnabled == true)
                {
                    tie = false;
                    break;
                }
            }

            return tie;
        }
    }
}
