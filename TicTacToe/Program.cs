using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {

        static int X;
        static int Y;
        static Board board;
        static Game game;
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, do you want to play Tic-Tac-Toe? ");
            Console.WriteLine("If you do, press any key.  If you want to quit at any time hit the 'q' key");
            char play = Console.ReadKey().KeyChar;

            while (play !='q')
            {

                X = 0;
                Y = 5;

                board = new Board(X,Y);
                Console.SetCursorPosition(X, Y);
                Console.Write(board.toString());
                game = new Game(board);
                char winningPlayer = game.playGame();

                Console.SetCursorPosition(0, 14);

                if(winningPlayer == 'N')
                {
                    Console.WriteLine("Cats Game... Wah Wah.");
                }
                else
                {
                    Console.WriteLine($"{winningPlayer} wins!!!");
                }
                
                Console.WriteLine("Press any key to play again or q to quit.");
                play = Console.ReadKey().KeyChar;

                Console.Clear();

                //cursorX = 5;
                //cursorY = 7;
                //Console.WriteLine(Console.CursorLeft);
                //Console.WriteLine(Console.CursorTop);
                //Console.SetCursorPosition(cursorX, cursorY);
                //Console.ReadKey();

                
            }
            
        }

    }

    class Board
    {

        private int top;
        private int bottom;
        private int left;
        private int right;
        private Coordinate[] coords = new Coordinate[9];
        private int coordIndex = 0;
        public char[] grid = new char[9];
        private int cursorX;
        private int cursorY;

        public int CursorX
        {

            get
            {
                return cursorX;
            }
            set
            {
                cursorX = value;

                if(cursorX < left)
                {
                    cursorX = left + 1;
                }
                else if(cursorX > right)
                {
                    cursorX = right - 1;
                }
            }
        }

        public int CursorY
        {
            get
            {
                return cursorY;
            }
            set
            {
                cursorY = value;

                if(cursorY < top)
                {
                    cursorY = top;
                }
                else if(cursorY  > bottom)
                {
                    cursorY = bottom;
                }
            }
        }



        public Board(int left, int top)
        {
            this.top = top;
            this.bottom = top + 4;
            this.left = left;
            this.right = left + 10;
            cursorX = left + 5;
            cursorY = top + 2;
        }

        public void move(ConsoleKey direction)
        {

            switch (direction)
            {
                case ConsoleKey.UpArrow:
                    CursorY = CursorY - 2;
                    break;
                case ConsoleKey.DownArrow:
                    CursorY += 2;
                    break;
                case ConsoleKey.LeftArrow:
                    CursorX -= 4;
                    break;
                case ConsoleKey.RightArrow:
                    CursorX += 4;
                    break;
            }

            Console.SetCursorPosition(cursorX, cursorY);
        }

        public bool mark(char turn)
        {

            bool isNew = true;

            for(int i = 0; i < coordIndex; i++)
            {
                if (coords[i].Column == cursorX && coords[i].Row == cursorY) isNew = false;

            }


            if (isNew)
            {
                Coordinate newCoord = new Coordinate(cursorX, cursorY, turn);
                coords[coordIndex] = newCoord;
                Console.Write(turn);
                coordIndex++;

                makeGrid(newCoord);


            }

            return isNew;

        }

        //public char isThereAWinner()
        //{
        //    char isWinner = 'n';
        //    HashSet<char> horizontal = new HashSet<char>();
        //    HashSet<char> vertical = new HashSet<char>();
        //    HashSet<char> leftDiag = new HashSet<char>();
        //    HashSet<char> rightDiag = new HashSet<char>();

        //    if (coordIndex > 4)
        //    {

        //        for (int i = 0; i < 3; i++)
        //        {
        //            foreach (Coordinate crd in coords)
        //            {
        //                if (crd.Column == i)
        //                {
        //                    vertical.Add(crd.mark);
        //                }

        //                if (crd.Row == i)
        //                {
        //                    horizontal.Add(crd.mark);
        //                }

        //                if (crd.Column == i && crd.Row == i)
        //                {
        //                    leftDiag.Add(crd.mark);
        //                }

        //                if (crd.Column == 0 && crd.Row == 2)
        //                {
        //                    rightDiag.Add(crd.mark);
        //                }

        //                if (crd.Column == 1 && crd.Row == 1)
        //                {
        //                    rightDiag.Add(crd.mark);
        //                }

        //                if (crd.Column == 2 && crd.Row == 0)
        //                {
        //                    rightDiag.Add(crd.mark);
        //                }
        //            }

        //            if (horizontal.Count == 1)
        //            {
        //                if (horizontal.Contains('X'))
        //                {
        //                    isWinner = 'X';
        //                }
        //                else
        //                {
        //                    isWinner = 'O';
        //                }
        //            }

        //            if (vertical.Count == 1)
        //            {
        //                if (vertical.Contains('X'))
        //                {
        //                    isWinner = 'X';
        //                }
        //                else
        //                {
        //                    isWinner = 'O';
        //                }
        //            }

        //            if (leftDiag.Count == 1)
        //            {
        //                if (leftDiag.Contains('X'))
        //                {
        //                    isWinner = 'X';
        //                }
        //                else
        //                {
        //                    isWinner = 'O';
        //                }
        //            }

        //            if (rightDiag.Count == 1)
        //            {
        //                if (rightDiag.Contains('X'))
        //                {
        //                    isWinner = 'X';
        //                }
        //                else
        //                {
        //                    isWinner = 'O';
        //                }
        //            }
        //        }
        //    }

        //    return isWinner;
        //}


        public string toString()
        {
            String outString = "";
            outString = outString + "   |   |   \n";
            outString = outString + "---+---+---\n";
            outString = outString + "   |   |   \n";
            outString = outString + "---+---+---\n";
            outString = outString + "   |   |   \n";


            return outString;
        }

        private void makeGrid(Coordinate crd)
        {
            if(crd.Column == left + 1 && crd.Row == top)
            {
                grid[0] = crd.mark;
            }

            if (crd.Column == left + 5 && crd.Row == top)
            {
                grid[1] = crd.mark;
            }

            if (crd.Column == left + 9 && crd.Row == top)
            {
                grid[2] = crd.mark;
            }

            if (crd.Column == left + 1 && crd.Row == top + 2)
            {
                grid[3] = crd.mark;
            }

            if (crd.Column == left + 5 && crd.Row == top + 2)
            {
                grid[4] = crd.mark;
            }

            if (crd.Column == left + 9 && crd.Row == top + 2)
            {
                grid[5] = crd.mark;
            }

            if (crd.Column == left + 1 && crd.Row == top + 4)
            {
                grid[6] = crd.mark;
            }

            if (crd.Column == left + 5 && crd.Row == top + 4)
            {
                grid[7] = crd.mark;
            }

            if (crd.Column == left + 9 && crd.Row == top + 4)
            {
                grid[8] = crd.mark;
            }

        }

        
    }

    class Game
    {
        public char turn = 'X';
        public Boolean isWinner = false;
        public char Winner = 'N';
        public System.ConsoleKey enough = System.ConsoleKey.N;
        private int turnNumber = 0;
        private Board board;
        

        public Game(Board board)
        {
            this.board = board;

        }

        public char playGame()
        {

            Console.SetCursorPosition(board.CursorX, board.CursorY);
            

            while (enough != System.ConsoleKey.Q && !isWinner && turnNumber < 9)
            {
                //Console.WriteLine(board.CursorX);
                //Console.WriteLine(board.CursorY);
                ConsoleKeyInfo markerInput = Console.ReadKey(true);
                enough = markerInput.Key;
                //Console.WriteLine(enough);

                if(enough == System.ConsoleKey.Q)
                {
                    break;
                }
                else if(enough == ConsoleKey.UpArrow || enough == ConsoleKey.DownArrow || enough == ConsoleKey.LeftArrow || enough == ConsoleKey.RightArrow)
                {
                    board.move(enough);
                }
                else
                {
                    bool increment = board.mark(turn);

                    if (turn == 'X' && increment)
                    {
                        turn = 'O';
                        turnNumber++;
                    }
                    else if(increment)
                    {
                        turn = 'X';
                        turnNumber++;
                    }

                    
                }

                findWinner();   
            }

            return Winner;
            
        }

        public void findWinner()
        {
            if(board.grid[0] == 'X' &&  board.grid[1] == 'X' && board.grid[2] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[0] == 'O' && board.grid[1] == 'O' && board.grid[2] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[3] == 'X' && board.grid[4] == 'X' && board.grid[5] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[3] == 'O' && board.grid[4] == 'O' && board.grid[5] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[6] == 'X' && board.grid[7] == 'X' && board.grid[8] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[6] == 'O' && board.grid[7] == 'O' && board.grid[8] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[0] == 'X' && board.grid[3] == 'X' && board.grid[6] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[0] == 'O' && board.grid[3] == 'O' && board.grid[6] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[1] == 'X' && board.grid[4] == 'X' && board.grid[7] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[1] == 'O' && board.grid[4] == 'O' && board.grid[4] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[2] == 'X' && board.grid[5] == 'X' && board.grid[8] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[2] == 'O' && board.grid[5] == 'O' && board.grid[8] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[0] == 'X' && board.grid[4] == 'X' && board.grid[8] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[0] == 'O' && board.grid[4] == 'O' && board.grid[8] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }

            if (board.grid[6] == 'X' && board.grid[4] == 'X' && board.grid[2] == 'X')
            {
                Winner = 'X';
                isWinner = true;
            }
            else if (board.grid[6] == 'O' && board.grid[4] == 'O' && board.grid[2] == 'O')
            {
                Winner = 'O';
                isWinner = true;
            }
        }

    }

    class Coordinate
    {
        private int row;
        private int column;
        public char mark;

        public int Row
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
            }
        }

        public Coordinate(int X, int Y, char mark)
        {
            row = Y;
            column = X;
            this.mark = mark;
        }
    }

}
