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
using static Tablero.Outputs;

namespace Tablero
{
    class Piece
    {
        public int type, team, x, y;
        public bool moved, promoted;

        public Piece(int type, int team, int x, int y)
        {
            this.type = type;
            this.team = team;
            this.x = x;
            this.y = y;
            moved = false;
            promoted = false;
        }

        public void SetDir(int x, int y)
        {
            this.x = x;
            this.y = y;
            moved = true;
        }
    }

    class Board
    {
        protected int x, y, turn = 1;
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

        protected void PrintPiece(int x, int y)
        {
            if (!IsPiece(x, y))
                Write("  ");
            else
            {
                Piece current = this[x, y];
                if (current.team == 1) //White
                    ForegroundColor = ConsoleColor.DarkYellow;
                else //Black
                    ForegroundColor = ConsoleColor.Black;
                Write("{0} ", (Pieces)current.type);
            }
        }

        public void PrintBoard()
        {
            ConsoleColor dblue = ConsoleColor.DarkBlue;
            ConsoleColor cyan = ConsoleColor.Cyan;
            ConsoleColor black = ConsoleColor.Black;
            ConsoleColor white = ConsoleColor.White;

            BackgroundColor = black;
            ForegroundColor = white;

            WriteLine("\n    {0} TURN", ((Teams)(turn % 2 == 0 ? 2 : 1)).ToString());
            Write("  ");
            for (int i = 0; i < x; i++)
                Write("{0} ", (Letters)i + 1);
            WriteLine();
            for (int i = 0; i < y; i++)
            {
                Write("{0} ", y - i);
                for (int j = 0; j < x; j++)
                {
                    BackgroundColor = (i + j) % 2 == 0 ? cyan : dblue;
                    PrintPiece(j + 1, this.y - i);
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

        protected void PlacePiece(Piece piece)
        {
            this[piece.x, piece.y] = piece;
        }

        protected bool IsInBounds(int x, int y)
        {
            if (x <= this.x && x > 0 && y <= this.y && y > 0)
                return true;
            else
                return false;
        }

        protected bool IsPiece(int x, int y)
        {
            return this[x, y] != null;
        }

        protected bool SameTeam(in Piece piece, int x, int y)
        {
            return this[x, y].team == piece.team;
        }

        protected bool EnemyExists(in Piece piece, int x, int y)
        {
            return IsPiece(x, y) && !SameTeam(piece, x, y);
        }

        protected bool YourTurn(int x, int y)
        {
            return turn % 2 == this[x, y].team % 2;
        }
    }

    class Chess : Board
    {
        public Chess()
        {
            this.x = 8;
            this.y = 8;
            this.board = new Piece[y, x];
            this.SetUp();
        }

        public void SetUp()
        {
            /*Piece p1w = new Piece(1, 1, 1, 2);
            Piece p2w = new (1, 1, 2, 2);
            Piece p3w = new(1, 1, 3, 2);
            Piece p4w = new(1, 1, 4, 2);
            Piece p5w = new (1, 1, 5, 2);
            Piece p6w = new (1, 1, 6, 2);
            Piece p7w = new (1, 1, 7, 2);
            Piece p8w = new (1, 1, 8, 2);
            Piece lRw = new (2, 1, 1, 1);
            Piece lNw = new (3, 1, 2, 1);
            Piece lBw = new (4, 1, 3, 1);
            Piece  Qw = new (5, 1, 4, 1);
            Piece  Kw = new (6, 1, 5, 1);
            Piece rBw = new (4, 1, 6, 1);
            Piece rNw = new (3, 1, 7, 1);
            Piece rRw = new (2, 1, 8, 1);
            Piece[] white = new Piece[16] { p1w, p2w, p3w, p4w, p5w, p6w, p7w, p8w, lRw, lNw, lBw, Qw, Kw, rBw, rNw, rRw };

            foreach (Piece w in white)
                    PlacePiece(w);

            Piece p1b = new (1, 2, 1, 7);
            Piece p2b = new (1, 2, 2, 7);
            Piece p3b = new (1, 2, 3, 7);
            Piece p4b = new (1, 2, 4, 7);
            Piece p5b = new (1, 2, 5, 7);
            Piece p6b = new (1, 2, 6, 7);
            Piece p7b = new (1, 2, 7, 7);
            Piece p8b = new (1, 2, 8, 7);
            Piece lRb = new (2, 2, 1, 8);
            Piece lNb = new (3, 2, 2, 8);
            Piece lBb = new (4, 2, 3, 8);
            Piece  Qb = new (5, 2, 4, 8);
            Piece  Kb = new (6, 2, 5, 8);
            Piece rBb = new (4, 2, 6, 8);
            Piece rNb = new (3, 2, 7, 8);
            Piece rRb = new (2, 2, 8, 8);
            Piece[] black = new Piece[16] { p1b, p2b, p3b, p4b, p5b, p6b, p7b, p8b, lRb, lNb, lBb, Qb, Kb, rBb, rNb, rRb };

            foreach (Piece b in black)
                    PlacePiece(b);*/
            Piece test1 = new Piece(6, 1, 5, 1);
            Piece test2 = new Piece(2, 1, 1, 1);
            Piece test3 = new Piece(2, 1, 8, 1);
            Piece test4 = new Piece(6, 2, 5, 8);
            Piece test5 = new Piece(2, 2, 1, 8);
            Piece test6 = new Piece(2, 2, 8, 8);
            PlacePiece(test1);
            PlacePiece(test2);
            PlacePiece(test3);
            PlacePiece(test4);
            PlacePiece(test5);
            PlacePiece(test6);
        }

        public Outputs AlgebraicNotation(string expression)
        { //0 is retry, 1 is successful move, 2 is game end
            int x, y, letter, number;
            try {
                x = (int)Enum.Parse(typeof(Letters), expression[0].ToString().ToUpper());
                y = Convert.ToInt32(expression[1].ToString());
                letter = (int)Enum.Parse(typeof(Letters), expression[2].ToString().ToUpper());
                number = Convert.ToInt32(expression[3].ToString());
            }
            catch {
                PrintBoard();
                WriteLine("ERROR: problem parsing instructions");
                return 0;
            }

            if (!IsPiece(x, y))
            {
                PrintBoard();
                WriteLine("ERROR: no piece");
                return NoPiece;
            }

            if(!YourTurn(x, y))
            {
                PrintBoard();
                WriteLine("ERROR: not your turn!");
                return WrongTurn;
            }

            string message;
            Outputs output = Step(this[x, y], letter, number);
            switch(output)
            {
                case UnknownError:
                    PrintBoard();
                    WriteLine("ERROR: unknown error");
                    break;
                case OutOfBounds:
                    PrintBoard();
                    WriteLine("ERROR: out of bounds");
                    break;
                case Blocked:
                    PrintBoard();
                    WriteLine("ERROR: piece blocking the way");
                    break;
                case IllegalMove:
                    PrintBoard();
                    WriteLine("ERROR: illegal move");
                    break;
                case NoPiece:
                    PrintBoard();
                    WriteLine("ERROR: no piece");
                    break;
                case Moved:
                    if (CanPromote(letter, number))
                        output = Promote(letter, number);
                    turn++;
                    PrintBoard();
                    message = ((Pieces)this[letter, number].type).ToString();
                    message += (((Letters)letter).ToString()).ToLower();
                    message += number;
                    WriteLine(message);
                    break;
                case Killed:
                    if (CanPromote(letter, number))
                        output = Promote(letter, number);
                    turn++;
                    PrintBoard();
                    message = ((Pieces)this[letter, number].type).ToString();
                    message += ":" + (((Letters)letter).ToString()).ToLower();
                    message += number;
                    WriteLine(message);
                    break;
                case CastledLong:
                    turn++;
                    PrintBoard();
                    WriteLine("O-O-O");
                    break;
                case CastledShort:
                    turn++;
                    PrintBoard();
                    WriteLine("O-O");
                    break;
                case BlackWon:
                    PrintBoard();
                    WriteLine("BLACK WINS!");
                    break;
                case WhiteWon:
                    PrintBoard();
                    WriteLine("WHITE WINS!");
                    break;
                default:
                    PrintBoard();
                    WriteLine("ERROR: problem moving piece");
                    break;
            }
            return output;
        }

        private Outputs Step(Piece piece, int x, int y)
        { //OutOfBounds, IllegalMove, Blocked, Moved, Killed, BW, WW
            if (!IsInBounds(x, y))
                return OutOfBounds;
            switch(piece.type)
            {
                case 1: //Pawn
                    if (PawnPos(piece.x, piece.y, x, y))    //Upwards
                    {
                        if (Math.Abs(x - piece.x) == 1)    //Diagonal
                        {
                            if (EnemyExists(piece, x, y))    //Can kill
                                return Kill(piece, x, y);    //Kills
                            else
                                return IllegalMove;
                        }
                        else if (x == piece.x)    //Straightforward
                        {
                            if (!IsPiece(x, y))    //It's empty
                                return Move(piece, x, y);    //Move
                            else
                                return Blocked;
                        }
                        else
                            return IllegalMove;
                    }
                    else if (StarterPawnPos(piece.x, piece.y, x, y))
                    {
                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else
                            return Blocked;
                    }
                    else
                        return IllegalMove;                    
                case 2: //Rook
                    if (HorizontalPos(piece.x, piece.y, x, y))    //Horizontal
                    {
                        int sum = (x - piece.x) / Math.Abs(x - piece.x);
                        for (int i = piece.x + sum; i != x; i += sum)
                            if (IsPiece(i, y))    //Blocking the path
                                return Blocked;

                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else
                            return Blocked;    //Friendly fire!                            
                    }
                    else if (VerticalPos(piece.x, piece.y, x, y))    //Vertical
                    {
                        int sum = (y - piece.y) / Math.Abs(y - piece.y);
                        for (int i = piece.y + sum; i != y; i += sum)
                            if (IsPiece(x, i))    //Blocking the path
                                return Blocked;

                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else
                            return Blocked;    //Friendly fire!
                    }
                    else
                        return IllegalMove;
                case 3: //Knight
                    if (KnightPos(piece.x, piece.y, x, y))    //In position
                    {
                        if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else if(!IsPiece(x, y))
                            return Move(piece, x, y);    //Move
                        else
                            return Blocked;
                    }
                    else
                        return IllegalMove;
                case 4: //Bishop
                    if (BishopPos(piece.x, piece.y, x, y))    //In position
                    {
                        int multx = (x - piece.x) / Math.Abs(x - piece.x);
                        int multy = (y - piece.y) / Math.Abs(y - piece.y);
                        int dist = Math.Abs(x - piece.x);
                        for (int i = 1; i < dist; i++)
                            if (IsPiece(multx*i + piece.x, multy*i + piece.y))    //Blocking the path
                                    return Blocked;

                            if (!IsPiece(x, y))    //It's empty
                                return Move(piece, x, y);    //Move
                            else if (EnemyExists(piece, x, y))    //Target acquired!
                                return Kill(piece, x, y);    //Kills
                            else
                                return Blocked;    //Friendly fire!
                    }
                    else
                        return IllegalMove;
                case 5: //Queen (copied the rook's + bishop's code)
                    if (HorizontalPos(piece.x, piece.y, x, y))    //Horizontal
                    {
                        int sum = (x - piece.x) / Math.Abs(x - piece.x);
                        for (int i = piece.x + sum; i != x; i += sum)
                            if (IsPiece(i, y))    //Blocking the path
                                return Blocked;

                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else
                            return Blocked;    //Friendly fire!                            
                    }
                    else if (VerticalPos(piece.x, piece.y, x, y))    //Vertical
                    {
                        int sum = (y - piece.y) / Math.Abs(y - piece.y);
                        for (int i = piece.y + sum; i != y; i += sum)
                            if (IsPiece(x, i))    //Blocking the path
                                return Blocked;

                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else
                            return Blocked;    //Friendly fire!
                    }
                    else if (BishopPos(piece.x, piece.y, x, y))    //In position
                    {
                        int multx = (x - piece.x) / Math.Abs(x - piece.x);
                        int multy = (y - piece.y) / Math.Abs(y - piece.y);
                        int dist = Math.Abs(x - piece.x);
                        for (int i = 1; i < dist; i++)
                            if (IsPiece(multx * i + piece.x, multy * i + piece.y))    //Blocking the path
                                return Blocked;

                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else
                            return Blocked;    //Friendly fire!
                    }
                    else
                        return IllegalMove;
                case 6: //King
                    if (KingPos(piece.x, piece.y, x, y))
                    {
                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else if (EnemyExists(piece, x, y))    //Target acquired!
                            return Kill(piece, x, y);    //Kills
                        else    //Friendly fire!
                            return Blocked;
                    }
                    else if(CastlingPos(piece.x, piece.y, x, y))
                    {
                        if (this[piece.x, piece.y].team == 1) //White
                        {
                            if (piece.x == 5 && x == 1)
                            {
                                Move(this[piece.x, piece.y], 2, 1);
                                Move(this[x, y], 3, 1);
                                return CastledLong;
                            }
                            else if ((piece.x == 5 && x == 8))
                            {
                                Move(this[piece.x, piece.y], 7, 1);
                                Move(this[x, y], 6, 1);
                                return CastledShort;
                            }
                            return IllegalMove;
                        }
                        else //Black
                        {
                            if (piece.x == 5 && x == 1)
                            {
                                Move(this[piece.x, piece.y], 2, 8);
                                Move(this[x, y], 3, 8);
                                return CastledLong;
                            }
                            else if ((piece.x == 5 && x == 8))
                            {
                                Move(this[piece.x, piece.y], 7, 8);
                                Move(this[x, y], 6, 8);
                                return CastledShort;
                            }
                            return IllegalMove;
                        }
                    }
                    else
                        return IllegalMove;
                default:
                    return NoPiece;
            }
        }

        private Outputs Move(Piece piece, int x, int y)
        {
            try {
                this[piece.x, piece.y] = null;
                piece.SetDir(x, y);
                PlacePiece(piece);
                return Moved;
            }
            catch {
                return UnknownError;
            }
        }

        private Outputs Kill(Piece piece, int x, int y)
        {
            try {  
                this[piece.x, piece.y] = null;
                piece.SetDir(x, y);
                if (this[x, y].type == 6)
                {  
                    if (this[x, y].team == 2)
                    {
                        PlacePiece(piece);
                        return WhiteWon;
                    }
                    else
                    {
                        PlacePiece(piece);
                        return BlackWon;
                    } 
                }
                else
                {
                    PlacePiece(piece);
                    return Killed;
                }
            }
            catch {
                return UnknownError;
            }
        }

        private Outputs Promote(int x, int y)
        {
            do
            {
                Clear();
                PrintBoard();
                WriteLine("Select a valid piece to promote to: ");
                Piece p;
                string c;
                try
                {
                    c = ReadLine().ToCharArray()[0].ToString();
                    if (c == Pieces.p.ToString() || c == Pieces.K.ToString())
                        continue;
                    p = new Piece((int)Enum.Parse(typeof(Pieces), c), this[x, y].team, x, y);
                    this[x, y].promoted = true;
                    PlacePiece(p);
                    Clear();
                    break;
                }
                catch
                {
                    continue;
                }
            }
            while (true);
            return Promoted;
        }

        private bool CastlingPos(int x0, int y0, int x1, int y1)
        {
            if (y0 == y1 && SameTeam(this[x0, y0], x1, y1) && this[x0, y0].type == 6 && this[x1, y1].type == 2 && !this[x0, y0].moved && !this[x1, y1].moved)
            {
                int sum = (x1 - x0) / Math.Abs(x1 - x0);
                for (int i = x0 + sum; i != x1; i += sum)
                    if (IsPiece(i, y1))    //Blocking the path
                        return false;
                return true;
            }
            else
                return false;
        }

        private bool CanPromote(int x, int y)
        {
            return this[x, y].promoted == false && this[x, y].type == (int)Pieces.p && (this[x, y].team == (int)Teams.White && y == 8) || (this[x, y].team == (int)Teams.Black && y == 1);
        }

        private bool PawnPos(int x0, int y0, int x1, int y1)
        {
            return (y1 - y0 == 1 && this[x0, y0].team == (int)Teams.White) || (y1 - y0 == -1 && this[x0, y0].team == (int)Teams.Black);
        }

        private static bool StarterPawnPos(int x0, int y0, int x1, int y1)
        {
            return x0 == x1 && ((y0 == 2 && y1 == 4) || (y0 == 7 && y1 == 5));
        }

        private static bool HorizontalPos(int x0, int y0, int x1, int y1)
        {
            return x0 != x1 && y0 == y1;
        }

        private static bool VerticalPos(int x0, int y0, int x1, int y1)
        {
            return x0 == x1 && y0 != y1;
        }

        private static bool KnightPos(int x0, int y0, int x1, int y1) //Verification for knight-victim position
        {
            return (Math.Abs(x0 - x1) == 2 && Math.Abs(y0 - y1) == 1) || (Math.Abs(x0 - x1) == 1 && Math.Abs(y0 - y1) == 2);
        }

        private static bool BishopPos(int x0, int y0, int x1, int y1)
        {
            return Math.Abs(x0 - x1) == Math.Abs(y0 - y1);
        }

        private static bool KingPos(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x0 - x1), dy = Math.Abs(y0 - y1);
            return dx < 2 && dy < 2 && (dx != 0 || dy != 0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World! {0}", (char)1);
            Board tab = new Board();
            //tab.printBoard();
            Chess c = new Chess();
            c.PrintBoard();
            /*string ch = "g";
            ch = ch.ToUpper();
            int i = (int)Enum.Parse(typeof(Letters), ch);
            int a = 4;
            char c1 = ((Pieces)a).ToString()[0];
            int s = c.algebraicNotation("b2b4");
            WriteLine(s);
            s = c.algebraicNotation("g4b4");
            WriteLine(s);*/
            Outputs output;
            string expression = ReadLine();
            Clear();
            output = c.AlgebraicNotation(expression);
            while(output != WhiteWon || output != BlackWon)
            {
                WriteLine(output);
                expression = ReadLine();
                Clear();
                output = c.AlgebraicNotation(expression);
            }
            ReadLine();
        }
    }
}
