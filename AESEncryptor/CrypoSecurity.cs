using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AESEncryptor
{
    public class CrypoSecurity
    {
        private SecurityManager securityManager;
        public CrypoSecurity()
        {
            securityManager = new SecurityManager();
        }

        public string Encrypt(string clipertext, string passcode)
        {
            ICryptoTransform crypto = securityManager.CreateEncrptor(passcode);
            byte[] encryptResult = CryptoEncrypt(crypto, clipertext);
            return Convert.ToBase64String(encryptResult);
        }
        public string Decrypt(string encryptedText, string passcode)
        {
            ICryptoTransform crypto = securityManager.CreateDecryptor(passcode);
            return CryptoDecrypt(crypto, Convert.FromBase64String(encryptedText));
        }
        public string Encrypt(string clipertext, int iterationCount, string passcode, int saltCount, Algorithm algorithm, CipherMode cipherMode)
        {
            var salt = securityManager.GenerateSalt(saltCount);
            ICryptoTransform crypto = securityManager.CreateEncrptor(passcode, iterationCount, salt, algorithm, cipherMode);
            byte[] encryptResult = CryptoEncrypt(crypto, clipertext);
            return Convert.ToBase64String(encryptResult);
        }
        public string Decrypt(string encryptedText, int iterationCount, string passcode, int saltCount, Algorithm algorithm, CipherMode cipherMode)
        {
            var salt = securityManager.GenerateSalt(saltCount);
            ICryptoTransform crypto = securityManager.CreateDecryptor(passcode, iterationCount, salt, algorithm, cipherMode);
            return CryptoDecrypt(crypto, Convert.FromBase64String(encryptedText));
        }
        private byte[] CryptoEncrypt(ICryptoTransform crypto, string clipertext)
        {
            byte[] encryptResult;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypto, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cs))
                    {
                        streamWriter.Write(clipertext);
                        streamWriter.Close();
                    }
                    encryptResult = ms.ToArray();
                }
            }
            return encryptResult;
        }
        private string CryptoDecrypt(ICryptoTransform crypto, byte[] encryptedbyte)
        {
            string plainText = string.Empty;
            using (MemoryStream ms = new MemoryStream(encryptedbyte))
            {
                using (CryptoStream cs = new CryptoStream(ms, crypto, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cs))
                    {
                        plainText = streamReader.ReadToEnd();
                    }
                }
            }
            return plainText;
        }
    }
}
