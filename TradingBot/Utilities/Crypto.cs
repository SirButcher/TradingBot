using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace TradingBot
{
    class Crypto
    {
        public string GetAPIKey()
        {
            string key = "N/A";

            using (StreamReader sr = new StreamReader("/Files/APIKey.txt"))
            {
                key = sr.ReadToEnd();
            }

            return key;
        }

        public string GetSecretKey()
        {
            string key = "N/A";

            using (StreamReader sr = new StreamReader("/Files/SecretKey.txt"))
            {
                key = sr.ReadToEnd();
            }

            return key;
        }

        public string CreateSHA512(string inputString)
        {
            SHA512 sha512 = SHA512.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);

            return GetStringFromByte(hash);
        }

        private static string GetStringFromByte(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString().ToUpper();
        }

    }


}
