using ConsoleTables;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace DiceGame
{
    public static class CmdView
    {
        private const string HmacVerifierLink = "https://www.liavaag.org/English/SHA-Generator/HMAC/";

        public static void GenerateAndDisplayDiceProbabilityTableAndInfo(int[,] dices, string[] diceStrings, int cellWidth = 15)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.Write("\nYou can independently verify the initial HMAC, confirming the computer didn't alter its number. \n");
            Console.Write("A convenient tool for this is ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"HMAC Verifier Link: {HmacVerifierLink}");
            Console.ForegroundColor = originalColor;
            Console.WriteLine(". \nTo confirm the generated HMAC matches the one shown initially,\ninput the Key (secret key) and Message (computer's number) provided by the game.\n");
            Console.WriteLine("Probability of the win fоr the user:");
            Console.WriteLine();
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            void PrintHorizontalBorder()
            {
                Console.Write("+");
                for (int i = 0; i <= diceStrings.Length; i++)
                {
                    Console.Write(new string('-', cellWidth));
                    Console.Write("+");
                }
                Console.WriteLine();
            }
            PrintHorizontalBorder();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("| {0,-" + (cellWidth - 2) + "} |", "User dice");
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < diceStrings.Length; i++)
            {
                Console.Write(" {0,-" + (cellWidth - 2) + "} |", diceStrings[i]);
            }
            Console.WriteLine();
            Console.ResetColor();
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;

            PrintHorizontalBorder();
            for (int row = 0; row < diceStrings.Length; row++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("| {0,-" + (cellWidth - 2) + "} |", diceStrings[row]);
                Console.ForegroundColor = originalForegroundColor;
                for (int col = 0; col < diceStrings.Length; col++)
                {
                    var rowDice = diceStrings[row].Split(',').Select(d => int.Parse(d)).ToArray();
                    var colDice = diceStrings[col].Split(',').Select(d => int.Parse(d)).ToArray();
                    var getProbability = Math.Round(CalculationsAndViews.GetProbability(rowDice, colDice), 4);
                    Console.Write(" {0,-" + (cellWidth - 2) + "} |", getProbability.ToString());
                }
                Console.WriteLine();
                Console.ResetColor();
                Console.BackgroundColor = originalBackgroundColor;
                Console.ForegroundColor = originalForegroundColor;
                PrintHorizontalBorder();
            }
            Console.ResetColor();
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;
            Console.WriteLine();
        }

        public static void DisplayInstructions(int diceFaces, int minDicesCount)
        {
            var output =   $"Each dice must have exactly {diceFaces}  faces \n" +
                          "The values of dices must be positive integers  \n" +
                          "The example of valid arguments: 1,2,3,4,5,6  1,2,3,4,5,6  1,2,3,4,5,6  1,2,3,4,5,6\n";
            Console.WriteLine(output);
        }

        public static void DisplayZeroToNCommand(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i} - {i}");
            }
            CmdView.DisplayExitHelpCommands();
        }

        public static void DisplayExitHelpCommands()
        {
            Console.WriteLine("X - Exit");
            Console.WriteLine("? - Help");
        }
        public static void DisplayExitHelp(char enumValue, int[,] dices, string[] inputs)
        {
            if (enumValue == (char)ExitHelp.Exit)
            {
                Environment.Exit(0);
            }
            if (enumValue == (char)ExitHelp.Help)
            {
                GenerateAndDisplayDiceProbabilityTableAndInfo(dices, inputs);
            }
        }

        public static void DisplayDiceSelections(Dictionary<int, string> selectionDices)
        {
            foreach (var kv in selectionDices)
            {
                Console.WriteLine($"{kv.Key} - {kv.Value}");
            }
            CmdView.DisplayExitHelpCommands();
        }


    }
}