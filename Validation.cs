using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public static class Validation
    {
        public static string GetValidationMessage(string[] dices, int diceFaces, int minDicesCount)
        {
            string errorMessage = string.Empty;
            if (dices is null || dices.Length < minDicesCount)
                errorMessage = $"Args count must be more or equal {minDicesCount}";
            else
            {
                dices.ToList().ForEach(dice =>
                {
                    var diceFaceItems = dice.Split(',');
                    if (diceFaceItems.Length != diceFaces)
                        errorMessage = $"Each dice must have exactly {diceFaces} faces";
                    diceFaceItems.ToList().ForEach(diceItem =>
                    {
                        if (!int.TryParse(diceItem, out int value) || value <= 0)
                            errorMessage = "The values of dices must be positive integers";
                    });
                });
            }
            return errorMessage;
        }
        public static bool IsValidExitHelpInput(string input)
        {
            return Enum.GetValues<ExitHelp>().Select(e => ((char)(byte)e).ToString()).Contains(input.ToUpper());
        }
        public static string GetValidationBitInput(string input)
        {
            var isValidBitInput = Enum.GetValues<ZeroToOne>().Select(e => ((char)(byte)e).ToString()).Contains(input.ToUpper());
            return GetValidationMessage(isValidBitInput);
        }

        private static string GetValidationMessage(bool isValidInput)
        {
            return isValidInput ? string.Empty : "Enter the above mentioned values.";
        }

        public static string GetValidationMessageZeroToFiveInput(string input)
        {
            var isValidNumberZeroToFiveInput = Enum.GetValues<ZeroToFive>().Select(e => ((char)(byte)e).ToString()).Contains(input.ToUpper());
            return GetValidationMessage(isValidNumberZeroToFiveInput);
        }

        public static string GetValidationMessageDiceSelectionInput(Dictionary<int, string> diceDict, string input)
        {
            string validationMessage = string.Empty;
            string invalidMessage = "Enter the above mentioned values.";
            if (int.TryParse(input, out int inputValue))
            {
                if (!diceDict.ContainsKey(inputValue))
                {
                    validationMessage = invalidMessage;
                }
            }
            else
            {
                validationMessage = invalidMessage;
            }
            return validationMessage;
        }
        public static int ValidateAndGetSelectedInput(SelectionType selection, int[,] dices, string[] inputs, Dictionary<int, string> diceDict)
        {
            var inputCMD = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
            string validationMessage = string.Empty;
            do
            {
                char commandChar = string.IsNullOrEmpty(inputCMD) ? '\0' : inputCMD[0];
                CmdView.DisplayExitHelp(commandChar, dices, inputs);
                validationMessage = GetSelectionInputValidationMessage(selection, inputCMD, diceDict);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    Console.WriteLine(validationMessage);
                    inputCMD = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;

                }
            }
            while (!string.IsNullOrEmpty(validationMessage) || inputCMD[0] == ((char)ExitHelp.Help));
            return int.Parse(inputCMD);
        }

        public static void StartProcessValidation(string[] inputCMD, int diceFaces, int minDicesCount)
        {
            string validationMessage = GetValidationMessage(inputCMD, diceFaces, minDicesCount);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                Console.WriteLine(validationMessage);
                CmdView.DisplayInstructions(diceFaces, minDicesCount);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static string GetSelectionInputValidationMessage( SelectionType selection, string input , Dictionary<int, string> diceDict)
        {
            string validationMessage = string.Empty;
            switch (selection)
            {
                case SelectionType.ZeroOne:
                    {
                        validationMessage = GetValidationBitInput(input);
                        break;
                    }
                case SelectionType.ZeroFive:
                    {
                        validationMessage = GetValidationMessageZeroToFiveInput(input);
                        break;
                    }
                default:
                    {
                        validationMessage = GetValidationMessageDiceSelectionInput(diceDict, input); 
                        break;
                    }
            }
            return validationMessage;
        }

        public static string GameContinuing()
        {
            string inputCMD = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
            char commandChar = string.IsNullOrEmpty(inputCMD) ? '\0' : inputCMD[0];
            string validationMessage = string.Empty;

            do
            {
                commandChar = string.IsNullOrEmpty(inputCMD) ? '\0' : inputCMD[0];

                if ((inputCMD.Length != 1) || (commandChar != (char)YesNo.Yes && commandChar != (char)YesNo.No))
                {

                    validationMessage = "Please input Y/N value";
                    Console.WriteLine(validationMessage);
                    inputCMD = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
                }
                if (inputCMD.Length == 1 && commandChar == (char)YesNo.No)
                {
                    Environment.Exit(0);
                }
            }
            while (!string.IsNullOrEmpty(validationMessage) && commandChar != (char)YesNo.Yes);


                return validationMessage;
            
            
        }
    }
}
