using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

namespace DiceGame
{
    public static class Hmac
    {
        public static string ComputeHmac(string message, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            HMac hmac = new HMac(new Sha3Digest(256));
            hmac.Init(new KeyParameter(keyBytes));
            hmac.BlockUpdate(messageBytes, 0, messageBytes.Length);
            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);
            return Convert.ToHexStringLower(result); 
        }
        public static bool VerifyHmac(string message, string key, string expectedHmac)
        {
            string computedHmac = ComputeHmac(message, key);
            return computedHmac.Equals(expectedHmac, StringComparison.OrdinalIgnoreCase);
        }
    }
}
