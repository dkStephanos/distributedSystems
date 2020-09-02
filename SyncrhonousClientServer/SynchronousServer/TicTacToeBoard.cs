using System;
namespace SynchronousServer
{
    public class TicTacToeBoard
    {
        public TicTacToeBoard()
        {
            string[] boardCells = new string[9];
        }

        private static string drawBoard(string[] boardCells)
        {
            string currBoard = "";

            currBoard += "     |     |      ";

            currBoard += String.Format("  {0}  |  {1}  |  {2}", boardCells[0], boardCells[1], boardCells[2]);

            currBoard += "_____|_____|_____ ";

            currBoard += "     |     |      ";

            currBoard += String.Format("  {0}  |  {1}  |  {2}", boardCells[3], boardCells[4], boardCells[5]);

            currBoard += "_____|_____|_____ ";

            currBoard += "     |     |      ";

            currBoard += String.Format("  {0}  |  {1}  |  {2}", boardCells[6], boardCells[7], boardCells[8]);

            currBoard += "     |     |      ";

            return currBoard;

        }
    }
}
