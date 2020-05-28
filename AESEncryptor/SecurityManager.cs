using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AESEncryptor
{
    public enum Algorithm
    {
        Aes,
        DES,
        RC2,
        Rijndael,
        TripleDES,
    }
    public class SecurityManager
    {
        private string defaultPassword = "Kappsoft";
        private byte[] defaultSalt;
        private int defaultIteration = 10000;
        private byte[] Key;
        private byte[] IV;

        private SymmetricAlgorithm ChooseAlgorithmType(Algorithm type)
        {
            switch (type)
            {
                case Algorithm.Aes:
                    return new AesManaged();
                case Algorithm.DES:
                    return new DESCryptoServiceProvider();
                case Algorithm.RC2:
                    return new RC2CryptoServiceProvider();
                case Algorithm.Rijndael:
                    return new RijndaelManaged();
                case Algorithm.TripleDES:
                    return new TripleDESCryptoServiceProvider();
                default:
                    return new RijndaelManaged();
            }
        }

        public ICryptoTransform CreateEncrptor(string passcode, Algorithm algorithm = Algorithm.Rijndael)
        {
            string passcodes = string.IsNullOrEmpty(passcode) ? defaultPassword : passcode;
            SymmetricAlgorithm alg = ChooseAlgorithmType(algorithm);
            defaultSalt = GenerateSalt();
            GenerateDeriveBytes(passcodes, defaultIteration, defaultSalt);
            alg.Key = this.Key;
            alg.IV = this.IV;
            return alg.CreateEncryptor();
        }

        public ICryptoTransform CreateEncrptor(string passcode, int iteration, byte[] salt, Algorithm algorithm = Algorithm.Rijndael, CipherMode cipherMode = CipherMode.CBC)
        {
            SymmetricAlgorithm alg = ChooseAlgorithmType(algorithm);
            GenerateDeriveBytes(passcode, iteration, salt);
            alg.Key = this.Key;
            alg.IV = this.IV;
            alg.Mode = cipherMode;
            return alg.CreateEncryptor();
        }
        public ICryptoTransform CreateDecryptor(string passcode, Algorithm algorithm = Algorithm.Rijndael)
        {
            string passcodes = string.IsNullOrEmpty(passcode) ? defaultPassword : passcode;
            SymmetricAlgorithm alg = ChooseAlgorithmType(algorithm);
            defaultSalt = GenerateSalt();
            GenerateDeriveBytes(passcodes, defaultIteration, defaultSalt);
            alg.Key = this.Key;
            alg.IV = this.IV;
            return alg.CreateDecryptor();
        }

        public ICryptoTransform CreateDecryptor(string passcode, int iteration, byte[] salt, Algorithm algorithm = Algorithm.Rijndael, CipherMode cipherMode = CipherMode.CBC)
        {
            SymmetricAlgorithm alg = ChooseAlgorithmType(algorithm);
            GenerateDeriveBytes(passcode, iteration, salt);
            alg.Key = this.Key;
            alg.IV = this.IV;
            alg.Mode = cipherMode;
            return alg.CreateDecryptor();
        }
        public byte[] GenerateSalt(int length = 10)
        {
            byte[] salt = new byte[length];
            for (int i = 0; i < length; i++)
            {
                salt[i] = (byte)i;
            }
            return salt;
        }
        private void GenerateDeriveBytes(string passcode, int iterations, byte[] salt)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passcode, salt, iterations);
            Key = rfc2898DeriveBytes.GetBytes(32);
            IV = rfc2898DeriveBytes.GetBytes(16);
        }
    }
}