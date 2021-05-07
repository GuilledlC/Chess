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
