using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public static class CalculationsAndViews
    {
        public static int CountWins(int[] a, int[] b)
        {
            return a.SelectMany(_ => b, (x, y) => x > y).Count(x => x);
        }
        public static double GetProbability(int[] a, int[] b)
        {
            return (double)CountWins(a, b) / (double)(a.Length * b.Length);
        }
        public static Dictionary<int, string> GetIndexSelectionDictionary (string[] inputs, Player player, int excludeIndex)
        {
            if (player == Player.Computer)
            {
                inputs = inputs.Where((_, indexer) => indexer != excludeIndex).ToArray();
            }

            var indexSelection = new Dictionary<int, string>();
            for (int i = 0; i < inputs.Length; i++)
            {
                indexSelection.Add(i, inputs[i]);
            }
            return indexSelection;
        }
        public static int MainProcessAndDisplayRolls(int diceFaces , Player player , int[] chosenDice, int[,] dices , string[] inputs , Dictionary<int, string> diceDict)
        {
            Console.WriteLine("It's time for your roll.");
            var compDiceFace = RandomGeneration.GenerateRandomNumber(diceFaces);
            Console.WriteLine($"I selected a random value in the range 0..{diceFaces - 1}");
            var cpGenSecretKeyForDiceFaces = RandomGeneration.GenerateSecretKeyString(32);
            var cpHmacForRandomDiceface = Hmac.ComputeHmac(compDiceFace.ToString(), cpGenSecretKeyForDiceFaces);
            Console.WriteLine($"(HMAC={cpHmacForRandomDiceface}).");
            Console.WriteLine($"Choose value in the range 0..{diceFaces - 1}");
            CmdView.DisplayZeroToNCommand(diceFaces);
            var inputUserDiceFace = Validation.ValidateAndGetSelectedInput(SelectionType.ZeroFive, dices, inputs, diceDict);
            Console.WriteLine($"Your selection: {inputUserDiceFace}");
            Console.WriteLine($"My number is {compDiceFace} (KEY={cpGenSecretKeyForDiceFaces}).");
            var fareNumber = (compDiceFace + inputUserDiceFace) % diceFaces;
            Console.WriteLine($"The fair number generation result is {inputUserDiceFace} + {compDiceFace} = {fareNumber} (mod {diceFaces}).");
            var fareValue = chosenDice[fareNumber];
            if(player == Player.Computer)
            {

                Console.WriteLine($"My roll result is {fareValue}");
            }
            else
            {
                Console.WriteLine($"Your result is {fareValue}");
            }
            return fareNumber; 
        }
        public static  void MakeWinnerDecisionAndDisplay( int userFareNumber , int compFareNumber)
        {
            if (userFareNumber > compFareNumber)
            {
                Console.WriteLine($"You win! {userFareNumber} > {compFareNumber}");
            }
            else if (userFareNumber < compFareNumber) 
            {
                Console.WriteLine($"You lose! {userFareNumber} < {compFareNumber}");
            }
            else
            {
                Console.WriteLine($"It's a tie! {userFareNumber} = {compFareNumber}");
            }
        }
    }
}
