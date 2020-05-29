using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESEncryptor
{
    public class CommandExecutor
    {
        public void ChooseProcess()
        {
            Console.WriteLine("Choose The Mode For Generate of keys Enter K ," +
                " To recover Text from keys Enter T and To Exit E");
            var mode = Console.ReadLine();
            switch (mode.ToUpper())
            {
                case "K":
                    {
                        StartKeyGeneration();
                        break;
                    }
                case "T":
                    {
                        StartTextGeneration();
                        break;
                    }
                case "E":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("The mode is not available");
                        ChooseProcess();
                        break;
                    }
            }
        }
        private void StartTextGeneration()
        {
            Console.WriteLine("Provide the Keys");
            string[] keys = new string[3];
            int keycount = 0;
            while (keycount < 3)
            {
                Console.WriteLine($"Enter Key {keycount + 1} :");
                keys[keycount] = Console.ReadLine();
                keycount++;
            }
            Console.WriteLine("Enter The password");
            string password = Utility.WritePassword();
            Console.WriteLine(Environment.NewLine);
            if (keys.Any(x => x.Length == 0) || password.Length == 0)
            {
                Console.WriteLine("Invalid Passcode or Key Please Re Enter");
            }
            GenerateText(keys, password);
            ChooseProcess();
        }

        private void StartKeyGeneration()
        {
            Console.WriteLine("Please Enter The Text");
            var text = Console.ReadLine();
            Console.WriteLine("Please Enter The Password");
            var passcode = Utility.WritePassword();
            Console.WriteLine(Environment.NewLine);
            if (text.Length == 0 || passcode.Length == 0)
            {
                Console.WriteLine("Invalid Passcode or text Please ReEnter");
                return;
            }
            GenerateKeys(text, passcode);
            ChooseProcess();
        }
        public void GenerateKeys(string text, string password)
        {
            try
            {
                SecurityKeyGenerator securityKeyGenerator = new SecurityKeyGenerator();
                var val = securityKeyGenerator.GenerateKeys(text, password);
                if (val == null) Console.WriteLine("Error in Generating Key");
                int count = 0;
                foreach (var key in val)
                {
                    Console.WriteLine($"Key {++count} :  {key.Key}");
                }
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Encryption");
            }
        }
        public void GenerateKeys(string text, string password, out IEnumerable<SecurityKey> securityKeys)
        {
            SecurityKeyGenerator securityKeyGenerator = new SecurityKeyGenerator();
            securityKeys = securityKeyGenerator.GenerateKeys(text, password);
        }
        public void GenerateText(IEnumerable<string> keys, string password)
        {
            SecurityKeyGenerator securityKeyGenerator = new SecurityKeyGenerator();
            try
            {
                string text = securityKeyGenerator.GenerateTextFromKeys(keys.Select(x => new SecurityKey() { Key = x }), password);
                Console.WriteLine("Your Text :");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(text);
            }
            catch (Exception)
            {
                Console.WriteLine("Error in Decrypting the Keys");
            }
        }
        public void GenerateText(IEnumerable<string> keys, string password, out string text)
        {
            SecurityKeyGenerator securityKeyGenerator = new SecurityKeyGenerator();
            text = securityKeyGenerator.GenerateTextFromKeys(keys.Select(x => new SecurityKey() { Key = x }), password);
        }
    }
}