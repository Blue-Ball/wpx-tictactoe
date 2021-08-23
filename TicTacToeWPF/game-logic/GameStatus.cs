using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_WPF
{
    public static class GameStatus
    {
        private static bool gameOver;
        public static bool tie;
        public static string winner;

        public static void SetGameStatus(bool _gameOver, string _winner, bool _tie)
        {
            gameOver = _gameOver;
            winner = _winner;
            tie = _tie;
        }

        public static bool isGameOver() { return gameOver;  }


    }
}
