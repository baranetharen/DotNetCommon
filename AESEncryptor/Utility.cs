using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESEncryptor
{
    public static class Utility
    {
        public static string WritePassword()
        {
            string pass = string.Empty;
            StringBuilder stringBuilder = new StringBuilder(pass);
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (!char.IsControl(key.KeyChar))
                {
                    stringBuilder.Append(key.KeyChar);
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        stringBuilder.Remove(pass.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return stringBuilder.ToString();
        }
    }
}
