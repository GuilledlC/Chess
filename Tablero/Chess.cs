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
using System.Collections.Generic;
using static System.Console; //Very useful!
using static Tablero.Outputs;

namespace Tablero
{
    class Chess : Board
    {
        public List<Piece> whitePieces = new();
        public List<Piece> blackPieces = new();

        public Chess()
        {
            this.x = 8;
            this.y = 8;
            this.board = new Piece[y, x];
            this.SetUp();
        }
        
        public void SetUp()
        {
            Piece p1w = new(1, 1, 1, 2);
            Piece p2w = new(1, 1, 2, 2);
            Piece p3w = new(1, 1, 3, 2);
            Piece p4w = new(1, 1, 4, 2);
            Piece p5w = new(1, 1, 5, 2);
            Piece p6w = new(1, 1, 6, 2);
            Piece p7w = new(1, 1, 7, 2);
            Piece p8w = new(1, 1, 8, 2);
            Piece lRw = new(2, 1, 1, 1);
            Piece lNw = new(3, 1, 2, 1);
            Piece lBw = new(4, 1, 3, 1);
            Piece Qw = new(5, 1, 4, 1);
            Piece Kw = new(6, 1, 5, 1);
            Piece rBw = new(4, 1, 6, 1);
            Piece rNw = new(3, 1, 7, 1);
            Piece rRw = new(2, 1, 8, 1);
            whitePieces.Add(p1w);
            whitePieces.Add(p2w);
            whitePieces.Add(p3w);
            whitePieces.Add(p4w);
            whitePieces.Add(p5w);
            whitePieces.Add(p6w);
            whitePieces.Add(p7w);
            whitePieces.Add(p8w);
            whitePieces.Add(lRw);
            whitePieces.Add(lNw);
            whitePieces.Add(lBw);
            whitePieces.Add(Qw);
            whitePieces.Add(Kw);
            whitePieces.Add(rBw);
            whitePieces.Add(rNw);
            whitePieces.Add(rRw);
            foreach (Piece w in whitePieces)
                PlacePiece(w);

            Piece p1b = new(1, 2, 1, 7);
            Piece p2b = new(1, 2, 2, 7);
            Piece p3b = new(1, 2, 3, 7);
            Piece p4b = new(1, 2, 4, 7);
            Piece p5b = new(1, 2, 5, 7);
            Piece p6b = new(1, 2, 6, 7);
            Piece p7b = new(1, 2, 7, 7);
            Piece p8b = new(1, 2, 8, 7);
            Piece lRb = new(2, 2, 1, 8);
            Piece lNb = new(3, 2, 2, 8);
            Piece lBb = new(4, 2, 3, 8);
            Piece Qb = new(5, 2, 4, 8);
            Piece Kb = new(6, 2, 5, 8);
            Piece rBb = new(4, 2, 6, 8);
            Piece rNb = new(3, 2, 7, 8);
            Piece rRb = new(2, 2, 8, 8);
            blackPieces.Add(p1b);
            blackPieces.Add(p2b);
            blackPieces.Add(p3b);
            blackPieces.Add(p4b);
            blackPieces.Add(p5b);
            blackPieces.Add(p6b);
            blackPieces.Add(p7b);
            blackPieces.Add(p8b);
            blackPieces.Add(lRb);
            blackPieces.Add(lNb);
            blackPieces.Add(lBb);
            blackPieces.Add(Qb);
            blackPieces.Add(Kb);
            blackPieces.Add(rBb);
            blackPieces.Add(rNb);
            blackPieces.Add(rRb);
            foreach (Piece b in blackPieces)
                PlacePiece(b);

            /*test1 = new(6, 1, 3, 3);
            whitePieces.Add(test1);
            PlacePiece(test1);

            Piece test2 = new(2, 2, 7, 2);
            blackPieces.Add(test2);
            PlacePiece(test2);*/
        }

