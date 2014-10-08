using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A05_Connect_Four
{
    class Program
    {
        private static int[,] board = new int[6, 7];
        private static Dictionary<string, ConsoleColor> players = new Dictionary<string, ConsoleColor>();
        private static string currentPlayer;
        private static bool gameOver = false;

        static void Main(string[] args)
        {
            InitialSetup();

            DisplayBoard();
            
            foreach(KeyValuePair<string, ConsoleColor> pair in players)
            {
                if(pair.Value == ConsoleColor.Green)
                {
                    currentPlayer = pair.Key;
                }
            }

            while(!gameOver)
            {
                Console.ForegroundColor = players[currentPlayer];
                Console.Write("\n\n\t\t   What column would you like to drop in, {0}? ", currentPlayer);
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();
                int selection;

                if (input.Trim().Equals(""))
                {
                    Console.WriteLine("\nThat column doesn't exist. Please select another column.");
                }
                else
                {
                    selection = Convert.ToInt32(input);
                    selection--;

                    if (selection >= 0 && selection <= 6)
                    {
                        DropChip(selection);
                    }
                    else
                    {
                        DisplayBoard();
                        Console.WriteLine("\nThat column doesn't exist. Please select another column.");
                    }
                }
            }

            if(gameOver)
            {
                Console.ForegroundColor = players[currentPlayer];
                Console.WriteLine("\nThe winner is {0}!", currentPlayer);
            }
        }

        private static void InitialSetup()
        {
            Console.WriteLine("Please enter the name of the first player: ");
            string player1 = Console.ReadLine();
            players.Add(player1, ConsoleColor.Green);
            Console.WriteLine("{0}'s color is Green.", player1);
            Console.WriteLine("\n\nPlease enter the name of the second player: ");
            string player2 = Console.ReadLine();
            players.Add(player2, ConsoleColor.DarkCyan);
            Console.WriteLine("{0}'s color is Dark Cyan.", player2);
            Console.WriteLine("\n\nThank you for playing! The game will start shortly.");
            Thread.Sleep(4000);
        }

        private static void DisplayBoard()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\t\t\t\t 1 2 3 4 5 6 7\n");
            Console.ForegroundColor = ConsoleColor.White;
            string tabs = "\t\t\t\t ";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(tabs);
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(board[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else if(board[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(board[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(board[i, j]);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void GetOtherPlayer()
        {
            if(players[currentPlayer] == ConsoleColor.Green)
            {
                foreach(KeyValuePair<string,ConsoleColor> pair in players)
                {
                    if(pair.Value != ConsoleColor.Green)
                    {
                        currentPlayer = pair.Key;
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, ConsoleColor> pair in players)
                {
                    if (pair.Value != ConsoleColor.DarkCyan)
                    {
                        currentPlayer = pair.Key;
                    }
                }
            }
        }

        private static bool WinnerFound(int row, int col)
        {
            int playerNumber = board[row, col];
            int count = 1;
            int changeableRow = row;
            int changeableCol = col;

            //check up and down
            while(--changeableRow >= 0)
            {
                if(board[changeableRow, col] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            changeableRow = row;
            while(++changeableRow < board.GetLength(0))
            {
                if(board[changeableRow, col] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if(count >= 4)
            {
                return true;
            }
            
            //reset values changed in above code
            count = 1;
            changeableRow = row;

            //check left and right
            while (--changeableCol >= 0)
            {
                if (board[row, changeableCol] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            changeableCol = col;
            while (++changeableCol < board.GetLength(1))
            {
                if (board[row, changeableCol] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if (count >= 4)
            {
                return true;
            }

            //reset values changed in above code
            count = 1;
            changeableCol = col;

            //check bottom-right and top-left
            while (--changeableRow >= 0 && --changeableCol >= 0)
            {
                if (board[changeableRow, changeableCol] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            changeableCol = col;
            changeableRow = row;
            while (++changeableRow < board.GetLength(0) && ++changeableCol < board.GetLength(1))
            {
                if (board[changeableRow, changeableCol] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if (count >= 4)
            {
                return true;
            }

            //reset values changed by above code
            count = 1;
            changeableRow = row;
            changeableCol = col;

            //check bottom-left and top-right
            while (++changeableRow < board.GetLength(0) && --changeableCol >= 0)
            {
                if (board[changeableRow, changeableCol] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            changeableCol = col;
            changeableRow = row;
            while (--changeableRow >= 0 && ++changeableCol < board.GetLength(1))
            {
                if (board[changeableRow, changeableCol] == playerNumber)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if (count >= 4)
            {
                return true;
            }

            return false;
        }

        private static void DropChip(int selection)
        {
            if (board[0,selection] == 0)
            {
                DropMotion(selection);
                if (!gameOver)
                {
                    GetOtherPlayer();
                }
            }
            else
            {
                RejectDrop();
            }
        }

        private static void DropMotion(int selection)
        {
            for (int i = board.GetLength(0) - 1; i >= 0; i--)
            {
                if (board[i, selection] == 0)
                {
                    //falling motion
                    for (int row = 0; row < i; row++)
                    {
                        board[row, selection] = (players[currentPlayer] == ConsoleColor.Green) ? 1 : 2;
                        DisplayBoard();
                        Thread.Sleep(50);
                        board[row, selection] = 0;
                    }

                    board[i, selection] = (players[currentPlayer] == ConsoleColor.Green) ? 1 : 2;   
                    gameOver = WinnerFound(i, selection);
                    break;
                }
            }
            DisplayBoard();
        }

        private static void RejectDrop()
        {
            DisplayBoard();
            Console.WriteLine("This column is full already. Please select another column.");
        }
    }
}
