using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using static System.Console; //Very useful!
using static Tablero.Outputs;

namespace Tablero
{
    class Game
    {
        static readonly string games = "Games.json";
        static readonly string highscores = "Highscores.json";

        private Outputs PlayChess()
        {
            Clear();
            WriteLine("Hello World! {0}", (char)1);
            Chess c = new Chess();
            c.PrintBoard();
            Outputs output;
            string expression = ReadLine();
            Clear();
            output = c.AlgebraicNotation(expression);
            while (output != WhiteWon && output != BlackWon)
            {
                WriteLine(output);
                expression = ReadLine();
                Clear();
                output = c.AlgebraicNotation(expression);
            }
            WriteLine("\n    " + output);
            c.PrintBoard(false);
            //ReadLine();
            return output;
        }

        private void ShowStats()
        {
            Clear();
            int total, count = 0;

            if (!File.Exists(games))
                File.WriteAllText(games, "0\n0\n0");
            if (!File.Exists(highscores))
                File.WriteAllText(highscores, "Name 0");

            List<string> temp = File.ReadAllLines(games).ToList();

            if (!Int32.TryParse(temp[0], out int whiteWon))
                whiteWon = 0;
            if (!Int32.TryParse(temp[1], out int blackWon))
                blackWon = 0;
            if (!Int32.TryParse(temp[2], out int draws))
                draws = 0;

            total = whiteWon + blackWon + draws;
            total = total > 0 ? total : 1;

            WriteLine("\nStatistics\n\n" +
                "White wins: {0} ({1}%)\n" +
                "Black wins: {2} ({3}%)\n" +
                "Draws:      {4} ({5}%)\n" +
                "Most wins:\n",
                whiteWon, whiteWon/total, blackWon, blackWon/total, draws, draws/total);

            temp.Clear();
            temp = File.ReadAllLines(highscores).ToList();

            while (count < temp.Count)
            {
                string[] current = temp[count].Split('-');
                if (!Int32.TryParse(current[1], out int score))
                    score = 0;
                WriteLine("{0}.{1} - {2}", ++count, current[0], score);
            }
            Write("... ");
        }

        public void GameLoop()
        {
            string press;
            int selection;

            while(true)
            {
                Clear();
                WriteLine("\nWelcome to Guille_dlC's Chess!\n\n" +
                    "Press...\n" +
                    "1 - Play Chess\n" +
                    "2 - Check Stats\n" +
                    "3 - Exit Game\n");

                do
                    press = Console.ReadLine();
                while (!Int32.TryParse(press, out selection) || selection > 3 || selection < 1) ;

                if (selection == 1)
                    PlayChess();
                else if (selection == 2)
                    ShowStats();
                else
                    return;
                Console.ReadLine();
            }
        }
    }
}