        public Outputs AlgebraicNotation(string expression)
        { //0 is retry, 1 is successful move, 2 is game end
            if (expression.ToUpper() == "WW")
                return WhiteWon;
            else if (expression.ToUpper() == "BW")
                return BlackWon;

            int x, y, letter, number;
            try
            {
                x = (int)Enum.Parse(typeof(Letters), expression[0].ToString().ToUpper());
                y = Convert.ToInt32(expression[1].ToString());
                letter = (int)Enum.Parse(typeof(Letters), expression[2].ToString().ToUpper());
                number = Convert.ToInt32(expression[3].ToString());
            }
            catch
            {
                PrintBoard();
                WriteLine("ERROR: problem parsing instructions");
                return UnknownError;
            }

            if (!IsPiece(x, y))
            {
                PrintBoard();
                WriteLine("ERROR: no piece");
                return NoPiece;
            }

            if (!YourTurn(x, y))
            {
                PrintBoard();
                WriteLine("ERROR: not your turn!");
                return WrongTurn;
            }

            int type = 1, team = 1;
            bool moved, promoted;
            List<Piece> currentTeam;
            Piece currentKing;
            if (turn % 2 == 1)
                currentTeam = whitePieces;
            else
                currentTeam = blackPieces;

            currentKing = currentTeam.Find(x => x.type == (int)Pieces.K);
            foreach (Piece p in currentTeam)
                if (p.passantVictim)
                    p.passantVictim = false;

            if (Check(currentKing) && IsPiece(letter, number))
            {
                type = this[letter, number].type;
                team = this[letter, number].team;
                moved = this[letter, number].moved;
                promoted = this[letter, number].promoted;
            }

            string message;
            Outputs output = Step(this[x, y], letter, number);
            switch (output)
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

            if (Check(currentKing))
            {
                Move(this[letter, number], x, y);
                if (output == Killed)
                    this[letter, number] = new Piece(type, team, letter, number);
                else if (output == Promoted)
                    this[x, y].type = (int)Pieces.p;
                else if (output == CastledShort)
                    Move(this[6, y], 8, y);
                else if (output == CastledLong)
                    Move(this[4, y], 1, y);
                turn--;
                output = Checked;
                Clear();
                PrintBoard();
            }

            return output;
        }

