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
using System.IO;
using System.Linq;
using static System.Console; //Very useful!
using static Tablero.Outputs;

namespace Tablero
{
    class Program
    {
        static void Main(string[] args)
        {
            //Board tab = new Board();
            //tab.printBoard();
            /*string ch = "g";
            ch = ch.ToUpper();
            int i = (int)Enum.Parse(typeof(Letters), ch);
            int a = 4;
            char c1 = ((Pieces)a).ToString()[0];
            int s = c.algebraicNotation("b2b4");
            WriteLine(s);
            s = c.algebraicNotation("g4b4");
            WriteLine(s);

            string path = "Save.txt";
            if(!File.Exists(path))
            {
                File.WriteAllText(path, "Statistics\n\nWhite Wins: 0(0.0 %)\nBlack Wins: 0(0.0 %)\nDraws:      0(0.0 %)\n\n\nMost wins:\n\n1.Name - Score");
            }


            List<string> save = File.ReadAllLines(path).ToList();
            string whiteScore = save[2];
            string blackScore = save[3];    
            string drawScore = save[4];
            int ws = Int32.Parse(whiteScore.Substring(12, whiteScore.IndexOf('(') - 12));
            int bs = Int32.Parse(blackScore.Substring(12, blackScore.IndexOf('(') - 12));
            int ds = Int32.Parse(drawScore.Substring(12, drawScore.IndexOf('(') - 12));
            Console.WriteLine("{0} {1} {2}", ws, bs, ds);
            List<string> mostWins = new List<string>();
            mostWins.AddRange(save.GetRange(9, save.Count - 9));*/

            Game game = new Game();
            game.GameLoop();
        }
    }
}
