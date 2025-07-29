using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public static class DiceData
    {
       public static int[,] GetDicesMultArray ( string[] args, int diceFaces)
        {
            int[,] dices = new int[args.Length, diceFaces];
            for (int i = 0; i < args.Length; i++)
            {
                string[] faces = args[i].Split(',');
                for (int j = 0; j < diceFaces; j++)
                {
                   dices[i, j] = int.Parse(faces[j]);
                }
            }
            return dices;
        }

        public static  ( int [] , int [] ) SelectDices (Player starterPlayer ,  string [] inputs , int[, ] dices,  Dictionary<int, string> diceDict)
        {
            int compRandomIndex;
            int[] compDice;
            int[] userDice;
            if (starterPlayer == Player.User)
            {
                Console.WriteLine($"Choose your dice:");
                CmdView.DisplayDiceSelections(diceDict);
                var selectedDiceIndexInput = Validation.ValidateAndGetSelectedInput(SelectionType.DiceRange, dices, inputs, diceDict);
                Console.WriteLine($"Your selection is {selectedDiceIndexInput}");
                Console.WriteLine($"You choose the {diceDict[selectedDiceIndexInput]} dice.");
                ( compRandomIndex,  compDice) = RandomGeneration.GetRandomDice(inputs, starterPlayer, selectedDiceIndexInput);
                userDice = diceDict[compRandomIndex].Split(',').Select(d => int.Parse(d)).ToArray();    
                Console.WriteLine($"I make move and choose the {diceDict[compRandomIndex]} dice.");


            }
            else
            {
                (compRandomIndex, compDice) = RandomGeneration.GetRandomDice(inputs, starterPlayer);
                Console.WriteLine($"I make move and choose the {diceDict[compRandomIndex]} dice.");
                Console.WriteLine($"Choose your dice:");
                var excludeDiceDict = CalculationsAndViews.GetIndexSelectionDictionary(inputs, starterPlayer, compRandomIndex);
                CmdView.DisplayDiceSelections(excludeDiceDict);
                var selectedDiceIndexInput = Validation.ValidateAndGetSelectedInput(SelectionType.DiceRange, dices, inputs, excludeDiceDict);
                Console.WriteLine($"Your selection is {selectedDiceIndexInput}");
                userDice = diceDict[compRandomIndex].Split(',').Select(d => int.Parse(d)).ToArray();
                Console.WriteLine($"You choose the {excludeDiceDict[selectedDiceIndexInput]} dice.");
            }
            return (userDice, compDice);    
        }
    }
}
