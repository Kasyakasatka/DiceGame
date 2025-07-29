using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class RandomGeneration
    {
        public static int GenerateRandomNumber(int range)
        {
            return RandomNumberGenerator.GetInt32(range);
        }
        public static byte[] GenerateSha3_256SecretKey(int keyLengthInBytes)
        {
            byte[] key = new byte[keyLengthInBytes];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        public static string GenerateSha3_256SecretKeyString(int keyLengthInBytes)
        {
            var secretKey = GenerateSha3_256SecretKey(keyLengthInBytes);
            return Hex.ToHexString(secretKey).ToUpper();
        }
        public static (int, int[]) GetRandomDice(string[] diceStrings, Player starterPlayer , int excludeIndex =-1)
        {

            int randomInd;
            if (starterPlayer == Player.User)
            {
                var excludeList = Enumerable.Range(0, diceStrings.Length).Where(i=> i!= excludeIndex).ToList();
                randomInd = RandomNumberGenerator.GetInt32(0, excludeList.Count);
                randomInd = excludeList[randomInd];
            }
            else
            {
                randomInd = RandomNumberGenerator.GetInt32(0, diceStrings.Length);
            }
            return (randomInd, diceStrings[randomInd].Split(',').Select(d => int.Parse(d)).ToArray());
            
        }
        public static Player GetStarterPlayer(int compBit, int userBit)
        {
            if (compBit == userBit)
                return Player.User;
            else
                return Player.Computer;
        }
    }
}
