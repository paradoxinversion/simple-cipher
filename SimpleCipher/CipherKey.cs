using System;
using System.Collections.Generic;

namespace SimpleCipher
{
    [Serializable]
    ///The Cipher Key is what allows Simple Cipher to encode and decode messages.
    ///An encoded message can only be decoded by the same Cipher Key.
    public class CipherKey
    {
        /// <summary>
        /// The Dictionary used to encode messages. Each key is the decoded character, each value is the coded character.
        /// </summary>
        private Dictionary<char, char> cipherEncodeKey;
        /// <summary>
        /// The Dictionary used to decode messages. Each key us the coded character, each value is the decoded character.
        /// </summary>
        private Dictionary<char, char> cipherDecodeKey;

        public Dictionary<char, char> CipherEncodeKey
        {
            get
            {
                return cipherEncodeKey;
            }

            set
            {
                cipherEncodeKey = value;
            }
        }

        public Dictionary<char, char> CipherDecodeKey
        {
            get
            {
                return cipherDecodeKey;
            }

            set
            {
                cipherDecodeKey = value;
            }
        }

        /// <summary>
        /// Initialize a Cipher key based on new encode and decode messages.
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="decode"></param>
        public CipherKey(Dictionary<char, char> encode, Dictionary<char, char> decode)
        {
            CipherEncodeKey = encode;
            CipherDecodeKey = decode;
        }

        /// <summary>
        /// Initialize a Cipher Key based on an existing one.
        /// Use this for loading cipher keys that already exist.
        /// </summary>
        /// <param name="existingKey"></param>
        public CipherKey(CipherKey existingKey)
        {
            CipherEncodeKey = existingKey.CipherEncodeKey;
            CipherDecodeKey = existingKey.CipherDecodeKey;
        }
    }
   
}
