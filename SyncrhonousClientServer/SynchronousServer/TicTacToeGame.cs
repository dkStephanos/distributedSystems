using System;
namespace SynchronousServer
{
    public class TicTacToeGame
    {

        string playerChoice;
        TicTacToeBoard board;

        public TicTacToeGame(string playerChoice)
        {
            this.playerChoice = playerChoice;
            this.board = new TicTacToeBoard();
        }

        public static string checkForResult(string[] boardCells)
        {
            //Check for win in first row
            if (boardCells[0] == boardCells[1] && boardCells[1] == boardCells[2])
            {
                return boardCells[0] + " won!";
            }

            //Check for win in second row
            else if (boardCells[3] == boardCells[4] && boardCells[4] == boardCells[5])
            {
                return boardCells[3] + " won!";
            }

            //Check for win in third row
            else if (boardCells[6] == boardCells[7] && boardCells[7] == boardCells[8])
            {
                return boardCells[6] + " won!";
            }

            //Check for win in first col
            else if (boardCells[0] == boardCells[3] && boardCells[3] == boardCells[6])
            {
                return boardCells[0] + " won!";
            }

            //Check for win in second col
            else if (boardCells[1] == boardCells[4] && boardCells[4] == boardCells[7])
            {
                return boardCells[1] + " won!";
            }

            //Check for win in third col
            else if (boardCells[2] == boardCells[5] && boardCells[5] == boardCells[8])
            {
                return boardCells[2] + " won!";
            }

            //Check for win in first diagonal
            else if (boardCells[0] == boardCells[4] && boardCells[4] == boardCells[8])
            {
                return boardCells[0] + " won!";
            }

            //Check for win in second diagonal
            else if (boardCells[2] == boardCells[4] && boardCells[4] == boardCells[6])
            {
                return boardCells[2] + " won!";
            }

            //Finally, check for draw 
            else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
            {
                return "Draw";
            }

            //If we get this far, there is no outcome yet, return None
            return "None";
        }
    }
}
