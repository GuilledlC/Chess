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

        protected bool SamedTeam(in Piece piece, int x, int y) //delete :(
        {
            return this[x, y].team == piece.team;
        }

        protected bool EnemyExists(in Piece piece, int x, int y)
        {
            return IsPiece(x, y) && piece.IsEnemy(this[x, y]);
        }

        protected bool YourTurn(int x, int y)
        {
            return turn % 2 == this[x, y].team % 2;
        }
    }
}