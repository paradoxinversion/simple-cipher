using System;
using System.Collections.Generic;

namespace SimpleCipher
{
    public static class SimpleCipherManager
    {
        public static CipherKey cipherKey;
        public static string inputString;
        private static char[] allAlpha;
        private static char[] allPunc;
        private static char[] allNum;
        private static char[] allChars;
        public static Random cryptoRand;

        /// <summary>
        /// Initializes all of the character arrays
        /// </summary>
        public static void SetCharacters()
        {
            cryptoRand = new Random();
            allAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            allPunc = " !@#$%^&*()_+-={}|[]:;\',./<>?\\\"".ToCharArray();
            allNum = "0123456789".ToCharArray();
            allChars = new char[allAlpha.Length + allPunc.Length + allNum.Length];
            int currentIndex = 0;
            for (int x = 0; x < allAlpha.Length; x++)
            {
                allChars[currentIndex] = allAlpha[x];
                currentIndex++;
            }
            for (int x = 0; x < allPunc.Length; x++)
            {
                allChars[currentIndex] = allPunc[x];
                currentIndex++;
            }
            for (int x = 0; x < allNum.Length; x++)
            {
                allChars[currentIndex] = allNum[x];
                currentIndex++;
            }
        }

        /// <summary>
        /// Creates a Cipher Key. 
        /// </summary>
        /// <returns></returns>
        public static CipherKey CreateCipherKey()
        {
            Dictionary<char,char> cipherEncodeKey = new Dictionary<char, char>();
            Dictionary<char, char> cipherDecodeKey = new Dictionary<char, char>();
            
            foreach (char character in allChars)
            {
                cipherEncodeKey.Add(character, char.Parse("X"));
            }

            List<int> usedIndexes = new List<int>();
            List<char> shuffledValues = new List<char>();
            int currentIndex = -1;
            int tries = 0;
            while (shuffledValues.Count < allChars.Length && tries < 5000)
            {
                currentIndex = cryptoRand.Next(allChars.Length - 1);
                if (!usedIndexes.Contains(currentIndex))
                {
                    shuffledValues.Add(allChars[currentIndex]);
                    usedIndexes.Add(currentIndex);
                }
                tries++;
            }
            for (int x = 0; x < shuffledValues.Count; x++)
            {
                cipherEncodeKey[allChars[x]] = shuffledValues[x];
            }
            for (int x = 0; x < shuffledValues.Count; x++)
            {
                cipherDecodeKey.Add(shuffledValues[x], allChars[x]);
            }
            CipherKey newCipherKey = new CipherKey(cipherEncodeKey, cipherDecodeKey);
            return newCipherKey;
        }
        public static void SetCipherKey(CipherKey newKey)
        {
            cipherKey = newKey;
        }

        public static string EncodeMessage(string originalMessage)
        {
            string codedMessage = string.Empty;
            if (cipherKey != null)
            {
                char[] om = originalMessage.ToCharArray();
                char[] nm = new char[originalMessage.Length];

                for (int x = 0; x < om.Length; x++)
                {
                    char originalChar = om[x];
                    char encodedChar = cipherKey.CipherEncodeKey[originalChar];
                    nm[x] = encodedChar;
                }

                codedMessage = new string(nm);
            }
            else
            {
                codedMessage = "No Cipher Key Set.";
            }
           
            
            return codedMessage;
        }

        public static string DecodeMessage(string encodedMessage)
        {
            string decodedMessage = string.Empty;
            if (cipherKey != null)
            {
                char[] em = encodedMessage.ToCharArray();
                char[] nm = new char[em.Length];

                for (int x = 0; x < em.Length; x++)
                {
                    char encodedChar = em[x];
                    char decodedChar = cipherKey.CipherDecodeKey[em[x]];
                    nm[x] = decodedChar;
                }
                decodedMessage = new string(nm);
            }
            else
            {
                decodedMessage = "No Cipher Key Set.";
            }
            
            return decodedMessage;
        }
    }
}
