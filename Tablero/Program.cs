/*Copyright (c) 2021, Guillermo de la Cal All rights reserved. Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
	1 - Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
	2 - Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer
		in the documentation and/or other materials provided with the distribution. THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS
		AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
		MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
		LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
		PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
		THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
		OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/

using System;
using static System.Console; //Very useful!

namespace Tablero
{
    class Piece
    {
        public int type, team, x, y;

        public Piece(int type, int team, int x, int y)
        {
            this.type = type;
            this.team = team;
            this.x = x;
            this.y = y;
        }

        public void setDir(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Board
    {
        protected int x, y;
        protected Piece[,] board;

        public Piece this[int x, int y]
        {
            get
            {
                return board[this.y - y, x - 1];
            }
            set
            {
                board[this.y - y, x - 1] = value;
            }
        }

        public Board(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.board = new Piece[y, x];
        }
        public Board()
        {
            this.x = 8;
            this.y = 8;
            this.board = new Piece[y, x];
        }

        protected void printPiece(int x, int y)
        {
            Piece current = this[x, y];
            if (current == null)
                Write("  ");
            else
            {
                if (current.team == 1) //White
                    ForegroundColor = ConsoleColor.DarkYellow;
                else //Black
                    ForegroundColor = ConsoleColor.Black;
                Write("{0} ", (Pieces)current.type);
            }
        }

        public void printBoard()
        {
            ConsoleColor dblue = ConsoleColor.DarkBlue;
            ConsoleColor cyan = ConsoleColor.Cyan;
            ConsoleColor black = ConsoleColor.Black;
            ConsoleColor white = ConsoleColor.White;

            BackgroundColor = black;
            ForegroundColor = white;

            Write("\n  ");
            for (int i = 0; i < x; i++)
                Write("{0} ", (Letters)i + 1);
            WriteLine();
            for (int i = 0; i < y; i++)
            {
                Write("{0} ", y - i);
                for (int j = 0; j < x; j++)
                {
                    BackgroundColor = (i + j) % 2 == 0 ? cyan : dblue;
                    printPiece(j + 1, this.y - i);
                }
                BackgroundColor = black;
                ForegroundColor = white;
                WriteLine("{0} ", y - i);
            }
            Write("  ");
            for (int i = 0; i < x; i++)
                Write("{0} ", (Letters)i + 1);
            WriteLine();
        }

        protected void placePiece(Piece piece)
        {
            this[piece.x, piece.y] = piece;
        }

        protected bool isInBounds(int x, int y)
        {
            if (x <= this.x && x > 0 && y <= this.y && y > 0)
                return true;
            else
                return false;
        }

        protected bool isPiece(int x, int y)
        {
            return this[x, y] != null;
        }

        protected bool sameTeam(in Piece piece, int x, int y)
        {
            return this[x, y].team == piece.team;
        }

        protected bool enemyExists(in Piece piece, int x, int y)
        {
            return isPiece(x, y) && !sameTeam(piece, x, y);
        }
    }

    class Chess : Board
    {
        public Chess()
        {
            this.x = 8;
            this.y = 8;
            this.board = new Piece[y, x];
            this.setUp();
        }

        public void setUp()
        {
            Piece p1w = new Piece(1, 1, 1, 2);
            Piece p2w = new Piece(1, 1, 2, 2);
            Piece p3w = new Piece(1, 1, 3, 2);
            Piece p4w = new Piece(1, 1, 4, 2);
            Piece p5w = new Piece(1, 1, 5, 2);
            Piece p6w = new Piece(1, 1, 6, 2);
            Piece p7w = new Piece(1, 1, 7, 2);
            Piece p8w = new Piece(1, 1, 8, 2);
            Piece lRw = new Piece(2, 1, 1, 1);
            Piece lNw = new Piece(3, 1, 2, 1);
            Piece lBw = new Piece(4, 1, 3, 1);
            Piece  Qw = new Piece(5, 1, 4, 1);
            Piece  Kw = new Piece(6, 1, 5, 1);
            Piece rBw = new Piece(4, 1, 6, 1);
            Piece rNw = new Piece(3, 1, 7, 1);
            Piece rRw = new Piece(2, 1, 8, 1);
            Piece[] white = new Piece[16] { p1w, p2w, p3w, p4w, p5w, p6w, p7w, p8w, lRw, lNw, lBw, Qw, Kw, rBw, rNw, rRw };

            foreach (Piece w in white)
                    placePiece(w);

            Piece p1b = new Piece(1, 2, 1, 7);
            Piece p2b = new Piece(1, 2, 2, 7);
            Piece p3b = new Piece(1, 2, 3, 7);
            Piece p4b = new Piece(1, 2, 4, 7);
            Piece p5b = new Piece(1, 2, 5, 7);
            Piece p6b = new Piece(1, 2, 6, 7);
            Piece p7b = new Piece(1, 2, 7, 7);
            Piece p8b = new Piece(1, 2, 8, 7);
            Piece lRb = new Piece(2, 2, 1, 8);
            Piece lNb = new Piece(3, 2, 2, 8);
            Piece lBb = new Piece(4, 2, 3, 8);
            Piece  Qb = new Piece(5, 2, 4, 8);
            Piece  Kb = new Piece(6, 2, 5, 8);
            Piece rBb = new Piece(4, 2, 6, 8);
            Piece rNb = new Piece(3, 2, 7, 8);
            Piece rRb = new Piece(2, 2, 8, 8);
            Piece[] black = new Piece[16] { p1b, p2b, p3b, p4b, p5b, p6b, p7b, p8b, lRb, lNb, lBb, Qb, Kb, rBb, rNb, rRb };

            foreach (Piece b in black)
                    placePiece(b);
        }

        public bool algebraicNotation(string expression)
        {
            int x, y, letter, number;
            try {
                x = Convert.ToInt32(expression[0].ToString());
                y = Convert.ToInt32(expression[1].ToString());
                letter = Convert.ToInt32(expression[2].ToString());
                number = Convert.ToInt32(expression[3].ToString());
            }
            catch {
                return false;
            }

            try {
                return move(this[x, y], letter, number);
            }
            catch {
                return false;
            }
        }

        private bool move(Piece piece, int x, int y)
        {
            if (!isInBounds(x, y))
                return false;
            switch(piece.type)
            {
                case 1: //Pawn
                    if (y == piece.y + 1)    //Upwards
                    {
                        if (Math.Abs(x - piece.x) == 1)    //Diagonal
                        {
                            if (enemyExists(piece, x, y))    //Can kill
                            {
                                piece.setDir(x, y);    //Kills
                                placePiece(piece);
                            }
                            else
                                return false;
                        }
                        else if (x == piece.x)    //Straightforward
                        {
                            if (!isPiece(x, y))    //It's empty
                            {
                                this[piece.x, piece.y] = null;    //Move
                                piece.setDir(x, y);
                                placePiece(piece);
                            }
                            else
                                return false;
                                
                        }
                        else
                            return false;
                    }
                    else if (starterPawnPos(piece.x, piece.y, x, y))
                    {
                        if (!isPiece(x, y))    //It's empty
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else
                            return false;
                    }
                    else
                        return false;                    
                    break;
                case 2: //Rook
                    if (horizontalPos(piece.x, piece.y, x, y))    //Horizontal
                    {
                        int sum = (x - piece.x) / Math.Abs(x - piece.x);
                        for (int i = piece.x; i != x; i += sum)
                        {
                            if (isPiece(i, y))    //Blocking the path
                                return false;
                        }

                        if (!isPiece(x, y))    //It's empty
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else if (enemyExists(piece, x, y))    //Target acquired!
                        {
                            piece.setDir(x, y);    //Kills
                            placePiece(piece);
                        }
                        else
                            return false;    //Friendly fire!
                    }
                    else if (verticalPos(piece.x, piece.y, x, y))              //Vertical
                    {
                        int sum = (y - piece.y) / Math.Abs(y - piece.y);
                        for (int i = piece.y; i != y; i += sum)
                        {
                            if (isPiece(x, i))    //Blocking the path
                                return false;
                        }

                        if (!isPiece(x, y))    //It's empty
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else if (enemyExists(piece, x, y))    //Target acquired!
                        {
                            piece.setDir(x, y);    //Kills
                            placePiece(piece);
                        }
                        else
                            return false;    //Friendly fire!
                    }
                    else
                        return false;
                    break;
                case 3: //Knight
                    if (knightPos(piece.x, piece.y, x, y))    //In position
                    {
                        if (enemyExists(piece, x, y))    //Target acquired!
                        {
                            piece.setDir(x, y);    //Kills
                            placePiece(piece);
                        }
                        else if(!isPiece(x, y))
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                    break;
                case 4: //Bishop
                    if (bishopPos(piece.x, piece.y, x, y))    //In position
                    {
                        int sumx = (x - piece.x) / Math.Abs(x - piece.x);
                        int sumy = (y - piece.y) / Math.Abs(y - piece.y);
                        for (int i = piece.x; i != x; i += sumx)
                        {
                            for (int j = piece.y; j != y; j += sumy)
                            {
                                if (isPiece(i, j))    //Blocking the path
                                    return false;
                            }

                            if (!isPiece(x, y))    //It's empty
                            {
                                this[piece.x, piece.y] = null;    //Move
                                piece.setDir(x, y);
                                placePiece(piece);
                            }
                            else if (enemyExists(piece, x, y))    //Target acquired!
                            {
                                piece.setDir(x, y);    //Kills
                                placePiece(piece);
                            }
                            else
                                return false;    //Friendly fire!
                        }
                    }
                    else
                        return false;
                    break;
                case 5: //Queen (copied the rook's + bishop's code)
                    if (horizontalPos(piece.x, piece.y, x, y))    //Horizontal
                    {
                        int sum = (x - piece.x) / Math.Abs(x - piece.x);
                        for (int i = piece.x; i != x; i += sum)
                        {
                            if (isPiece(i, y))    //Blocking the path
                                return false;
                        }

                        if (!isPiece(x, y))    //It's empty
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else if (enemyExists(piece, x, y))    //Target acquired!
                        {
                            piece.setDir(x, y);    //Kills
                            placePiece(piece);
                        }
                        else
                            return false;    //Friendly fire!
                    }
                    else if (verticalPos(piece.x, piece.y, x, y))              //Vertical
                    {
                        int sum = (y - piece.y) / Math.Abs(y - piece.y);
                        for (int i = piece.y; i != y; i += sum)
                        {
                            if (isPiece(x, i))    //Blocking the path
                                return false;
                        }

                        if (!isPiece(x, y))    //It's empty
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else if (enemyExists(piece, x, y))    //Target acquired!
                        {
                            piece.setDir(x, y);    //Kills
                            placePiece(piece);
                        }
                        else
                            return false;    //Friendly fire!
                    }
                    else if (bishopPos(piece.x, piece.y, x, y))    //In position
                    {
                        int sumx = (x - piece.x) / Math.Abs(x - piece.x);
                        int sumy = (y - piece.y) / Math.Abs(y - piece.y);
                        for (int i = piece.x; i != x; i += sumx)
                        {
                            for (int j = piece.y; j != y; j += sumy)
                            {
                                if (isPiece(i, j))    //Blocking the path
                                    return false;
                            }

                            if (!isPiece(x, y))    //It's empty
                            {
                                this[piece.x, piece.y] = null;    //Move
                                piece.setDir(x, y);
                                placePiece(piece);
                            }
                            else if (enemyExists(piece, x, y))    //Target acquired!
                            {
                                piece.setDir(x, y);    //Kills
                                placePiece(piece);
                            }
                            else
                                return false;    //Friendly fire!
                        }
                    }
                    else
                        return false;
                    break;
                case 6: //King
                    if (kingPos(piece.x, piece.y, x, y))
                    {
                        if (!isPiece(x, y))    //It's empty
                        {
                            this[piece.x, piece.y] = null;    //Move
                            piece.setDir(x, y);
                            placePiece(piece);
                        }
                        else if (enemyExists(piece, x, y))    //Target acquired!
                        {
                            piece.setDir(x, y);    //Kills
                            placePiece(piece);
                        }
                        else    //Friendly fire!
                            return false;
                    }
                    else
                        return false;
                    break;
            }
            return true;
        }

        private bool starterPawnPos(int x0, int y0, int x1, int y1)
        {
            return x0 == x1 && ((y0 == 2 && y1 == 4) || (y0 == 7 && y1 == 5));
        }

        private bool horizontalPos(int x0, int y0, int x1, int y1)
        {
            return x != x0 && y == y0;
        }

        private bool verticalPos(int x0, int y0, int x1, int y1)
        {
            return x == x0 && y != y0;
        }

        private bool knightPos(int x0, int y0, int x1, int y1) //Verification for knight-victim position
        {
            return (Math.Abs(x0 - x1) == 2 && Math.Abs(y0 - y1) == 1) || (Math.Abs(x0 - x1) == 1 && Math.Abs(y0 - y1) == 2);
        }

        private bool bishopPos(int x0, int y0, int x1, int y1)
        {
            return Math.Abs(x0 - x1) == Math.Abs(y0 - y1);
        }

        private bool kingPos(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x0 - x), dy = Math.Abs(y0 - y);
            return dx < 2 && dy < 2 && (x != 0 || y != 0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World! {0}", (char)1);
            Board tab = new Board();
            tab.printBoard();
            Chess c = new Chess();
            c.printBoard();
            string ch = "p";
            int i = (int)Enum.Parse(typeof(Pieces), ch);
            Console.WriteLine(i);
            if (c.algebraicNotation("2225"))
                WriteLine("AAAAAAAAAAAAAAAA");
            else
                WriteLine("BBBBBBBBBBBBBBBB");
            c.printBoard();

        }
    }
}
