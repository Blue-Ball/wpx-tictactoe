using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicTacToe_WPF
{
    class ArtificialIntelligence
    {
        public static string AI_SYMBOL = Constants.O_SYMBOL;
        public static string ENEMY_SYMBOL = Constants.X_SYMBOL;

        public static Button performEasyMove(List<Button> buttons)
        {
            List<Button> notClicked = new List<Button>();

            foreach (Button button in buttons)
            {
                if (button.IsEnabled == true)
                {
                    notClicked.Add(button);
                }
            }

            Random random = new Random();
            int index = random.Next(0, notClicked.Count);
            Button move = notClicked.ElementAt(index);

            performMove(move);
            return move;
        }

        public static Button performHardMove(List<Button> buttons)
        {
            Button returnButton = null;

            returnButton = possibleWin(buttons);
            if (returnButton != null)
            {
                return returnButton;
            }

            returnButton = avoidEnemyWin(buttons);
            if (returnButton != null)
            {
                return returnButton;
            }

            returnButton = performEasyMove(buttons);

            return returnButton;
           
        }

        private static Button possibleWin(List<Button> buttons)
        {
            Button ret;
            if ( (ret=checkForHorizontalWin(buttons)) !=null)
            {
                return ret;
            }
            else if ((ret = checkForVerticalWin(buttons))!=null)
            {
                return ret;
            }
            else if ((ret=checkForDiagonalWin(buttons))!=null)
            {
                return ret;
            }

            return null;
        }

        private static Button avoidEnemyWin(List<Button> buttons)
        {
            Button ret = null;
            if ((ret=checkForEnemyHorizontalWin(buttons))!=null)
            {
                return ret;
            }
            else if ((ret=checkForEnemyVerticalWin(buttons))!=null)
            {
                return ret;
            }
            else if ((ret=checkForEnemyDiagonalWin(buttons))!=null)
            {
                return ret;
            }

            return null;
        }

        private static void prepareWin(List<Button> buttons)
        {

        }

        /******************
         * HELPER METHODS *
         ******************/
        private static bool isAi(Button button)
        {
            if (button.Content.Equals(AI_SYMBOL))
            {
                return true;
            }

            return false;
        }

        private static bool isEnemy(Button button)
        {
            if (button.Content.Equals(ENEMY_SYMBOL))
            {
                return true;
            }

            return false;
        }

        private static void performMove(Button button)
        {
            button.Content = AI_SYMBOL;
            button.IsEnabled = false;
        }

        /***********************************
         * OUTSOURCED LOGIC - WINNING MOVE *
         ***********************************/
        private static Button checkForHorizontalWin(List<Button> buttons)
        {
            for (int i = 0; i < 3; i++)
            {
                Button firstButton = buttons.ElementAt(i * 3);
                Button secondButton = buttons.ElementAt((i * 3) + 1);
                Button thirdButton = buttons.ElementAt((i * 3) + 2);

                if (isAi(firstButton) && isAi(secondButton))
                {
                    if (thirdButton.IsEnabled == true)
                    {
                        performMove(thirdButton);
                        return thirdButton;
                        
                    }
                }
                else if (isAi(firstButton) && isAi(thirdButton))
                {
                    if (secondButton.IsEnabled == true)
                    {
                        performMove(secondButton);
                        return secondButton;
                    }
                }
                else if (isAi(secondButton) && isAi(thirdButton))
                {
                    if (firstButton.IsEnabled == true)
                    {
                        performMove(firstButton);
                        return firstButton;
                    }
                }
            }

            return null;
        }

        private static Button checkForVerticalWin(List<Button> buttons)
        {
            for (int i = 0; i < 3; i++)
            {
                Button firstButton = buttons.ElementAt(i);
                Button secondButton = buttons.ElementAt(i + 3);
                Button thirdButton = buttons.ElementAt(i + (2 * 3));

                if (isAi(firstButton) && isAi(secondButton))
                {
                    if (thirdButton.IsEnabled == true)
                    {
                        performMove(thirdButton);
                        return thirdButton;
                    }
                }
                else if (isAi(firstButton) && isAi(thirdButton))
                {
                    if (secondButton.IsEnabled == true)
                    {
                        performMove(secondButton);
                        return secondButton;
                    }
                }
                else if (isAi(secondButton) && isAi(thirdButton))
                {
                    if (firstButton.IsEnabled == true)
                    {
                        performMove(firstButton);
                        return firstButton;
                    }
                }
            }

            return null;
        }

        public static Button checkForDiagonalWin(List<Button> buttons)
        {
            Button topLeft = buttons.ElementAt(0);
            Button topRight = buttons.ElementAt(2);
            Button center = buttons.ElementAt(4);
            Button bottomLeft = buttons.ElementAt(6);
            Button bottomRight = buttons.ElementAt(8);

            Button ret = null;
            if ( (ret=checkTopLeftToBottomRight(topLeft, center, bottomRight)) !=null)
            {
                return ret;
            }
            else if ( (ret=checkBottomLeftToTopRight(bottomLeft, center, topRight))!=null)
            {
                return ret;
            }

            return null;
        }

        private static Button checkTopLeftToBottomRight(Button topLeft, Button center, Button bottomRight)
        {
            if (isAi(topLeft) && isAi(center))
            {
                if (bottomRight.IsEnabled == true)
                {
                    performMove(bottomRight);
                    return bottomRight;
                }
            }
            else if (isAi(topLeft) && isAi(bottomRight))
            {
                if (center.IsEnabled == true)
                {
                    performMove(center);
                    return center;
                }
            }
            else if (isAi(center) && isAi(bottomRight))
            {
                if (topLeft.IsEnabled == true)
                {
                    performMove(topLeft);
                    return topLeft;
                }
            }

            return null;
        }

        private static Button checkBottomLeftToTopRight(Button bottomLeft, Button center, Button topRight)
        {
            if (isAi(bottomLeft) && isAi(center))
            {
                if (topRight.IsEnabled == true)
                {
                    performMove(topRight);
                    return topRight;
                }
            }
            else if (isAi(bottomLeft) && isAi(topRight))
            {
                if (center.IsEnabled == true)
                {
                    performMove(center);
                    return center;
                }
            }
            else if (isAi(center) && isAi(topRight))
            {
                if (bottomLeft.IsEnabled == true)
                {
                    performMove(bottomLeft);
                    return bottomLeft;
                }
            }

            return null;
        }

        /**********************************
         * OUTSOURCED LOGIC - AVOID ENEMY *
         **********************************/
        private static Button checkForEnemyHorizontalWin(List<Button> buttons)
        {
            for (int i = 0; i < 3; i++)
            {
                Button firstButton = buttons.ElementAt(i * 3);
                Button secondButton = buttons.ElementAt((i * 3) + 1);
                Button thirdButton = buttons.ElementAt((i * 3) + 2);

                if (isEnemy(firstButton) && isEnemy(secondButton))
                {
                    if (thirdButton.IsEnabled == true)
                    {
                        performMove(thirdButton);
                        return thirdButton;
                    }
                }
                else if (isEnemy(firstButton) && isEnemy(thirdButton))
                {
                    if (secondButton.IsEnabled == true)
                    {
                        performMove(secondButton);
                        return secondButton;
                    }
                }
                else if (isEnemy(secondButton) && isEnemy(thirdButton))
                {
                    if (firstButton.IsEnabled == true)
                    {
                        performMove(firstButton);
                        return firstButton;
                    }
                }
            }

            return null;
        }

        private static Button checkForEnemyVerticalWin(List<Button> buttons)
        {
            for (int i = 0; i < 3; i++)
            {
                Button firstButton = buttons.ElementAt(i);
                Button secondButton = buttons.ElementAt(i + 3);
                Button thirdButton = buttons.ElementAt(i + (2 * 3));

                if (isEnemy(firstButton) && isEnemy(secondButton))
                {
                    if (thirdButton.IsEnabled == true)
                    {
                        performMove(thirdButton);
                        return thirdButton;
                    }
                }
                else if (isEnemy(firstButton) && isEnemy(thirdButton))
                {
                    if (secondButton.IsEnabled == true)
                    {
                        performMove(secondButton);
                        return secondButton;
                    }
                }
                else if (isEnemy(secondButton) && isEnemy(thirdButton))
                {
                    if (firstButton.IsEnabled == true)
                    {
                        performMove(firstButton);
                        return firstButton;
                    }
                }
            }

            return null;
        }

        public static Button checkForEnemyDiagonalWin(List<Button> buttons)
        {
            Button topLeft = buttons.ElementAt(0);
            Button topRight = buttons.ElementAt(2);
            Button center = buttons.ElementAt(4);
            Button bottomLeft = buttons.ElementAt(6);
            Button bottomRight = buttons.ElementAt(8);

            Button ret = checkEnemyTopLeftToBottomRight(topLeft, center, bottomRight);
            if (ret!=null)
            {
                return ret;
            }
            else if ((ret=checkEnemyBottomLeftToTopRight(bottomLeft, center, topRight))!=null)
            {
                return ret;
            }

            return null;
        }

        private static Button checkEnemyTopLeftToBottomRight(Button topLeft, Button center, Button bottomRight)
        {
            if (isEnemy(topLeft) && isEnemy(center))
            {
                if (bottomRight.IsEnabled == true)
                {
                    performMove(bottomRight);
                    return bottomRight;
                }
            }
            else if (isEnemy(topLeft) && isEnemy(bottomRight))
            {
                if (center.IsEnabled == true)
                {
                    performMove(center);
                    return center;
                }
            }
            else if (isEnemy(center) && isEnemy(bottomRight))
            {
                if (topLeft.IsEnabled == true)
                {
                    performMove(topLeft);
                    return topLeft;
                }
            }

            return null;
        }

        private static Button checkEnemyBottomLeftToTopRight(Button bottomLeft, Button center, Button topRight)
        {
            if (isEnemy(bottomLeft) && isEnemy(center))
            {
                if (topRight.IsEnabled == true)
                {
                    performMove(topRight);
                    return topRight;
                }
            }
            else if (isEnemy(bottomLeft) && isEnemy(topRight))
            {
                if (center.IsEnabled == true)
                {
                    performMove(center);
                    return center;
                }
            }
            else if (isEnemy(center) && isEnemy(topRight))
            {
                if (bottomLeft.IsEnabled == true)
                {
                    performMove(bottomLeft);
                    return bottomLeft;
                }
            }

            return null;
        }
    }
}