        private Outputs Step(Piece piece, int x, int y)
        { //OutOfBounds, IllegalMove, Blocked, Moved, Killed, BW, WW
            if (!IsInBounds(x, y))
                return OutOfBounds;
            switch (piece.type)
            {
                case 1: //Pawn
                    if (PawnMove(piece.x, piece.y, x, y))    //Upwards and Straightforward
                    {
                        if (!IsPiece(x, y))    //It's empty
                            return Move(piece, x, y);    //Move
                        else
                            return Blocked;
                    }
                    else if (PawnKill(piece.x, piece.y, x, y))    //Upwards and Diagonal
                    {
                        if (EnemyExists(piece, x, y))    //Can kill
                            return Kill(piece, x, y);    //Kills
                        else if (PassantExists(piece.team, x, y))    //En Passant
                        {
                            Move(this[x, y + (piece.team == 1?-1:1)], x, y);
                            return Kill(piece, x, y);
                        }
                        else
                            return IllegalMove;
                    }
                    else if (StarterPawnPos(piece.x, piece.y, x, y))
                    {
                        if (!IsPiece(x, y))    //It's empty
                        {
                            piece.passantVictim = true;
                            return Move(piece, x, y);    //Move
                        }
                        else
                            return Blocked;
                    }
                    else
                        return IllegalMove;
                case 2: //Rook
                    if (HorizontalPos(piece.x, piece.y, x, y))    //Horizontal
                    {
                        if (BlockingPath(piece.x, piece.y, x, y, 'H'))
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
                        if (BlockingPath(piece.x, piece.y, x, y, 'V'))
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
                        else if (!IsPiece(x, y))
                            return Move(piece, x, y);    //Move
                        else
                            return Blocked;
                    }
                    else
                        return IllegalMove;
                case 4: //Bishop
                    if (BishopPos(piece.x, piece.y, x, y))    //In position
                    {
                        if (BlockingPath(piece.x, piece.y, x, y, 'D'))
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
                        if (BlockingPath(piece.x, piece.y, x, y, 'H'))
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
                        if (BlockingPath(piece.x, piece.y, x, y, 'V'))
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
                        if (BlockingPath(piece.x, piece.y, x, y, 'D'))
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
                    else if (CastlingPos(piece.x, piece.y, x, y))
                    {
                        if (piece.x == 5 && x == 1) //Long
                            {
                                Move(this[piece.x, piece.y], 3, piece.y);
                                Move(this[x, y], 4, piece.y);
                                return CastledLong;
                            }
                        else if ((piece.x == 5 && x == 8)) //Short
                        {
                            Move(this[piece.x, piece.y], 7, piece.y);
                            Move(this[x, y], 6, piece.y);
                            return CastledShort;
                        }
                        return IllegalMove;
                    }
                    else
                        return IllegalMove;
                default:
                    return NoPiece;
            }
        }

        private Outputs Move(Piece piece, int x, int y)
        {
            try
            {
                this[piece.x, piece.y] = null;
                piece.SetDir(x, y);
                PlacePiece(piece);
                return Moved;
            }
            catch
            {
                return UnknownError;
            }
        }

        private Outputs Kill(Piece piece, int x, int y)
        {
            try
            {
                this[piece.x, piece.y] = null;
                piece.SetDir(x, y);
                Enemies(piece).Remove(this[x, y]);
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
            catch
            {
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
                string c;
                try
                {
                    c = ReadLine().ToCharArray()[0].ToString().ToUpper();
                    if (c == Pieces.p.ToString() || c == Pieces.K.ToString())
                        continue;
                    this[x, y].Promote((int)Enum.Parse(typeof(Pieces), c));
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

        private List<Piece> Enemies(Piece piece)
        {
            if (piece.team == (int)Teams.White)
                return blackPieces;
            else
                return whitePieces;
        }

        private bool Check(int x, int y)
        {
            List<Piece> enemyPieces = Enemies(this[x, y]);
            foreach (Piece p in enemyPieces)
            {
                switch (p.type)
                {
                    case 1:
                        if (PawnKill(p.x, p.y, x, y))
                            return true;
                        break;
                    case 2:
                        if (HorizontalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'H'))
                                return false;
                            return true;
                        }
                        else if (VerticalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'V'))
                                return false;
                            return true;
                        }
                        break;
                    case 3:
                        if (KnightPos(p.x, p.y, x, y))
                            return true;
                        break;
                    case 4:
                        if (BishopPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'D'))
                                return false;
                            return true;
                        }
                        break;
                    case 5:
                        if (HorizontalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'H'))
                                return false;
                            return true;
                        }
                        else if (VerticalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'V'))
                                return false;
                            return true;
                        }
                        else if (BishopPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'D'))
                                return false;
                            return true;
                        }
                        break;
                    case 6:
                        if (KingPos(p.x, p.y, x, y))
                            return true;
                        break;
                }
            }
            return false;
        }

        private bool Check(Piece piece)
        {
            int x = piece.x;
            int y = piece.y;
            List<Piece> enemyPieces = Enemies(this[x, y]);
            foreach (Piece p in enemyPieces)
            {
                switch (p.type)
                {
                    case 1:
                        if (PawnKill(p.x, p.y, x, y))
                            return true;
                        break;
                    case 2:
                        if (HorizontalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'H'))
                                return false;
                            return true;
                        }
                        else if (VerticalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'V'))
                                return false;
                            return true;
                        }
                        break;
                    case 3:
                        if (KnightPos(p.x, p.y, x, y))
                            return true;
                        break;
                    case 4:
                        if (BishopPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'D'))
                                return false;
                            return true;
                        }
                        break;
                    case 5:
                        if (HorizontalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'H'))
                                return false;
                            return true;
                        }
                        else if (VerticalPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'V'))
                                return false;
                            return true;
                        }
                        else if (BishopPos(p.x, p.y, x, y))
                        {
                            if (BlockingPath(p.x, p.y, x, y, 'D'))
                                return false;
                            return true;
                        }
                        break;
                    case 6:
                        if (KingPos(p.x, p.y, x, y))
                            return true;
                        break;
                }
            }
            return false;
        }

        private bool BlockingPath(int x0, int y0, int x1, int y1, char typeOfPath)
        {
            int dist = Math.Abs(x1 - x0);
            int multx = Math.Sign(x1 - x0);
            int multy = Math.Sign(y1 - y0);
            typeOfPath = typeOfPath.ToString().ToUpper()[0];

            if (typeOfPath == 'H')
            {
                for (int i = x0 + multx; i != x1; i += multx)
                    if (IsPiece(i, y1))    //Blocking the path
                        return true;
            }
            else if (typeOfPath == 'V')
            {
                for (int i = y0 + multy; i != y1; i += multy)
                    if (IsPiece(x1, i))    //Blocking the path
                        return true;
            }
            else if (typeOfPath == 'D')
            {
                for (int i = 1; i < dist; i++)
                    if (IsPiece(multx * i + x0, multy * i + y0))    //Blocking the path
                        return true;
            }
            else
                return true;
            return false;
        }   

        private bool CastlingPos(int x0, int y0, int x1, int y1)
        {
            if (IsPiece(x0, y0) && IsPiece(x1, y1) && this[x0, y0].type == (int)Pieces.K && this[x1, y1].type == (int)Pieces.R && !this[x0, y0].IsEnemy(this[x1, y1]) && !this[x0, y0].moved && !this[x1, y1].moved && !Check(x0, y0) && !BlockingPath(x0, y0, x1, y1, 'H'))
                return true;
            else
                return false;
        }

        private bool CanPromote(int x, int y)
        {
            return this[x, y].promoted == false && this[x, y].type == (int)Pieces.p && (this[x, y].team == (int)Teams.White && y == 8) || (this[x, y].team == (int)Teams.Black && y == 1);
        }

        private bool PawnMove(int x0, int y0, int x1, int y1)
        {
            return x0 == x1 && ((y1 - y0 == 1 && this[x0, y0].team == (int)Teams.White) || (y1 - y0 == -1 && this[x0, y0].team == (int)Teams.Black));
        }

        private bool PawnKill(int x0, int y0, int x1, int y1)
        {
            return Math.Abs(x0 - x1) == 1 && ((y1 - y0 == 1 && this[x0, y0].team == (int)Teams.White) || (y1 - y0 == -1 && this[x0, y0].team == (int)Teams.Black));
        }

        private bool PassantExists(int team, int x, int y)
        {
            return (team == 1 && y == 6 && IsPiece(x, y - 1) && this[x, y - 1].team != team && this[x, y -1].passantVictim) || (team == 2 && y == 3 && IsPiece(x, y + 1) && this[x, y + 1].team != team && this[x, y + 1].passantVictim);
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
}