using System;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;


namespace DiceGame
{
    static class Program
    {
        static void Main(string[] args)
        {
            int diceFaces = 6;
            int minDicesCount = 3;
            int[] compDice;
            int[] userDice;
            Dictionary<int, string> diceDict = args.Select((value, index) => new { Index = index, Value = value }) 
                                                   .ToDictionary(x => x.Index, x => x.Value);
            Validation.StartProcessValidation(args, diceFaces, minDicesCount);
            var dices = DiceData.GetDicesMultArray(args, diceFaces);
            Console.WriteLine("Let's determine who makes the first move.");
            var cpGenRandomBit = RandomGeneration.GenerateRandomNumber(1);
            Console.WriteLine("I selected a random value in the range 0..1");
            var cpGenSecretKeyForBit = RandomGeneration.GenerateSha3_256SecretKeyString(32);
            var cpGenHmac = SHA3.ComputeHmacSha3_256(cpGenRandomBit.ToString(), cpGenSecretKeyForBit);
            Console.WriteLine($"(HMAC={cpGenHmac}).");
            Console.WriteLine("Try to guess my selection.");
            CmdView.DisplayZeroToNCommand(2);
            var inputBit = Validation.ValidateAndGetSelectedInput(SelectionType.ZeroOne,  dices, args, diceDict);
            Console.WriteLine($"Your selection is {inputBit}");
            Console.WriteLine($"My selection: {cpGenRandomBit} (Key = {cpGenSecretKeyForBit}");
            var starterPlayer = RandomGeneration.GetStarterPlayer(cpGenRandomBit, inputBit);
            (userDice, compDice) = DiceData.SelectDices(starterPlayer, args, dices, diceDict);
            do
            {
                int comResult = CalculationsAndViews.MainProcessAndDisplayRolls(diceFaces, Player.Computer, compDice, dices, args, diceDict);
                int userResult = CalculationsAndViews.MainProcessAndDisplayRolls(diceFaces, Player.User, userDice, dices, args, diceDict);
                CalculationsAndViews.MakeWinnerDecisionAndDisplay(userResult, comResult);
                Console.WriteLine(@"Do you want to continue?('Y'- Yes, 'N'-No)");
            }
            while (string.IsNullOrEmpty(Validation.GameContinuing()));

        }
    }
}