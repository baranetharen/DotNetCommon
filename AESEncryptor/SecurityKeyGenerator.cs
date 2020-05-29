using System;
using System.Collections.Generic;
using System.Linq;


namespace AESEncryptor
{
    public class SecurityKeyGenerator
    {
        int keycount;
        CrypoSecurity crypoSecurity;
        public SecurityKeyGenerator(int count = 3)
        {
            crypoSecurity = new CrypoSecurity();
            keycount = count;
        }
        public IEnumerable<SecurityKey> GenerateKeys(string text, string passcode)
        {
            var encryptedText = crypoSecurity.Encrypt(text, passcode);
            if (encryptedText.Length < keycount) throw new Exception("The Word Length should be Greater than the KeyCount");
            return SeperateKeys(encryptedText);
        }
        public IEnumerable<SecurityKey> SeperateKeys(string encryptedText)
        {
            int slength = encryptedText.Length;
            SecurityKey[] keys = new SecurityKey[keycount];
            int middlePosition = slength / keycount;
            int rem = slength % keycount;
            int startPosition = 0;
            int endPosition = middlePosition;
            for (int i = 0; i < keycount; i++)
            {
                startPosition = middlePosition * i;
                var key = encryptedText.Substring(startPosition, middlePosition);
                keys[i] = new SecurityKey() { Key = key };
            }
            if (rem > 0)
            {
                var key = encryptedText.Substring(startPosition + middlePosition, rem);
                keys[keycount - 1].Key = keys[keycount - 1].Key + key;
            }
            return keys;
        }

        public string GenerateTextFromKeys(IEnumerable<SecurityKey> keys, string passcode)
        {
            var crypttext = JoinKeys(keys);
            var decryptedText = crypoSecurity.Decrypt(crypttext, passcode);
            return decryptedText;
        }
        public string JoinKeys(IEnumerable<SecurityKey> keys, string delimiter = "")
        {
            return string.Join(delimiter, keys.Select(x => x.Key));
        }
    }
}
